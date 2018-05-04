using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.CSharp;
using LusiadasSolucaoWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LusiadasSolucaoWeb.Models;
using System.Web.Mvc;
using System.Web;

namespace LusiadasSolucaoWeb.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            HomeController controller = new HomeController();
            
            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNull(result);

        }

        [TestMethod()]
        public void CheckConnectionTest()
        {

            HomeController controller = new HomeController();

            bool result = controller.CheckConnection("User Id=TESTE;Password=TESTE;Data Source=TESTE");
            Assert.IsTrue(!result);
        }

    }
}