using DraftCanvas;
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
    }
}
