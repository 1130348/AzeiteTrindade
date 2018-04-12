using Microsoft.VisualStudio.TestTools.UnitTesting;
using LDFHelper.Helpers;
using System;
using LusiadasDAL;
using System.Configuration;


namespace LusiadasSolucaoWeb.Models.Tests
{
    [TestClass()]
    public class ATPModelTests : LDFTableModel
    {
        [TestMethod()]
        public void ATPModelTest()
        {
            pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["TableRowsPerPage"]);
            //como a tabela dashboard nao existe  nas outras BD a escolha ainda nao é dinamica
            //ToDo : (string)HttpContext.Current.Session[Constants.SS_LOCAL_CONN]
            //dbParams = new LDFTableDBParams("BDHPTQLD", "MEDICO", "V_DASHBOARD_ATP", "*", "", "", null, null);
            dbParams = new LDFTableDBParams("BDHLUQLD", "MEDICO", "V_DASHBOARD_ATP_V2", "*", "", "", null, null);
            objType = typeof(VwDashboardATP);

            LDFTableHeaders();
            ReArrangeHeaders();

            Assert.Fail();
        }

        [TestMethod()]
        private void ReArrangeHeaders()
        {

            foreach (LDFTableHeader item in list_headers)
            {
                if (item.headerID == "TRIAGEM" || item.headerID == "NOTA_MEDICA" || item.headerID == "BOX_APLICAVEL" || item.headerID == "CADEIRAO_PRESENTE" || item.headerID == "PENSO_APLICAVEL" || item.headerID == "ANALISES_OK" || item.headerID == "MEDICACAO_PRESCRITA_PCE" || item.headerID == "IMAGIOLOGIA_REQUESITADOS" || item.headerID == "ECG_REQUESITADOS" || item.headerID == "CEXTERNA_REQUISITADOS")
                    item.headerStyle.Add("text-align", "center");

            }
                Assert.Fail();
           
        }
    }
}