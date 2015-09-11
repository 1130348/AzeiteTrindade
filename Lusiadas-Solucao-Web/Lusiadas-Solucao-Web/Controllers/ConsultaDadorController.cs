using LusiadasSolucaoWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LusiadasSolucaoWeb.Controllers
{
    public class ConsultaDadorController : LDFTableController
    {
        // GET: ConsultaDador
        public ActionResult Index()
        {
            EnfermeirosModel enfModel = new EnfermeirosModel();
            ConsultaDadorModel consDadorModel = new ConsultaDadorModel();

            Tuple<ConsultaDadorModel, EnfermeirosModel> tp = new Tuple<ConsultaDadorModel, EnfermeirosModel>(consDadorModel, enfModel);
            return View(tp);
        }

        [HttpGet]
        public PartialViewResult ShowHistoricoDador(string tdoente, string doente)
        {
            Session["HistDor_TDOENTE"] = tdoente;
            Session["HistDor_DOENTE"] = doente;
            HistoricoDadorModel hist = new HistoricoDadorModel();
            return PartialView("_historicoDor", hist);
        }

        [HttpPost]
        public JsonResult SaveFormularioDador(string tdoente, string doente, string nint, string nregoper, string formulario)
        {
            FormularioModel form = new FormularioModel();
            return Json(form.AddDor(tdoente, doente, nint, nregoper, formulario));
        }

        [HttpPost]
        public JsonResult RemoveHist(string rowID)
        {
            HistoricoDadorModel hist = new HistoricoDadorModel();
            return Json(hist.RemoveRow(rowID));
        }

    }
}