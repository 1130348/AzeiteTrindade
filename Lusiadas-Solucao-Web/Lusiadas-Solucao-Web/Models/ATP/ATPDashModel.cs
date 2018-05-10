using LDFHelper.Helpers;
using LusiadasDAL;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace LusiadasSolucaoWeb.Models
{
    public class ATPDashModel : LDFTableModel
    {



        #region ATPDashModel

        

        private string schema = "MEDICO";
        private string tabela = "V_DASH_DESLOC_ATP_V3";
        private string query = "*";
        private string condition = "";
        private string orderQ = "DT_CONS";

        public ATPDashModel()
        {

            pageSize = (int)HttpContext.Current.Session["Tabela"];  
            
            dbParams = new LDFTableDBParams(GetConnectionString(), schema, tabela, query, condition, orderQ, null, null);
            objType = typeof(VwDashboardATP);

            LDFTableHeaders();
            ReArrangeHeaders();
        }



        public ATPDashModel(int num)
        {

            pageSize = num;

            dbParams = new LDFTableDBParams(GetConnectionString(), schema, tabela, query, condition, orderQ, null, null);
            objType = typeof(VwDashboardATP);

            LDFTableHeaders();
            ReArrangeHeaders();
        }

        private string GetConnectionString()
        {
            string nome = "";
            try
            {
                if (String.IsNullOrEmpty(HttpContext.Current.Session[Constants.SS_LOCAL_CONN].ToString()))
                {
                    nome = "BDHPT";

                }
                else
                {
                    nome = HttpContext.Current.Session[Constants.SS_LOCAL_CONN].ToString();

                }

                return nome;

            }
            catch (Exception e)
            {
                nome = "BDHPT";
                return nome;
            }
        }

        private void ReArrangeHeaders()
        {
            foreach (LDFTableHeader item in list_headers)
            {

                if (item.headerID == "DT_CONS" || item.headerID == "IMAGEM" || item.headerID == "COR_TRIAGEM" || item.headerID == "DT_NOTA_MEDICA" || item.headerID == "LOCALIZACAO" || item.headerID == "MEDICO_NOME" || item.headerID == "DOENTE" || item.headerID == "ANALISES_OK" || item.headerID == "MEDICACAO_PRESCRITA_PCE" || item.headerID == "IMAGIOLOGIA_REQUESITADOS" || item.headerID == "ECG_REQUESITADOS" || item.headerID == "CEXTERNA_REQUISITADOS")
                item.headerStyle.Add("text-align", "center");
            }
        }


        public void LDFTableTreatData()
        {
            try
            {

                string nomeDescr = "", curdate = "", dtnasc = "";
                string medicacao_presc;
                int medAMB, medPCE, medAGL, mEDPGL;


                foreach (LDFTableRow item in list_rows)
                {

                    #region NumeroDoente
                    item.rowItems.First(q => q.itemColumnName == "DOENTE").itemValue = Generic.GetItemValue(item, "DOENTE");
                    #endregion

                    #region Nome Doente
                    dtnasc = Generic.GetItemValue(item, "DT_NASC");
                    if (!String.IsNullOrEmpty(dtnasc))
                        curdate = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dtnasc));
                    nomeDescr = "<div class='row' >";
                    nomeDescr += "<div class='col-xs-12' style='line-height: 120%;'>" + " <i class='fa fa-" + ((Generic.GetItemValue(item, "SEXO") == "F") ? "venus text-pink" : "mars text-blue") + "'></i>&nbsp;" + Generic.GetItemValue(item, "NOME") + " (" + GetBirthDate(Convert.ToDateTime(dtnasc)) + ")";

                    if (!String.IsNullOrEmpty(curdate))
                    {
                        nomeDescr += "<br>";
                        nomeDescr += "<b>Obs: </b>" + " " + Generic.GetItemValue(item, "OBS_CONS") + "</div>";

                    }
                    else
                    {
                        nomeDescr += "</div>";
                    }

                    nomeDescr += " </div>";
                    nomeDescr += "</div>";



                    item.rowItems.First(q => q.itemColumnName == "NOME").itemValue = nomeDescr;

                    #endregion


                    #region Bandage

                    string imagem = "";
                    if (Generic.GetItemValue(item, "COD_SERV").Equals("2154") || Generic.GetItemValue(item, "COD_SERV").Equals("2155") || Generic.GetItemValue(item, "COD_SERV").Equals("1154") || Generic.GetItemValue(item, "COD_SERV").Equals("1112"))
                    {
                        imagem = "<div class='row'>";
                        imagem += "<div class='col-xs-12'>" + " <img src ='/Content/img/bandage.png' height ='36' width ='36'> " + " </div>";
                        imagem += "</div>";
                    }
                    else
                    {
                        imagem = "<div class='row'>";
                        imagem += "<div class='col-xs-12'>" + "<i class='fa fa-minus text-gray' aria-hidden='true'></i>" + " </div>";
                        imagem += "</div>";
                    }
                    item.rowItems.First(q => q.itemColumnName == "IMAGEM").itemValue = imagem;

                    #endregion


                    #region NomeMedico
                    curdate = "";

                    if (Generic.GetItemValue(item, "MEDICO_NOME") != null)
                    {

                        item.rowItems.First(q => q.itemColumnName == "MEDICO_NOME").itemValue = Generic.GetItemValue(item, "MEDICO_NOME");

                    }
                    else
                    {

                        item.rowItems.First(q => q.itemColumnName == "MEDICO_NOME").itemValue = "A Designar Médico";

                    }
                    #endregion


                    #region DataConsulta

                    string admissao, admissao2;
                    string dataAdm = Generic.GetItemValue(item, "DT_CONS");

                    admissao = String.Format("{0:HH:mm}", Convert.ToDateTime(dataAdm));
                    admissao2 = String.Format("{0:dd/MM}", Convert.ToDateTime(dataAdm));
                    DateTime thisDay = DateTime.Today;
                    if (Convert.ToDateTime(dataAdm).DayOfYear + 1 == thisDay.DayOfYear)
                    {
                        admissao2 = "Ontem,";
                    }
                    else if (Convert.ToDateTime(dataAdm).DayOfYear == thisDay.DayOfYear)
                    {
                        admissao2 = "";
                    }



                    item.rowItems.First(q => q.itemColumnName == "DT_CONS").itemValue = "<div class='col-xs-12' style='line-height: 120%;'> <b>" + admissao2 + " " + admissao + "</b>" + "<br>" + "há " + HowLong(Convert.ToDateTime(dataAdm)) + "</div>";

                    #endregion


                    #region Triagem
                    string local = "";
                    string tria = Generic.GetItemValue(item, "COR_TRIAGEM");


                    switch (tria)
                    {
                        case "ESPERA":
                            local = "<div class='row'>";


                            dataAdm = Generic.GetItemValue(item, "DT_TRIAGEM");

                            admissao = String.Format("{0:HH:mm}", Convert.ToDateTime(Generic.GetItemValue(item, "DT_TRIAGEM")));
                            admissao2 = String.Format("{0:dd/MM}", Convert.ToDateTime(Generic.GetItemValue(item, "DT_TRIAGEM")));
                            thisDay = DateTime.Today;
                            if (Convert.ToDateTime(dataAdm).DayOfYear + 1 == thisDay.DayOfYear)
                            {
                                admissao2 = "Ontem,";
                            }
                            else if (Convert.ToDateTime(dataAdm).DayOfYear == thisDay.DayOfYear)
                            {
                                admissao2 = "";
                            }

                            local += "<div class='col-xs-12' style='line-height: 120%;'>" + "<i class='fa fa-circle text-blue' style='font-size:24px;margin-top:10px;'></i>";
                            if (!admissao2.Equals(""))
                            {
                                local += "<br>" + admissao2;
                            }

                            local += "<br>" + admissao + " </div>";
                            local += "</div>";

                            item.rowItems.First(q => q.itemColumnName == "COR_TRIAGEM").itemValue = local;
                            break;
                        case "BOX":
                            local = "<div class='row'>";
                            local += "<div class='col-xs-12' style='line-height: 120%;'>" + "<i class='fa fa-circle' style='color:#cccc00;font-size:24px;margin-top:10px;'></i>" +
                                "<br>" + String.Format("{0:HH:mm}", Convert.ToDateTime(Generic.GetItemValue(item, "DT_TRIAGEM"))) + " </div>";
                            local += "</div>";
                            item.rowItems.First(q => q.itemColumnName == "COR_TRIAGEM").itemValue = local;
                            break;
                        case "RS":
                            local = "<div class='row'>";
                            local += "<div class='col-xs-12' style='line-height: 120%;'>" + "<i class='fa fa-circle text-orange' style='font-size:24px;margin-top:10px;'></i>" +
                                "<br>" + String.Format("{0:HH:mm}", Convert.ToDateTime(Generic.GetItemValue(item, "DT_TRIAGEM"))) + " </div>";
                            local += "</div>";
                            item.rowItems.First(q => q.itemColumnName == "COR_TRIAGEM").itemValue = local;
                            break;
                        case "ESPERAGP":
                            local = "<div class='row'>";
                            local += "<div class='col-xs-12' style='line-height: 120%;'>" + "<i class='fa fa-circle text-green' style='font-size:24px;margin-top:10px;'></i>" +
                                "<br>" + String.Format("{0:HH:mm}", Convert.ToDateTime(Generic.GetItemValue(item, "DT_TRIAGEM"))) + " </div>";
                            local += "</div>";
                            item.rowItems.First(q => q.itemColumnName == "COR_TRIAGEM").itemValue = local;
                            break;
                        default:
                            local = "<div class='row'>";
                            local += "<div class='col-xs-12' style='line-height: 120%;'>" + "<i class='fa fa-pause text-grey' aria-hidden='true'></i>" + " </div>";
                            local += "</div>";
                            item.rowItems.First(q => q.itemColumnName == "COR_TRIAGEM").itemValue = local;
                            break;
                    }

                    #endregion


                    #region Local
                    if (!Generic.GetItemValue(item, "LOCALIZACAO").IsEmpty())
                    {
                        item.rowItems.First(q => q.itemColumnName == "LOCALIZACAO").itemValue = Generic.GetItemValue(item, "LOCALIZACAO");
                    }
                    else
                    {
                        local = "<div class='row'>";
                        local += "<div class='col-xs-12'>" + "<i class='fa fa-minus text-gray' aria-hidden='true'></i>" + " </div>";
                        local += "</div>";
                        item.rowItems.First(q => q.itemColumnName == "LOCALIZACAO").itemValue = local;
                    }
                    #endregion

                    #region NotaMedica

                    string nota = "";
                    string dtnota = Generic.GetItemValue(item, "DT_NOTA_MEDICA");


                    dataAdm = Generic.GetItemValue(item, "DT_NOTA_MEDICA");

                    try
                    {
                        admissao = String.Format("{0:HH:mm}", Convert.ToDateTime(Generic.GetItemValue(item, "DT_NOTA_MEDICA")));
                        admissao2 = String.Format("{0:dd/MM}", Convert.ToDateTime(Generic.GetItemValue(item, "DT_NOTA_MEDICA")));
                        thisDay = DateTime.Today;
                        if (Convert.ToDateTime(dataAdm).DayOfYear + 1 == thisDay.DayOfYear)
                        {
                            admissao2 = "Ontem,";
                        }
                        else if (Convert.ToDateTime(dataAdm).DayOfYear == thisDay.DayOfYear)
                        {
                            admissao2 = "";
                        }

                        if (!String.IsNullOrEmpty(dtnota))
                        {
                            string dtnota2 = String.Format("{0:HH:mm}", Convert.ToDateTime(dtnota));
                            nota = "<div class='row'>";
                            nota += "<div class='col-xs-12'>" + "<i class='fa fa-check text-green' aria-hidden='true'></i>"
                            + "<br>" + admissao2 + "&nbsp;"
                            + admissao + " </div>";
                            nota += "</div>";
                            item.rowItems.First(q => q.itemColumnName == "DT_NOTA_MEDICA").itemValue = nota;

                        }
                        else
                        {
                            nota = "<div class='row'>";
                            nota += "<div class='col-xs-12'>" + "<i class='fa fa-pause text-orange' aria-hidden='true'></i>" + " </div>";
                            nota += "</div>";
                            item.rowItems.First(q => q.itemColumnName == "DT_NOTA_MEDICA").itemValue = nota;
                        }
                    }
                    catch (Exception e)
                    {
                        nota = "<div class='row'>";
                        nota += "<div class='col-xs-12'>" + "<i class='fa fa-pause text-orange' aria-hidden='true'></i>" + " </div>";
                        nota += "</div>";
                        item.rowItems.First(q => q.itemColumnName == "DT_NOTA_MEDICA").itemValue = nota;

                    }

                    #endregion

                    #region Analises

                    string anal = "";
                    int analisesEntregues = Convert.ToInt32(Generic.GetItemValue(item, "ENTREGUE_LAB"));
                    int analisesRealizadas = Convert.ToInt32(Generic.GetItemValue(item, "ANALISES_OK"));

                    if (!Generic.GetItemValue(item, "ANALISES_SOLICITADAS").Contains('N'))
                    {

                        int analisesPedidas = Convert.ToInt32(Generic.GetItemValue(item, "ANALISES_SOLICITADAS"));

                        if (analisesPedidas == 0)
                        {
                            anal = "<div class='row'>";
                            anal += "<div class='col-xs-12'>" + "<i class='fa fa-minus text-gray' aria-hidden='true'></i>" + "</div>";
                            anal += "</div>";
                            item.rowItems.First(q => q.itemColumnName == "ANALISES_OK").itemValue = anal;
                        }
                        else
                        {

                            if (analisesRealizadas >= analisesPedidas)
                            {

                                anal = "<div class='row'>";
                                anal += "<div class='col-xs-12'>" + "<i class='fa fa-check text-green' aria-hidden='true'></i>" + "<br>" + analisesRealizadas + "/" + analisesPedidas + "</div>";
                                anal += "</div>";
                                item.rowItems.First(q => q.itemColumnName == "ANALISES_OK").itemValue = anal;

                            }
                            else if (analisesEntregues < analisesPedidas)
                            {

                                anal = "<div class='row'>";
                                anal += "<div class='col-xs-12'>" + "<i class='fa fa-pause text-orange' aria-hidden='true'></i>" + "<br>" + analisesRealizadas + "/" + analisesPedidas + "</div>";
                                anal += "</div>";
                                item.rowItems.First(q => q.itemColumnName == "ANALISES_OK").itemValue = anal;


                            }
                            else
                            {
                                anal = "<div class='row'>";
                                anal += "<div class='col-xs-12'>" + "<i class='fa fa-play' aria-hidden='true' style='color:#cccc00;'></i>" + "<br>" + analisesRealizadas + "/" + analisesPedidas + "</div>";
                                anal += "</div>";
                                item.rowItems.First(q => q.itemColumnName == "ANALISES_OK").itemValue = anal;
                            }
                        }

                    }
                    else
                    {

                        anal = "<div class='row'>";
                        anal += "<div class='col-xs-12'>" + "<i class='fa fa-minus text-gray' aria-hidden='true'></i>" + "</div>";
                        anal += "</div>";
                        item.rowItems.First(q => q.itemColumnName == "ANALISES_OK").itemValue = anal;

                    }

                    #endregion



                    #region Medicacao
                    medicacao_presc = Generic.GetItemValue(item, "MEDICACAO_PRESCRITA_PCE");

                    medAMB = Convert.ToInt32(Generic.GetItemValue(item, "MEDICACAO_ADMINISTRADA_AMB"));
                    medPCE = Convert.ToInt32(Generic.GetItemValue(item, "MEDICACAO_PRESCRITA_PCE"));
                    medAGL = Convert.ToInt32(Generic.GetItemValue(item, "MEDICACAO_ADMINISTRADA_GLINTT"));
                    mEDPGL = Convert.ToInt32(Generic.GetItemValue(item, "MEDICACAO_PRESCRITA_GLINTT"));
                    if (medPCE + mEDPGL == 0)
                    {
                        nota = "<div class='row'>";
                        nota += "<div class='col-xs-12'>" + "<i class='fa fa-minus text-gray' aria-hidden='true'></i>" + "</div>";
                        nota += "</div>";
                        item.rowItems.First(q => q.itemColumnName == "MEDICACAO_PRESCRITA_PCE").itemValue = nota;


                    }
                    else
                    {

                        if (medAMB + medAGL >= medPCE + mEDPGL)
                        {

                            nota = "<div class='row'>";
                            nota += "<div class='col-xs-12'>" + "<i class='fa fa-check text-green' aria-hidden='true'></i>" + "<br>" + (medAMB + medAGL) + "/" + (medPCE + mEDPGL) + " </div>";
                            nota += "</div>";
                            item.rowItems.First(q => q.itemColumnName == "MEDICACAO_PRESCRITA_PCE").itemValue = nota;

                        }
                        else
                        {
                            nota = "<div class='row'>";
                            nota += "<div class='col-xs-12'>" + "<i class='fa fa-pause text-orange' aria-hidden='true'></i>" + "<br>" + (medAMB + medAGL) + "/" + (medPCE + mEDPGL) + " </div>";
                            nota += "</div>";
                            item.rowItems.First(q => q.itemColumnName == "MEDICACAO_PRESCRITA_PCE").itemValue = nota;
                        }

                    }

                    #endregion




                    #region Imagiologia

                    if (Convert.ToInt32(Generic.GetItemValue(item, "IMAGIOLOGIA_REQUESITADOS")) <= 0)
                    {

                        anal = "<div class='row'>";
                        anal += "<div class='col-xs-12'>" + "<i class='fa fa-minus text-gray' aria-hidden='true'></i>" + "</div>";
                        anal += "</div>";
                        item.rowItems.First(q => q.itemColumnName == "IMAGIOLOGIA_REQUESITADOS").itemValue = anal;

                    }
                    else
                    {


                        string loc = Generic.GetItemValue(item, "IMAG_GRUPO_ATO");
                        int imReal = Convert.ToInt32(Generic.GetItemValue(item, "IMAGIOLOGIA_REALIZADOS"));
                        int imPedi = Convert.ToInt32(Generic.GetItemValue(item, "IMAGIOLOGIA_REQUESITADOS"));
                        int imDesloc = Convert.ToInt32(Generic.GetItemValue(item, "IMAG_DESLOC"));
                        if (imReal >= imPedi)
                        {
                            nota = "<div class='container'>";

                            if (loc.Contains(';'))
                            {


                                List<string> names = loc.Split(';').ToList<string>();
                                //names.Reverse();


                                foreach (string name in names)
                                {
                                    if (name.ToUpper().Contains("RADIO"))
                                    {
                                        nota += "<div class='block'>";
                                        nota += "<img src='/Content/img/xray.png' height='36' width='36'>";
                                        nota += "</div>";

                                    }

                                    if (name.ToUpper().Contains("ECO"))
                                    {
                                        nota += "<div class='block'>";
                                        nota += "<img src='/Content/img/ultra.png' height='36' width='36'>";
                                        nota += "</div>";

                                    }

                                    if (name.ToUpper().Contains("AXIAL"))
                                    {
                                        nota += "<div class='block'>";
                                        nota += "<img src='/Content/img/xcat.png' height='36' width='36'>";
                                        nota += "</div>";

                                    }

                                    if (name.ToUpper().Contains("RESSO"))
                                    {
                                        nota += "<div class='block'>";
                                        nota += "<img src='/Content/img/rm.png' height='36' width='36'>";
                                        nota += "</div>";

                                    }



                                }





                                nota += "<div class='block' style='line-height:150%;' >";
                                nota += "<i class='fa fa-check text-green' aria-hidden='true'></i>";
                                nota += " <br> " + (imReal) + "/" + (imPedi);
                                nota += "</div></div>";

                                item.rowItems.First(q => q.itemColumnName == "IMAGIOLOGIA_REQUESITADOS").itemValue = nota;


                            }
                            else
                            {

                                if (loc.ToUpper().Contains("RADIO"))
                                {
                                    nota += "<div class='block'>";
                                    nota += "<img src='/Content/img/xray.png' height='36' width='36'><br>";
                                    nota += "</div><div class='block'>";
                                    nota += "<i class='fa fa-check text-green' aria-hidden='true'></i>" + "<br>" + (imReal) + "/" + (imPedi) + " </div>";


                                }

                                if (loc.ToUpper().Contains("ECO"))
                                {
                                    nota += "<div class='block'>";
                                    nota += "<img src='/Content/img/ultra.png' height='36' width='36'><br>";
                                    nota += "</div><div class='block'>";
                                    nota += "<i class='fa fa-check text-green' aria-hidden='true'></i>" + "<br>" + (imReal) + "/" + (imPedi) + " </div>";

                                }

                                if (loc.ToUpper().Contains("AXIAL"))
                                {
                                    nota += "<div class='block'>";
                                    nota += "<img src='/Content/img/xcat.png' height='36' width='36'><br>";
                                    nota += "</div><div class='block'>";
                                    nota += "<i class='fa fa-check text-green' aria-hidden='true'></i>" + "<br>" + (imReal) + "/" + (imPedi) + " </div>";

                                }

                                if (loc.ToUpper().Contains("RESSO"))
                                {
                                    nota += "<div class='block'>";
                                    nota += "<img src='/Content/img/rm.png' height='36' width='36'><br>";
                                    nota += "</div><div class='block'>";
                                    nota += "<i class='fa fa-check text-green' aria-hidden='true'></i>" + "<br>" + (imReal) + "/" + (imPedi) + " </div>";

                                }




                                nota += "</div>";
                                item.rowItems.First(q => q.itemColumnName == "IMAGIOLOGIA_REQUESITADOS").itemValue = nota;


                            }


                        }
                        else if (imDesloc < 1)
                        {

                            nota = "<div class='container'>";

                            if (loc.Contains(';'))
                            {


                                List<string> names = loc.Split(';').ToList<string>();
                                //names.Reverse();


                                foreach (string name in names)
                                {
                                    if (name.ToUpper().Contains("RADIO"))
                                    {
                                        nota += "<div class='block'>";
                                        nota += "<img src='/Content/img/xray.png' height='36' width='36'>";
                                        nota += "</div>";

                                    }

                                    if (name.ToUpper().Contains("ECO"))
                                    {
                                        nota += "<div class='block'>";
                                        nota += "<img src='/Content/img/ultra.png' height='36' width='36'>";
                                        nota += "</div>";

                                    }

                                    if (name.ToUpper().Contains("AXIAL"))
                                    {
                                        nota += "<div class='block'>";
                                        nota += "<img src='/Content/img/xcat.png' height='36' width='36'>";
                                        nota += "</div>";

                                    }

                                    if (name.ToUpper().Contains("RESSO"))
                                    {
                                        nota += "<div class='block'>";
                                        nota += "<img src='/Content/img/rm.png' height='36' width='36'>";
                                        nota += "</div>";

                                    }




                                }


                                nota += "<div class='block' style='line-height:150%;'>";
                                nota += "<i class='fa fa-pause text-orange' aria-hidden='true' ></i>";

                                nota += " <br> " + (imReal) + " / " + (imPedi);
                                nota += "</div></div>";

                                item.rowItems.First(q => q.itemColumnName == "IMAGIOLOGIA_REQUESITADOS").itemValue = nota;


                            }
                            else
                            {

                                nota = "<div class='container'>";
                                if (loc.ToUpper().Contains("RADIO"))
                                {
                                    nota += "<div class='block'>";
                                    nota += "<img src='/Content/img/xray.png' height='36' width='36'><br>";
                                    nota += "</div><div class='block'>" + "<i class='fa fa-pause text-orange' aria-hidden='true' ></i>" + "<br>" + (imReal) + "/" + (imPedi) + " </div>";


                                }

                                if (loc.ToUpper().Contains("ECO"))
                                {
                                    nota += "<div class='block'>";
                                    nota += "<img src='/Content/img/ultra.png' height='36' width='36'><br>";
                                    nota += "</div><div class='block'>" + "<i class='fa fa-pause text-orange' aria-hidden='true' ></i>" + "<br>" + (imReal) + "/" + (imPedi) + " </div>";

                                }

                                if (loc.ToUpper().Contains("AXIAL"))
                                {
                                    nota += "<div class='block'>";
                                    nota += "<img src='/Content/img/xcat.png' height='36' width='36'><br>";
                                    nota += "</div><div class='block'>" + "<i class='fa fa-pause text-orange' aria-hidden='true'></i>" + "<br>" + (imReal) + "/" + (imPedi) + " </div>";

                                }

                                if (loc.ToUpper().Contains("RESSO"))
                                {
                                    nota += "<div class='block'>";
                                    nota += "<img src='/Content/img/rm.png' height='36' width='36'><br>";
                                    nota += "</div><div class='block'>" + "<i class='fa fa-pause text-orange' aria-hidden='true' ></i>" + "<br>" + (imReal) + "/" + (imPedi) + " </div>";

                                }


                                nota += "</div>";
                                item.rowItems.First(q => q.itemColumnName == "IMAGIOLOGIA_REQUESITADOS").itemValue = nota;


                            }



                        }
                        else
                        {


                            nota = "<div class='container'>";

                            if (loc.Contains(';'))
                            {


                                List<string> names = loc.Split(';').ToList<string>();
                                //names.Reverse();


                                foreach (string name in names)
                                {
                                    if (name.ToUpper().Contains("RADIO"))
                                    {
                                        nota += "<div class='block'>";
                                        nota += "<img src='/Content/img/xray.png' height='36' width='36'>";
                                        nota += "</div>";

                                    }

                                    if (name.ToUpper().Contains("ECO"))
                                    {
                                        nota += "<div class='block'>";
                                        nota += "<img src='/Content/img/ultra.png' height='36' width='36'>";
                                        nota += "</div>";

                                    }

                                    if (name.ToUpper().Contains("AXIAL"))
                                    {
                                        nota += "<div class='block'>";
                                        nota += "<img src='/Content/img/xcat.png' height='36' width='36'>";
                                        nota += "</div>";

                                    }

                                    if (name.ToUpper().Contains("RESSO"))
                                    {
                                        nota += "<div class='block'>";
                                        nota += "<img src='/Content/img/rm.png' height='36' width='36'>";
                                        nota += "</div>";

                                    }


                                }


                                nota += "<div class='block' style='line-height:150%;'>";
                                nota += "<i class='fa fa-play' aria-hidden='true' style='color:#cccc00;'></i>";
                                nota += "<br>" + (imReal) + "/" + (imPedi);
                                nota += "</div></div>";

                                item.rowItems.First(q => q.itemColumnName == "IMAGIOLOGIA_REQUESITADOS").itemValue = nota;


                            }
                            else
                            {


                                nota = "<div class='container'>";
                                if (loc.ToUpper().Contains("RADIO"))
                                {
                                    nota += "<div class='block'>";
                                    nota += "<img src='/Content/img/xray.png' height='36' width='36'><br>";
                                    nota += "</div><div class='block'>" + "<i class='fa fa-play' aria-hidden='true' style='color:#cccc00;'></i>" + "<br>" + (imReal) + "/" + (imPedi) + " </div>";


                                }

                                if (loc.ToUpper().Contains("ECO"))
                                {
                                    nota += "<div class='block'>";
                                    nota += "<img src='/Content/img/ultra.png' height='36' width='36'><br>";
                                    nota += "</div><div class='block'>" + "<i class='fa fa-play' aria-hidden='true' style='color:#cccc00;'></i>" + "<br>" + (imReal) + "/" + (imPedi) + " </div>";

                                }

                                if (loc.ToUpper().Contains("AXIAL"))
                                {
                                    nota += "<div class='block'>";
                                    nota += "<img src='/Content/img/xcat.png' height='36' width='36'><br>";
                                    nota += "</div><div class='block'>" + "<i class='fa fa-play' aria-hidden='true' style='color:#cccc00;'></i>" + "<br>" + (imReal) + "/" + (imPedi) + " </div>";

                                }

                                if (loc.ToUpper().Contains("RESSO"))
                                {
                                    nota += "<div class='block'>";
                                    nota += "<img src='/Content/img/rm.png' height='36' width='36'><br>";
                                    nota += "</div><div class='block'>" + "<i class='fa fa-play' aria-hidden='true' style='color:#cccc00;;'></i>" + "<br>" + (imReal) + "/" + (imPedi) + " </div>";

                                }


                                nota += "</div>";
                                item.rowItems.First(q => q.itemColumnName == "IMAGIOLOGIA_REQUESITADOS").itemValue = nota;
                            }

                        }
                    }

                    #endregion


                    #region EGC

                    if (Convert.ToInt32(Generic.GetItemValue(item, "ECG_REQUESITADOS")) <= 0)
                    {

                        anal = "<div class='row'>";
                        anal += "<div class='col-xs-12'>" + "<i class='fa fa-minus text-gray' aria-hidden='true'></i>" + "</div>";
                        anal += "</div>";
                        item.rowItems.First(q => q.itemColumnName == "ECG_REQUESITADOS").itemValue = anal;

                    }
                    else
                    {


                        if (Convert.ToInt32(Generic.GetItemValue(item, "ECG_REALIZADOS")) >= Convert.ToInt32(Generic.GetItemValue(item, "ECG_REQUESITADOS")))
                        {

                            nota = "<div class='row'>";
                            nota += "<div class='col-xs-12'>" + "<i class='fa fa-check text-green' aria-hidden='true'></i>" + "<br>" + Generic.GetItemValue(item, "ECG_REALIZADOS") + "/" + Generic.GetItemValue(item, "ECG_REQUESITADOS") + " </div>";
                            nota += "</div>";
                            item.rowItems.First(q => q.itemColumnName == "ECG_REQUESITADOS").itemValue = nota;

                        }
                        else
                        {
                            nota = "<div class='row'>";
                            nota += "<div class='col-xs-12'>" + "<i class='fa fa-pause text-orange' aria-hidden='true'></i>" + "<br>" + Generic.GetItemValue(item, "ECG_REALIZADOS") + "/" + Generic.GetItemValue(item, "ECG_REQUESITADOS") + " </div>";
                            nota += "</div>";
                            item.rowItems.First(q => q.itemColumnName == "ECG_REQUESITADOS").itemValue = nota;
                        }

                    }

                    #endregion


                    #region ConsultaExterna

                    if (String.IsNullOrEmpty(Generic.GetItemValue(item, "CEXTERNA_REQUISITADOS")))
                    {

                        anal = "<div class='row'>";
                        anal += "<div class='col-xs-12'>" + "<i class='fa fa-minus text-gray' aria-hidden='true'></i>" + "</div>";
                        anal += "</div>";
                        item.rowItems.First(q => q.itemColumnName == "CEXTERNA_REQUISITADOS").itemValue = anal;

                    }
                    else
                    {


                        if (Generic.GetItemValue(item, "CEXTERNA_REQUISITADOS").Equals(Generic.GetItemValue(item, "CEXTERNA_REALIZADOS")))
                        {
                            nota = "<div class='row'>";
                            nota += "<div class='col-xs-12'>" + "<font color='#00cc00'>" + Generic.GetItemValue(item, "CEXTERNA_REQUISITADOS") + "</font>" + " </div>";
                            nota += "</div>";
                            item.rowItems.First(q => q.itemColumnName == "CEXTERNA_REQUISITADOS").itemValue = nota;
                        }
                        else if (Generic.GetItemValue(item, "CEXTERNA_REQUISITADOS").Equals(Generic.GetItemValue(item, "CEXTERNA_DESLOC")))
                        {
                            nota = "<div class='row'>";
                            nota += "<div class='col-xs-12'>" + "<font color='#e6e600'>" + Generic.GetItemValue(item, "CEXTERNA_REQUISITADOS") + "</font>" + " </div>";
                            nota += "</div>";
                            item.rowItems.First(q => q.itemColumnName == "CEXTERNA_REQUISITADOS").itemValue = nota;

                        }
                        else
                        {

                            nota = "<div class='row'>";
                            nota += "<div class='col-xs-12'>" + "<font color='red'>" + Generic.GetItemValue(item, "CEXTERNA_REQUISITADOS") + "</font>" + " </div>";
                            nota += "</div>";
                            item.rowItems.First(q => q.itemColumnName == "CEXTERNA_REQUISITADOS").itemValue = nota;

                        }

                    }

                    #endregion


                }

            }
            catch (Exception e)
            {

            }
        }



        #endregion

        #region Aux Methods

        private string GetBirthDate(Nullable<DateTime> dt)
        {
            string res = "";
            DateTime curDate = DateTime.Now; 

            if (dt != null)
            {

                var today = DateTime.Today;
                // Calculate the age.
                var age = today.Year - dt.Value.Year;
                // Go back to the year the person was born in case of a leap year
                if (dt > today.AddYears(-age)) age--;

                if (age > 0)
                {
                    return age.ToString() + " anos";
                }
                else
                {
                    curDate = curDate.AddTicks(dt.Value.Ticks * -1);
                    if (curDate.Year > 2)
                        res = curDate.Year + " anos";
                    else
                    {
                        res = curDate.Month-1 + " meses";
                    }
                    return res;
                }

            }

            return "Erro";
        }

        private string HowLong(DateTime data)
        {

            DateTime now = DateTime.Now;
            TimeSpan span = now.Subtract(data);

            if (span.Minutes<1)
            {
                return (int)span.TotalHours + "h ";
            }else if (span.TotalHours<1)
            {
                return (int)span.Minutes + "m";
            }
            else
            {
                return (int)span.TotalHours + "h " + span.Minutes + "m";
            }
          
            

        }

        private bool IsDoenteMoved(string doente, string ncons)
        {
            DeslocacoesModel desloc = new DeslocacoesModel();
            return desloc.IsDoenteDesloc(doente, ncons);
        }

        private bool DoenteHasProduct(string doente, string episodio)
        {
            DeslocacoesModel desloc = new DeslocacoesModel();
            return desloc.HasProduct(doente, episodio);
        }

        #endregion

    }
}
