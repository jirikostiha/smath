namespace Wayout.Mathematics.Geometry.D3
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BallTest
    {
        [TestMethod]
        public void Volume()
        {
            Assert.AreEqual(4.1887902, Ball.Volume(1), 0.0001);
        }

        [TestMethod]
        public void RadiusFromVolume()
        {
            Assert.AreEqual(1, Ball.RadiusFromVolume(4.1887902), 0.0001);
        }
    }
}
