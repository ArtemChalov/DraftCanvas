using System;
using DraftCanvas.Servicies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class PointHashTests
    {
        [TestMethod]
        public void CreateHash_Test_0_0()
        {
            int expected = 0;
            var actual = PointHash.CreateHash(0, 0);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateHash_Test_0_1()
        {
            int expected = 1;
            var actual = PointHash.CreateHash(0, 1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateHash_Test_1_0()
        {
            int expected = 1048576;
            var actual = PointHash.CreateHash(1, 0);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateHash_Test_2_348()
        {
            int expected = 2097500;
            var actual = PointHash.CreateHash(2, 348);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPointIndex_1048576_Test()
        {
            int expected = 1;
            var actual = PointHash.GetPointIndex(1048576);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetPointIndex_2097500_Test()
        {
            int expected = 2;
            var actual = PointHash.GetPointIndex(2097500);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetOwnerID_1048576_Test()
        {
            int expected = 0;
            var actual = PointHash.GetIdFromHash(1048576);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetOwnerID_2097500_Test()
        {
            int expected = 348;
            var actual = PointHash.GetIdFromHash(2097500);
            Assert.AreEqual(expected, actual);
        }
    }
}
