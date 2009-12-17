using Microsoft.VisualStudio.TestTools.UnitTesting;
using KTUzd.Models;

namespace UnitTestai
{
    
    
    /// <summary>
    ///Polinomo klasės testai
    ///</summary>
    [TestClass]
    public class PolynomialTest
    {

        /// <summary>
        ///Polinomo matametinės operacijos testas
        ///</summary>
        [TestMethod]
        public void PolynomialConstructorTest()
        {            
            var target = new Polynomial("x^5-1", 2, 1);
            var target2 = new Polynomial("x^5", 2, 1);
            var t2 = target - target2;
            Assert.IsTrue(t2 == 1);            
        }
    }
}
