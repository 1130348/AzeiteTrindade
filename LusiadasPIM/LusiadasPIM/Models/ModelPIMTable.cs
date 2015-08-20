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
    public class ModelPIMTable : LDFTableHelper
    {
         //private List<vwPatientWork> list_vwPatient  { get; set; }
        public List<LDFTableHeader> list_headers    { get; set; }
        public List<LDFTableRow>    list_rows       { get; set; }
        public LDFTableCallback     callbacks       { get; set; }
        public int pageCount = 0;
        public int rowCount = 0;
        public List<VwInternamentos> list_vwInternamentos { get; set; }


        public ModelPIMTable()
        {

            list_headers        = new List<LDFTableHeader>();
            list_rows           = new List<LDFTableRow>();
            callbacks           = new LDFTableCallback();

            UrlHelper helper                = new UrlHelper(HttpContext.Current.Request.RequestContext);
            callbacks.loadCallBack          = helper.Action("LoadData", "PIM");
            callbacks.paginationCallBack    = helper.Action("PaginationData", "PIM");
            callbacks.orderCallBack         = helper.Action("OrderData", "PIM");
            callbacks.searchCallBack        = "FilterData";
            callbacks.autorefreshCallBack   = "AutoRefreshTable";

            RenderHeader();
        }

        public void RenderHeader()
        {
            LDFTableHeader header   = new LDFTableHeader();
            header.headerName       = "Código";
            header.headerID         = "T_DOENTE";
            header.headerFilter     = true;
            header.headerType       = typeof(string);
            list_headers.Add(header);


            header                  = new LDFTableHeader();
            header.headerName       = "Doente";
            header.headerID         = "NOME_DOENTE";
            header.headerFilter     = true;
            header.headerType       = typeof(string);
            list_headers.Add(header);
                   
            //header                  = new LDFTableHeader();
            //header.headerName       = "Data Nasc.";
            //header.headerID         = "PATIENT_BIRTHDATE";
            //header.headerType       = typeof(string);
            //list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "";
            header.headerID         = "FLOORNUMBER";
            header.headerVisible    = false;
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "Piso";
            header.headerID         = "FLOOR_DESCR";
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "Cama";
            header.headerID         = "BEDNUMBER";
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "";
            header.headerID         = "COD_VALENCIA";
            header.headerVisible    = false;
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "Valência";
            header.headerID         = "VALENCIA";
            header.headerType       = typeof(string);
            list_headers.Add(header);


            header                  = new LDFTableHeader();
            header.headerName       = "";
            header.headerID         = "MEDIC_COD";
            header.headerVisible    = false;
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "Médico";
            header.headerID         = "MEDIC_NAME";
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "Data Internamento";
            header.headerID         = "DT_INTERN";
            header.headerFilter     = true;
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "Data Cirurgia";
            header.headerID         = "DT_CIRURG";
            header.headerFilter     = true;
            header.headerMobile     = false;
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "Duração";
            header.headerID         = "DURATION";
            header.headerMobile     = false;
            header.headerType       = typeof(string);
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "";
            header.headerID         = "COL1_CLICKABLE";
            list_headers.Add(header);

            header                  = new LDFTableHeader();
            header.headerName       = "";
            header.headerID         = "COL2_CLICKABLE";
            list_headers.Add(header);
        }

        public void RenderRows(UserInfo uinfo)
        {
            DALInternamento intDAL  = new DALInternamento();
            list_vwInternamentos    = intDAL.GetListInternamento();
            string dtCirurgia = "";

            if (list_vwInternamentos != null)
            {
                rowCount = list_vwInternamentos.Count;
                for (int i = 0; i < list_vwInternamentos.Count; i++)
                {
                    LDFTableRow row     = new LDFTableRow();
                    row.rowItems        = new List<LDFTableItem>();


                    LDFTableItem item   = new LDFTableItem();
                    item.itemColumnName = "T_DOENTE";
                    item.itemValue      = String.Format("{0}{1}", list_vwInternamentos[i].T_DOENTE, list_vwInternamentos[i].DOENTE);
                    row.rowItems.Add(item);

                    item                = new LDFTableItem();
                    item.itemColumnName = "NOME_DOENTE";
                    item.itemValue      = list_vwInternamentos[i].NOME;
                    row.rowItems.Add(item);

                    //item                = new LDFTableItem();
                    //item.itemColumnName = "PATIENT_BIRTHDATE";
                    //item.itemValue      = list_vwInternamentos[i].DT_ALTA.ToString();
                    //row.rowItems.Add(item);

                    item                = new LDFTableItem();
                    item.itemColumnName = "FLOORNUMBER";
                    item.itemValue      = list_vwInternamentos[i].COD_SERV;
                    row.rowItems.Add(item);

                    item                = new LDFTableItem();
                    item.itemColumnName = "FLOOR_DESCR";
                    item.itemValue      = "<span data-toggle='tooltip' class='text-blue' title='' data-original-title='" + list_vwInternamentos[i].DESCR_SERV + "'>" + list_vwInternamentos[i].COD_SERV + "</span>";
                    row.rowItems.Add(item);

                    item                = new LDFTableItem();
                    item.itemColumnName = "BEDNUMBER";
                    item.itemValue      = String.Format("{0} - {1}", list_vwInternamentos[i].SALA, list_vwInternamentos[i].CAMA);
                    row.rowItems.Add(item);


                    item                = new LDFTableItem();
                    item.itemColumnName = "COD_VALENCIA";
                    item.itemValue      = list_vwInternamentos[i].COD_SERV_VALENCIA;
                    row.rowItems.Add(item);

                    item                = new LDFTableItem();
                    item.itemColumnName = "VALENCIA";
                    item.itemValue      = list_vwInternamentos[i].DESCR_SERV;
                    row.rowItems.Add(item);

                    item                = new LDFTableItem();
                    item.itemColumnName = "MEDIC_NAME";
                    item.itemValue      = list_vwInternamentos[i].NOME_MEDICO;
                    row.rowItems.Add(item);

                    item                = new LDFTableItem();
                    item.itemColumnName = "DT_INTERN";
                    item.itemValue      = String.Format("{0} {1}", list_vwInternamentos[i].DT_INT.ToShortDateString(), list_vwInternamentos[i].HORA_INT);
                    row.rowItems.Add(item);


                    if ( list_vwInternamentos[i].DT_OPER != null )
                        dtCirurgia = String.Format("{0:dd/MM/yyyy}", list_vwInternamentos[i].DT_OPER);
                    else 
                        dtCirurgia = "";

                    item                = new LDFTableItem();
                    item.itemColumnName = "DT_CIRURG";
                    item.itemValue      = String.Format("{0} {1}", dtCirurgia, list_vwInternamentos[i].HR_INI);
                    row.rowItems.Add(item);

                    item                = new LDFTableItem();
                    item.itemColumnName = "DURATION";
                    item.itemValue      = list_vwInternamentos[i].DURACAO;
                    row.rowItems.Add(item);

                    item                = new LDFTableItem();
                    
                    item.itemColumnName = "COL1_CLICKABLE";
                    item.itemValue = "<span data-toggle='tooltip' class='text-blue' title='' data-original-title='eResults'><a target='_blank' href=\"" + GetLinkUrl(ConfigurationManager.AppSettings["eResultsDetailURL"], list_vwInternamentos[i], uinfo) + "\" class='fa fa-file-text-o' style='font-size:20px;'></a></span>";
                    row.rowItems.Add(item);

                    item                = new LDFTableItem();
                    item.itemColumnName = "COL2_CLICKABLE";
                    item.itemValue      = "<span data-toggle='tooltip' class='text-blue' title='' data-original-title='PIM'><a target='_blank' href=\"" + GetLinkUrl(ConfigurationManager.AppSettings["PIMDetailURL"], list_vwInternamentos[i], uinfo) + "\">PIM</a></span>";
                    row.rowItems.Add(item);

                    list_rows.Add(row);
                }

                pageCount = list_vwInternamentos.Count / Convert.ToInt32(ConfigurationManager.AppSettings["PIMRowsPerPage"]);
            }
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

        internal List<LDFTableRow> FilterData(object tableRows, string valenciaCod, string pisoCod, bool viewMine, string numMecan)
        {
            int itemsPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["PIMRowsPerPage"]);

            list_rows = tableRows as List<LDFTableRow>;
            

            if (!String.IsNullOrEmpty(valenciaCod))
            {
                list_rows = list_rows.Where(p => p.rowItems.Any(r => r.itemColumnName == "COD_VALENCIA" && r.itemValue == valenciaCod)).ToList();
            }

            if (!String.IsNullOrEmpty(pisoCod))
            {
                list_rows = list_rows.Where(p => p.rowItems.Any(r => r.itemColumnName == "FLOORNUMBER" && r.itemValue == pisoCod)).ToList();
            }

            if (viewMine)
            {
                list_rows = list_rows.Where(p => p.rowItems.Any(r => r.itemColumnName == "MEDIC_COD" && r.itemValue == numMecan)).ToList();
            }

            List<LDFTableRow> fullList = list_rows;
            pageCount   = list_rows.Count / itemsPerPage;
            rowCount    = list_rows.Count;
            list_rows   = list_rows.Take(itemsPerPage).ToList();

            return fullList;
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

    }
}