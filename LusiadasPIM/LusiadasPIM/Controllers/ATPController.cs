using LusiadasSolucaoWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LusiadasSolucaoWeb.Controllers
{
    public class ATPController : Controller
    {
        UserInfo        uinfo       = new UserInfo();
        ATPTableModel   atpTable    = new ATPTableModel();

        [AuthenticationHandler("VIEW_ATP")]
        [LogActionHandler(0, "ENTROU")]
        [LogErrorHandler("UPSI DUPSI")]
        public ActionResult Index()
        {
            if (Session[Constants.SS_USER] != null)
            {
                uinfo = (UserInfo)Session[Constants.SS_USER];
                ViewBag.UserName = String.Format("{0} {1}", uinfo.titulo, uinfo.nome);
                ViewBag.NUM_CEDULA = uinfo.getcatProfissional();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View(atpTable);
        }

        [HttpPost]
        public JsonResult LoadTable()
        {
            if (Session[Constants.SS_USER] != null)
            {
                uinfo = (UserInfo)Session[Constants.SS_USER];
            }

            int itemsPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["PIMRowsPerPage"]);
            atpTable.LoadRows();
            Session["FULL_TABLE"]   = Session["NEW_TABLE"] = atpTable.list_rows;
            Session["VWFULL_TABLE"] = atpTable.list_vwDashboardATP;

            atpTable.list_rows = atpTable.list_rows.Take(itemsPerPage).ToList();

            return Json(new { atpTable.list_rows, pageCount = atpTable.pageCount, rowCount = atpTable.rowCount });
        }

        [HttpPost]
        public JsonResult PageTable(int pageNumber, string[] orderData)
        {
            var listItems = Session["FULL_TABLE"];
            if (Session["NEW_TABLE"] != null)
                listItems = Session["NEW_TABLE"];

            atpTable.PageTable(listItems, pageNumber, orderData);

            return Json(new { atpTable.list_rows, pageCount = atpTable.pageCount, rowCount = atpTable.rowCount });
        }


    }
}
