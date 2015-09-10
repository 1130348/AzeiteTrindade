using LusiadasSolucaoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LusiadasSolucaoWeb.Controllers
{
    public class ConsultaDadorController : LayoutController
    {
        // GET: ConsultaDador
        public ActionResult Index()
        {
            EnfermeirosModel enfModel = new EnfermeirosModel();
            return View(enfModel);
        }
    }
}