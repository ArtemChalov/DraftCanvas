using System;
using DraftCanvas;
using DraftCanvas.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class DcLineSegmentLengthChangeTests
    {
        static DrCanvas _canvas;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _canvas = new DraftCanvas.DrCanvas(800, 800);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 20.851621, 9.148379)]
        [DataRow(200, 100, 100, 200, 206.851621, 93.148379)]
        public void AddLength_Has_Height_Constraint(double x1, double y1, double x2, double y2, double newX1, double newX2)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            lineSegment.AddLocalConstraint(Constraints.Heigth);
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Length += 10;

            Assert.AreEqual(151.421356, lineSegment.Length, 0.000001);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(y1, lineSegment.Y1);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(y2, lineSegment.Y2);
        }
    }
}
