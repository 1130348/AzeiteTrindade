using LusiadasDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LusiadasSolucaoWeb.Models
{
    public class FormularioModel
    {
        public string AddDor(string tdoente, string doente, string nInt, string nRegOper, string formulario)
        {
            TblConsultaDor  dor     = new TblConsultaDor();
            string          res     = "";
            bool            status  = false; 

            UserInfo uinfo      = (UserInfo)HttpContext.Current.Session[Constants.SS_USER];
            var obj             = JsonConvert.DeserializeObject(formulario);

            dor.NUM_CONS_DOR    = "";
            dor.T_DOENTE        = tdoente;
            dor.DOENTE          = doente;
            dor.N_INT           = nInt;
            dor.N_REG_OPER      = nRegOper;
            dor.SOS             = ((dynamic)((JObject)(obj))).SOS;
            dor.NAUSEA          = ((dynamic)((JObject)(obj))).NAUSEA;
            dor.VOMITOS         = ((dynamic)((JObject)(obj))).VOMITOS;
            dor.INSONIA         = ((dynamic)((JObject)(obj))).INSONIA;
            dor.CEFALEIA        = ((dynamic)((JObject)(obj))).CEFALEIA;
            dor.ALT_TERAP       = ((dynamic)((JObject)(obj))).ALT_TERAP;
            dor.DOR             = ((dynamic)((JObject)(obj))).DOR;
            dor.BAL_INFUSOR     = ((dynamic)((JObject)(obj))).BAL_INFUSOR;
            dor.BAL_INFUSOR_SIT = ((dynamic)((JObject)(obj))).BAL_INFUSOR_SIT;
            dor.OUTROS_SINT     = ((dynamic)((JObject)(obj))).OUTROS_SINT;
            dor.OBS             = ((dynamic)((JObject)(obj))).OBS;
            dor.ENF_RESP        = uinfo.userID;
            dor.ENF             = ((dynamic)((JObject)(obj))).ENF;
            dor.ANEST_VISITA    = ((dynamic)((JObject)(obj))).ANEST_VISITA;
            dor.USER_CRI        = uinfo.userID;


            DALConsultaDor dal = new DALConsultaDor();
            status = dal.AddDaDor((string)HttpContext.Current.Session[Constants.SS_LOCAL_CONN], dor);

            res="{ \"header\" : { \"status\" : "+status.ToString().ToLower()+", \"message\" : \"" + (status ? "":"Ocorreu um erro") + "\"} }";

            return res;
        }
    }
}