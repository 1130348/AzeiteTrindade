using LusiadasSolucaoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LusiadasSolucaoWeb.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            Error teste = new Error();
            return View("Default");
        }
        public ActionResult NotFound()
        {
            Error teste = new Error();
            return View("Error404");
        }
    }
}