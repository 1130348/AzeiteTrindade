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

        private string schema = "MEDICO";
        private string tabela = "V_DESLOC_PROD_V2";
        private string query = "*";
        private string orderQ = "DOENTE";

        public string tdoente = "" ;
        public string doente = "";
        public string ncons = "";
        public string tEpis = "";
        public string epis = "";
        public string serv = "";


        #region DeslocProdModel

        public DeslocProdModel()
        {

            PreencheDoente();
            pageSize    = 5;
            dbParams = new LDFTableDBParams((string)HttpContext.Current.Session[Constants.SS_LOCAL_CONN], schema, tabela, query, GetCondition(), orderQ, null, null);
            objType     = typeof(VDeslocProd);


            UrlHelper helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            callbacks.loadCallBack = helper.Action("LoadLDFTable", "Deslocacoes");
            callbacks.searchCallBack = helper.Action("LoadLDFTable", "Deslocacoes");
         
            LDFTableHeaders();
        }


        private void PreencheDoente()
        {
            tdoente = (string)HttpContext.Current.Session["DeslocProd_TDOENTE"];
            doente = (string)HttpContext.Current.Session["DeslocProd_DOENTE"];
            ncons = (string)HttpContext.Current.Session["DeslocProd_NCONS"];
            tEpis = (string)HttpContext.Current.Session["DeslocProd_TEPIS"];
            epis = (string)HttpContext.Current.Session["DeslocProd_EPIS"];
            serv = (string)HttpContext.Current.Session["DeslocProd_COD_SERV"];
        }

        private String GetCondition()
        {

            string query = "";
            if (!String.IsNullOrEmpty(ncons))
            {
                query = "T_DOENTE='" + tdoente + "' AND DOENTE='" + doente + "' AND N_CONS='" + ncons + "' AND T_EPISODIO='" + tEpis + "' AND EPISODIO='" + epis + "'";
            }
            else
            {
                query = "T_DOENTE='" + tdoente + "' AND DOENTE='" + doente + "' AND T_EPISODIO='" + tEpis + "' AND EPISODIO='" + epis + "'";
            }

            return query;
        }

        #endregion

        #region Row Methods

        internal bool UpdateRow(UserInfo uinfo, string doenten,string tdoente, string deslocCod,string numCons)
        {
            DALDeslocacoes infDAL = new DALDeslocacoes();
            VwDeslocProd doente = infDAL.GetDeslocProdUser(tdoente, doenten,numCons);

            return infDAL.InsertDeslocProd(PreencheTable(uinfo, doente.T_DOENTE, doente.DOENTE, doente.N_CONS, doente.T_EPISODIO, doente.EPISODIO, doente.PRODUTO, doente.COD_SERV_ORIG, deslocCod));
        }

        internal bool AddNewRow(UserInfo uinfo, string tdoente, string doente, string ncons, string tEpis, string epis, string selProd, string selOrig, string selDest)
        {
            DALDeslocacoes infDAL = new DALDeslocacoes();           

            return infDAL.InsertDeslocProd(PreencheTable(uinfo,tdoente,doente,ncons,tEpis,epis,selProd,selOrig,selDest));

        }

        internal TblDeslocProd PreencheTable(UserInfo uinfo, string tdoente, string doente, string ncons, string tEpis, string epis, string selProd, string selOrig, string selDest)
        {
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

            return tbl;
        }

        #endregion
    }
}