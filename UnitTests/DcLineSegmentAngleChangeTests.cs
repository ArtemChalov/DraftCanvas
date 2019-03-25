using System;
using System.Windows;
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
        [DataRow(0, 200, 100)]
        [DataRow(30, 186.60254, 150)]
        [DataRow(45, 170.710678, 170.710678)]
        [DataRow(60, 150, 186.60254)]
        [DataRow(90, 100, 200)]
        [DataRow(120, 50, 186.60254)]
        [DataRow(135, 29.289322, 170.710678)]
        [DataRow(150, 13.39746, 150)]
        [DataRow(180, 0, 100)]
        [DataRow(210, 13.39746, 50)]
        [DataRow(225, 29.289322, 29.289322)]
        [DataRow(240, 50, 13.39746)]
        [DataRow(270, 100, 0)]
        [DataRow(300, 150, 13.39746)]
        [DataRow(315, 170.710678, 29.289322)]
        [DataRow(330, 186.60254, 50)]
        public void Change_Line_Angle_No_Local_Constraint_No_Points_Constraint_Or_First_Point_Has_Constraint(double newAngle, double newX2, double newY2)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(new Point(100, 100), 100, 10);
            double expectedLength = lineSegment.Length;
            double expectedX1 = lineSegment.X1;
            double expectedY1 = lineSegment.Y1;
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Angle = newAngle;

            Assert.AreEqual(expectedLength, lineSegment.Length);
            Assert.AreEqual(expectedX1, lineSegment.X1);
            Assert.AreEqual(expectedY1, lineSegment.Y1);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(-30, 330, 186.60254, 50)]
        [DataRow(360, 0, 200, 100)]
        [DataRow(390, 30, 186.60254, 150)]
        public void Change_Line_Angle_No_Local_Constraint_No_Points_Constraint_Or_First_Point_Has_Constraint_2(double newAngle, double expectedAngle, double newX2, double newY2)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(new Point(100, 100), 100, 10);
            double expectedLength = lineSegment.Length;
            double expectedX1 = lineSegment.X1;
            double expectedY1 = lineSegment.Y1;
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Angle = newAngle;

            Assert.AreEqual(expectedLength, lineSegment.Length);
            Assert.AreEqual(expectedX1, lineSegment.X1);
            Assert.AreEqual(expectedY1, lineSegment.Y1);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(expectedAngle, lineSegment.Angle, 0.000001);
        }
    }
}
