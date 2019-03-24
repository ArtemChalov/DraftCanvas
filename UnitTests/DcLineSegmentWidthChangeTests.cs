using DraftCanvas;
using DraftCanvas.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class DcLineSegmentWidthChangeTests
    {
        static DrCanvas _canvas;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _canvas = new DrCanvas(800, 800);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 95, 205, 42.273689)]
        [DataRow(200, 100, 100, 200, 205, 95, 137.726311)]
        [DataRow(100, 200, 200, 100, 95, 205, 317.726311)]
        [DataRow(200, 200, 100, 100, 205, 95, 222.273689)]
        public void Add_Line_Width_No_Local_Constraint_No_Points_Constraint(double x1, double y1, double x2, double y2, double newX1, double newX2, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Width += 10;

            Assert.AreEqual(148.660687, lineSegment.Length, 0.000001);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(y1, lineSegment.Y1);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(y2, lineSegment.Y2);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 105, 195, 48.012788)]
        [DataRow(200, 100, 100, 200, 195, 105, 131.987212)]
        [DataRow(100, 200, 200, 100, 105, 195, 311.987212)]
        [DataRow(200, 200, 100, 100, 195, 105, 228.012788)]
        public void Subtract_Line_Width_No_Local_Constraint_No_Points_Constraint(double x1, double y1, double x2, double y2, double newX1, double newX2, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Width -= 10;

            Assert.AreEqual(134.53624, lineSegment.Length, 0.000001);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(y1, lineSegment.Y1);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(y2, lineSegment.Y2);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 95, 105.559028, 205, 194.440972, 38.938824)]
        [DataRow(200, 100, 100, 200, 205, 105.559028, 95, 194.440972, 141.061176)]
        [DataRow(100, 200, 200, 100, 95, 194.440972, 205, 105.559028, 321.061176)]
        [DataRow(200, 200, 100, 100, 205, 194.440972, 95, 105.559028, 218.938824)]
        public void Add_Line_Width_When_Has_Length_Constraint_No_Points_Constraint(double x1, double y1, double x2, double y2, double newX1, double newY1, double newX2, double newY2, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            double expectedLength = lineSegment.Length;
            lineSegment.AddLocalConstraint(Constraints.Length);
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Width += 10;

            Assert.AreEqual(expectedLength, lineSegment.Length);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(newY1, lineSegment.Y1, 0.000001);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 105, 95.456439, 195, 204.543561, 50.476392)]
        [DataRow(200, 100, 100, 200, 195, 95.456439, 105, 204.543561, 129.523608)]
        [DataRow(100, 200, 200, 100, 105, 204.543561, 195, 95.456439, 309.523608)]
        [DataRow(200, 200, 100, 100, 195, 204.543561, 105, 95.456439, 230.476392)]
        public void Subtract_Line_Width_When_Has_Length_Constraint_No_Points_Constraint(double x1, double y1, double x2, double y2, double newX1, double newY1, double newX2, double newY2, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            double expectedLength = lineSegment.Length;
            lineSegment.AddLocalConstraint(Constraints.Length);
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Width -= 10;

            Assert.AreEqual(expectedLength, lineSegment.Length);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(newY1, lineSegment.Y1, 0.000001);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 95, 95, 205, 205, 155.563492)]
        [DataRow(200, 100, 100, 200, 205, 95, 95, 205, 155.563492)]
        [DataRow(100, 200, 200, 100, 95, 205, 205, 95, 155.563492)]
        [DataRow(200, 200, 100, 100, 205, 205, 95, 95, 155.563492)]
        public void Add_Line_Width_When_Has_Angle_Constraint_No_Points_Constraint(double x1, double y1, double x2, double y2, double newX1, double newY1, double newX2, double newY2, double newLength)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            double expectedAngle = lineSegment.Angle;
            lineSegment.AddLocalConstraint(Constraints.Angle);
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Width += 10;

            Assert.AreEqual(newLength, lineSegment.Length, 0.000001);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(newY1, lineSegment.Y1, 0.000001);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(expectedAngle, lineSegment.Angle);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 105, 105, 195, 195, 127.279221)]
        [DataRow(200, 100, 100, 200, 195, 105, 105, 195, 127.279221)]
        [DataRow(100, 200, 200, 100, 105, 195, 195, 105, 127.279221)]
        [DataRow(200, 200, 100, 100, 195, 195, 105, 105, 127.279221)]
        public void Subtract_Line_Width_When_Has_Angle_Constraint_No_Points_Constraint(double x1, double y1, double x2, double y2, double newX1, double newY1, double newX2, double newY2, double newLength)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            double expectedAngle = lineSegment.Angle;
            lineSegment.AddLocalConstraint(Constraints.Angle);
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Width -= 10;

            Assert.AreEqual(newLength, lineSegment.Length, 0.000001);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(newY1, lineSegment.Y1, 0.000001);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(expectedAngle, lineSegment.Angle);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 210, 148.660687, 42.273689)]
        [DataRow(100, 100, 0, 200, -10, 148.660687, 137.726311)]
        [DataRow(100, 100, 200, 0, 210, 148.660687, 317.726311)]
        [DataRow(100, 100, 0, 0, -10, 148.660687, 222.273689)]
        public void Add_Line_Width_When_No_Local_Constraint_And_Has_First_Point_Constraint(double x1, double y1, double x2, double y2, double newX2, double newLength, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            _canvas.AddToVisualCollection(new DcLineSegment(50, 100, 100, 100));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Width += 10;

            Assert.AreEqual(newLength, lineSegment.Length, 0.000001);
            Assert.AreEqual(x1, lineSegment.X1);
            Assert.AreEqual(y1, lineSegment.Y1);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(y2, lineSegment.Y2);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 190, 134.53624, 48.012788)]
        [DataRow(100, 100, 0, 200, 10, 134.53624, 131.987212)]
        [DataRow(100, 100, 200, 0, 190, 134.53624, 311.987212)]
        [DataRow(100, 100, 0, 0, 10, 134.53624, 228.012788)]
        public void Subtract_Line_Width_When_No_Local_Constraint_And_Has_First_Point_Constraint(double x1, double y1, double x2, double y2, double newX2, double newLength, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            _canvas.AddToVisualCollection(new DcLineSegment(50, 100, 100, 100));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Width -= 10;

            Assert.AreEqual(newLength, lineSegment.Length, 0.000001);
            Assert.AreEqual(x1, lineSegment.X1);
            Assert.AreEqual(y1, lineSegment.Y1);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(y2, lineSegment.Y2);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 210, 210, 155.563492, 110)]
        [DataRow(100, 100, 0, 200, -10, 210, 155.563492, 110)]
        [DataRow(100, 100, 200, 0, 210, -10, 155.563492, 110)]
        [DataRow(100, 100, 0, 0, -10, -10, 155.563492, 110)]
        public void Add_Line_Width_When_Has_Angle_LocalConstraint_And_Has_First_Point_Constraint(double x1, double y1, double x2, double y2, double newX2, double newY2, double newLength, double newHeight)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            lineSegment.AddLocalConstraint(Constraints.Angle);
            _canvas.AddToVisualCollection(new DcLineSegment(50, 100, 100, 100));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Width += 10;

            Assert.AreEqual(newLength, lineSegment.Length, 0.000001);
            Assert.AreEqual(x1, lineSegment.X1);
            Assert.AreEqual(y1, lineSegment.Y1);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(newHeight, lineSegment.Height, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 190, 190, 127.279221, 90)]
        [DataRow(100, 100, 0, 200, 10, 190, 127.279221, 90)]
        [DataRow(100, 100, 200, 0, 190, 10, 127.279221, 90)]
        [DataRow(100, 100, 0, 0, 10, 10, 127.279221, 90)]
        public void Subtract_Line_Width_When_Has_Angle_LocalConstraint_And_Has_First_Point_Constraint(double x1, double y1, double x2, double y2, double newX2, double newY2, double newLength, double newHeight)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            lineSegment.AddLocalConstraint(Constraints.Angle);
            _canvas.AddToVisualCollection(new DcLineSegment(50, 100, 100, 100));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Width -= 10;

            Assert.AreEqual(newLength, lineSegment.Length, 0.000001);
            Assert.AreEqual(x1, lineSegment.X1);
            Assert.AreEqual(y1, lineSegment.Y1);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(newHeight, lineSegment.Height, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 210, 188.881944, 38.938824, 88.881944)]
        [DataRow(100, 100, 0, 200, -10, 188.881944, 141.061176, 88.881944)]
        [DataRow(100, 100, 200, 0, 210, 11.118056, 321.061176, 88.881944)]
        [DataRow(100, 100, 0, 0, -10, 11.118056, 218.938824, 88.881944)]
        public void Add_Line_Width_When_Has_Length_LocalConstraint_And_Has_First_Point_Constraint(double x1, double y1, double x2, double y2, double newX2, double newY2, double newAngle, double newHeight)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            double expectedLength = lineSegment.Length;
            lineSegment.AddLocalConstraint(Constraints.Length);
            _canvas.AddToVisualCollection(new DcLineSegment(50, 100, 100, 100));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Width += 10;

            Assert.AreEqual(expectedLength, lineSegment.Length);
            Assert.AreEqual(x1, lineSegment.X1);
            Assert.AreEqual(y1, lineSegment.Y1);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
            Assert.AreEqual(newHeight, lineSegment.Height, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 190, 209.087121, 50.476392, 109.087121)]
        [DataRow(100, 100, 0, 200, 10, 209.087121, 129.523608, 109.087121)]
        [DataRow(100, 100, 200, 0, 190, -9.087121, 309.523608, 109.087121)]
        [DataRow(100, 100, 0, 0, 10, -9.087121, 230.476392, 109.087121)]
        public void Subtract_Line_Width_When_Has_Length_LocalConstraint_And_Has_First_Point_Constraint(double x1, double y1, double x2, double y2, double newX2, double newY2, double newAngle, double newHeight)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            double expectedLength = lineSegment.Length;
            lineSegment.AddLocalConstraint(Constraints.Length);
            _canvas.AddToVisualCollection(new DcLineSegment(50, 100, 100, 100));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Width -= 10;

            Assert.AreEqual(expectedLength, lineSegment.Length);
            Assert.AreEqual(x1, lineSegment.X1);
            Assert.AreEqual(y1, lineSegment.Y1);
            Assert.AreEqual(newX2, lineSegment.X2, 0.000001);
            Assert.AreEqual(newY2, lineSegment.Y2, 0.000001);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
            Assert.AreEqual(newHeight, lineSegment.Height, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 90, 148.660687, 42.273689)]
        [DataRow(300, 100, 200, 200, 310, 148.660687, 137.726311)]
        [DataRow(100, 300, 200, 200, 90, 148.660687, 317.726311)]
        [DataRow(300, 300, 200, 200, 310, 148.660687, 222.273689)]
        public void Add_Line_Height_When_No_Local_Constraint_And_Has_Second_Point_Constraint(double x1, double y1, double x2, double y2, double newX1, double newLength, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            _canvas.AddToVisualCollection(new DcLineSegment(100, 150, 200, 200));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Width += 10;

            Assert.AreEqual(newLength, lineSegment.Length, 0.000001);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(y1, lineSegment.Y1);
            Assert.AreEqual(x2, lineSegment.X2);
            Assert.AreEqual(y2, lineSegment.Y2);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 110, 134.53624, 48.012788)]
        [DataRow(300, 100, 200, 200, 290, 134.53624, 131.987212)]
        [DataRow(100, 300, 200, 200, 110, 134.53624, 311.987212)]
        [DataRow(300, 300, 200, 200, 290, 134.53624, 228.012788)]
        public void Subtract_Line_Height_When_No_Local_Constraint_And_Has_Second_Point_Constraint(double x1, double y1, double x2, double y2, double newX1, double newLength, double newAngle)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            _canvas.AddToVisualCollection(new DcLineSegment(100, 150, 200, 200));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Width -= 10;

            Assert.AreEqual(newLength, lineSegment.Length, 0.000001);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(y1, lineSegment.Y1);
            Assert.AreEqual(x2, lineSegment.X2);
            Assert.AreEqual(y2, lineSegment.Y2);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 90, 111.118056, 38.938824, 88.881944)]
        [DataRow(300, 100, 200, 200, 310, 111.118056, 141.061176, 88.881944)]
        [DataRow(100, 300, 200, 200, 90, 288.881944, 321.061176, 88.881944)]
        [DataRow(300, 300, 200, 200, 310, 288.881944, 218.938824, 88.881944)]
        public void Add_Line_Width_When_Has_Length_LocalConstraint_And_Has_Second_Point_Constraint(double x1, double y1, double x2, double y2, double newX1, double newY1, double newAngle, double newHeight)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            double expectedLength = lineSegment.Length;
            lineSegment.AddLocalConstraint(Constraints.Length);
            _canvas.AddToVisualCollection(new DcLineSegment(100, 150, 200, 200));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Width += 10;

            Assert.AreEqual(expectedLength, lineSegment.Length);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(newY1, lineSegment.Y1, 0.000001);
            Assert.AreEqual(x2, lineSegment.X2);
            Assert.AreEqual(y2, lineSegment.Y2);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
            Assert.AreEqual(newHeight, lineSegment.Height, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 110, 90.912879, 50.476392, 109.087121)]
        [DataRow(300, 100, 200, 200, 290, 90.912879, 129.523608, 109.087121)]
        [DataRow(100, 300, 200, 200, 110, 309.087121, 309.523608, 109.087121)]
        [DataRow(300, 300, 200, 200, 290, 309.087121, 230.476392, 109.087121)]
        public void Subtract_Line_Width_When_Has_Length_LocalConstraint_And_Has_Second_Point_Constraint(double x1, double y1, double x2, double y2, double newX1, double newY1, double newAngle, double newHeight)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            double expectedLength = lineSegment.Length;
            lineSegment.AddLocalConstraint(Constraints.Length);
            _canvas.AddToVisualCollection(new DcLineSegment(100, 150, 200, 200));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Width -= 10;

            Assert.AreEqual(expectedLength, lineSegment.Length);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(newY1, lineSegment.Y1, 0.000001);
            Assert.AreEqual(x2, lineSegment.X2);
            Assert.AreEqual(y2, lineSegment.Y2);
            Assert.AreEqual(newAngle, lineSegment.Angle, 0.000001);
            Assert.AreEqual(newHeight, lineSegment.Height, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 90, 90, 155.563492, 110)]
        [DataRow(300, 100, 200, 200, 310, 90, 155.563492, 110)]
        [DataRow(100, 300, 200, 200, 90, 310, 155.563492, 110)]
        [DataRow(300, 300, 200, 200, 310, 310, 155.563492, 110)]
        public void Add_Line_Width_When_Has_Angle_LocalConstraint_And_Has_Second_Point_Constraint(double x1, double y1, double x2, double y2, double newX1, double newY1, double newLength, double newHeigth)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            double expectedAngle = lineSegment.Angle;
            lineSegment.AddLocalConstraint(Constraints.Angle);
            _canvas.AddToVisualCollection(new DcLineSegment(100, 150, 200, 200));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Width += 10;

            Assert.AreEqual(newLength, lineSegment.Length, 0.000001);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(newY1, lineSegment.Y1, 0.000001);
            Assert.AreEqual(x2, lineSegment.X2);
            Assert.AreEqual(y2, lineSegment.Y2);
            Assert.AreEqual(expectedAngle, lineSegment.Angle);
            Assert.AreEqual(newHeigth, lineSegment.Height, 0.000001);
        }

        [TestMethod]
        [DataRow(100, 100, 200, 200, 110, 110, 127.279221, 90)]
        [DataRow(300, 100, 200, 200, 290, 110, 127.279221, 90)]
        [DataRow(100, 300, 200, 200, 110, 290, 127.279221, 90)]
        [DataRow(300, 300, 200, 200, 290, 290, 127.279221, 90)]
        public void Subtract_Line_Width_When_Has_Angle_LocalConstraint_And_Has_Second_Point_Constraint(double x1, double y1, double x2, double y2, double newX1, double newY1, double newLength, double newHeigth)
        {
            // Init
            _canvas.Clear();
            DcLineSegment lineSegment = new DcLineSegment(x1, y1, x2, y2);
            double expectedAngle = lineSegment.Angle;
            lineSegment.AddLocalConstraint(Constraints.Angle);
            _canvas.AddToVisualCollection(new DcLineSegment(100, 150, 200, 200));
            _canvas.AddToVisualCollection(lineSegment);

            // Act
            lineSegment.Width -= 10;

            Assert.AreEqual(newLength, lineSegment.Length, 0.000001);
            Assert.AreEqual(newX1, lineSegment.X1, 0.000001);
            Assert.AreEqual(newY1, lineSegment.Y1, 0.000001);
            Assert.AreEqual(x2, lineSegment.X2);
            Assert.AreEqual(y2, lineSegment.Y2);
            Assert.AreEqual(expectedAngle, lineSegment.Angle);
            Assert.AreEqual(newHeigth, lineSegment.Height, 0.000001);
        }
    }
}
