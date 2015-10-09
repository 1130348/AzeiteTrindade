using LDF.ParameterManager;
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
    public class DeslocacoesModel : LDFTableModel
    {
        public DeslocacoesModel()
        {
            pageSize    = Convert.ToInt32(ConfigurationManager.AppSettings["TableRowsPerPage"]);
            dbParams = new LDFTableDBParams((string)HttpContext.Current.Session[Constants.SS_LOCAL_CONN], "MEDICO", "V_DOENTES_PRESENTES", "*", "", "DESCR_SERV", null, null);
            objType     = typeof(VDoentesPresentes);

            LDFTableHeaders();
            FillHeader();
        }

        public void FillHeader()
        {
            list_headers.Add(new LDFTableHeader() { headerID = "LOCAL_ATUAL", headerName = "Local Atual" });
            list_headers.Add(new LDFTableHeader() { headerID = "COL_CLICKABLE" });
        }

        public void LDFTableTreatData()
        {
            string dtConsulta = "", tdoente = "", doente = "", tepisodio = "";
            string selectValencias = "", deslocActive = "", modalInfo = "";
            
            ValenciaModel valModel = (ValenciaModel)HttpContext.Current.Session["InfADValencias"];
            
            foreach (LDFTableRow item in list_rows)
            {
                item.rowItems.Add(new LDFTableItem("LOCAL_ATUAL", ""));
                item.rowItems.Add(new LDFTableItem("COL_CLICKABLE", ""));

                tdoente     = Generic.GetItemValue(item, "T_DOENTE");
                doente      = Generic.GetItemValue(item, "DOENTE");
                tepisodio   = Generic.GetItemValue(item, "T_EPISODIO");

                item.rowItems.First(q => q.itemColumnName == "T_DOENTE").itemValue = tdoente + doente;
                item.rowItems.First(q => q.itemColumnName == "T_EPISODIO").itemValue = (tepisodio.Contains("Internamento") ? "Internamento" : "Consulta");

                //String deveria receber DateTime e recebe int32
                dtConsulta = "";
                if (!String.IsNullOrEmpty(Generic.GetItemValue(item, "HR_CONS")))
                    dtConsulta = String.Format("{0:dd/MM/yyyy}", Convert.ToInt32(Generic.GetItemValue(item, "DT_CONS")));

                item.rowItems.First(q => q.itemColumnName == "HR_CONS").itemValue = String.Format("{0} {1}", dtConsulta, Generic.GetItemValue(item, "HR_CONS"));
                item.rowItems.First(q => q.itemColumnName == "COL_CLICKABLE").itemValue = "<a data-toggle='modal' data-target='#modal-desloc-prod' data-tdoente='" + tdoente + "' data-doente='" + doente + "' data-nome='" + Generic.GetItemValue(item, "NOME") + "' data-codserv='" + Generic.GetItemValue(item, "COD_SERV") + "' data-ultloc='" + Generic.GetItemValue(item, "U_LOCAL") + "' data-ncons='" + Generic.GetItemValue(item, "N_CONS") + "' data-tEpis='" + tepisodio + "' data-epis='" + Generic.GetItemValue(item, "EPISODIO") + "' class='fa fa-flask fa-lg infADModalDeslocProd' title='Movimentações de produtos'></a>";

                deslocActive = "";
                if ((Generic.GetItemValue(item, "COD_SERV") != Generic.GetItemValue(item, "U_LOCAL")) && (!String.IsNullOrEmpty(Generic.GetItemValue(item, "U_LOCAL"))))
                    deslocActive = "active";

                selectValencias = "<select data-select-row='" + tdoente + "_" + doente + "' class='infad-selected-item " + deslocActive + "' data-previous-elem='0'>";
                if (String.IsNullOrEmpty(Generic.GetItemValue(item, "U_LOCAL")))
                    selectValencias += "<option disabled value='0' " + (String.IsNullOrEmpty(Generic.GetItemValue(item, "U_LOCAL")) ? "selected" : "") + ">Sem deslocação</option>";
                else
                    selectValencias += "<option disabled value='0' selected >" + Generic.GetItemValue(item, "U_LOCAL_DESCR") + "</option>";


                selectValencias += "<optgroup label='Localização Origem'>";
                selectValencias += "<option value='" + Generic.GetItemValue(item, "COD_SERV") + "' " + ((Generic.GetItemValue(item, "COD_SERV") == Generic.GetItemValue(item, "U_LOCAL")) ? "selected" : "") + ">" + Generic.GetItemValue(item, "DESCR_SERV") + "</option>";
                selectValencias += "</optgroup>";

                selectValencias += "<optgroup label='Todas as localizações'>";

                
                foreach (Valencia itemVal in valModel.listValencias)
                {
                    if (itemVal.COD_SERV != Generic.GetItemValue(item, "COD_SERV") && Generic.GetItemValue(item, "U_LOCAL") != itemVal.COD_SERV)
                        selectValencias += "<option value='" + itemVal.COD_SERV + "' " + ((itemVal.COD_SERV == Generic.GetItemValue(item, "U_LOCAL")) ? "selected" : "") + ">" + itemVal.DESCR_SERV + "</option>";
                }
                selectValencias += "</optgroup>";
                selectValencias += "</select>";

                modalInfo = "";
                if (!String.IsNullOrEmpty(Generic.GetItemValue(item, "U_LOCAL")))
                    modalInfo = "data-toggle='modal' data-target='#modal-desloc-timeline' data-tdoente='" + tdoente + "' data-doente='" + doente + "' data-nome='" + Generic.GetItemValue(item, "NOME") + "'";

                string infoValue = "<a " + modalInfo + " class='fa fa-info-circle fa-lg " + (String.IsNullOrEmpty(Generic.GetItemValue(item, "U_LOCAL")) ? "text-muted" : "text-primary") + " infADModalDesloc' title='Histórico de movimentações' style='padding-left:7px;'></a>";
                selectValencias += infoValue;

                item.rowItems.First(q => q.itemColumnName == "LOCAL_ATUAL").itemValue = selectValencias;

                

            }
        }

        internal bool UpdateRow(UserInfo uinfo, string itemRow, string deslocCod)
        {
            DALDeslocacoes infDAL = new DALDeslocacoes();
            VwDoentesPresentes doente = infDAL.GetDeslocUser(itemRow.Split('_')[0], itemRow.Split('_')[1]);

            TblDesloc tbl = new TblDesloc();
            tbl.T_DOENTE = doente.T_DOENTE;
            tbl.DOENTE = doente.DOENTE;
            tbl.N_CONS = doente.N_CONS;
            tbl.T_EPISODIO = doente.T_EPISODIO;
            tbl.EPISODIO = doente.EPISODIO;
            tbl.COD_SERV = deslocCod;
            tbl.USER_CRI = uinfo.userID;
            tbl.DT_DESL = DateTime.Now;
            tbl.DT_CRI = DateTime.Now;

            return infDAL.InsertDoenteDesloc(tbl);
        }
    }
}