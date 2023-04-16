namespace Wayout.Mathematics.Geometry.D3
{
    using System;
    using Functions;

    //sphere + <subset> + (en)closed/ful(l)/vol/bulk/(in)fill(ing)/content/load/span + body (mass,density) + hetero


    /// <summary>
    /// Ball
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Ball_(mathematics)">wikipedia</a>
    /// </remarks>
    public static class Ball
    {
        public static double Volume(double radius) => (4d / 3d) * PI * Power3.f(radius);

        public static double RadiusFromVolume(double volume) => Root3.f(3*volume / (4*PI));
    }
}
