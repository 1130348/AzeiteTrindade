using LDFHelper.Helpers;
using LusiadasDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LusiadasSolucaoWeb.Models
{
    public class PIMModel : LDFTableModel
    {
        public PIMModel()
        {
            pageSize    = Convert.ToInt32(ConfigurationManager.AppSettings["TableRowsPerPage"]);
            dbParams = new LDFTableDBParams((string)HttpContext.Current.Session[Constants.SS_LOCAL_CONN], "MEDICO", "V_INTERNAMENTOS", "*", "DT_ALTA IS NULL", "COD_SERV", null, null);
            objType     = typeof(VwInternamentos);

            LDFTableHeaders();
            FillHeader();
        }

        public void FillHeader()
        {
            list_headers.Add(new LDFTableHeader() { headerID = "COL1_CLICKABLE" });
            list_headers.Add(new LDFTableHeader() { headerID = "COL2_CLICKABLE" });
            list_headers.First(q => q.headerID == "DT_OPER").headerMobile = false;
            list_headers.First(q => q.headerID == "DURACAO").headerMobile = false;
        }

        public void LDFTableTreatData()
        {
            string dtCirurgia = "", dtInternamento = "";
            DateTime dtInt = DateTime.Now;
            UserInfo userInfo = (UserInfo)HttpContext.Current.Session[Constants.SS_USER];
            foreach (LDFTableRow item in list_rows)
            {
                item.rowItems.Add(new LDFTableItem("COL1_CLICKABLE", ""));
                item.rowItems.Add(new LDFTableItem("COL2_CLICKABLE", ""));

                item.rowItems.First(q => q.itemColumnName == "COL1_CLICKABLE").itemValue = "<span data-toggle='tooltip' class='text-blue' title='' data-original-title='eResults'><a target='_blank' href=\"" + GetLinkUrl(ConfigurationManager.AppSettings["eResultsDetailURL"], item, userInfo) + "\" class='fa fa-file-text-o' style='font-size:20px;'></a></span>";
                item.rowItems.First(q => q.itemColumnName == "COL2_CLICKABLE").itemValue = "<span data-toggle='tooltip' class='text-blue' title='' data-original-title='PIM'><a target='_blank' href=\"" + GetLinkUrl(ConfigurationManager.AppSettings["PIMDetailURL"], item, userInfo) + "\">PIM</a></span>";
                item.rowItems.First(q => q.itemColumnName == "T_DOENTE").itemValue  = Generic.GetItemValue(item, "T_DOENTE") + Generic.GetItemValue(item, "DOENTE");
                item.rowItems.First(q => q.itemColumnName == "COD_SERV").itemValue  = "<span data-toggle='tooltip' class='text-blue' title='' data-original-title='" + Generic.GetItemValue(item, "DESCR_SERV") + "'>" + Generic.GetItemValue(item, "COD_SERV") + "</span>";
                item.rowItems.First(q => q.itemColumnName == "CAMA").itemValue      = Generic.GetItemValue(item, "SALA") + " - " + Generic.GetItemValue(item, "CAMA");

                dtCirurgia = "";
                if (!String.IsNullOrEmpty(Generic.GetItemValue(item, "DT_INT")))
                {
                    dtInt       = Convert.ToDateTime(Generic.GetItemValue(item, "DT_INT"));
                    dtCirurgia  = dtInt.ToString("dd/MM/yyyy") + " " + Generic.GetItemValue(item, "HORA_INT");
                }
                item.rowItems.First(q => q.itemColumnName == "DT_INT").itemValue = dtCirurgia;

                dtInternamento = "";
                if (!String.IsNullOrEmpty(Generic.GetItemValue(item, "DT_OPER")))
                {
                    dtInt = Convert.ToDateTime(Generic.GetItemValue(item, "DT_OPER"));
                    dtInternamento = dtInt.ToString("dd/MM/yyyy") + " " + Generic.GetItemValue(item, "HR_INI");
                }
                item.rowItems.First(q => q.itemColumnName == "DT_OPER").itemValue = dtInternamento;

            }
        }

        private string GetLinkUrl(string url, LDFTableRow item, UserInfo uinfo)
        {
            string res = url;

            string[] arr = url.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string str in arr)
            {
                if (item.rowItems.Count(q => q.itemColumnName == str) == 1)
                    res = res.Replace("[" + str + "]", item.rowItems.First(q => q.itemColumnName == str).itemValue);
            }


            foreach (string str in arr)
            {
                if (uinfo.GetType().GetProperty(str.Replace("SS_", "")) != null && uinfo.GetType().GetProperty(str.Replace("SS_", "")).GetValue(uinfo) != null)
                    res = res.Replace("[" + str + "]", uinfo.GetType().GetProperty(str.Replace("SS_", "")).GetValue(uinfo).ToString());
            }

            return res;
        }

    }
}