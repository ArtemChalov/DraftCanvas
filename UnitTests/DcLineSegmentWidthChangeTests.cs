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
    }
}
