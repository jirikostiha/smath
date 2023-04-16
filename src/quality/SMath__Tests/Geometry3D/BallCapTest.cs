namespace Wayout.Mathematics.Geometry.D3
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BallCapTest
    {
        [TestMethod()]
        public void SurfaceAreaOfZeroCapHeight()
            => Assert.AreEqual(0, BallCap.SurfaceArea(1, 0), 0.00001);
        
        [TestMethod()]
        public void SurfaceAreaOfZeroPolarAngle()
            => Assert.AreEqual(0, BallCap.SurfaceAreaByPolarAngle(1, 0), 0.00001);

        [TestMethod()]
        public void SurfaceAreaOfZeroCapRadius()
            => Assert.AreEqual(0, BallCap.SurfaceAreaByCapRadius(0, 0), 0.0001);

        [TestMethod()]
        public void SurfaceAreaOfHemisphere()
            => Assert.AreEqual(9.424778, BallCap.SurfaceArea(1, 1), 0.0001);

        [TestMethod()]
        public void SurfaceAreaOfHemisphereByCapRadius()
            => Assert.AreEqual(9.424778, BallCap.SurfaceAreaByCapRadius(1, 1), 0.0001);

        [TestMethod()]
        public void SurfaceAreaOfHemisphereByPolarAngle()
            => Assert.AreEqual(9.424778, BallCap.SurfaceAreaByPolarAngle(1, Math.PI/2d), 0.0001);

        [TestMethod()]
        public void VolumeOfZeroCapHeight()
            => Assert.AreEqual(0, BallCap.Volume(1, 0), 0.00001);

        [TestMethod()]
        public void VolumeOfZeroPolarAngle()
            => Assert.AreEqual(0, BallCap.VolumeByPolarAngle(1, 0), 0.00001);

        [TestMethod()]
        public void VolumeOfZeroCapRadius()
            => Assert.AreEqual(0, BallCap.VolumeByCapRadius(0, 0), 0.0001);

        [TestMethod()]
        public void VolumeOfHemisphere()
            => Assert.AreEqual(2.094395, BallCap.Volume(1, 1), 0.0001);

        [TestMethod()]
        public void VolumeOfHemisphereByCapRadius()
            => Assert.AreEqual(2.094395, BallCap.VolumeByCapRadius(1, 1), 0.0001);

        [TestMethod()]
        public void VolumeOfHemisphereByPolarAngle()
            => Assert.AreEqual(2.094395, BallCap.VolumeByPolarAngle(1, Math.PI/2d), 0.0001);
    }
}
