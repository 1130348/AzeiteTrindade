using Microsoft.VisualStudio.TestTools.UnitTesting;
using LusiadasSolucaoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Mocks;
using LusiadasSolucaoWeb.Controllers;
using System.Web.Mvc;

namespace LusiadasSolucaoWeb.Models.Tests
{
    [TestClass()]
    public class DeslocHistModelTests
    {
        [TestMethod()]
        public void LoadHistDeslocTest()
        {
            //Mock DB
            DeslocHistModel teste = new DeslocHistModel();
            bool resposta=teste.LoadHistDesloc("Teste","Teste","Teste");

            Assert.IsTrue(!resposta);

        }
    }
}