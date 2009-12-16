using System.Collections.Generic;
using KTUzd.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace UnitTestai
{
    
    
    /// <summary>
    ///This is a test class for CyclotomicCosetTest and is intended
    ///to contain all CyclotomicCosetTest Unit Tests
    ///</summary>
    [TestClass]
    public class CyclotomicCosetTest
    {


        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        private static void CosetCalculation(List<int> expected, int n, int q, int i)
        {
            CyclotomicCoset actual = CyclotomicCoset.Calculate(n, q, i);
            Assert.AreEqual(actual, expected);            
        }

        /// <summary>
        ///A test for Calculate
        ///</summary>
        [TestMethod]
        public void CalculateCoset1()
        {
            CosetCalculation(new List<int> { 1, 2, 4, 8 }, 15, 2, 1);
        }

        [TestMethod]
        public void CalculateCoset3()
        {
            CosetCalculation(new List<int> { 3, 6, 12, 9 }, 15, 2, 3);
        }

        [TestMethod]
        public void CalculateCoset0()
        {
            CosetCalculation(new List<int> { 0 }, 15, 2, 0);
        }

        [TestMethod]
        public void CalculateCoset5()
        {
            CosetCalculation(new List<int> { 5, 10 }, 15, 2, 5);
        }

        [TestMethod]
        public void CalculateCoset7()
        {
            CosetCalculation(new List<int> { 7, 14, 13, 11 }, 15, 2, 7);
        }

        [TestMethod]
        public void CalculateCosetX()
        {
            CosetCalculation(new List<int> { 1,2,4,8,7,5 }, 9, 2, 4);
        }


        /// <summary>
        ///A test for GetCyclotomicCosetsSet
        ///</summary>
        [TestMethod]
        public void GetCyclotomicCosetsSetTest2()
        {
            const int n = 13; 
            const int q = 3; 
            const int expected = 5;
            int actual = CyclotomicCoset.GetCyclotomicCosetsSet(n, q).Count;
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod]
        public void GetCyclotomicCosetsSetTest()
        {
            const int n = 9; 
            const int q = 2; 
            const int expected = 3;
            int actual = CyclotomicCoset.GetCyclotomicCosetsSet(n, q).Count;
            Assert.AreEqual(expected, actual);
        }
    }
}
