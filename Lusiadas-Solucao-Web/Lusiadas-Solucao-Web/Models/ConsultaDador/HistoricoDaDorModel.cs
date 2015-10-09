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
        }

        public void LDFTableTreatData()
        {
            ParameterModel defs = new ParameterModel();
            defs.FillDefinitions();
            DALSDServ dal = new DALSDServ();
            foreach (LDFTableRow item in list_rows)
            {
                foreach(LDFTableItem rItem in item.rowItems)
                {
                    if(defs.list_defs.Any(q => q.VALOR == rItem.itemValue))
                    {
                        rItem.itemValue = defs.list_defs.First(q => q.VALOR == rItem.itemValue).NOME_CAMPO;
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

        public bool EditRow(string rowID, string formulario)
        {
           List<string[]> obj2 = formulario.Split('&').Select(q => q.Split('=')).ToList();

            UserInfo uinfo = (UserInfo)HttpContext.Current.Session[Constants.SS_USER];
           
            TblConsultaDor obj = new TblConsultaDor();
            obj.NUM_CONS_DOR = (string)HttpContext.Current.Session["HistDor_ROWID"];
            obj.ROWID = (string)HttpContext.Current.Session["HistDor_ROWID"];
            obj.T_DOENTE = (string)HttpContext.Current.Session["HistDor_T_DOENTE"];
            obj.DOENTE = (string)HttpContext.Current.Session["HistDor_DOENTE"];

            obj.SOS = obj2.First(q => q[0] == "SOS")[1].ToString();
            obj.NAUSEA = obj2.First(q => q[0] == "NAUSEA")[1].ToString();
            obj.VOMITOS = obj2.First(q => q[0] == "VOMITOS")[1].ToString();
            obj.INSONIA = obj2.First(q => q[0] == "INSONIA")[1].ToString();
            obj.CEFALEIA = obj2.First(q => q[0] == "CEFALEIA")[1].ToString();
            obj.ALT_TERAP = obj2.First(q => q[0] == "ALT_TERAP")[1].ToString();
            obj.DOR = obj2.First(q => q[0] == "DOR")[1].ToString();
            obj.BAL_INFUSOR = obj2.First(q => q[0] == "BAL_INFUSOR")[1].ToString();
            obj.BAL_INFUSOR_SIT = obj2.First(q => q[0] == "BAL_INFUSOR_SIT")[1].ToString();
            obj.OUTROS_SINT = obj2.First(q => q[0] == "OUTROS_SINT")[1].ToString();
            obj.OBS = obj2.First(q => q[0] == "OBS")[1].ToString();
            obj.ENF = obj2.First(q => q[0] == "ENF")[1].ToString();
            obj.ANEST_VISITA = obj2.First(q => q[0] == "ANEST_VISITA")[1].ToString();
            obj.USER_ACT = uinfo.userID;
            obj.DT_ACT = DateTime.Now;






            if (ParameterManager.UpdateParameter<TblConsultaDor>("BDHLUQLD", "MEDICO", "CONSULTA_DOR", obj, new List<string> { "BAL_INFUSOR", "BAL_INFUSOR_SIT", "SOS", "DOR", "NAUSEA", "VOMITOS", "INSONIA", "CEFALEIA", "OBS", "ENF", "ANEST_VISITA", "USER_ACT", "OUTROS_SINT", "ALT_TERAP" }, new List<string> { "NUM_CONS_DOR" }) > 0)
            {
                OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["BDHLUQLD"].ConnectionString);
                
                OracleCommand cmd = new OracleCommand("UPDATE CONSULTA_DOR SET DT_ACT = :dt_act where NUM_CONS_DOR = :num_cons_dor", conn);
                
                cmd.Parameters.Add(new OracleParameter("dt_act", OracleDbType.Date)).Value = DateTime.Now;
                cmd.Parameters.Add(new OracleParameter("num_cons_dor", OracleDbType.Varchar2)).Value = obj.NUM_CONS_DOR;

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            

                return true;
            }
            else
            {
                return false;
            }


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