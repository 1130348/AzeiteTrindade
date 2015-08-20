using LusiadasPIM.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace LusiadasPIM.Controllers
{
    public class PIMController : Controller
    {
        ModelPIMTable pimTable = new ModelPIMTable();
        UserInfo uinfo = new UserInfo();

        public ActionResult Index()
        {
            if (Session[Constants.SS_USER] != null)
            {
                uinfo               = (UserInfo)Session[Constants.SS_USER];
                ViewBag.UserName    = String.Format("{0} {1}", uinfo.titulo, uinfo.nome);
                ViewBag.NUM_CEDULA  = uinfo.getcatProfissional();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


            PisosModel pisos = new PisosModel();
            pisos.LoadPisos();

            ValenciaModel valencias = new ValenciaModel();
            valencias.LoadValencias(uinfo.listCodServ);

            Tuple<ModelPIMTable, PisosModel, ValenciaModel> tp = new Tuple<ModelPIMTable, PisosModel, ValenciaModel>(pimTable, pisos, valencias);

            return View(tp);
        }

        #region PIM Table

        [HttpPost]
        public JsonResult LoadData()
        {
            if (Session[Constants.SS_USER] != null)
            {
                uinfo = (UserInfo)Session[Constants.SS_USER];
            }

            int itemsPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["PIMRowsPerPage"]);
            pimTable.RenderRows(uinfo);
            Session["FULL_TABLE"]   = Session["NEW_TABLE"] = pimTable.list_rows;
            pimTable.list_rows      = pimTable.list_rows.Take(itemsPerPage).ToList();

            return Json(new { pimTable.list_rows, pageCount = pimTable.pageCount, rowCount = pimTable.rowCount });
        }

        [HttpPost]
        public JsonResult PaginationData(int pageNumber, string[] orderData)
        {
            var listItems = Session["FULL_TABLE"];
            if (Session["NEW_TABLE"] != null)
                listItems = Session["NEW_TABLE"];

            pimTable.PageTable(listItems, pageNumber, orderData);

            return Json(new { pimTable.list_rows, pageCount = pimTable.pageCount, rowCount = pimTable.rowCount });
        }

        [HttpPost]
        public JsonResult OrderData(string[] orderValues)
        {
            var listItems = Session["FULL_TABLE"];
            if (Session["NEW_TABLE"] != null)
                listItems = Session["NEW_TABLE"];

            pimTable.OrderTable(listItems, orderValues);

            return Json(new { pimTable.list_rows, pageCount = pimTable.pageCount, rowCount = pimTable.rowCount });
        }

        #endregion

        #region Filter Table

        [HttpGet]
        public JsonResult FilterData(string valenciaCod, string pisoCod, string viewMine)
        {
            bool onlyMine   = (Convert.ToInt32(viewMine) == 1 ? true : false);
            UserInfo uinfo  = Session[Constants.SS_USER] as UserInfo;

            Session["NEW_TABLE"] = pimTable.FilterData(Session["FULL_TABLE"], valenciaCod, pisoCod, onlyMine, uinfo.numMecan);

            return Json(new { pimTable.list_rows, pageCount = pimTable.pageCount, rowCount = pimTable.rowCount }, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}
