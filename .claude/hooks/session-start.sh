#!/bin/bash
# Claude Code on the web starts from a container without a .NET SDK, so build, format
# and test are all unavailable until one is installed. This puts the SDK the repository
# needs in place before the session begins.
set -euo pipefail

# a developer machine brings its own toolchain, this is only for the remote container
if [ "${CLAUDE_CODE_REMOTE:-}" != "true" ]; then
  exit 0
fi

# src/smath.slnx is a solution in the XML format, which needs 9.0.200 or newer, and
# Directory.Build.props sets LangVersion to latest, so the SDK also decides the language
readonly REQUIRED_MAJOR=10

SUDO=""
if [ "$(id -u)" -ne 0 ]; then
  SUDO="sudo"
fi

if [ -n "${CLAUDE_ENV_FILE:-}" ] && ! grep -q DOTNET_CLI_TELEMETRY_OPTOUT "$CLAUDE_ENV_FILE" 2>/dev/null; then
  {
    echo 'export DOTNET_CLI_TELEMETRY_OPTOUT=1'
    echo 'export DOTNET_NOLOGO=1'
  } >> "$CLAUDE_ENV_FILE"
fi

if command -v dotnet >/dev/null 2>&1 && dotnet --list-sdks | grep -q "^${REQUIRED_MAJOR}\."; then
  echo "dotnet SDK ${REQUIRED_MAJOR} is already installed"
else
  # dot.net/v1/dotnet-install.sh redirects to builds.dotnet.microsoft.com, which the
  # egress policy rejects, and the Ubuntu feed carries 8.0 only. packages.microsoft.com
  # is reachable and carries the current SDK.
  if [ ! -f /etc/apt/sources.list.d/microsoft-prod.list ]; then
    # shellcheck disable=SC1091
    . /etc/os-release
    feed="$(mktemp -d)"
    curl -fsSL --retry 3 \
      "https://packages.microsoft.com/config/ubuntu/${VERSION_ID}/packages-microsoft-prod.deb" \
      -o "$feed/packages-microsoft-prod.deb"
    $SUDO dpkg -i "$feed/packages-microsoft-prod.deb"
    rm -rf "$feed"
  fi

  # the image carries a few third party PPAs this network cannot reach, and apt reports
  # them as an error even though every feed the SDK comes from was refreshed
  $SUDO apt-get update -qq || true
  $SUDO env DEBIAN_FRONTEND=noninteractive apt-get install -y -qq "dotnet-sdk-${REQUIRED_MAJOR}.0"
fi

dotnet --list-sdks

# warm the package cache while the container state is still being snapshotted, so the
# first build of the session does not spend its time restoring
dotnet restore "${CLAUDE_PROJECT_DIR:-.}/src/smath.slnx" || true
