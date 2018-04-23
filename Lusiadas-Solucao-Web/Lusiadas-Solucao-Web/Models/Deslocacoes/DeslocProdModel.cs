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
        public string serv = "";

        #region DeslocProdModel

        public DeslocProdModel()
        {
            tdoente = (string)HttpContext.Current.Session["DeslocProd_TDOENTE"];
            doente = (string)HttpContext.Current.Session["DeslocProd_DOENTE"];
            ncons = (string)HttpContext.Current.Session["DeslocProd_NCONS"];
            tEpis = (string)HttpContext.Current.Session["DeslocProd_TEPIS"];
            epis = (string)HttpContext.Current.Session["DeslocProd_EPIS"];
            serv = (string)HttpContext.Current.Session["DeslocProd_COD_SERV"];


            string query="";
            if (!String.IsNullOrEmpty(ncons))
            {
                query = "T_DOENTE='" + tdoente + "' AND DOENTE='" + doente + "' AND N_CONS='" + ncons + "' AND T_EPISODIO='" + tEpis + "' AND EPISODIO='" + epis + "'";
            }
            else
            {
                query = "T_DOENTE='" + tdoente + "' AND DOENTE='" + doente + "' AND T_EPISODIO='" + tEpis + "' AND EPISODIO='" + epis + "'";
            }

            

            pageSize    = 5;
            dbParams = new LDFTableDBParams((string)HttpContext.Current.Session[Constants.SS_LOCAL_CONN], "MEDICO", "V_DESLOC_PROD_V2", "*", query, "DOENTE", null, null);
            objType     = typeof(VDeslocProd);


            UrlHelper helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            callbacks.loadCallBack = helper.Action("LoadLDFTable", "Deslocacoes");
            callbacks.searchCallBack = helper.Action("LoadLDFTable", "Deslocacoes");
         
            LDFTableHeaders();
        }


        //public void LDFTableTreatData()
        //{
        //    string tdoente = "", doente = "", tepisodio = "";
        //    string respMov = "";

        //    //ValenciaModel valModel = (ValenciaModel)HttpContext.Current.Session["InfADValencias"];

        //    foreach (LDFTableRow item in list_rows)
        //    {

        //        tdoente     = Generic.GetItemValue(item, "T_DOENTE");
        //        doente      = Generic.GetItemValue(item, "DOENTE");
        //        tepisodio   = Generic.GetItemValue(item, "T_EPISODIO");

                
        //        respMov = "<div class='row'>";
        //        respMov += "<div class='col-xs-12'>" + Generic.GetItemValue(item, "TITULO") + " " + Generic.GetItemValue(item, "NOME");
        //        respMov += "</div>";

        //        item.rowItems.First(q => q.itemColumnName == "NOME").itemValue = respMov;

        //    }
        //}

        #endregion

        #region Row Methods

        internal bool UpdateRow(UserInfo uinfo, string doenten,string tdoente, string deslocCod,string numCons)
        {
            DALDeslocacoes infDAL = new DALDeslocacoes();
            VwDeslocProd doente = infDAL.GetDeslocProdUser(tdoente, doenten,numCons);


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