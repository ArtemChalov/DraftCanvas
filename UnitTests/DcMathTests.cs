using System.Diagnostics;
using System.Windows;
using DraftCanvas.Primitives;
using DraftCanvas.Servicies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using prmath;

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
            var actual = PrimMath.GetAngleByHeight(lineSegment.Height, lineSegment.Length, lineSegment.X1, lineSegment.Y1, lineSegment.X2, lineSegment.Y2);
            Assert.AreEqual(lineSegment.Angle, actual, 0.0001);
        }

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
        public void GetAngleByWidth_Tests(double x1, double x2, double length, double angle)
        {
            DcLineSegment lineSegment = new DcLineSegment(new Point(x1, x2), length, angle);

            var actual = PrimMath.GetAngleByWidth(lineSegment.Width, lineSegment.Length, lineSegment.X1, lineSegment.Y1, lineSegment.X2, lineSegment.Y2);

            Assert.AreEqual(lineSegment.Angle, actual, 0.0001);
        }

        [TestMethod]
        [DataRow(100, 200, 100, 30, 86.602540)]
        [DataRow(100, 200, 100, 45, 70.710678)]
        [DataRow(100, 200, 100, 60, 50.000000)]
        [DataRow(100, 200, 100, 90, 0)]
        [DataRow(100, 200, 100, 120, -50.000000)]
        [DataRow(100, 200, 100, 135, -70.710678)]
        [DataRow(100, 200, 100, 150, -86.602540)]
        [DataRow(100, 200, 100, 210, -86.602540)]
        [DataRow(100, 200, 100, 225, -70.710678)]
        [DataRow(100, 200, 100, 240, -50.000000)]
        [DataRow(100, 200, 100, 270, 0)]
        [DataRow(100, 200, 100, 300, 50.000000)]
        [DataRow(100, 200, 100, 315, 70.710678)]
        [DataRow(100, 200, 100, 330, 86.602540)]
        public void XoffsetByTan_Tests(double x1, double x2, double length, double angle, double dx)
        {
            DcLineSegment lineSegment = new DcLineSegment(new Point(x1, x2), length, angle);

            var actual = PrimMath.XoffsetByTan(lineSegment.Height, lineSegment.Angle);

            Assert.AreEqual(dx, actual, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 100, 0, 0)]
        [DataRow(100, 100, 100, 30, 50)]
        [DataRow(100, 100, 100, 45, 70.710678)]
        [DataRow(100, 100, 100, 60, 86.602540)]
        [DataRow(100, 100, 100, 120, 86.602540)]
        [DataRow(100, 100, 100, 135, 70.710678)]
        [DataRow(100, 100, 100, 150, 50)]
        [DataRow(100, 100, 100, 180, 0)]
        [DataRow(100, 100, 100, 210, -50)]
        [DataRow(100, 100, 100, 225, -70.710678)]
        [DataRow(100, 100, 100, 240, -86.602540)]
        [DataRow(100, 100, 100, 300, -86.602540)]
        [DataRow(100, 100, 100, 315, -70.710678)]
        [DataRow(100, 100, 100, 330, -50.000000)]
        public void YoffsetByTan_Tests(double x1, double x2, double length, double angle, double dy)
        {
            DcLineSegment lineSegment = new DcLineSegment(new Point(x1, x2), length, angle);

            var actual = PrimMath.YoffsetByTan(lineSegment.Width, lineSegment.Angle);

            Assert.AreEqual(dy, actual, 0.000001);
        }
    }
}
