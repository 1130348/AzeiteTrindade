using Microsoft.VisualStudio.TestTools.UnitTesting;
using LusiadasSolucaoWeb.Models;

namespace LusiadasSolucaoWeb.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            var controller = new HomeController();
            //var result =  async ViewResult;
        }

        [TestMethod()]
        public void CheckConnectionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void IndexTest1(LoginModel login)
        {
            Assert.Fail();
        }
    }
}