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
    public class ATPModel : LDFTableModel
    {

        #region ATPModel

        public ATPModel()
        {
            pageSize    = Convert.ToInt32(ConfigurationManager.AppSettings["TableRowsPerPage"]);
            dbParams    = new LDFTableDBParams("DBSolucaoWeb", "MEDICO", "V_DASHBOARD_ATP", "*", "", "", null, null);
            objType     = typeof(VwDashboardATP);

            LDFTableHeaders();
            ReArrangeHeaders();
        }

        private void ReArrangeHeaders()
        {
           foreach(LDFTableHeader item in list_headers)
           {
               if (item.headerName == "Triagem" || item.headerName == "Nota Médica" || item.headerName == "Box" || item.headerName == "Cadeirão" || item.headerName == "Penso" || item.headerName == "Análises" || item.headerName == "Medicação" || item.headerName == "Imagiologia" || item.headerName == "ECG" || item.headerName == "Consulta Externa")
                   item.headerStyle.Add("text-align", "center");
           }
        }

        public void LDFTableTreatData()
        {
            string nomeDescr = "", field = "", curdate = "", dtnasc = "", hora = "";
            string red = "text-red", green = "text-green", yellow = "text-yellow", gray = "text-gray";

            foreach (LDFTableRow item in list_rows)
            {

                item.rowItems.First(q => q.itemColumnName == "T_DOENTE").itemValue = Generic.GetItemValue(item, "T_DOENTE") + Generic.GetItemValue(item, "DOENTE");

                dtnasc = Generic.GetItemValue(item, "DT_NASC");
                if (!String.IsNullOrEmpty(dtnasc))
                    curdate = String.Format("{0:dd/MM/yyyy}", dtnasc);
                nomeDescr = "<div class='row'>";
                nomeDescr += "<div class='col-xs-12'>" + Generic.GetItemValue(item, "NOME") + " <i class='fa fa-" + ((Generic.GetItemValue(item, "SEXO") == "F") ? "venus text-pink" : "mars text-blue") + "'></i></div>";
                nomeDescr += "</div>";
                if (!String.IsNullOrEmpty(curdate))
                {
                    nomeDescr += "<div class='row'>";
                    nomeDescr += "<div class='col-xs-12'>" + curdate + " (" + GetBirthDate(Convert.ToDateTime(dtnasc)) + ")</div>";
                    nomeDescr += "</div>";
                }
                item.rowItems.First(q => q.itemColumnName == "NOME").itemValue = nomeDescr;

                curdate = "";
                hora = Generic.GetItemValue(item, "HR_CONS");
                if (!String.IsNullOrEmpty(hora))
                    curdate = String.Format("{0:HH:mm:ss}", Convert.ToDateTime(hora));

                item.rowItems.First(q => q.itemColumnName == "HR_CONS").itemValue = curdate;


                string triagem = Generic.GetItemValue(item, "TRIAGEM");
                string nota_medica = Generic.GetItemValue(item, "NOTA_MEDICA");
                string box_aplicavel = Generic.GetItemValue(item, "BOX_APLICAVEL");
                string penso_aplicavel = Generic.GetItemValue(item, "PENSO_APLICAVEL");
                string analises_ok = Generic.GetItemValue(item, "ANALISES_OK");
                string medicacao_presc = Generic.GetItemValue(item, "MEDICACAO_PRESCRITA_PCE");

                int medAMB = Convert.ToInt32(Generic.GetItemValue(item, "MEDICACAO_ADMINISTRADA_AMB"));
                int medPCE = Convert.ToInt32(Generic.GetItemValue(item, "MEDICACAO_PRESCRITA_PCE"));
                int medAGL = Convert.ToInt32(Generic.GetItemValue(item, "MEDICACAO_ADMINISTRADA_GLINTT"));
                int mEDPGL = Convert.ToInt32(Generic.GetItemValue(item, "MEDICACAO_PRESCRITA_GLINTT"));

                item.rowItems.First(q => q.itemColumnName == "TRIAGEM").itemValue = "<i class='fa fa-circle lg " + ((triagem == "S") ? green : red) + "'></i>";
                item.rowItems.First(q => q.itemColumnName == "NOTA_MEDICA").itemValue = "<i class='fa fa-circle lg " + ((nota_medica == "S") ? green : red) + "'></i>";

                field = green;
                if (Generic.GetItemValue(item, "BOX_APLICAVEL") == "N")
                    field = gray;
                else if (Generic.GetItemValue(item, "BOX_PRESENTE") == "N")
                    field = red;
                item.rowItems.First(q => q.itemColumnName == "BOX_APLICAVEL").itemValue = "<i class='fa fa-circle lg " + field + "'></i>";

                field = green;
                if (Generic.GetItemValue(item, "PENSO_APLICAVEL") == "N")
                    field = gray;
                else if (Generic.GetItemValue(item, "PENSO_REGISTO") == "N")
                    field = red;
                item.rowItems.First(q => q.itemColumnName == "PENSO_APLICAVEL").itemValue = "<i class='fa fa-circle lg " + field + "'></i>";

                field = green;
                if (Generic.GetItemValue(item, "ANALISES_SOLICITADAS") == "N")
                    field = gray;
                else if (Generic.GetItemValue(item, "ANALISES_SOLICITADAS") == "S" && Generic.GetItemValue(item, "COLHEITA_OK") == "N" && analises_ok == "N")
                    field = yellow;
                else if (Generic.GetItemValue(item, "ANALISES_SOLICITADAS") == "S" && Generic.GetItemValue(item, "COLHEITA_OK") == "S" && analises_ok == "N")
                    field = red;
                item.rowItems.First(q => q.itemColumnName == "ANALISES_OK").itemValue = "<i class='fa fa-circle lg " + field + "'></i>";

                field = green;
                if (Generic.GetItemValue(item, "MEDICACAO_PRESCRITA_PCE") == "0" && Generic.GetItemValue(item, "MEDICACAO_PRESCRITA_GLINTT") == "0")
                    field = gray;
                else if (medAMB < medPCE || medAGL < mEDPGL)
                    field = red;
                item.rowItems.First(q => q.itemColumnName == "MEDICACAO_PRESCRITA_PCE").itemValue = "<i class='fa fa-circle lg " + field + "'></i>";
                
                field = green;
                if (Generic.GetItemValue(item, "IMAGIOLOGIA_REQUESITADOS") == "0")
                    field = gray;
                item.rowItems.First(q => q.itemColumnName == "IMAGIOLOGIA_REQUESITADOS").itemValue = "<i class='fa fa-circle lg " + field + "'></i>";

                field = green;
                if (Generic.GetItemValue(item, "ECG_REQUESITADOS") == "0")
                    field = gray;
                else if (Convert.ToInt32(Generic.GetItemValue(item, "ECG_REQUESITADOS")) <= Convert.ToInt32(Generic.GetItemValue(item, "ECG_REALIZADOS")))
                    field = red;
                item.rowItems.First(q => q.itemColumnName == "ECG_REQUESITADOS").itemValue = "<i class='fa fa-circle lg " + field + "'></i>";

                field = green;
                if (Generic.GetItemValue(item, "CEXTERNA_REQUISITADOS") == "0")
                    field = gray;
                else if (Convert.ToInt32(Generic.GetItemValue(item, "CEXTERNA_REALIZADOS")) < Convert.ToInt32(Generic.GetItemValue(item, "CEXTERNA_REQUISITADOS")))
                    field = red;
                item.rowItems.First(q => q.itemColumnName == "CEXTERNA_REQUISITADOS").itemValue = "<i class='fa fa-circle lg " + field + "'></i>";
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