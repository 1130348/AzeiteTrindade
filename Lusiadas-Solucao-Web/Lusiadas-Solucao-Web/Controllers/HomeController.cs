using LusiadasSolucaoWeb.Models;
using System;
using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace LusiadasSolucaoWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                Session.Clear();
                Session["Lisboa"] = CheckConnection("User Id=medico;Password=medico;Data Source=BDHLU");
                Session["Porto"] = CheckConnection("User Id=hpp;Password=hppnorte;Data Source=BDHPT");
                Session["Algarve"] = CheckConnection("User Id=hpp;Password=hppnorte;Data Source=BDHPTQLD");
                return View();
            }
            catch (Exception err)
            {
                return null;
            }
            
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
