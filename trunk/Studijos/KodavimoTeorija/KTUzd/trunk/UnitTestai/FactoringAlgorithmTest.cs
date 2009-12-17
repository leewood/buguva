using KTUzd.Solution;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KTUzd.Models;

namespace UnitTestai
{
    
    
    /// <summary>
    ///Skaidymo algoritmo testai
    ///to contain all FactoringAlgorithmTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FactoringAlgorithmTest
    {

        /// <summary>
        ///Pilmo skaidymo testas
        ///</summary>
        [TestMethod()]
        public void FullFactorizationTest()
        {
            Polynomial poly = new Polynomial();
            poly.Q = 5;
            poly.P = 5;
            poly.M = 1;
            poly[8] = 1;
            poly[0] = -1;
            //Polynomial[] expected = null; // TODO: Initialize to an appropriate value
            Polynomial[] actual = FactoringAlgorithm.FullFactorization(poly);
            Assert.AreEqual(3, actual.Length);            
        }

    }
}
