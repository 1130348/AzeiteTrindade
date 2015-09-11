using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LusiadasDAL;

namespace LusiadasSolucaoWeb.Models
{
    public class FormularioModel
    {
        public string AddDor(string tdoente, string doente, string nInt, string nRegOper, string formulario)
        {
            bool    status  = false;
            string  res     = "";
            TblConsultaDor dor = new TblConsultaDor();

            UserInfo uinfo = (UserInfo)HttpContext.Current.Session[Constants.SS_USER];

            List<string[]> obj = formulario.Split('&').Select(q => q.Split('=')).ToList();
            dor.NUM_CONS_DOR    = "";
            dor.T_DOENTE        = tdoente;
            dor.DOENTE          = doente;
            dor.N_INT           = nInt;
            dor.N_REG_OPER      = nRegOper;
            dor.SOS             = obj.First(q => q[0] == "SOS")[1].ToString();
            dor.NAUSEA          = obj.First(q => q[0] == "NAUSEA")[1].ToString();
            dor.VOMITOS         = obj.First(q => q[0] == "VOMITOS")[1].ToString();
            dor.INSONIA         = obj.First(q => q[0] == "INSONIA")[1].ToString();
            dor.CEFALEIA        = obj.First(q => q[0] == "CEFALEIA")[1].ToString();
            dor.ALT_TERAP       = obj.First(q => q[0] == "ALT_TERAP")[1].ToString();
            dor.DOR             = obj.First(q => q[0] == "DOR")[1].ToString();
            dor.BAL_INFUSOR     = obj.First(q => q[0] == "BAL_INFUSOR")[1].ToString();
            dor.BAL_INFUSOR_SIT = obj.First(q => q[0] == "BAL_INFUSOR_SIT")[1].ToString();
            dor.OUTROS_SINT     = obj.First(q => q[0] == "OUTROS_SINT")[1].ToString();
            dor.OBS             = obj.First(q => q[0] == "OBS")[1].ToString();
            dor.ENF_RESP        = uinfo.userID;
            dor.ENF             = obj.First(q => q[0] == "ENF")[1].ToString();
            dor.ANEST_VISITA    = obj.First(q => q[0] == "ANEST_VISITA")[1].ToString();
            dor.USER_CRI        = uinfo.userID;

            DALConsultaDor dal = new DALConsultaDor();
            status = dal.AddDador("BDHLUQLD", dor);

            res = "{ \"header\" : { \"status\" : "+status.ToString().ToLower()+", \"message\" : \"" + ( status ? "" : "Ocorreu um erro" ) + "\"} }";
            return res;
        }

    }
}