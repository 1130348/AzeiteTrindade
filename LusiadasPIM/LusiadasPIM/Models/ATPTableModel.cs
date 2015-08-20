using LDFHelper.Helpers;
using PIMDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LusiadasPIM.Models
{
    public class ATPTableModel : LDFTableHelper
    {
        #region Variables

        public List<LDFTableHeader> list_headers        { get; set; }
        public List<LDFTableRow>    list_rows           { get; set; }
        public LDFTableCallback     callbacks           { get; set; }
        public int                  pageCount = 0;
        public int                  rowCount = 0;
        public List<VwDashboardATP> list_vwDashboardATP { get; set; }

        #endregion

        #region DeslocProdTable

        public ATPTableModel()
        {
            list_headers    = new List<LDFTableHeader>();
            list_rows       = new List<LDFTableRow>();
            callbacks       = new LDFTableCallback();

            UrlHelper helper                = new UrlHelper(HttpContext.Current.Request.RequestContext);
            callbacks.loadCallBack          = helper.Action("LoadTable", "ATP");
            callbacks.paginationCallBack    = helper.Action("PageTable", "ATP");
            callbacks.orderCallBack         = helper.Action("OrderTable", "ATP");

            RenderHeader();
        }

        #endregion

        #region Initialize Methods

        public void RenderHeader()
        {

            LDFTableHeader header   = new LDFTableHeader();
            header.headerName       = "Doente";
            header.headerID         = "DOENTE";
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "Nome";
            header.headerID         = "NOME";
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "Hora";
            header.headerID         = "HR_CONS";
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "Triagem";
            header.headerID         = "TRIAGEM";
            header.headerStyle.Add("text-align", "center");
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "Nota Médica";
            header.headerID         = "NOTA_MEDICA";
            header.headerStyle.Add("text-align", "center");
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "Box";
            header.headerID         = "BOX";
            header.headerStyle.Add("text-align", "center");
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "Cadeirão";
            header.headerID         = "CADEIRAO";
            header.headerStyle.Add("text-align", "center");
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "Penso";
            header.headerID         = "PENSO";
            header.headerStyle.Add("text-align", "center");
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "Análises";
            header.headerID         = "ANALISES";
            header.headerStyle.Add("text-align", "center");
            header.headerType       = typeof(string);
            list_headers.Add(header);


            header                  = new LDFTableHeader();
            header.headerName       = "Medicação";
            header.headerID         = "MEDICACAO";
            header.headerStyle.Add("text-align", "center");
            header.headerType       = typeof(string);
            list_headers.Add(header);

            
            header                  = new LDFTableHeader();
            header.headerName       = "Imagiologia";
            header.headerID         = "IMAGIOLOGIA";
            header.headerStyle.Add("text-align", "center");
            header.headerType       = typeof(string);
            list_headers.Add(header);


            header                  = new LDFTableHeader();
            header.headerName       = "ECG";
            header.headerID         = "ECG";
            header.headerStyle.Add("text-align", "center");
            header.headerType       = typeof(string);
            list_headers.Add(header);


            header                  = new LDFTableHeader();
            header.headerName       = "Consulta Externa";
            header.headerID         = "CONSULTA_EXTERNA";
            header.headerStyle.Add("text-align", "center");
            header.headerType       = typeof(string);
            list_headers.Add(header);


        }

        public void LoadRows()
        {
            DALAtPermanente atpDAL  = new DALAtPermanente();
            list_vwDashboardATP     = atpDAL.GetListATP();
            RenderRows();
        }

        private void RenderRows()
        {
            string nomeDescr = "", curdate = "", field = "";
            string red = "text-red", green = "text-green", yellow = "text-yellow", gray = "text-gray";

            if (list_vwDashboardATP != null)
            {
                rowCount = list_vwDashboardATP.Count;
                for (int i = 0; i < list_vwDashboardATP.Count; i++)
                {
                    LDFTableRow row = new LDFTableRow();
                    row.rowItems    = new List<LDFTableItem>();

                    LDFTableItem item   = new LDFTableItem();
                    item.itemColumnName = "DOENTE";
                    item.itemValue      = String.Format("{0}{1}", list_vwDashboardATP[i].T_DOENTE, list_vwDashboardATP[i].DOENTE);
                    row.rowItems.Add(item);

                    if (list_vwDashboardATP[i].DT_NASC != null)
                        curdate = String.Format("{0:dd/MM/yyyy}", list_vwDashboardATP[i].DT_NASC);
                    nomeDescr = "<div class='row'>";
                    nomeDescr += "<div class='col-xs-12'>" + list_vwDashboardATP[i].NOME + " <i class='fa fa-" + ((list_vwDashboardATP[i].SEXO == "F") ? "venus text-pink" : "mars text-blue") + "'></i></div>";
                    nomeDescr += "</div>";
                    if (!String.IsNullOrEmpty(curdate))
                    {
                        nomeDescr += "<div class='row'>";
                        nomeDescr += "<div class='col-xs-12'>" + curdate + " (" + GetBirthDate(list_vwDashboardATP[i].DT_NASC) + ")</div>";
                        nomeDescr += "</div>";
                    }
                    item                = new LDFTableItem();
                    item.itemColumnName = "NOME";
                    item.itemValue      = nomeDescr;
                    row.rowItems.Add(item);


                    curdate = "";
                    if (list_vwDashboardATP[i].HR_CONS != null)
                        curdate = String.Format("{0:HH:mm:ss}", list_vwDashboardATP[i].HR_CONS);

                    item                = new LDFTableItem();
                    item.itemColumnName = "HR_CONS";
                    item.itemValue      = curdate;
                    row.rowItems.Add(item);

                    item                = new LDFTableItem();
                    item.itemColumnName = "TRIAGEM";
                    item.itemValue      = "<i class='fa fa-circle lg " + ( (list_vwDashboardATP[i].TRIAGEM == "S") ? "text-green" : "text-red" ) + "'></i>";
                    row.rowItems.Add(item);

                    item                = new LDFTableItem();
                    item.itemColumnName = "NOTA_MEDICA";
                    item.itemValue      = "<i class='fa fa-circle lg " + ( (list_vwDashboardATP[i].NOTA_MEDICA == "S") ? "text-green" : "text-red" ) + "'></i>";
                    row.rowItems.Add(item);

                    field = green;
                    if (list_vwDashboardATP[i].BOX_APLICAVEL == "N")
                        field = gray;
                    else if (list_vwDashboardATP[i].BOX_PRESENTE == "N")
                        field = red;
                    item                = new LDFTableItem();
                    item.itemColumnName = "BOX";
                    item.itemValue      = "<i class='fa fa-circle lg " + field + "'></i>";
                    row.rowItems.Add(item);


                    item                = new LDFTableItem();
                    item.itemColumnName = "CADEIRAO";
                    item.itemValue      = list_vwDashboardATP[i].CADEIRAO_PRESENTE;
                    row.rowItems.Add(item);


                    field = green;
                    if (list_vwDashboardATP[i].PENSO_APLICAVEL == "N")
                        field = gray;
                    else if (list_vwDashboardATP[i].PENSO_REGISTO == "N")
                        field = red;
                    item                = new LDFTableItem();
                    item.itemColumnName = "PENSO";
                    item.itemValue      = "<i class='fa fa-circle lg " + field + "'></i>";
                    row.rowItems.Add(item);

                    field = green;
                    if (list_vwDashboardATP[i].ANALISES_SOLICITADAS == "N")
                        field = gray;
                    else if (list_vwDashboardATP[i].ANALISES_SOLICITADAS == "S" && list_vwDashboardATP[i].COLHEITA_OK == "N" && list_vwDashboardATP[i].ANALISES_OK == "N")
                        field = yellow;
                    else if (list_vwDashboardATP[i].ANALISES_SOLICITADAS == "S" && list_vwDashboardATP[i].COLHEITA_OK == "S" && list_vwDashboardATP[i].ANALISES_OK == "N")
                        field = red;
                    item                = new LDFTableItem();
                    item.itemColumnName = "ANALISES";
                    item.itemValue      = "<i class='fa fa-circle lg " + field + "'></i>";
                    row.rowItems.Add(item);


                    field = green;
                    if (list_vwDashboardATP[i].MEDICACAO_PRESCRITA_PCE.Value == 0 && list_vwDashboardATP[i].MEDICACAO_PRESCRITA_GLINTT.Value == 0)
                        field = gray;
                    else if (list_vwDashboardATP[i].MEDICACAO_ADMINISTRADA_AMB.Value < list_vwDashboardATP[i].MEDICACAO_PRESCRITA_PCE.Value || list_vwDashboardATP[i].MEDICACAO_ADMINISTRADA_GLINTT.Value < list_vwDashboardATP[i].MEDICACAO_PRESCRITA_GLINTT.Value)
                        field = red;
                    item                = new LDFTableItem();
                    item.itemColumnName = "MEDICACAO";
                    item.itemValue      = "<i class='fa fa-circle lg " + field + "'></i>";
                    row.rowItems.Add(item);


                    field = green;
                    if (list_vwDashboardATP[i].IMAGIOLOGIA_REQUESITADOS.Value == 0)
                        field = gray;
                    item                = new LDFTableItem();
                    item.itemColumnName = "IMAGIOLOGIA";
                    item.itemValue      = "<i class='fa fa-circle lg " + field + "'></i>";
                    row.rowItems.Add(item);


                    field = green;
                    if (list_vwDashboardATP[i].ECG_REQUESITADOS.Value == 0)
                        field = gray;
                    else if (list_vwDashboardATP[i].ECG_REQUESITADOS.Value <= list_vwDashboardATP[i].ECG_REALIZADOS.Value)
                        field = red;
                    item                = new LDFTableItem();
                    item.itemColumnName = "ECG";
                    item.itemValue      = "<i class='fa fa-circle lg " + field + "'></i>";
                    row.rowItems.Add(item);


                    field = green;
                    if (list_vwDashboardATP[i].CEXTERNA_REQUISITADOS.Value == 0)
                        field = gray;
                    else if (list_vwDashboardATP[i].CEXTERNA_REALIZADOS.Value < list_vwDashboardATP[i].CEXTERNA_REQUISITADOS.Value)
                        field = red;
                    item                = new LDFTableItem();
                    item.itemColumnName = "CONSULTA_EXTERNA";
                    item.itemValue      = "<i class='fa fa-circle lg " + field + "'></i>";
                    row.rowItems.Add(item);


                    list_rows.Add(row);
                }

                pageCount = list_vwDashboardATP.Count / Convert.ToInt32(ConfigurationManager.AppSettings["PIMRowsPerPage"]);
            }
        }

        #endregion



        internal void PageTable(object tableRows, int pageNumber, string[] orderData)
        {
            int itemsPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["PIMRowsPerPage"]);

            list_rows = tableRows as List<LDFTableRow>;
            pageCount = list_rows.Count / itemsPerPage;
            rowCount = list_rows.Count;

            list_rows = TablePagination(list_rows, itemsPerPage, pageNumber, orderData);

        }


        #region Aux Methods

        private string GetBirthDate(Nullable<DateTime> dt)
        {
            string res = "";
            DateTime curDate = DateTime.Now;

            if (dt != null)
            {
                curDate = curDate.AddTicks(dt.Value.Ticks*-1);
                if (curDate.Year > 0)
                    res = curDate.Year + " anos";
                else
                {
                    res = curDate.Month + " meses e " + curDate.Day + " dia"; 
                }
            }

            return res;
        }


        #endregion

    }
}