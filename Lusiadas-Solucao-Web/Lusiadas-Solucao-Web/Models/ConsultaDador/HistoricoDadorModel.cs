using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LDFHelper.Helpers;
using LusiadasDAL;
using System.Configuration;
using System.Web.Mvc;
using LDF.ParameterManager;

namespace LusiadasSolucaoWeb.Models
{
    public class HistoricoDadorModel : LDFTableModel
    {
        public HistoricoDadorModel()
        {
            string tdoente  = (string)HttpContext.Current.Session["HistDor_TDOENTE"];
            string doente   = (string)HttpContext.Current.Session["HistDor_DOENTE"];

            string query = "T_DOENTE='"+tdoente+"' AND DOENTE='"+doente+"' AND FLAG_ESTADO IS NULL";

            pageSize    = Convert.ToInt32(ConfigurationManager.AppSettings[Constants.SS_TABLEPAGE]);
            dbParams    = new LDFTableDBParams("BDHLUQLD", "MEDICO", "CONSULTA_DOR", "*", query, "", null, null);
            objType     = typeof(TblConsultaDor);

            UrlHelper helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            callbacks.loadCallBack      = helper.Action("LoadLDFTable", "ConsultaDador");
            callbacks.removeCallBack    = helper.Action("RemoveHist", "ConsultaDador");

            LDFTableHeaders();
        }

        public void LDFTableTreatData()
        {
            foreach(LDFTableRow item in list_rows)
            {
                item.rowItems.First(q => q.itemColumnName == "SOS").itemValue = (Generic.GetItemValue(item, "SOS") == "S" ? "Sim" : "Não");
            }
        }


        public bool RemoveRow(string rowID)
        {
            TblConsultaDor obj = new TblConsultaDor();
            obj.NUM_CONS_DOR    = rowID;
            obj.FLAG_ESTADO     = "A";
            if (ParameterManager.UpdateParameter<TblConsultaDor>("BDHLUQLD", "MEDICO", "CONSULTA_DOR", obj, new List<string> { "FLAG_ESTADO", "DT_ANUL", "USR_ANUL" }, new List<string> { "NUM_CONS_DOR" }) > 0)
                return true;
            else
                return false;


        }

    }
}