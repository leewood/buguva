using System.Collections.Generic;
using KTUzd.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace UnitTestai
{
    
    
    /// <summary>
    ///CyclotomicCosetTest testavimo klasė
    ///</summary>
    [TestClass]
    public class CyclotomicCosetTest
    {

        /// <summary>
        /// Apskaičiuoja kosetą pagal parametrus ir palygina su duota seka
        /// </summary>
        /// <param name="expected">Palyginamoji seka</param>
        /// <param name="n">Polinomo laipsnis</param>
        /// <param name="q">Parametras q</param>
        /// <param name="i">Parametras i</param>
        private static void CosetCalculation(List<int> expected, int n, int q, int i)
        {
            CyclotomicCoset actual = CyclotomicCoset.Calculate(n, q, i);
            Assert.AreEqual(actual, expected);            
        }

        /// <summary>
        ///Pirmas koseto skaičiavimo testas
        ///</summary>
        [TestMethod]
        public void CalculateCoset1()
        {
            CosetCalculation(new List<int> { 1, 2, 4, 8 }, 15, 2, 1);
        }

        /// <summary>
        ///Antras koseto skaičiavimo testas
        ///</summary>
        [TestMethod]
        public void CalculateCoset3()
        {
            CosetCalculation(new List<int> { 3, 6, 12, 9 }, 15, 2, 3);
        }

        /// <summary>
        ///Trečias koseto skaičiavimo testas
        ///</summary>
        [TestMethod]
        public void CalculateCoset0()
        {
            CosetCalculation(new List<int> { 0 }, 15, 2, 0);
        }

        /// <summary>
        ///Ketvirtas koseto skaičiavimo testas
        ///</summary>
        [TestMethod]
        public void CalculateCoset5()
        {
            CosetCalculation(new List<int> { 5, 10 }, 15, 2, 5);
        }

        /// <summary>
        ///Penktas koseto skaičiavimo testas
        ///</summary>
        [TestMethod]
        public void CalculateCoset7()
        {
            CosetCalculation(new List<int> { 7, 14, 13, 11 }, 15, 2, 7);
        }

        /// <summary>
        ///Antras kosetų sekos skaičiavimo testas
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

        /// <summary>
        ///Pirmas kosetų sekos skaičiavimo testas
        ///</summary>
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
