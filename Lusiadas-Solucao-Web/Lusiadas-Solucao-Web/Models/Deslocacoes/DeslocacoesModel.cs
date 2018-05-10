using LDF.ParameterManager;
using LDFHelper.Helpers;
using LusiadasDAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LusiadasSolucaoWeb.Models
{
    public class DeslocacoesModel : LDFTableModel
    {

        private string schema = "MEDICO";
        private string tabela = "V_DOENTES_PRESENTES_V3";
        private string query = "*";
        private string condition = "";
        private string orderQ = "DESCR_SERV";

        public DeslocacoesModel()
        {
            pageSize    = Convert.ToInt32(ConfigurationManager.AppSettings["TableRowsPerPage"]);
            dbParams = new LDFTableDBParams((string)HttpContext.Current.Session["PERMA"], schema, tabela, query, condition,orderQ, null, null);
            objType     = typeof(VDoentesPresentes);

            LDFTableHeaders();
            FillHeader();
        }

        public void FillHeader()
        {
            
            list_headers.Add(new LDFTableHeader() { headerID = "LOCAL_ATUAL", headerName = "Local Atual"});
            list_headers.Add(new LDFTableHeader() { headerID = "COL_CLICKABLE" });
        }

        public void LDFTableTreatData()
        {
            string dtConsulta = "", tdoente = "", doente = "", tepisodio = "", episodio = "",n_cons="";
            string selectValencias = "", deslocActive = "", modalInfo = "";
            int nProd;

            #region Parametrizações
            ValenciaModel valModel = (ValenciaModel)HttpContext.Current.Session["InfADValencias"];
            PisosModel pisosModel = (PisosModel)HttpContext.Current.Session["InfADPisos"];
            ParameterModel destinos = (ParameterModel)HttpContext.Current.Session["InfADDeslocProd"];

            #endregion

            foreach (LDFTableRow item in list_rows)
            {
                item.rowItems.Add(new LDFTableItem("LOCAL_ATUAL", ""));
                item.rowItems.Add(new LDFTableItem("COL_CLICKABLE", ""));

                tdoente     = Generic.GetItemValue(item, "T_DOENTE");
                doente      = Generic.GetItemValue(item, "DOENTE");
                tepisodio   = Generic.GetItemValue(item, "T_EPISODIO");
                episodio = Generic.GetItemValue(item, "EPISODIO");
                n_cons = Generic.GetItemValue(item, "N_CONS");
                nProd = Convert.ToInt32(Generic.GetItemValue(item, "PROD"));

                #region Doente
                item.rowItems.First(q => q.itemColumnName == "T_DOENTE").itemValue = doente;
                item.rowItems.First(q => q.itemColumnName == "T_EPISODIO").itemValue = (tepisodio.Contains("Internamento") ? "Internamento" : "Consulta");
                #endregion

                #region Hora

                dtConsulta = "";
                if (!String.IsNullOrEmpty(Generic.GetItemValue(item, "HR_CONS")))
                    dtConsulta = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(Generic.GetItemValue(item, "DT_CONS")));



                item.rowItems.First(q => q.itemColumnName == "HR_CONS").itemValue = String.Format("{0} {1}", dtConsulta, Generic.GetItemValue(item, "HR_CONS"));
                #endregion

                #region Produto

                if (nProd>0) {
                    item.rowItems.First(q => q.itemColumnName == "COL_CLICKABLE").itemValue = "<a data-toggle='modal' data-target='#modal-desloc-prod' data-tdoente='" + tdoente + "' data-doente='" + doente + "' data-nome='" + Generic.GetItemValue(item, "NOME") + "' data-codserv='" + Generic.GetItemValue(item, "COD_SERV") + "' data-ultloc='" + Generic.GetItemValue(item, "U_LOCAL") + "' data-ncons='" + n_cons
                        + "' data-tEpis='" + tepisodio + "' data-epis='" + episodio + "' class='fa fa-flask fa-lg infADModalDeslocProd' title='Movimentações de produtos'></a>";
                }
                else
                {
               
                    item.rowItems.First(q => q.itemColumnName == "COL_CLICKABLE").itemValue = "<a id='show' data-toggle='modal' data-target='#modal-desloc-prod' data-tdoente='" + tdoente + "' data-doente='" + doente + "' data-nome='" + Generic.GetItemValue(item, "NOME") + "' data-codserv='" + Generic.GetItemValue(item, "COD_SERV") + "' data-ultloc='" + Generic.GetItemValue(item, "U_LOCAL") + "' data-ncons='" + n_cons
                    + "' data-tEpis='" + tepisodio + "' data-epis='" + episodio + "' class='fa fa-plus fa-lg text-gray infADModalDeslocProd' title='Movimentações de produtos'></a>";
                }
                deslocActive = "";

                #endregion

                #region Desloc DropBox

                if (!String.IsNullOrEmpty(Generic.GetItemValue(item, "U_LOCAL")))
                    deslocActive = "active";

                selectValencias = "<select data-select-row='" + tdoente + "_" + doente + "' value='"+n_cons+"'" +" class='infad-selected-item " + deslocActive + "' data-previous-elem='0'>";
                if (String.IsNullOrEmpty(Generic.GetItemValue(item, "U_LOCAL")))
                    selectValencias += "<option disabled value='0' " + (String.IsNullOrEmpty(Generic.GetItemValue(item, "U_LOCAL")) ? "selected" : "") + ">Sem deslocação</option>";
                else
                    selectValencias += "<option disabled value='0' selected >" + Generic.GetItemValue(item, "U_LOCAL_DESCR") + "</option>";


                selectValencias += "<optgroup label='Localização Origem'>";
                selectValencias += "<option value='" + Generic.GetItemValue(item, "COD_SERV") + "' " + ((Generic.GetItemValue(item, "COD_SERV") == Generic.GetItemValue(item, "U_LOCAL")) ? "selected" : "") + ">" + Generic.GetItemValue(item, "DESCR_SERV") + "</option>";
                selectValencias += "</optgroup>";

                selectValencias += "<optgroup label='Localizações Parametrizadas'>";


                foreach (String itemVal in destinos.list_destDoentes)
                {

                    foreach (Valencia valP in valModel.listValencias)
                    {
                                       
                        if (itemVal == valP.COD_SERV)
                        {
                            selectValencias += "<option value='" + valP.COD_SERV + "' " + ((valP.COD_SERV == Generic.GetItemValue(item, "U_LOCAL")) ? "selected" : "") + ">" + valP.DESCR_SERV + "</option>";
                        }

                    }

                }
                selectValencias += "</optgroup>";

                selectValencias += "<optgroup label='Localizações + Frequentes'>";


                foreach (Valencia itemVal in valModel.listValenciasParametrizadas)
                {
                    if (itemVal.COD_SERV != Generic.GetItemValue(item, "COD_SERV") && Generic.GetItemValue(item, "U_LOCAL") != itemVal.COD_SERV)
                        selectValencias += "<option value='" + itemVal.COD_SERV + "' " + ((itemVal.COD_SERV == Generic.GetItemValue(item, "U_LOCAL")) ? "selected" : "") + ">" + itemVal.DESCR_SERV + "</option>";
                }
                selectValencias += "</optgroup>";

                selectValencias += "<optgroup label='Pisos'>";

                foreach (Piso itemPiso in pisosModel.listPisos)
                {
                    if (itemPiso.COD_SERV != Generic.GetItemValue(item, "COD_SERV") && Generic.GetItemValue(item, "U_LOCAL") != itemPiso.COD_SERV)
                        selectValencias += "<option value='" + itemPiso.COD_SERV + "' " + ((itemPiso.COD_SERV == Generic.GetItemValue(item, "U_LOCAL")) ? "selected" : "") + ">" + itemPiso.DESCR_SERV + "</option>";
                }
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
                {
                    modalInfo = "data-toggle='modal' data-target='#modal-desloc-timeline' data-tdoente='" + tdoente + "' data-doente='" + doente + "' data-nome='" + Generic.GetItemValue(item, "NOME") + "' value='" + n_cons+ "'" ;
                    if (deslocActive == "active")
                    {
                        string infoValue = "<a " + modalInfo + " class='fa fa-info-circle fa-lg " + (String.IsNullOrEmpty(Generic.GetItemValue(item, "U_LOCAL")) ? "text-muted" : "text-primary") + " infADModalDesloc' title='Histórico de movimentações' style='padding-left:7px;'></a>";
                        selectValencias += infoValue;
                    }
                }
                item.rowItems.First(q => q.itemColumnName == "LOCAL_ATUAL").itemValue = selectValencias;

                #endregion

            }
        }



        internal bool UpdateRow(UserInfo uinfo, string itemRow, string deslocCod, string numCons)
        {
            DALDeslocacoes infDAL = new DALDeslocacoes();
            VwDoentesPresentes doente;
            if (!String.IsNullOrEmpty(numCons))
            {
               doente = infDAL.GetDeslocUserComNcons(itemRow.Split('_')[0], itemRow.Split('_')[1],numCons);
            }
            else
            {
                doente = infDAL.GetDeslocUser(itemRow.Split('_')[0], itemRow.Split('_')[1]);
            }           

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

            bool res=infDAL.InsertDoenteDesloc(tbl);
            return res;
           
        }


        public bool IsDoenteDesloc(string doente, string ncons)
        {
            DBDeslocacoesContext efInt = new DBDeslocacoesContext();
            VwDoentesPresentes d = efInt.vwDoentesPresentes.Where(q => q.DOENTE == doente && q.N_CONS == ncons).FirstOrDefault();

            if (d != null)
            {
                if (!String.IsNullOrEmpty(d.U_LOCAL) && d.U_LOCAL != d.COD_SERV)
                    return true;
                else
                    return false;
            }else 
                return false;
        }


        public bool HasProduct(string doente, string episodio)
        {
            DBDeslocacoesContext efInt = new DBDeslocacoesContext();
            VwDeslocProd d = efInt.vwDeslocProd.Where(q => q.DOENTE == doente && q.EPISODIO==episodio).FirstOrDefault();

            if (d != null)
            {
                return true;
            }
            else
                return false;
        }

    }
}