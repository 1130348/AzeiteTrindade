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
               if (item.headerID == "TRIAGEM" || item.headerID == "NOTA_MEDICA" || item.headerID == "BOX_APLICAVEL" || item.headerID == "CADEIRAO_PRESENTE" || item.headerID == "PENSO_APLICAVEL" || item.headerID == "ANALISES_OK" || item.headerID == "MEDICACAO_PRESCRITA_PCE" || item.headerID == "IMAGIOLOGIA_REQUESITADOS" || item.headerID == "ECG_REQUESITADOS" || item.headerID == "CEXTERNA_REQUISITADOS")
                   item.headerStyle.Add("text-align", "center");
           }
        }

        public void LDFTableTreatData()
        {
            string nomeDescr = "", field = "", curdate = "", dtnasc = "", hora = "";
            string red = "text-red", green = "text-green", yellow = "text-yellow", gray = "text-gray";
            string triagem, nota_medica, box_aplicavel, penso_aplicavel, analises_ok, medicacao_presc;
            int medAMB, medPCE, medAGL, mEDPGL;


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


                triagem         = Generic.GetItemValue(item, "TRIAGEM");
                nota_medica     = Generic.GetItemValue(item, "NOTA_MEDICA");
                box_aplicavel   = Generic.GetItemValue(item, "BOX_APLICAVEL");
                penso_aplicavel = Generic.GetItemValue(item, "PENSO_APLICAVEL");
                analises_ok     = Generic.GetItemValue(item, "ANALISES_OK");
                medicacao_presc = Generic.GetItemValue(item, "MEDICACAO_PRESCRITA_PCE");

                medAMB = Convert.ToInt32(Generic.GetItemValue(item, "MEDICACAO_ADMINISTRADA_AMB"));
                medPCE = Convert.ToInt32(Generic.GetItemValue(item, "MEDICACAO_PRESCRITA_PCE"));
                medAGL = Convert.ToInt32(Generic.GetItemValue(item, "MEDICACAO_ADMINISTRADA_GLINTT"));
                mEDPGL = Convert.ToInt32(Generic.GetItemValue(item, "MEDICACAO_PRESCRITA_GLINTT"));

                GetFieldCircle(item, "TRIAGEM", ((triagem == "S") ? green : red));
                GetFieldCircle(item, "NOTA_MEDICA", ((nota_medica == "S") ? green : red));
                GetFieldCircle(item, "CADEIRAO_PRESENTE", gray);

                field = green;
                if (Generic.GetItemValue(item, "BOX_APLICAVEL") == "N")
                    field = gray;
                else if (Generic.GetItemValue(item, "BOX_PRESENTE") == "N")
                    field = red;
                GetFieldCircle(item, "BOX_APLICAVEL", field);

                field = green;
                if (Generic.GetItemValue(item, "PENSO_APLICAVEL") == "N")
                    field = gray;
                else if (Generic.GetItemValue(item, "PENSO_REGISTO") == "N")
                    field = red;
                GetFieldCircle(item, "PENSO_APLICAVEL", field);

                field = green;
                if (Generic.GetItemValue(item, "ANALISES_SOLICITADAS") == "N")
                    field = gray;
                else if (Generic.GetItemValue(item, "ANALISES_SOLICITADAS") == "S" && Generic.GetItemValue(item, "COLHEITA_OK") == "N" && analises_ok == "N")
                    field = yellow;
                else if (Generic.GetItemValue(item, "ANALISES_SOLICITADAS") == "S" && Generic.GetItemValue(item, "COLHEITA_OK") == "S" && analises_ok == "N")
                    field = red;
                GetFieldCircle(item, "ANALISES_OK", field);

                

                field = green;
                if (Generic.GetItemValue(item, "MEDICACAO_PRESCRITA_PCE") == "0" && Generic.GetItemValue(item, "MEDICACAO_PRESCRITA_GLINTT") == "0")
                    field = gray;
                else if (medAMB < medPCE || medAGL < mEDPGL)
                    field = red;
                GetFieldCircle(item, "MEDICACAO_PRESCRITA_PCE", field);
                
                field = green;
                if (Generic.GetItemValue(item, "IMAGIOLOGIA_REQUESITADOS") == "0")
                    field = gray;
                GetFieldCircle(item, "IMAGIOLOGIA_REQUESITADOS", field);

                field = green;
                if (Generic.GetItemValue(item, "ECG_REQUESITADOS") == "0")
                    field = gray;
                else if (Convert.ToInt32(Generic.GetItemValue(item, "ECG_REQUESITADOS")) <= Convert.ToInt32(Generic.GetItemValue(item, "ECG_REALIZADOS")))
                    field = red;
                GetFieldCircle(item, "ECG_REQUESITADOS", field);

                field = green;
                if (Generic.GetItemValue(item, "CEXTERNA_REQUISITADOS") == "0")
                    field = gray;
                else if (Convert.ToInt32(Generic.GetItemValue(item, "CEXTERNA_REALIZADOS")) < Convert.ToInt32(Generic.GetItemValue(item, "CEXTERNA_REQUISITADOS")))
                    field = red;
                GetFieldCircle(item, "CEXTERNA_REQUISITADOS", field);
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

        private void GetFieldCircle(LDFTableRow item, string rowName, string value)
        {
            item.rowItems.First(q => q.itemColumnName == rowName).itemValue  = "<i class='fa fa-circle lg " + value + "'></i>";
        }
        
        #endregion

    }
}