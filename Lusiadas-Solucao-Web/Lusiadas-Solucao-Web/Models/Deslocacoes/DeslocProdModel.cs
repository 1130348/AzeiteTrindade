using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LusiadasDAL;
using LDFHelper.Helpers;
using System.Web.Mvc;
using System.Configuration;

namespace LusiadasSolucaoWeb.Models
{
    public class DeslocProdModel : LDFTableModel
    {
        public string tdoente = "";
        public string doente = "";
        public string ncons = "";
        public string tEpis = "";
        public string epis = "";

        #region DeslocProdModel

        public DeslocProdModel()
        {
            tdoente = (string)HttpContext.Current.Session["DeslocProd_TDOENTE"];
            doente = (string)HttpContext.Current.Session["DeslocProd_DOENTE"];
            ncons = (string)HttpContext.Current.Session["DeslocProd_NCONS"];
            tEpis = (string)HttpContext.Current.Session["DeslocProd_TEPIS"];
            epis = (string)HttpContext.Current.Session["DeslocProd_EPIS"];

            string query = "T_DOENTE='" + tdoente + "' AND DOENTE='" + doente + "' AND T_EPISODIO='" + tEpis + "' AND EPISODIO='" + epis + "'";

            pageSize    = Convert.ToInt32(ConfigurationManager.AppSettings["TableRowsPerPage"]);
            dbParams    = new LDFTableDBParams("DBSolucaoWeb", "MEDICO", "V_DESLOC_PROD", "*", query, "DOENTE", null, null);
            objType     = typeof(VDeslocProd);


            UrlHelper helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            callbacks.loadCallBack = helper.Action("LoadLDFTable", "Deslocacoes");
            callbacks.searchCallBack = helper.Action("LoadLDFTable", "Deslocacoes");
         
            LDFTableHeaders();
            ReArrangeHeaders();
        }

        private void ReArrangeHeaders()
        {
            list_headers.Add(new LDFTableHeader() { headerID = "LOCAL_ATUAL", headerName = "Local Atual" });
        }


        public void LDFTableTreatData()
        {
            string tdoente = "", doente = "", tepisodio = "";
            string selectValencias = "", deslocActive = "";

            ValenciaModel valModel = (ValenciaModel)HttpContext.Current.Session["InfADValencias"];
            foreach (LDFTableRow item in list_rows)
            {
                item.rowItems.Add(new LDFTableItem("LOCAL_ATUAL", ""));

                tdoente     = Generic.GetItemValue(item, "T_DOENTE");
                doente      = Generic.GetItemValue(item, "DOENTE");
                tepisodio   = Generic.GetItemValue(item, "T_EPISODIO");

                deslocActive = "";
                if ((Generic.GetItemValue(item, "COD_SERV") != Generic.GetItemValue(item, "COD_SERV_ORIG")) && (!String.IsNullOrEmpty(Generic.GetItemValue(item, "COD_SERV"))))
                    deslocActive = "active";

                selectValencias = "<select data-select-row='" + tdoente + "_" + doente + "' class='infaddeslocprod-selected-item " + deslocActive + "' data-previous-elem='" + Generic.GetItemValue(item, "COD_SERV") + "'>";
                selectValencias += "<option disabled value='0' " + (String.IsNullOrEmpty(Generic.GetItemValue(item, "COD_SERV")) ? "selected" : "") + ">Sem deslocação</option>";

                selectValencias += "<optgroup label='Localização Origem'>";
                selectValencias += "<option value='" + Generic.GetItemValue(item, "COD_SERV_ORIG") + "' " + ((Generic.GetItemValue(item, "COD_SERV_ORIG") == Generic.GetItemValue(item, "COD_SERV")) ? "selected" : "") + ">" + Generic.GetItemValue(item, "DESCR_SERV") + "</option>";
                selectValencias += "</optgroup>";

                selectValencias += "<optgroup label='Todas as localizações'>";
                foreach (Valencia itemVal in valModel.listValencias)
                {
                    if (itemVal.COD_SERV != Generic.GetItemValue(item, "COD_SERV_ORIG") && Generic.GetItemValue(item, "COD_SERV") != itemVal.COD_SERV)
                        selectValencias += "<option value='" + itemVal.COD_SERV + "' " + ((itemVal.COD_SERV == Generic.GetItemValue(item, "COD_SERV")) ? "selected" : "") + ">" + itemVal.DESCR_SERV + "</option>";
                }
                selectValencias += "</optgroup>";
                selectValencias += "</select>";
                item.rowItems.First(q => q.itemColumnName == "LOCAL_ATUAL").itemValue = selectValencias;

            }
        }

        #endregion

        #region Row Methods

        internal bool UpdateRow(UserInfo uinfo, string itemRow, string deslocCod)
        {
            DALDeslocacoes infDAL = new DALDeslocacoes();
            VwDeslocProd doente = infDAL.GetDeslocProdUser(itemRow.Split('_')[0], itemRow.Split('_')[1]);


            TblDeslocProd tbl = new TblDeslocProd();
            tbl.T_DOENTE = doente.T_DOENTE;
            tbl.DOENTE = doente.DOENTE;
            tbl.PRODUTO = doente.PRODUTO;
            tbl.N_CONS = doente.N_CONS;
            tbl.T_EPISODIO = doente.T_EPISODIO;
            tbl.EPISODIO = doente.EPISODIO;
            tbl.COD_SERV_ORIG = doente.COD_SERV_ORIG;
            tbl.COD_SERV = deslocCod;
            tbl.DT_DESL = DateTime.Now;
            tbl.USER_CRI = uinfo.userID;
            tbl.DT_CRI = DateTime.Now;

            return infDAL.InsertDeslocProd(tbl);
        }

        internal bool AddNewRow(UserInfo uinfo, string tdoente, string doente, string ncons, string tEpis, string epis, string selProd, string selOrig, string selDest)
        {
            DALDeslocacoes infDAL = new DALDeslocacoes();

            TblDeslocProd tbl = new TblDeslocProd();
            tbl.T_DOENTE = tdoente;
            tbl.DOENTE = doente;
            tbl.PRODUTO = selProd;
            tbl.N_CONS = ncons;
            tbl.T_EPISODIO = tEpis;
            tbl.EPISODIO = epis;
            tbl.COD_SERV_ORIG = selOrig;
            tbl.COD_SERV = selDest;
            tbl.DT_DESL = DateTime.Now;
            tbl.USER_CRI = uinfo.userID;
            tbl.DT_CRI = DateTime.Now;

            return infDAL.InsertDeslocProd(tbl);

        }

        #endregion
    }
}