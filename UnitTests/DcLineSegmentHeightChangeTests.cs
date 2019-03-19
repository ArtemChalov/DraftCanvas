﻿using DraftCanvas;
using DraftCanvas.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class DcLineSegmentHeightChangeTests
    {
        static DrCanvas _canvas;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _canvas = new DrCanvas(800, 800);
        }


        [TestMethod]
        [DataRow(100, 100, 200, 200, 95, 205, 47.726311)]
        [DataRow(200, 100, 100, 200, 95, 205, 132.273689)]
        [DataRow(100, 200, 200, 100, 205, 95, 312.273689)]
        [DataRow(200, 200, 100, 100, 205, 95, 227.726311)]
        public void Add_Line_Height_No_Local_Constraint_No_Points_Constraint(double x1, double y1, double x2, double y2, double newY1, double newY2, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Height += 10;

            Assert.AreEqual(148.660687, lineSegment.Length, 0.000001);
            Assert.AreEqual(x1, lineSegment.X1);
            Assert.AreEqual(newY1, lineSegment.Y1, 0.000001);
            Assert.AreEqual(x2, lineSegment.X2);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 105, 195, 41.987212)]
        [DataRow(200, 100, 100, 200, 105, 195, 138.012788)]
        [DataRow(100, 200, 200, 100, 195, 105, 318.012788)]
        [DataRow(200, 200, 100, 100, 195, 105, 221.987212)]
        public void Subtract_Line_Height_No_Local_Constraint_No_Points_Constraint(double x1, double y1, double x2, double y2, double newY1, double newY2, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Height -= 10;

            Assert.AreEqual(134.53624, lineSegment.Length, 0.000001);
            Assert.AreEqual(x1, lineSegment.X1);
            Assert.AreEqual(newY1, lineSegment.Y1, 0.000001);
            Assert.AreEqual(x2, lineSegment.X2);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 105.559028, 95, 194.440972, 205, 51.061176)]
        [DataRow(200, 100, 100, 200, 194.440972, 95, 105.559028, 205, 128.938824)]
        [DataRow(100, 200, 200, 100, 105.559028, 205, 194.440972, 95, 308.938824)]
        [DataRow(200, 200, 100, 100, 194.440972, 205, 105.559028, 95, 231.061176)]
        public void Add_Line_Height_When_Has_Length_Constraint_No_Points_Constraint(double x1, double y1, double x2, double y2, double newX1, double newY1, double newX2, double newY2, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            double expectedLength = lineSegment.Length;
            lineSegment.AddLocalConstraint(Constraints.Length);
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Height += 10;

            Assert.AreEqual(expectedLength, lineSegment.Length);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(newY1, lineSegment.Y1, 0.000001);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 95.456439, 105, 204.543561, 195, 39.523608)]
        [DataRow(200, 100, 100, 200, 204.543561, 105, 95.456439, 195, 140.476392)]
        [DataRow(100, 200, 200, 100, 95.456439, 195, 204.543561, 105, 320.476392)]
        [DataRow(200, 200, 100, 100, 204.543561, 195, 95.456439, 105, 219.523608)]
        public void Subtract_Line_Height_When_Has_Length_Constraint_No_Points_Constraint(double x1, double y1, double x2, double y2, double newX1, double newY1, double newX2, double newY2, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            double expectedLength = lineSegment.Length;
            lineSegment.AddLocalConstraint(Constraints.Length);
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Height -= 10;

            Assert.AreEqual(expectedLength, lineSegment.Length);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(newY1, lineSegment.Y1, 0.000001);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(10)]
        [DataRow(-10)]
        public void Add_Sub_Line_Height_When_Has_Length_And_Width_Constraints_No_Points_Constraint_NO_CHANGES(double delta)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(100, 100, 200, 200);
            double expectedLength = lineSegment.Length;
            double expectedAngle = lineSegment.Angle;
            double expectedX1 = lineSegment.X1;
            double expectedY1 = lineSegment.Y1;
            double expectedX2 = lineSegment.X2;
            double expectedY2 = lineSegment.Y2;
            lineSegment.AddLocalConstraint(Constraints.Length);
            lineSegment.AddLocalConstraint(Constraints.Width);
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Height += delta;

            Assert.AreEqual(expectedLength, lineSegment.Length);
            Assert.AreEqual(expectedX1, lineSegment.X1);
            Assert.AreEqual(expectedY1, lineSegment.Y1);
            Assert.AreEqual(expectedX2, lineSegment.X2);
            Assert.AreEqual(expectedY2, lineSegment.Y2);
            Assert.AreEqual(expectedAngle, lineSegment.Angle);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 95, 95, 205, 205, 155.563492)]
        [DataRow(200, 100, 100, 200, 205, 95, 95, 205, 155.563492)]
        [DataRow(100, 200, 200, 100, 95, 205, 205, 95, 155.563492)]
        [DataRow(200, 200, 100, 100, 205, 205, 95, 95, 155.563492)]
        public void Add_Line_Height_When_Has_Angle_Constraint_No_Points_Constraint(double x1, double y1, double x2, double y2, double newX1, double newY1, double newX2, double newY2, double newLength)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            double expectedAngle = lineSegment.Angle;
            lineSegment.AddLocalConstraint(Constraints.Angle);
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Height += 10;

            Assert.AreEqual(newLength, lineSegment.Length, 0.000001);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(newY1, lineSegment.Y1, 0.000001);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(expectedAngle, lineSegment.Angle);
        }
    }
}
