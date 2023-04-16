namespace Wayout.Mathematics.Geometry.D3
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SphereCapTest
    {
        [TestMethod()]
        public void CapHeightOfZeroRadius() => Assert.AreEqual(0, SphereCap.CapHeight(0, 0), 0.00001);

        [TestMethod()]
        public void CapHeightOfZeroCapRadius() => Assert.AreEqual(0, SphereCap.CapHeight(1, 0), 0.00001);

        [TestMethod()]
        public void CapHeightOfHemisphereFromRadius() => Assert.AreEqual(1, SphereCap.CapHeight(1, 1), 0.00001);

        [TestMethod()]
        public void CapHeightOfHemisphereFromCapRadius() => Assert.AreEqual(1, SphereCap.CapHeight(1, 1), 0.00001);

        [TestMethod()]
        public void CapRadiusOfZeroCapHeight() => Assert.AreEqual(0, SphereCap.CapRadius(1, 0), 0.00001);

        [TestMethod()]
        public void CapRadiusOfZeroRadius() => Assert.AreEqual(0, SphereCap.CapRadius(0, 0), 0.00001);

        [TestMethod()]
        public void CapRadiusOfHemisphereFromCapHeight() => Assert.AreEqual(1, SphereCap.CapRadius(1, 1), 0.00001);

        [TestMethod()]
        public void CapRadiusOfHemisphereFromRadius() => Assert.AreEqual(1, SphereCap.CapRadius(1, 1), 0.00001);


        [TestMethod()]
        public void RadiusOfZeroCapHeight() => Assert.AreEqual(double.NaN, SphereCap.Radius(0, 0));

        [TestMethod()]
        public void RadiusOfZeroCapRadius() => Assert.AreEqual(double.NaN, SphereCap.Radius(0, 0));

        [TestMethod()]
        public void RadiusOfHemisphereFromCapHeight() => Assert.AreEqual(1, SphereCap.Radius(1, 1), 0.00001);

        [TestMethod()]
        public void RadiusOfHemisphereFromCapRadius() => Assert.AreEqual(1, SphereCap.Radius(1, 1), 0.00001);


        [TestMethod()]
        public void SurfaceAreaOfZeroCapHeight()
            => Assert.AreEqual(0, SphereCap.SurfaceArea(1, 0), 0.00001);
        
        [TestMethod()]
        public void SurfaceAreaOfZeroPolarAngle()
            => Assert.AreEqual(0, SphereCap.SurfaceAreaByPolarAngle(1, 0), 0.00001);

        [TestMethod()]
        public void SurfaceAreaOfZeroCapRadius()
            => Assert.AreEqual(0, SphereCap.SurfaceAreaByCapRadius(0, 0), 0.0001);

        [TestMethod()]
        public void SurfaceAreaOfHemisphere()
            => Assert.AreEqual(6.283185, SphereCap.SurfaceArea(1, 1), 0.0001);

        [TestMethod()]
        public void SurfaceAreaOfHemisphereByCapRadius()
            => Assert.AreEqual(6.283185, SphereCap.SurfaceAreaByCapRadius(1, 1), 0.0001);

        [TestMethod()]
        public void SurfaceAreaOfHemisphereByPolarAngle()
            => Assert.AreEqual(6.283185, SphereCap.SurfaceAreaByPolarAngle(1, Math.PI / 2d), 0.0001);
    }
}
