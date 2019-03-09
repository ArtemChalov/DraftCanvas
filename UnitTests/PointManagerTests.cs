using System.Collections.Generic;
using DraftCanvas;
using DraftCanvas.Primitives;
using DraftCanvas.Servicies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class PointManagerTests
    {
        [TestMethod]
        public void AddOnePrimitive()
        {
            IDictionary<int, DcPoint> pointCollection = new Dictionary<int, DcPoint>();
            PointManager.AddPrimitivePoints(pointCollection, new DcLineSegment(100, 100, 100, 200));

            int expected = 2;
            int actual = pointCollection.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddTwoPrimitive()
        {
            IDictionary<int, DcPoint> pointCollection = new Dictionary<int, DcPoint>();
            PointManager.AddPrimitivePoints(pointCollection, new DcLineSegment(100, 100, 100, 200));
            PointManager.AddPrimitivePoints(pointCollection, new DcLineSegment(200, 100, 70, 200));

            int expected = 4;
            int actual = pointCollection.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetConstraint_Active_Reference_Added_To_Depended()
        {
            // Init
            IDictionary<int, DcPoint> pointCollection = new Dictionary<int, DcPoint>();
            DcLineSegment lineSegment = new DcLineSegment(100, 100, 100, 200);
            PointManager.AddPrimitivePoints(pointCollection, lineSegment);
            DcPoint point = new DcPoint(100, 200, PointHash.CreateHash(1, 1));

            // Action
            point = PointManager.SetConstraint(point, pointCollection);

            var expected = 0;
            foreach (var item in lineSegment.Points)
            {
                if (item.Value.X == 100 && item.Value.Y == 200)
                {
                    expected = item.Key;
                }
            }

            var actual = point.ActiveHash;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetConstraint_No_Active_References()
        {
            // Init
            IDictionary<int, DcPoint> pointCollection = new Dictionary<int, DcPoint>();
            DcLineSegment lineSegment = new DcLineSegment(100, 100, 100, 200);
            PointManager.AddPrimitivePoints(pointCollection, lineSegment);
            DcPoint point = new DcPoint(110, 200, PointHash.CreateHash(1, 1));

            // Action
            point = PointManager.SetConstraint(point, pointCollection);

            var expected = 0;

            var actual = point.ActiveHash;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetConstraint_Depended_Reference_Added_To_Active()
        {
            // Init
            IDictionary<int, DcPoint> pointCollection = new Dictionary<int, DcPoint>();
            DcLineSegment lineSegment = new DcLineSegment(100, 100, 100, 200);
            PointManager.AddPrimitivePoints(pointCollection, lineSegment);
            DcPoint point = new DcPoint(100, 200, PointHash.CreateHash(1, 1));

            // Action
            point = PointManager.SetConstraint(point, pointCollection);

            var ActiveHash = 0;
            foreach (var item in lineSegment.Points)
            {
                if (item.Value.X == 100 && item.Value.Y == 200)
                {
                    ActiveHash = item.Key;
                }
            }

            DcPoint activePoint = pointCollection[ActiveHash];

            var expected = point.GetHashCode();

            var actual = activePoint.DependedHash;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetConstraint_No_Depended_References()
        {
            // Init
            IDictionary<int, DcPoint> pointCollection = new Dictionary<int, DcPoint>();
            DcLineSegment lineSegment = new DcLineSegment(100, 100, 100, 200);
            PointManager.AddPrimitivePoints(pointCollection, lineSegment);
            DcPoint point = new DcPoint(110, 200, PointHash.CreateHash(1, 1));

            // Action
            point = PointManager.SetConstraint(point, pointCollection);

            var ActiveHash = 0;
            foreach (var item in lineSegment.Points)
            {
                if (item.Value.X == 100 && item.Value.Y == 200)
                {
                    ActiveHash = item.Key;
                }
            }

            DcPoint activePoint = pointCollection[ActiveHash];

            var expected = 0;

            var actual = activePoint.DependedHash;

            Assert.AreEqual(expected, actual);
        }
    }
}
