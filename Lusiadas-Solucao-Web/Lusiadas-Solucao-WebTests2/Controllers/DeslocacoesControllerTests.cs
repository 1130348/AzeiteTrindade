using Microsoft.VisualStudio.TestTools.UnitTesting;
using LusiadasSolucaoWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using LusiadasSolucaoWeb.Models;
using Rhino.Mocks;
using System.Web;
using System.IO;
using System.Security.Principal;

namespace LusiadasSolucaoWeb.Controllers.Tests
{
    [TestClass()]
    public class DeslocacoesControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            DeslocacoesController controller = new DeslocacoesController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void FilterDataTest()
        {
            DeslocacoesController controller = new DeslocacoesController();

            JsonResult result = controller.FilterData("1", "1", "1", "1", "1", "1");

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void UpdateDeslocRowTest()
        {
            DeslocacoesController controller = new DeslocacoesController();

            JsonResult result = controller.UpdateDeslocRow("Teste","Teste","1");


            Assert.IsNull(result);
        }

        [TestMethod()]
        public void ShowDeslocTimeLineTest()
        {
            DeslocacoesController controller = new DeslocacoesController();

            PartialViewResult result = controller.ShowDeslocTimeLine("TESTE","TESTE","1") as PartialViewResult;

            Assert.AreEqual(result.ViewName,"_timeLine");
        }

        [TestMethod()]
        public void ShowDeslocProdTest()
        {
            DeslocacoesController controller = new DeslocacoesController();

            PartialViewResult result = controller.ShowDeslocProd("TESTE", "TESTE", "1", "TESTE", "TESTE", "TESTE", "TESTE", "TESTE") as PartialViewResult;

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void UpdateDeslocProdTableTest()
        {
            DeslocacoesController controller = new DeslocacoesController();

            PartialViewResult result = controller.UpdateDeslocProdTable("TESTE", "TESTE", "1") as PartialViewResult;

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void AddDeslocProdTest()
        {
            DeslocacoesController controller = new DeslocacoesController();

            JsonResult result = controller.AddDeslocProd("TESTE", "TESTE", "1", "TESTE", "TESTE", "TESTE", "TESTE", "TESTE");

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void UpdateDeslocProdTest()
        {
            DeslocacoesController controller = new DeslocacoesController();


            JsonResult result = controller.UpdateDeslocProd("TESTE", "TESTE", "1");

            Assert.IsNull(result);
        }
    }
}