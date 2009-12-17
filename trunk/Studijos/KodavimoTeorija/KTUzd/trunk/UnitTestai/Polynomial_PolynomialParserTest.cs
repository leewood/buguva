using KTUzd.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace UnitTestai
{
    
    
    /// <summary>
    ///This is a test class for Polynomial_PolynomialParserTest and is intended
    ///to contain all Polynomial_PolynomialParserTest Unit Tests
    ///</summary>
    [TestClass()]
    public class Polynomial_PolynomialParserTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

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


        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod]
        public void ParseTest()
        {
            string s = "x^7+x+x-1"; 
            Polynomial expected = new Polynomial();
            expected[7] = 1;
            expected[1] = 2;
            expected[0] = -1;
            Polynomial actual= Polynomial.PolynomialParser.Parse(s);            
            Assert.IsTrue(expected == actual);                        
        }

        [TestMethod]
        public void ParseTwoWayTest()
        {            
            Polynomial expected = new Polynomial();
            expected[7] = 1;
            expected[1] = 2;
            expected[0] = -1;
            string s = expected.ToString();
            Polynomial actual = Polynomial.PolynomialParser.Parse(s);
            Assert.IsTrue(expected == actual);
        }

    }
}
