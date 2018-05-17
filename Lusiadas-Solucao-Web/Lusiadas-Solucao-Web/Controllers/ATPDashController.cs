using LDFHelper.Helpers;
using LusiadasSolucaoWeb.Models;
using LusiadasSolucaoWeb.Models.Secret;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Timers;
using System.Web;
using System.Web.Mvc;

namespace LusiadasSolucaoWeb.Controllers
{
    public class ATPDashController : LDFTableController
    {
        UserInfo uinfo = new UserInfo();

        public ActionResult Dash(string hosp,int num)
        {
            Session.Remove("Tabela");

            if (hosp.ToUpper().Equals("PORTO")|| hosp.ToUpper().Equals("LISBOA")|| hosp.ToUpper().Equals("ALGARVE"))
            {

                switch (hosp.ToUpper())
                {

                    case "PORTO":
                        hosp = "HPT";
                        break;
                    case "LISBOA":
                        hosp = "HLU";
                        break;
                    case "ALGARVE":
                        hosp = "HSUL";
                        break;
                    default :
                        hosp = "";
                        break;
                }

                Session[Constants.SS_LOCAL_CONN] = "BD" + hosp + "QLD"; ;

            }

            AddIP();
            

            Session["Tabela"] = num;
            if (num==null||num<=0)
            {


                ATPDashModel atpTable = new ATPDashModel(20);
                return View(atpTable);
            }
            else
            {
                
                ATPDashModel atpTable = new ATPDashModel(num);
                return View(atpTable);
            }
            
            
        }

        private void AddIP()
        {

            foreach (Pcs str in Globals.listaUsers)
            {

                if (str.getIp().Equals(GetIPAddress()))
                {
                    Globals.listaUsers.Remove(str);
                    break;
                }

            }



            try
            {
                string ip = GetIPAddress();

                HttpBrowserCapabilitiesBase bc = Request.Browser;
                Globals.listaUsers.Add(new Pcs(ip,GetUserEnvironment(Request),Request.Url.ToString()));

            }
            catch (Exception e)
            {
                 e.ToString();
            }


        }


        public String GetUserEnvironment(HttpRequestBase request)
        {
            var browser = request.Browser;
            var platform = GetUserPlatform(request);
            return string.Format("{0} {1} / {2}", browser.Browser, browser.Version, platform);
        }

        public String GetUserPlatform(HttpRequestBase request)
        {
            var ua = request.UserAgent;

            if (ua.Contains("Mac OS"))
                return "Mac OS";

            if (ua.Contains("Windows NT 5.1") || ua.Contains("Windows NT 5.2"))
                return "Windows XP";

            if (ua.Contains("Windows NT 6.0"))
                return "Windows Vista";

            if (ua.Contains("Windows NT 6.1"))
                return "Windows 7";

            if (ua.Contains("Windows NT 6.2"))
                return "Windows 8";

            if (ua.Contains("Windows NT 6.3"))
                return "Windows 8.1";

            if (ua.Contains("Windows NT 10"))
                return "Windows 10";

            //fallback to basic platform:
            return request.Browser.Platform + (ua.Contains("Mobile") ? " Mobile " : "");
        }

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }



        [HttpGet]
        public string GetDados()
        {
            
            GetData();

            return Globals.nDoentes+";"+Globals.semTriagem+";"+Globals.semNota;

        }


        public JsonResult LoadTableDash(int pagen)
        {
           
            UserInfo uinfo = Session[Constants.SS_USER] as UserInfo;

            List<LDFTableField> listFields = new List<LDFTableField>();


            ATPDashModel atpModel = new ATPDashModel((int)Session["Tabela"]);
            _model = atpModel;
            _modelName = "LusiadasSolucaoWeb.Models.ATPDashModel";

            JsonResult res = LoadLDFTableDash(pagen, null, JsonConvert.SerializeObject(listFields));

            return res;

        }

        public void GetData()
        {

            try
            {
                string oradb = "";
                if (Session[Constants.SS_LOCAL_CONN].ToString().Contains("HPT")) {

                    oradb = ConfigurationManager.ConnectionStrings["BDHPTQLD"].ConnectionString;
                }
                else if (Session[Constants.SS_LOCAL_CONN].ToString().Contains("HLU"))
                {
                    oradb = ConfigurationManager.ConnectionStrings["BDHLUQLD"].ConnectionString;
                }
                else
                {
                    oradb = ConfigurationManager.ConnectionStrings["BDHSULQLD"].ConnectionString;
                }
               
                OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection(oradb);  // C#
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select count(*) from MEDICO.V_DASH_DESLOC_ATP_V3";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        Globals.nDoentes = dr["COUNT(*)"].ToString();

                    }

                }
                else
                {
                    Globals.nDoentes = "Sem Dados";
                }

                OracleCommand cmd2 = new OracleCommand();
                cmd2.Connection = conn;
                cmd2.CommandText = "select count(*) from MEDICO.V_DASH_DESLOC_ATP_V3 where COR_TRIAGEM IS NULL";
                cmd2.CommandType = CommandType.Text;
                OracleDataReader dr2 = cmd2.ExecuteReader();

                if (dr2.HasRows)
                {

                    while (dr2.Read())
                    {
                        Globals.semTriagem = dr2["COUNT(*)"].ToString();

                    }

                }
                else
                {
                    Globals.semTriagem = "Sem Dados";
                }

                OracleCommand cmd3 = new OracleCommand();
                cmd3.Connection = conn;
                cmd3.CommandText = "select count(*) from MEDICO.V_DASH_DESLOC_ATP_V3 where DT_NOTA_MEDICA IS NULL";
                cmd3.CommandType = CommandType.Text;
                OracleDataReader dr3 = cmd3.ExecuteReader();

                if (dr3.HasRows)
                {

                    while (dr3.Read())
                    {
                        Globals.semNota = dr3["COUNT(*)"].ToString();

                    }

                }
                else
                {
                    Globals.semNota = "Sem Dados";
                }





                conn.Dispose();

            }
            catch (Exception err)
            {
                err.InnerException.ToString();
                string var=err.InnerException.ToString();
                Globals.nDoentes = "Sem Dados";
                Globals.semNota = "Sem Dados";
                Globals.semTriagem = "Sem Dados";

            }


        }

    }
}
