using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LDFHelper.Helpers;
using LusiadasDAL;
using System.Configuration;

namespace LusiadasSolucaoWeb.Models
{
    public class ConsultaDaDorModel : LDFTableModel
    {
        public ConsultaDaDorModel()
        {
            pageSize    = Convert.ToInt32(ConfigurationManager.AppSettings[Constants.SS_TABLEPAGE]);
            dbParams = new LDFTableDBParams((string)HttpContext.Current.Session[Constants.SS_LOCAL_CONN], "MEDICO", "V_POS_OPER", "*", "", "SALA", null, null);
            objType     = typeof(VwPosOrder);

            LDFTableHeaders();
            CreateNewColumns();
        }

        private void CreateNewColumns()
        {
            list_headers.Add(new LDFTableHeader() { headerID = "COL1_CLICK" });
            list_headers.Add(new LDFTableHeader() { headerID = "COL2_CLICK" });
            list_headers.Add(new LDFTableHeader() { headerID = "COL3_CLICK" });
        }

        public void LDFTableTreatData()
        {
            string tdoente, doente;
            foreach(LDFTableRow item in list_rows)
            {
                tdoente = Generic.GetItemValue(item, "T_DOENTE");
                doente = Generic.GetItemValue(item, "DOENTE");

                item.rowItems.First(q => q.itemColumnName == "T_DOENTE").itemValue = tdoente + doente;

                item.rowItems.Add(new LDFTableItem("COL1_CLICK", ""));
                item.rowItems.Add(new LDFTableItem("COL2_CLICK", ""));
                item.rowItems.Add(new LDFTableItem("COL3_CLICK", ""));

                item.rowItems.First(q => q.itemColumnName == "COL1_CLICK").itemValue = "<a data-toggle='modal' data-target='#modalHistoricoDaDor' data-tdoente='" + tdoente + "' data-doente='" + doente + "' data-nome='" + Generic.GetItemValue(item, "NOME") + "' class='fa fa-lg fa-history consdadorHistorico' title='Histórico Da Dor'></a>";
                item.rowItems.First(q => q.itemColumnName == "COL2_CLICK").itemValue = "<a data-toggle='modal' data-target='#modalPatologia' data-nome='" + Generic.GetItemValue(item, "NOME") + "' data-patologia='"+Generic.GetItemValue(item, "PAT_ASSOCIADAS")+"' class='fa fa-lg fa-medkit consdadorPatologia' title='Patologia Da Dor'></a>";
                item.rowItems.First(q => q.itemColumnName == "COL3_CLICK").itemValue = "<a data-toggle='modal' data-target='#modalFormularioDaDor' data-tdoente='" + tdoente + "' data-doente='" + doente + "' data-nome='" + Generic.GetItemValue(item, "NOME") + "' data-nint='" + Generic.GetItemValue(item, "N_INT") + "' data-nregoper='"+Generic.GetItemValue(item, "N_REG_OPER")+"' class='fa fa-lg fa-heartbeat consdadorFormularioDador' title='Formulário Da Dor'></a>";

            }
        }


    }
}