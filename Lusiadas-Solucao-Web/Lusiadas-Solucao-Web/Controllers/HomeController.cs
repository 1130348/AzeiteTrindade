using LusiadasSolucaoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LDF.ErrorLogging.Logging;

namespace LusiadasSolucaoWeb.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            Session.Clear();
            return View();
        }


        [HttpPost]
        public ActionResult Index(LoginModel login)
        {
            try
            {
                if (login.IsValid())
                {
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
            } catch (Exception err)
            {
                ModelState.AddModelError("error", err.Message);
            }
           
            return View();
        }

    }
}
