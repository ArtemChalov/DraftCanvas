﻿using DraftCanvas;
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
            _canvas = new DrCanvas(800, 800);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 96.464466, 96.464466, 203.535534, 203.535534)]
        [DataRow(200, 100, 100, 200, 203.535534, 96.464466, 96.464466, 203.535534)]
        [DataRow(100, 200, 200, 100, 96.464466, 203.535534, 203.535534, 96.464466)]
        [DataRow(200, 200, 100, 100, 203.535534, 203.535534, 96.464466, 96.464466)]
        public void Add_Line_Length_No_Local_Constraint_No_Points_Constraint(double x1, double y1, double x2, double y2, double newX1, double newY1, double newX2, double newY2)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            double expectedAngle = lineSegment.Angle;
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Length += 10;

            Assert.AreEqual(151.421356, lineSegment.Length, 0.000001);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(newY1, lineSegment.Y1, 0.000001);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(expectedAngle, lineSegment.Angle);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 103.535534, 103.535534, 196.464466, 196.464466)]
        [DataRow(200, 100, 100, 200, 196.464466, 103.535534, 103.535534, 196.464466)]
        [DataRow(100, 200, 200, 100, 103.535534, 196.464466, 196.464466, 103.535534)]
        [DataRow(200, 200, 100, 100, 196.464466, 196.464466, 103.535534, 103.535534)]
        public void Subtract_Line_Length_No_Local_Constraint_No_Points_Constraint(double x1, double y1, double x2, double y2, double newX1, double newY1, double newX2, double newY2)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            double expectedAngle = lineSegment.Angle;
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Length -= 10;

            Assert.AreEqual(131.421356, lineSegment.Length, 0.000001);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(newY1, lineSegment.Y1, 0.000001);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(expectedAngle, lineSegment.Angle);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 207.071068, 207.071068)]
        [DataRow(100, 100, 0, 200, -7.071068, 207.071068)]
        [DataRow(100, 100, 200, 0, 207.071068, -7.071068)]
        [DataRow(100, 100, 0, 0, -7.071068, -7.071068)]
        public void Add_Line_Length_No_Local_Constraint_And_Has_First_Point_Constraint(double x1, double y1, double x2, double y2, double newX2, double newY2)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            double expectedAngle = lineSegment.Angle;
            _canvas.AddToVisualCollection(new DcLineSegment(50, 100, 100, 100));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Length += 10;

            Assert.AreEqual(151.421356, lineSegment.Length, 0.000001);
            Assert.AreEqual(x1, lineSegment.X1);
            Assert.AreEqual(y1, lineSegment.Y1);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(expectedAngle, lineSegment.Angle);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 192.928932, 192.928932)]
        [DataRow(100, 100, 0, 200, 7.071068, 192.928932)]
        [DataRow(100, 100, 200, 0, 192.928932, 7.071068)]
        [DataRow(100, 100, 0, 0, 7.071068, 7.071068)]
        public void Subtract_Line_Length_No_Local_Constraint_And_Has_First_Point_Constraint(double x1, double y1, double x2, double y2, double newX2, double newY2)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            double expectedAngle = lineSegment.Angle;
            _canvas.AddToVisualCollection(new DcLineSegment(50, 100, 100, 100));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Length -= 10;

            Assert.AreEqual(131.421356, lineSegment.Length, 0.000001);
            Assert.AreEqual(x1, lineSegment.X1);
            Assert.AreEqual(y1, lineSegment.Y1);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(expectedAngle, lineSegment.Angle);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 92.928932, 92.928932)]
        [DataRow(300, 100, 200, 200, 307.071068, 92.928932)]
        [DataRow(100, 300, 200, 200, 92.928932, 307.071068)]
        [DataRow(300, 300, 200, 200, 307.071068, 307.071068)]
        public void Add_Line_Length_No_Local_Constraint_And_Has_Second_Point_Constraint(double x1, double y1, double x2, double y2, double newX1, double newY1)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            double expectedAngle = lineSegment.Angle;
            _canvas.AddToVisualCollection(new DcLineSegment(100, 150, 200, 200));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Length += 10;

            Assert.AreEqual(151.421356, lineSegment.Length, 0.000001);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(newY1, lineSegment.Y1, 0.000001);
            Assert.AreEqual(x2, lineSegment.X2);
            Assert.AreEqual(y2, lineSegment.Y2);
            Assert.AreEqual(expectedAngle, lineSegment.Angle);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 107.071068, 107.071068)]
        [DataRow(300, 100, 200, 200, 292.928932, 107.071068)]
        [DataRow(100, 300, 200, 200, 107.071068, 292.928932)]
        [DataRow(300, 300, 200, 200, 292.928932, 292.928932)]
        public void Subtract_Line_Length_No_Local_Constraint_And_Has_Second_Point_Constraint(double x1, double y1, double x2, double y2, double newX1, double newY1)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            double expectedAngle = lineSegment.Angle;
            _canvas.AddToVisualCollection(new DcLineSegment(100, 150, 200, 200));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Length -= 10;

            Assert.AreEqual(131.421356, lineSegment.Length, 0.000001);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(newY1, lineSegment.Y1, 0.000001);
            Assert.AreEqual(x2, lineSegment.X2);
            Assert.AreEqual(y2, lineSegment.Y2);
            Assert.AreEqual(expectedAngle, lineSegment.Angle);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 93.148379, 206.851621, 41.33106)]
        [DataRow(200, 100, 100, 200, 206.851621, 93.148379, 138.66894)]
        [DataRow(100, 200, 200, 100, 93.148379, 206.851621, 318.66894)]
        [DataRow(200, 200, 100, 100, 206.851621, 93.148379, 221.33106)]
        public void Add_Line_Length_When_Has_Height_Constraint_No_Points_Constraint(double x1, double y1, double x2, double y2, double newX1, double newX2, double newAngle)
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
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 107.363241, 192.636759, 49.544606)]
        [DataRow(200, 100, 100, 200, 192.636759, 107.363241, 130.455394)]
        [DataRow(100, 200, 200, 100, 107.363241, 192.636759, 310.455394)]
        [DataRow(200, 200, 100, 100, 192.636759, 107.363241, 229.544606)]
        public void Subtract_Line_Length_When_Has_Height_Constraint_No_Points_Constraint(double x1, double y1, double x2, double y2, double newX1, double newX2, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            lineSegment.AddLocalConstraint(Constraints.Heigth);
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Length -= 10;

            Assert.AreEqual(131.421356, lineSegment.Length, 0.000001);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(y1, lineSegment.Y1);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(y2, lineSegment.Y2);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 213.703241, 41.33106)]
        [DataRow(100, 100, 0, 200, -13.703241, 138.66894)]
        [DataRow(100, 100, 200, 0, 213.703241, 318.66894)]
        [DataRow(100, 100, 0, 0, -13.703241, 221.33106)]
        public void Add_Line_Length_When_Has_Height_Constraint_And_Has_First_Point_Constraint(double x1, double y1, double x2, double y2, double newX2, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            lineSegment.AddLocalConstraint(Constraints.Heigth);
            _canvas.AddToVisualCollection(new DcLineSegment(50, 100, 100, 100));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Length += 10;

            Assert.AreEqual(151.421356, lineSegment.Length, 0.000001);
            Assert.AreEqual(x1, lineSegment.X1);
            Assert.AreEqual(y1, lineSegment.Y1);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(y2, lineSegment.Y2);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 185.273518, 49.544606)]
        [DataRow(100, 100, 0, 200, 14.726482, 130.455394)] 
        [DataRow(100, 100, 200, 0, 185.273518, 310.455394)]
        [DataRow(100, 100, 0, 0, 14.726482, 229.544606)]
        public void Subtract_Line_Length_When_Has_Height_Constraint_And_Has_First_Point_Constraint(double x1, double y1, double x2, double y2, double newX2, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            lineSegment.AddLocalConstraint(Constraints.Heigth);
            _canvas.AddToVisualCollection(new DcLineSegment(50, 100, 100, 100));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Length -= 10;

            Assert.AreEqual(131.421356, lineSegment.Length, 0.000001);
            Assert.AreEqual(x1, lineSegment.X1);
            Assert.AreEqual(y1, lineSegment.Y1);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(y2, lineSegment.Y2);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 86.296759, 41.33106)]
        [DataRow(300, 100, 200, 200, 313.703241, 138.66894)]
        [DataRow(100, 300, 200, 200, 86.296759, 318.66894)]
        [DataRow(300, 300, 200, 200, 313.703241, 221.33106)]
        public void Add_Line_Length_When_Has_Height_Constraint_And_Has_Second_Point_Constraint(double x1, double y1, double x2, double y2, double newX1, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            lineSegment.AddLocalConstraint(Constraints.Heigth);
            _canvas.AddToVisualCollection(new DcLineSegment(100, 150, 200, 200));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Length += 10;

            Assert.AreEqual(151.421356, lineSegment.Length, 0.000001);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(y1, lineSegment.Y1);
            Assert.AreEqual(x2, lineSegment.X2);
            Assert.AreEqual(y2, lineSegment.Y2);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 114.726482, 49.544606)] 
        [DataRow(300, 100, 200, 200, 285.273518, 130.455394)] 
        [DataRow(100, 300, 200, 200, 114.726482, 310.455394)]
        [DataRow(300, 300, 200, 200, 285.273518, 229.544606)]
        public void Subtract_Line_Length_When_Has_Height_Constraint_And_Has_Second_Point_Constraint(double x1, double y1, double x2, double y2, double newX1, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            lineSegment.AddLocalConstraint(Constraints.Heigth);
            _canvas.AddToVisualCollection(new DcLineSegment(100, 150, 200, 200));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Length -= 10;

            Assert.AreEqual(131.421356, lineSegment.Length, 0.000001);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(y1, lineSegment.Y1);
            Assert.AreEqual(x2, lineSegment.X2);
            Assert.AreEqual(y2, lineSegment.Y2);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 93.148379, 206.851621, 48.66894)]
        [DataRow(200, 100, 100, 200, 93.148379, 206.851621, 131.33106)]
        [DataRow(100, 200, 200, 100, 206.851621, 93.148379, 311.33106)]
        [DataRow(200, 200, 100, 100, 206.851621, 93.148379, 228.66894)]
        public void Add_Line_Length_When_Has_Width_Constraint_No_Points_Constraint(double x1, double y1, double x2, double y2, double newY1, double newY2, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            lineSegment.AddLocalConstraint(Constraints.Width);
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Length += 10;

            Assert.AreEqual(151.421356, lineSegment.Length, 0.000001);
            Assert.AreEqual(x1, lineSegment.X1);
            Assert.AreEqual(newY1, lineSegment.Y1, 0.000001);
            Assert.AreEqual(x2, lineSegment.X2);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 107.363241, 192.636759, 40.455394)]
        [DataRow(200, 100, 100, 200, 107.363241, 192.636759, 139.544606)]
        [DataRow(100, 200, 200, 100, 192.636759, 107.363241, 319.544606)]
        [DataRow(200, 200, 100, 100, 192.636759, 107.363241, 220.455394)]
        public void Subtract_Line_Length_When_Has_Width_Constraint_No_Points_Constraint(double x1, double y1, double x2, double y2, double newY1, double newY2, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            lineSegment.AddLocalConstraint(Constraints.Width);
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Length -= 10;

            Assert.AreEqual(131.421356, lineSegment.Length, 0.000001);
            Assert.AreEqual(x1, lineSegment.X1);
            Assert.AreEqual(newY1, lineSegment.Y1, 0.000001);
            Assert.AreEqual(x2, lineSegment.X2);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 213.703241, 48.66894)]
        [DataRow(100, 100, 0, 200, 213.703241, 131.33106)]
        [DataRow(100, 100, 200, 0, -13.703241, 311.33106)]
        [DataRow(100, 100, 0, 0, -13.703241, 228.66894)]
        public void Add_Line_Length_When_Has_Width_Constraint_And_Has_First_Point_Constraint(double x1, double y1, double x2, double y2, double newY2, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            lineSegment.AddLocalConstraint(Constraints.Width);
            _canvas.AddToVisualCollection(new DcLineSegment(50, 100, 100, 100));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Length += 10;

            Assert.AreEqual(151.421356, lineSegment.Length, 0.000001);
            Assert.AreEqual(x1, lineSegment.X1);
            Assert.AreEqual(y1, lineSegment.Y1);
            Assert.AreEqual(x2, lineSegment.X2);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 185.273518, 40.455394)]
        [DataRow(100, 100, 0, 200, 185.273518, 139.544606)]
        [DataRow(100, 100, 200, 0, 14.726482, 319.544606)]
        [DataRow(100, 100, 0, 0, 14.726482, 220.455394)]
        public void Subtract_Line_Length_When_Has_Width_Constraint_And_Has_First_Point_Constraint(double x1, double y1, double x2, double y2, double newY2, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            lineSegment.AddLocalConstraint(Constraints.Width);
            _canvas.AddToVisualCollection(new DcLineSegment(50, 100, 100, 100));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Length -= 10;

            Assert.AreEqual(131.421356, lineSegment.Length, 0.000001);
            Assert.AreEqual(x1, lineSegment.X1);
            Assert.AreEqual(y1, lineSegment.Y1);
            Assert.AreEqual(x2, lineSegment.X2);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 86.296759, 48.66894)]
        [DataRow(300, 100, 200, 200, 86.296759, 131.33106)]
        [DataRow(100, 300, 200, 200, 313.703241, 311.33106)]
        [DataRow(300, 300, 200, 200, 313.703241, 228.66894)]
        public void Add_Line_Length_When_Has_Width_Constraint_And_Has_Second_Point_Constraint(double x1, double y1, double x2, double y2, double newY1, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            lineSegment.AddLocalConstraint(Constraints.Width);
            _canvas.AddToVisualCollection(new DcLineSegment(100, 150, 200, 200));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Length += 10;

            Assert.AreEqual(151.421356, lineSegment.Length, 0.000001);
            Assert.AreEqual(x1, lineSegment.X1);
            Assert.AreEqual(newY1, lineSegment.Y1, 0.000001);
            Assert.AreEqual(x2, lineSegment.X2);
            Assert.AreEqual(y2, lineSegment.Y2);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 114.726482, 40.455394)]
        [DataRow(300, 100, 200, 200, 114.726482, 139.544606)]
        [DataRow(100, 300, 200, 200, 285.273518, 319.544606)]
        [DataRow(300, 300, 200, 200, 285.273518, 220.455394)]
        public void Subtract_Line_Length_When_Has_Width_Constraint_And_Has_Second_Point_Constraint(double x1, double y1, double x2, double y2, double newY1, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            lineSegment.AddLocalConstraint(Constraints.Width);
            _canvas.AddToVisualCollection(new DcLineSegment(100, 150, 200, 200));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Length -= 10;

            Assert.AreEqual(131.421356, lineSegment.Length, 0.000001);
            Assert.AreEqual(x1, lineSegment.X1);
            Assert.AreEqual(newY1, lineSegment.Y1, 0.000001);
            Assert.AreEqual(x2, lineSegment.X2);
            Assert.AreEqual(y2, lineSegment.Y2);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }
    }
}
