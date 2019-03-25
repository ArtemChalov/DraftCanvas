using System;
using DraftCanvas;
using DraftCanvas.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class DcLineSegmentAngleChangeTests
    {
        static DrCanvas _canvas;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _canvas = new DrCanvas(800, 800);
        }

        [TestMethod]
        [DataRow(0, 241.421356, 100)]
        //[DataRow(200, 100, 100)]
        //[DataRow(100, 200, 200)]
        //[DataRow(200, 200, 100)]
        public void Change_Line_Angle_No_Local_Constraint_No_Points_Constraint_Or_First_Point_Has_Constraint(double newAngle, double newX2, double newY2)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(100, 100, 200, 200);
            double expectedLength = lineSegment.Length;
            double expectedX1 = lineSegment.X1;
            double expectedY1 = lineSegment.Y1;
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Angle += newAngle;

            Assert.AreEqual(expectedLength, lineSegment.Length);
            Assert.AreEqual(expectedX1, lineSegment.X1);
            Assert.AreEqual(expectedY1, lineSegment.Y1);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            //Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }
    }
}
