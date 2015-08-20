using LDFHelper.Helpers;
using PIMDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LusiadasSolucaoWeb.Models
{
    public class ModelInfoAdTable : LDFTableHelper
    {
        public List<LDFTableHeader> list_headers    { get; set; }
        public List<LDFTableRow>    list_rows       { get; set; }
        public LDFTableCallback     callbacks       { get; set; }
        public int pageCount = 0;
        public int rowCount = 0;
        public List<VwDoentesPresentes> list_vwDoentesPresentes { get; set; }

        public ModelInfoAdTable()
        {

            list_headers        = new List<LDFTableHeader>();
            list_rows           = new List<LDFTableRow>();
            callbacks           = new LDFTableCallback();

            UrlHelper helper                = new UrlHelper(HttpContext.Current.Request.RequestContext);
            callbacks.loadCallBack          = helper.Action("LoadData", "Deslocacoes");
            callbacks.paginationCallBack    = helper.Action("PaginationData", "Deslocacoes");
            callbacks.orderCallBack         = helper.Action("OrderData", "Deslocacoes");
            callbacks.searchCallBack        = "FilterData";

            RenderHeader();
        }

        public void RenderHeader()
        {
            LDFTableHeader header   = new LDFTableHeader();
            header.headerName       = "Doente";
            header.headerID         = "T_DOENTE";
            header.headerFilter     = true;
            header.headerType       = typeof(string);
            list_headers.Add(header);


            header                  = new LDFTableHeader();
            header.headerName       = "Nome";
            header.headerID         = "NOME_DOENTE";
            header.headerFilter     = true;
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "Episódio";
            header.headerID         = "T_EPISODIO";
            header.headerType       = typeof(string);
            list_headers.Add(header);
                   
            header                  = new LDFTableHeader();
            header.headerName       = "Data Consulta";
            header.headerID         = "DT_CONS";
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "";
            header.headerID         = "FLOORNUMBER";
            header.headerVisible    = false;
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "Localização Origem";
            header.headerID         = "FLOOR_DESCR";
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "Localização Actual";
            header.headerID         = "ULT_LOCAL";
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "";
            header.headerID         = "COL2_CLICKABLE";
            list_headers.Add(header);
        }

        public void LoadRows(ValenciaModel modelValencia)
        {
            DALDeslocacoes infDAL = new DALDeslocacoes();
            list_vwDoentesPresentes     = infDAL.GetListDoentesPresentes();
            RenderRows(modelValencia);
        }

        
        internal void PageTable(object tableRows, int pageNumber, string[] orderData)
        {
            int itemsPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["PIMRowsPerPage"]);

            list_rows   = tableRows as List<LDFTableRow>;
            pageCount   = list_rows.Count / itemsPerPage;
            rowCount    = list_rows.Count;

            list_rows = TablePagination(list_rows, itemsPerPage, pageNumber, orderData);
            //if (orderData != null)
            //{
            //    if (orderData[1] == "-1")
            //        list_rows = list_rows.OrderBy(x => x.rowItems.FirstOrDefault(q => q.itemColumnName == orderData[0]).itemValue).ToList();
            //    else
            //        list_rows = list_rows.OrderByDescending(x => x.rowItems.FirstOrDefault(q => q.itemColumnName == orderData[0]).itemValue).ToList();
            //}

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

        internal List<LDFTableRow> FilterData(ValenciaModel modelValencia, string servicoCod, string ultLocalCod, bool viewOnlocal)
        {
            int itemsPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["PIMRowsPerPage"]);
            DALDeslocacoes infDAL = new DALDeslocacoes();
            list_vwDoentesPresentes = infDAL.GetCondListDoentesPresentes(servicoCod, ultLocalCod, viewOnlocal);

            RenderRows(modelValencia);

            List<LDFTableRow> fullList = list_rows;
            pageCount   = list_rows.Count / itemsPerPage;
            rowCount    = list_rows.Count;
            list_rows   = list_rows.Take(itemsPerPage).ToList();

            return fullList;
        }



        private void RenderRows(ValenciaModel modelValencia)
        {
            string dtConsulta = "", selectValencias = "", deslocActive = "", modalInfo = "", prevElement = "";

            if (list_vwDoentesPresentes != null)
            {
                rowCount = list_vwDoentesPresentes.Count;
                for (int i = 0; i < list_vwDoentesPresentes.Count; i++)
                {
                    LDFTableRow row = new LDFTableRow();
                    row.rowItems = new List<LDFTableItem>();


                    LDFTableItem item   = new LDFTableItem();
                    item.itemColumnName = "T_DOENTE";
                    item.itemValue      = String.Format("{0}{1}", list_vwDoentesPresentes[i].T_DOENTE, list_vwDoentesPresentes[i].DOENTE);
                    row.rowItems.Add(item);

                    item = new LDFTableItem();
                    item.itemColumnName = "NOME_DOENTE";
                    item.itemValue = list_vwDoentesPresentes[i].NOME;
                    row.rowItems.Add(item);

                    item                = new LDFTableItem();
                    item.itemColumnName = "T_EPISODIO";
                    item.itemValue      = (list_vwDoentesPresentes[i].T_EPISODIO.Contains("Internamento") ? "Internamento" : "Consulta");
                    row.rowItems.Add(item);

                    if (list_vwDoentesPresentes[i].HR_CONS != null)
                        dtConsulta = String.Format("{0:dd/MM/yyyy}", list_vwDoentesPresentes[i].DT_CONS);
                    else
                        dtConsulta = "";

                    item                = new LDFTableItem();
                    item.itemColumnName = "DT_CONS";
                    item.itemValue      = String.Format("{0} {1}", dtConsulta, list_vwDoentesPresentes[i].HR_CONS);
                    row.rowItems.Add(item);

                    item                = new LDFTableItem();
                    item.itemColumnName = "FLOORNUMBER";
                    item.itemValue      = list_vwDoentesPresentes[i].COD_SERV;
                    row.rowItems.Add(item);

                    item                = new LDFTableItem();
                    item.itemColumnName = "FLOOR_DESCR";
                    item.itemValue      = list_vwDoentesPresentes[i].DESCR_SERV;
                    row.rowItems.Add(item);


                    deslocActive = "";
                    if ( (list_vwDoentesPresentes[i].COD_SERV != list_vwDoentesPresentes[i].U_LOCAL) && (list_vwDoentesPresentes[i].U_LOCAL != null) )
                        deslocActive = "active";

                    selectValencias = "<select data-select-row='" + i + "' class='infad-selected-item " + deslocActive + "' data-previous-elem='0'>";
                    if (list_vwDoentesPresentes[i].U_LOCAL == null)
                        selectValencias += "<option disabled value='0' " + (String.IsNullOrEmpty(list_vwDoentesPresentes[i].U_LOCAL) ? "selected" : "") + ">Sem deslocação</option>";
                    else
                        selectValencias += "<option disabled value='0' selected >" + list_vwDoentesPresentes[i].U_LOCAL_DESCR + "</option>";

                    
                    selectValencias += "<optgroup label='Localização Origem'>";
                    selectValencias += "<option value='" + list_vwDoentesPresentes[i].COD_SERV + "' " + ((list_vwDoentesPresentes[i].COD_SERV == list_vwDoentesPresentes[i].U_LOCAL) ? "selected" : "") + ">" + list_vwDoentesPresentes[i].DESCR_SERV + "</option>";
                    selectValencias += "</optgroup>";

                    selectValencias += "<optgroup label='Todas as localizações'>";
                    foreach (Valencia itemVal in modelValencia.listValencias)
                    {
                        if (itemVal.COD_SERV != list_vwDoentesPresentes[i].COD_SERV && list_vwDoentesPresentes[i].U_LOCAL != itemVal.COD_SERV)
                            selectValencias += "<option value='" + itemVal.COD_SERV + "' " + ((itemVal.COD_SERV == list_vwDoentesPresentes[i].U_LOCAL) ? "selected" : "") + ">" + itemVal.DESCR_SERV + "</option>";
                    }
                    selectValencias += "</optgroup>";
                    selectValencias += "</select>";

                    modalInfo = "";
                    if (list_vwDoentesPresentes[i].U_LOCAL != null)
                        modalInfo = "data-toggle='modal' data-target='#modal-desloc-timeline' data-tdoente='" + list_vwDoentesPresentes[i].T_DOENTE + "' data-doente='" + list_vwDoentesPresentes[i].DOENTE + "' data-nome='" + list_vwDoentesPresentes[i].NOME + "'";

                    string infoValue = "<a " + modalInfo + " class='fa fa-info-circle fa-lg " + ((list_vwDoentesPresentes[i].U_LOCAL == null) ? "text-muted" : "text-primary") + " infADModalDesloc' title='Histórico de movimentações' style='padding-left:7px;'></a>";
                    selectValencias += infoValue;

                    item = new LDFTableItem();
                    item.itemColumnName = "ULT_LOCAL";
                    item.itemValue = selectValencias;
                    row.rowItems.Add(item);


                    item                = new LDFTableItem();
                    item.itemColumnName = "COL2_CLICKABLE";
                    item.itemValue      = "<a data-toggle='modal' data-target='#modal-desloc-prod' data-tdoente='" + list_vwDoentesPresentes[i].T_DOENTE + "' data-doente='" + list_vwDoentesPresentes[i].DOENTE + "' data-nome='" + list_vwDoentesPresentes[i].NOME + "' data-codserv='" + list_vwDoentesPresentes[i].COD_SERV + "' data-ultloc='" + list_vwDoentesPresentes[i].U_LOCAL + "' data-ncons='" + list_vwDoentesPresentes[i].N_CONS + "' data-tEpis='" + list_vwDoentesPresentes[i].T_EPISODIO + "' data-epis='" + list_vwDoentesPresentes[i].EPISODIO + "' class='fa fa-archive fa-lg infADModalDeslocProd' title='Movimentações de produtos'></a>";
                    row.rowItems.Add(item);

                    list_rows.Add(row);
                }

                pageCount = list_vwDoentesPresentes.Count / Convert.ToInt32(ConfigurationManager.AppSettings["PIMRowsPerPage"]);
            }
        }

        private string GetLinkUrl(string url, VwInternamentos internamento, UserInfo uinfo)
        {
            string res = url;

            string[] arr = url.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string str in arr)
            {
                if (internamento.GetType().GetProperty(str) != null)
                    res = res.Replace("[" + str + "]", internamento.GetType().GetProperty(str).GetValue(internamento).ToString());
            }


            foreach (string str in arr)
            {
                if (uinfo.GetType().GetProperty(str.Replace("SS_", "")) != null)
                    res = res.Replace("[" + str + "]", uinfo.GetType().GetProperty(str.Replace("SS_", "")).GetValue(uinfo).ToString());
            }



            return res;
        }


        internal bool UpdateRow(UserInfo uinfo, object tableRows, string itemRow, string deslocCod)
        {
            DALDeslocacoes infDAL = new DALDeslocacoes();
            list_vwDoentesPresentes     = tableRows as List<VwDoentesPresentes>;
            VwDoentesPresentes doente   = list_vwDoentesPresentes[Convert.ToInt32(itemRow)];

            TblDesloc tbl   = new TblDesloc();
            tbl.T_DOENTE    = doente.T_DOENTE;
            tbl.DOENTE      = doente.DOENTE;
            tbl.N_CONS      = doente.N_CONS;
            tbl.T_EPISODIO  = doente.T_EPISODIO;
            tbl.EPISODIO    = doente.EPISODIO;
            tbl.COD_SERV    = deslocCod;
            tbl.USER_CRI    = uinfo.userID;
            tbl.DT_DESL     = DateTime.Now;
            tbl.DT_CRI      = DateTime.Now;

            return infDAL.InsertDoenteDesloc(tbl);
        }
    }
}