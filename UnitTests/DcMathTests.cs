using System.Windows;
using DraftCanvas.Primitives;
using DraftCanvas.Servicies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class DcMathTests
    {
        [TestMethod]
        [DataRow(100, 200, 100, -370)]
        [DataRow(100, 200, 100, -360)]
        [DataRow(100, 200, 100, -330)]
        [DataRow(100, 200, 100, -270)]
        [DataRow(100, 200, 100, -180)]
        [DataRow(100, 200, 100, -90)]
        [DataRow(100, 200, 100, -45)]
        [DataRow(100, 200, 100, 0)]
        [DataRow(100, 200, 100, 30)]
        [DataRow(100, 200, 100, 45)]
        [DataRow(100, 200, 100, 60)]
        [DataRow(100, 200, 100, 90)]
        [DataRow(100, 200, 100, 120)]
        [DataRow(100, 200, 100, 135)]
        [DataRow(100, 200, 100, 150)]
        [DataRow(100, 200, 100, 180)]
        [DataRow(100, 200, 100, 210)]
        [DataRow(100, 200, 100, 225)]
        [DataRow(100, 200, 100, 240)]
        [DataRow(100, 200, 100, 270)]
        [DataRow(100, 200, 100, 300)]
        [DataRow(100, 200, 100, 315)]
        [DataRow(100, 200, 100, 330)]
        [DataRow(100, 200, 100, 360)]
        [DataRow(100, 200, 100, 370)]
        public void GetAngleByHeight_Tests(double x1, double x2, double length, double angle)
        {
            DcLineSegment lineSegment = new DcLineSegment(new Point(x1, x2), length, angle);
            var actual = DcMath.GetAngleByHeight(lineSegment.Height, lineSegment.Length, lineSegment.X1, lineSegment.Y1, lineSegment.X2, lineSegment.Y2);
            Assert.AreEqual(lineSegment.Angle, actual, 0.0001);
        }
    }
}
