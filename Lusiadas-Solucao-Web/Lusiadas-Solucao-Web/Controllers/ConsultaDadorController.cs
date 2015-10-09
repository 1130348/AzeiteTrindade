using LDF.Authentication;
using LDF.ParameterManager;
using LusiadasDAL;
using LusiadasSolucaoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LusiadasSolucaoWeb.Controllers
{
    
    public class ConsultaDaDorController : LDFTableController
    {
        // GET: ConsultaDaDor
        //[AuthenticationHandler(Auth.CONSULTA_DA_DOR, Constants.SS_AUTH)]
        public ActionResult Index()
        {
            EnfermeirosModel enfModel = new EnfermeirosModel();
            AnestesistasModel anestModel = new AnestesistasModel();
            ConsultaDaDorModel consDaDorModel = new ConsultaDaDorModel();
            Tuple<EnfermeirosModel, AnestesistasModel> tpForm = new Tuple<EnfermeirosModel, AnestesistasModel>(enfModel, anestModel);
            Tuple<ConsultaDaDorModel, Tuple<EnfermeirosModel, AnestesistasModel>> tp = new Tuple<ConsultaDaDorModel, Tuple<EnfermeirosModel, AnestesistasModel>>(consDaDorModel, tpForm);
            return View(tp);
        }

        [HttpGet]
        public PartialViewResult ShowHistoricoDaDor(string tdoente, string doente)
        {
            Session["HistDor_T_DOENTE"] = tdoente;
            Session["HistDor_DOENTE"] = doente;

            HistoricoDaDorModel hist = new HistoricoDaDorModel();

            return PartialView("_historicoDor", hist);
        }

        [HttpGet]
        public PartialViewResult EditHistoricoDaDor(string rowid, string formulario)
        {
            string tdoente = (string)Session["HistDor_T_DOENTE"];
            string doente = (string)Session["HistDor_DOENTE"];
            Session["HistDor_ROWID"] = rowid;
            HistoricoDaDorModel hist = new HistoricoDaDorModel();

            formulario = hist.LDFTableUpdateTreatData(rowid, formulario);

            TblConsultaDor histRow = new TblConsultaDor();
            histRow.DOENTE = doente;
            histRow.T_DOENTE = tdoente;
            
            hist.FillObjectInfo(histRow,rowid, formulario);
            
            AnestesistasModel anestModel = new AnestesistasModel();
            EnfermeirosModel enfModel = new EnfermeirosModel(); 
            Tuple<EnfermeirosModel, AnestesistasModel> tpForm = new Tuple<EnfermeirosModel, AnestesistasModel>(enfModel, anestModel);
            Tuple<TblConsultaDor, Tuple<EnfermeirosModel, AnestesistasModel>> tp = new Tuple<TblConsultaDor, Tuple<EnfermeirosModel, AnestesistasModel>>(histRow, tpForm);

            return PartialView("_formularioEdit", tp);
        }


        [HttpPost]
        public JsonResult UpdateHistoricoDaDor(string rowID, string sos, string dor, string nausea, string vomitos, string Insonia, string cefaleia, string observacoes){
            return Json("true");
        }

        [HttpPost]
        public JsonResult SaveFormularioDaDor(string tdoente, string doente, string nint, string nregoper, string formulario)
        {
            FormularioModel form = new FormularioModel();
            return Json(form.AddDor(tdoente, doente, nint, nregoper, formulario));
        }

        [HttpPost]
        public JsonResult EditHistoricoTable(string tdoente, string doente, string rowid, string formulario)
        {
            HistoricoDaDorModel hist = new HistoricoDaDorModel();
            return Json(hist.EditRow(tdoente, doente, rowid, formulario));
        }


        [HttpPost]
        public JsonResult RemoveHist(string rowID)
        {
            HistoricoDaDorModel hist = new HistoricoDaDorModel();
            return Json(hist.RemoveRow(rowID));
        }
    }
}