using KTUzd.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace UnitTestai
{
    
    
    /// <summary>
    ///Vertimo iš tesktinės išraiškos testai
    ///</summary>
    [TestClass]
    public class PolynomialPolynomialParserTest
    {

        /// <summary>
        ///Bando versti iš "x^7+x+x-1" į polinomą
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            const string s = "x^7+x+x-1"; 
            var expected = new Polynomial();
            expected[7] = 1;
            expected[1] = 2;
            expected[0] = -1;
            Polynomial actual= Polynomial.PolynomialParser.Parse(s);            
            Assert.IsTrue(expected == actual);                        
        }

        /// <summary>
        /// Verčia iš duoto polinomo į tekstą, o tada atgal į polinomą ir palygina
        /// </summary>
        [TestMethod]
        public void ParseTwoWayTest()
        {            
            var expected = new Polynomial();
            expected[7] = 1;
            expected[1] = 2;
            expected[0] = -1;
            string s = expected.ToString();
            Polynomial actual = Polynomial.PolynomialParser.Parse(s);
            Assert.IsTrue(expected == actual);
        }

    }
}
