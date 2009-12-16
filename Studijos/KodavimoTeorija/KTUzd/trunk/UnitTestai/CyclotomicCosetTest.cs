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
        private static bool ListEqual<T>(IList<T> list1, IList<T> list2)
        {
            if (list1.Count != list2.Count)
            {
                return false;
            }
            for (int i = 0; i < list1.Count; i++)
            {
               if (!list1[i].Equals(list2[i]))
               {
                   return false;
               }
            }
            return true;
        }

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


        private void CosetCalculation(List<int> expected, int n, int q, int i)
        {
            List<int> actual = CyclotomicCoset.Calculate(n, q, i).Items;
            Assert.IsTrue(ListEqual(expected, actual));
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
        public void CalculateCoset2()
        {
            CosetCalculation(new List<int>(), 15, 2, 2);
        }

    }
}
