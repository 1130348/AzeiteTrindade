using LusiadasSolucaoWeb.Models;
using System;
using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Threading;
using System.Timers;

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
            if (!Globals.listaUsers.Contains(GetIPAddress()))
            {
                string ip = GetIPAddress();
                Globals.listaUsers.Add(ip + "-" + Environment.MachineName);

                try
                {
                    System.Timers.Timer aTimer = new System.Timers.Timer();
                    aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    aTimer.Interval = 1200000;
                    aTimer.Enabled = true;


                }
                catch (Exception e)
                {

                }
               


            }

        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Globals.listaUsers.RemoveAt(0);
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
