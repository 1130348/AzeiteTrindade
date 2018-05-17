using LusiadasSolucaoWeb.Models;
using System;
using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Threading;
using System.Timers;
using System.Web;
using LusiadasSolucaoWeb.Models.Secret;

namespace LusiadasSolucaoWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                //Session.Clear();
                Session["Lisboa"] = CheckConnection(ConfigurationManager.ConnectionStrings["BDHLUQLD"].ConnectionString);
                Session["Porto"] = CheckConnection(ConfigurationManager.ConnectionStrings["BDHPTQLD"].ConnectionString);
                Session["Algarve"] = CheckConnection(ConfigurationManager.ConnectionStrings["BDHSULQLD"].ConnectionString);
                AddIP();
                
                return View();
            }
            catch (Exception err)
            {
                return null;
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
                Globals.listaUsers.Add(new Pcs(ip, GetUserEnvironment(Request), Request.Url.ToString()));

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

        public bool CheckConnection(string cs)
        {
            string connectionString = cs; 
            using (var conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        [HttpPost]
        public ActionResult Index(LoginModel login)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    if (login.IsValid())
                    {
                        Session[Constants.SS_LOCAL_CONN] = "BD" + login.LocalConnection + "QLD";
                        Session["PERMA"] = "BD" + login.LocalConnection + "QLD";
                        UserInfo uinfo = login.GetUserInfo();
                        if (String.IsNullOrEmpty(uinfo.numMecan))
                            ModelState.AddModelError("error", "Utilizador inexistente na listagem de pessoal hospitalar");
                        else
                        {
                            if (String.IsNullOrEmpty(login.LocalConnection))
                            {
                                ModelState.AddModelError("error", "Deve escolher a unidade hospitalar que pretende consultar");
                            }
                            else
                            {
                                Session[Constants.SS_LOCAL_CONN] = "BD" + login.LocalConnection + "QLD";
                                Session[Constants.SS_USER] = uinfo;
                                return RedirectToAction("Index", "ATP");
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("error", "Credênciais inválidas");
                    }
                }                
            } 
            catch (Exception err)
            {
                ModelState.AddModelError("error", err.InnerException);
            }
           
            return View();
        }

    }
}
