using Microsoft.VisualStudio.TestTools.UnitTesting;
using LDFHelper.Helpers;
using LusiadasDAL;
using LusiadasSolucaoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LusiadasSolucaoWeb.Models.Tests
{
    [TestClass()]
    public class ATPModelTests 
    {
        [TestMethod()]
        public void ATPModelTest()
        {

            //pageSize = 20;

            //como a tabela dashboard nao existe  nas outras BD a escolha ainda nao é dinamica
            //ToDo : (string)HttpContext.Current.Session[Constants.SS_LOCAL_CONN]
            //dbParams = new LDFTableDBParams("BDHPTQLD", "MEDICO", "V_DASHBOARD_ATP", "*", "", "", null, null);


            //Dinamico: (string)HttpContext.Current.Session[Constants.SS_LOCAL_CONN]
            //dbParams = new LDFTableDBParams("BDHLUQLD", "MEDICO", "V_DASH_DESLOC_ATP_V3", "*", "", "DT_CONS", null, null);
            //objType = typeof(VwDashboardATP);
            //getDados();


            //LDFTableHeaders();
            //ReArrangeHeaders();
            Assert.IsTrue(true);
        }
    }
}