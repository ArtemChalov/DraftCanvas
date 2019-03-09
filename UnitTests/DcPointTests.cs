using System;
using System.Windows;
using DraftCanvas;
using DraftCanvas.Servicies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class DcPointTests
    {
        [TestMethod]
        public void PoinsAreEquals()
        {
            DcPoint point1 = new DcPoint(new Point(100, 300), PointHash.CreateHash(1, 2), 1);
            DcPoint point2 = new DcPoint(new Point(100, 300), PointHash.CreateHash(2, 3), 1);

            Assert.IsTrue(point1.Equals(point2));
        }

        [TestMethod]
        public void PoinsAreNotEquals()
        {
            DcPoint point1 = new DcPoint(new Point(100, 250), PointHash.CreateHash(1, 2), 1);
            DcPoint point2 = new DcPoint(new Point(100, 300), PointHash.CreateHash(2, 2), 1);

            Assert.IsFalse(point1.Equals(point2));
        }
    }
}
