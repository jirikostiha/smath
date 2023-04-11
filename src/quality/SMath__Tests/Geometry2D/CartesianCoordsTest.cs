namespace Wayout.Mathematics.Geometry
{
    using System;
    using System.Linq;
    using static System.Math;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CartesianCoordsTest
    {
        [DataTestMethod]
        [DataRow(0, 0, 0, 0,  0,    0, 0, DisplayName = "zero in zero")]
        [DataRow(0, 0, 0, 0,  PI,   0, 0, DisplayName = "zero in zero")]
        [DataRow(0, 0, 0, 0,  3*PI, 0, 0, DisplayName = "zero in zero")]
        [DataRow(0, 0, 0, 0, -4*PI, 0, 0, DisplayName = "zero in zero")]
        [DataRow(1, 0, 0, 0,  0,    1, 0, DisplayName = "around origin")]
        [DataRow(2, 0, 0, 0, PI,   -2, 0, DisplayName = "around origin")]
        [DataRow(4, 4, 0, 0, PI,   -4,-4, DisplayName = "around origin")]
        [DataRow(2, 1, 1, 1, PI,    0, 1, DisplayName = "around Q1 point")]
        [DataRow(3,-3, 3, 0, PI,    3, 3, DisplayName = "around Q4 point")]
        public void Rotate_AroundOtherPoint_CorrectResults(double x1, double x2, double cx1, double cx2, double angle, double ex1, double ex2)
        {
            Assert.AreEqual(ex1, CartesianCoords.Rotate(x1, x2, cx1, cx2, angle).X1, 0.000000001);
            Assert.AreEqual(ex2, CartesianCoords.Rotate(x1, x2, cx1, cx2, angle).X2, 0.000000001);
        }
    }
}
