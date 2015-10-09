using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LDFHelper.Helpers;
using LusiadasDAL;
using System.Configuration;
using System.Web.Mvc;
using LDF.ParameterManager;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using System.Globalization;
using Oracle.ManagedDataAccess.Client;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace LusiadasSolucaoWeb.Models
{
    public class HistoricoDaDorModel : LDFTableModel
    {

        
        public HistoricoDaDorModel()
        {
            string tdoente = (string)HttpContext.Current.Session["HistDor_T_DOENTE"];
            string doente = (string)HttpContext.Current.Session["HistDor_DOENTE"];

            string query = "T_DOENTE='" + tdoente + "' AND DOENTE = '" + doente + "' AND FLAG_ESTADO IS NULL";

            pageSize = Convert.ToInt32(ConfigurationManager.AppSettings[Constants.SS_TABLEPAGE]);
            dbParams = new LDFTableDBParams("BDHLUQLD", "MEDICO", "CONSULTA_DOR", "*", query, "", new List<string>() { "BAL_INFUSOR", "BAL_INFUSOR_SIT", "SOS", "DOR", "NAUSEA", "VOMITOS", "INSONIA", "CEFALEIA", "OBS", "ENF", "ANEST_VISITA", "ALT_TERAP", "OUTROS_SINT" }, new List<string>() { "NUM_CONS_DOR"});
            objType = typeof(TblConsultaDor);

            UrlHelper helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            callbacks.loadCallBack = helper.Action("LoadLDFTable", "ConsultaDaDor");
            callbacks.editCallBack = helper.Action("EditHistoricoTable", "ConsultaDaDor");
            callbacks.removeCallBack = helper.Action("RemoveHist", "ConsultaDaDor");

            LDFTableHeaders();
            CreateNewColumns();
        }

        private void CreateNewColumns()
        {
            list_headers.Add(new LDFTableHeader() { headerID = "COL1_CLICK" });
        }

        public void LDFTableTreatData()
        {
            ParameterModel defs = new ParameterModel();
            defs.FillDefinitions();
            DALSDServ dal = new DALSDServ();

            string query;
            foreach (LDFTableRow item in list_rows)
            {
                query = "data-tdoente='" + Generic.GetItemValue(item, "T_DOENTE") + "'";
                query += "data-doente='" + Generic.GetItemValue(item, "DOENTE") + "'";
                query += "data-nint='" + Generic.GetItemValue(item, "N_INT") + "'";
                query += "data-nregoper='" + Generic.GetItemValue(item, "N_REG_OPER") + "'";
                query += "data-rowID='" + item.rowID + "'";
                query += "data-balInfusor='" + Generic.GetItemValue(item, "BAL_INFUSOR") + "'";
                query += "data-balInfusorSit='" + Generic.GetItemValue(item, "BAL_INFUSOR_SIT") + "'";
                query += "data-SOS='" + Generic.GetItemValue(item, "SOS") + "'";
                query += "data-nausea='" + Generic.GetItemValue(item, "NAUSEA") + "'";
                query += "data-vomitos='" + Generic.GetItemValue(item, "VOMITOS") + "'";
                query += "data-insonia='" + Generic.GetItemValue(item, "INSONIA") + "'";
                query += "data-cefaleia='" + Generic.GetItemValue(item, "CEFALEIA") + "'";
                query += "data-altTerap='" + Generic.GetItemValue(item, "ALT_TERAP") + "'";
                query += "data-dor='" + Generic.GetItemValue(item, "DOR") + "'";
                query += "data-enf='" + Generic.GetItemValue(item, "ENF") + "'";
                query += "data-anestVisita='" + Generic.GetItemValue(item, "ANEST_VISITA") + "'";
                query += "data-outrosSint='" + Generic.GetItemValue(item, "OUTROS_SINT") + "'";
                query += "data-obs='" + Generic.GetItemValue(item, "OBS") + "'";

                item.rowItems.Add(new LDFTableItem("COL1_CLICK", ""));
//                item.rowItems.First(q => q.itemColumnName == "COL1_CLICK").itemValue = "<a data-toggle='modal' data-target='#modalFormularioDaDor' " + query + " data-rowID='" + item.rowID + "'  class='fa fa-lg fa-history editHistDaDor' title='Histórico Da Dor'></a>";
                item.rowItems.First(q => q.itemColumnName == "COL1_CLICK").itemValue = "<button type='button' class='btn btn-success editHistDaDor' data-toggle='modal' data-target='#modalFormularioDaDor' value='Editar' " + query + "><li class='fa fa-edit'></li> Editar</button>";

                foreach (LDFTableItem rItem in item.rowItems)
                {
                    List<Produtos> prods = defs.list_defs.Where(q => q.CODIGO_PAI != null && q.CODIGO_PAI.Split(';').Count(x => x == rItem.itemColumnName) == 1).ToList();
                    if (prods.Count > 0)
                    {
                        rItem.itemValue = prods.First(q => q.VALOR == rItem.itemValue).NOME_CAMPO;
                    }
                }
            }
        }

        public string LDFTableUpdateTreatData(string rowID, string fields)
        {
            ParameterModel defs = new ParameterModel();
            defs.FillDefinitions();
            dynamic item =Json.Decode(fields, typeof(object));
            //List<KeyValuePair<string, object>> headers = item;
            List<Tuple<string, string>> list = new List<Tuple<string, string>>();

            foreach (KeyValuePair<string, object> data in item)
            {
                var key_act = data.Key;
                if (defs.list_defs.Any(q => q.NOME_CAMPO == data.Value.ToString()))
                {
                    list.Add(new Tuple<string, string>(defs.list_defs.First((q => q.NOME_CAMPO == data.Value.ToString())).VALOR, data.Key));
                }
            }

            foreach (Tuple<string, string> tp in list)
            {
                item[tp.Item2] = tp.Item1;
            }

            string res=Json.Encode(item);
            return res;
        }

        public string EditRow(string tdoente, string doente, string rowID, string formulario)
        {
            UserInfo uinfo = (UserInfo)HttpContext.Current.Session[Constants.SS_USER];
            var obj = JsonConvert.DeserializeObject(formulario);
            bool status = false;

            TblConsultaDor dor = new TblConsultaDor();
            dor.NUM_CONS_DOR    = rowID;
            dor.ROWID           = rowID;
            dor.T_DOENTE        = tdoente;
            dor.DOENTE          = doente;
            
            dor.SOS = ((dynamic)((JObject)(obj))).SOS;
            dor.NAUSEA = ((dynamic)((JObject)(obj))).NAUSEA;
            dor.VOMITOS = ((dynamic)((JObject)(obj))).VOMITOS;
            dor.INSONIA = ((dynamic)((JObject)(obj))).INSONIA;
            dor.CEFALEIA = ((dynamic)((JObject)(obj))).CEFALEIA;
            dor.ALT_TERAP = ((dynamic)((JObject)(obj))).ALT_TERAP;
            dor.DOR = ((dynamic)((JObject)(obj))).DOR;
            dor.BAL_INFUSOR = ((dynamic)((JObject)(obj))).BAL_INFUSOR;
            dor.BAL_INFUSOR_SIT = ((dynamic)((JObject)(obj))).BAL_INFUSOR_SIT;
            dor.OUTROS_SINT = ((dynamic)((JObject)(obj))).OUTROS_SINT;
            dor.OBS = ((dynamic)((JObject)(obj))).OBS;
            dor.ENF_RESP = uinfo.userID;
            dor.ENF = ((dynamic)((JObject)(obj))).ENF;
            dor.ANEST_VISITA = ((dynamic)((JObject)(obj))).ANEST_VISITA;
            dor.USER_ACT = uinfo.userID;
            dor.DT_ACT = DateTime.Now;

            if (ParameterManager.UpdateParameter<TblConsultaDor>("BDHLUQLD", "MEDICO", "CONSULTA_DOR", dor, new List<string> { "BAL_INFUSOR", "BAL_INFUSOR_SIT", "SOS", "DOR", "NAUSEA", "VOMITOS", "INSONIA", "CEFALEIA", "OBS", "ENF", "ANEST_VISITA", "USER_ACT", "OUTROS_SINT", "ALT_TERAP" }, new List<string> { "NUM_CONS_DOR" }) > 0)
            {
                OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["BDHLUQLD"].ConnectionString);

                OracleCommand cmd = new OracleCommand("UPDATE CONSULTA_DOR SET DT_ACT = :dt_act where NUM_CONS_DOR = :num_cons_dor", conn);

                cmd.Parameters.Add(new OracleParameter("dt_act", OracleDbType.Date)).Value = DateTime.Now;
                cmd.Parameters.Add(new OracleParameter("num_cons_dor", OracleDbType.Varchar2)).Value = dor.NUM_CONS_DOR;

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                status =  true;
            }

            string res = "{ \"header\" : { \"status\" : " + status.ToString().ToLower() + ", \"message\" : \"" + (status ? "" : "Ocorreu um erro") + "\"} }";
            return res;
        }
        
        public bool RemoveRow(string rowID)
        {

            TblConsultaDor obj = new TblConsultaDor();
            obj.NUM_CONS_DOR = rowID;
            obj.ROWID = rowID;
            obj.FLAG_ESTADO = "A";

            if (ParameterManager.UpdateParameter<TblConsultaDor>("BDHLUQLD", "MEDICO", "CONSULTA_DOR", obj, new List<string> { "FLAG_ESTADO", "DT_ANUL", "USER_ANUL" }, new List<string> { "NUM_CONS_DOR" }) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

    }
}