using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PIMDAL;
using LDFHelper.Helpers;
using System.Web.Mvc;
using System.Configuration;

namespace LusiadasSolucaoWeb.Models
{
    public class ModelDeslocProdTable : LDFTableHelper
    {
        #region Variables

        public List<LDFTableHeader> list_headers        { get; set; }
        public List<LDFTableRow>    list_rows           { get; set; }
        public LDFTableCallback     callbacks           { get; set; }
        public int pageCount    = 0;
        public int rowCount     = 0;
        public List<VwDeslocProd>   list_vwDeslocProd   { get; set; }

        public string tdoente   = "";
        public string doente    = "";
        public string ncons     = "";
        public string tEpis     = "";
        public string epis      = "";

        #endregion

        #region DeslocProdTable

        public ModelDeslocProdTable()
        {

            list_headers        = new List<LDFTableHeader>();
            list_rows           = new List<LDFTableRow>();
            callbacks           = new LDFTableCallback();

            UrlHelper helper                = new UrlHelper(HttpContext.Current.Request.RequestContext);
            callbacks.loadCallBack          = helper.Action("LoadDeslocProdTable", "Deslocacoes");
            callbacks.paginationCallBack    = helper.Action("PageDeslocProdTable", "Deslocacoes");
            callbacks.orderCallBack         = helper.Action("OrderDeslocProdTable", "Deslocacoes");

            RenderHeader();
        }

        #endregion

        #region Init Methods

        public void RenderHeader()
        {
            LDFTableHeader header   = new LDFTableHeader();
            header.headerName       = "Data";
            header.headerID         = "DT_DESL";
            header.headerFilter     = true;
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "Produto";
            header.headerID         = "PRODUTO_DESCR";
            header.headerFilter     = true;
            header.headerType       = typeof(string);
            list_headers.Add(header);
                   
            header                  = new LDFTableHeader();
            header.headerName       = "Origem";
            header.headerID         = "SERV_ORIG";
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "Localização Actual";
            header.headerID         = "ULT_LOCAL";
            header.headerType       = typeof(string);
            list_headers.Add(header);
        }

        public void LoadRows(ValenciaModel modelValencia, string tdoente, string doente)
        {
            DALDeslocacoes infDAL = new DALDeslocacoes();
            list_vwDeslocProd           = infDAL.ListDeslocProd(tdoente, doente);
            RenderRows(modelValencia);
        }

        private void RenderRows(ValenciaModel modelValencia)
        {
            string selectValencias = "", deslocActive = "";

            if (list_vwDeslocProd != null)
            {
                rowCount = list_vwDeslocProd.Count;
                for (int i = 0; i < list_vwDeslocProd.Count; i++)
                {
                    LDFTableRow row = new LDFTableRow();
                    row.rowItems = new List<LDFTableItem>();


                    LDFTableItem item   = new LDFTableItem();
                    item.itemColumnName = "DT_DESL";
                    item.itemValue      = list_vwDeslocProd[i].DT_DESL.ToString("dd-MM-yyyy");
                    row.rowItems.Add(item);

                    item                = new LDFTableItem();
                    item.itemColumnName = "PRODUTO_DESCR";
                    item.itemValue      = list_vwDeslocProd[i].PRODUTO_DESCR;
                    row.rowItems.Add(item);

                    item                = new LDFTableItem();
                    item.itemColumnName = "SERV_ORIG";
                    item.itemValue      = list_vwDeslocProd[i].DESCR_SERV_ORIG;
                    row.rowItems.Add(item);


                    deslocActive = "";
                    if ((list_vwDeslocProd[i].COD_SERV != list_vwDeslocProd[i].COD_SERV_ORIG) && (list_vwDeslocProd[i].COD_SERV != null))
                        deslocActive = "active";

                    selectValencias = "<select data-select-row='" + i + "' class='infaddeslocprod-selected-item " + deslocActive + "' data-previous-elem='" + list_vwDeslocProd[i].COD_SERV + "'>";
                    selectValencias += "<option disabled value='0' " + (String.IsNullOrEmpty(list_vwDeslocProd[i].COD_SERV) ? "selected" : "") + ">Sem deslocação</option>";
                    selectValencias += "<optgroup label='Localização Origem'>";
                    selectValencias += "<option value='" + list_vwDeslocProd[i].COD_SERV_ORIG + "' " + ((list_vwDeslocProd[i].COD_SERV_ORIG == list_vwDeslocProd[i].COD_SERV) ? "selected" : "") + ">" + list_vwDeslocProd[i].DESCR_SERV + "</option>";
                    selectValencias += "</optgroup>";

                    selectValencias += "<optgroup label='Todas as localizações'>";
                    foreach (Valencia itemVal in modelValencia.listValencias)
                    {
                        if (itemVal.COD_SERV != list_vwDeslocProd[i].COD_SERV_ORIG && itemVal.COD_SERV != list_vwDeslocProd[i].COD_SERV)
                            selectValencias += "<option value='" + itemVal.COD_SERV + "' " + ((itemVal.COD_SERV == list_vwDeslocProd[i].COD_SERV) ? "selected" : "") + ">" + itemVal.DESCR_SERV + "</option>";
                    }
                    selectValencias += "</optgroup>";
                    selectValencias += "</select>";

                    item = new LDFTableItem();
                    item.itemColumnName = "ULT_LOCAL";
                    item.itemValue = selectValencias;
                    row.rowItems.Add(item);


                    list_rows.Add(row);
                }

                pageCount = list_vwDeslocProd.Count / Convert.ToInt32(ConfigurationManager.AppSettings["PIMRowsPerPage"]);
            }
        }

        #endregion

        #region Interact Methods 

        internal void PageTable(object tableRows, int pageNumber, string[] orderData)
        {
            int itemsPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["PIMRowsPerPage"]);

            list_rows   = tableRows as List<LDFTableRow>;
            pageCount   = list_rows.Count / itemsPerPage;
            rowCount    = list_rows.Count;

            //if (orderData != null)
            //{
            //    if (orderData[1] == "-1")
            //        list_rows = list_rows.OrderBy(x => x.rowItems.FirstOrDefault(q => q.itemColumnName == orderData[0]).itemValue).ToList();
            //    else
            //        list_rows = list_rows.OrderByDescending(x => x.rowItems.FirstOrDefault(q => q.itemColumnName == orderData[0]).itemValue).ToList();
            //}
            list_rows = TablePagination(list_rows, itemsPerPage, pageNumber, orderData);

            //list_rows = list_rows.Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToList();
        }

        internal void OrderTable(object tableRows, string[] orderValues)
        {
            int itemsPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["PIMRowsPerPage"]);

            list_rows   = tableRows as List<LDFTableRow>;
            pageCount   = list_rows.Count / itemsPerPage;
            rowCount    = list_rows.Count;


            //if (orderValues[1] == "1")
            //    list_rows = list_rows.OrderBy(x => x.rowItems.FirstOrDefault(q => q.itemColumnName == orderValues[0]).itemValue).ToList();
            //else
            //    list_rows = list_rows.OrderByDescending(x => x.rowItems.FirstOrDefault(q => q.itemColumnName == orderValues[0]).itemValue).ToList();
            list_rows = TableOrdering(list_rows, orderValues);

            list_rows = list_rows.Take(itemsPerPage).ToList();
        }

        #endregion

        #region Row Methods

        internal bool UpdateRow(UserInfo uinfo, object tableRows, string itemRow, string deslocCod)
        {
            DALDeslocacoes infDAL = new DALDeslocacoes();
            list_vwDeslocProd           = tableRows as List<VwDeslocProd>;
            VwDeslocProd doente         = list_vwDeslocProd[Convert.ToInt32(itemRow)];

            TblDeslocProd tbl   = new TblDeslocProd();
            tbl.T_DOENTE        = doente.T_DOENTE;
            tbl.DOENTE          = doente.DOENTE;
            tbl.PRODUTO         = doente.PRODUTO;
            tbl.N_CONS          = doente.N_CONS;
            tbl.T_EPISODIO      = doente.T_EPISODIO;
            tbl.EPISODIO        = doente.EPISODIO;
            tbl.COD_SERV_ORIG   = doente.COD_SERV_ORIG;
            tbl.COD_SERV        = deslocCod;
            tbl.DT_DESL         = DateTime.Now;
            tbl.USER_CRI        = uinfo.userID;
            tbl.DT_CRI          = DateTime.Now;

            return infDAL.InsertDeslocProd(tbl);
        }

        internal bool AddNewRow(UserInfo uinfo, string tdoente, string doente, string ncons, string tEpis, string epis, string selProd, string selOrig, string selDest)
        {
            DALDeslocacoes infDAL = new DALDeslocacoes();

            TblDeslocProd tbl   = new TblDeslocProd();
            tbl.T_DOENTE        = tdoente;
            tbl.DOENTE          = doente;
            tbl.PRODUTO         = selProd;
            tbl.N_CONS          = ncons;
            tbl.T_EPISODIO      = tEpis;
            tbl.EPISODIO        = epis;
            tbl.COD_SERV_ORIG   = selOrig;
            tbl.COD_SERV        = selDest;
            tbl.DT_DESL         = DateTime.Now;
            tbl.USER_CRI        = uinfo.userID;
            tbl.DT_CRI          = DateTime.Now;

            return infDAL.InsertDeslocProd(tbl);

        }

        #endregion
    }
}