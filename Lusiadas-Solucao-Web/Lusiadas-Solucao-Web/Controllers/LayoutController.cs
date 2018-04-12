using LusiadasSolucaoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LusiadasSolucaoWeb.Controllers
{
    public class LayoutController : Controller
    {
        UserInfo uinfo = new UserInfo();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session[Constants.SS_USER] != null)
            {
                uinfo               = (UserInfo)Session[Constants.SS_USER];
                ViewBag.UserName    = String.Format("{0} {1}", uinfo.titulo, uinfo.nome);
                ViewBag.NUM_CEDULA  = uinfo.getcatProfissional();
            }
            else
            {
                //filterContext.Result = RedirectToAction("Index", "Home");
            }
        }

    }
}