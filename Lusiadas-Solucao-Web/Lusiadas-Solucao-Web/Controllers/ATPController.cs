using LDF.Authentication;
using LusiadasSolucaoWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LusiadasSolucaoWeb.Controllers
{
    public class ATPController : LDFTableController
    {
        UserInfo        uinfo       = new UserInfo();

        //[AuthenticationHandler(Auth.VER_PAGINA, Constants.SS_AUTH)]
        //[LogActionHandler(0, "Index ATP", Constants.SS_USER_DESCR )]
        //[LogErrorHandler(Constants.WEB_GENERIC_ERROR)]
        public ActionResult Index()
        {
            ATPModel   atpTable    = new ATPModel();
            return View(atpTable);
        }

        //[HttpPost]
        //public JsonResult LoadTable()
        //{
        //    if (Session[Constants.SS_USER] != null)
        //    {
        //        uinfo = (UserInfo)Session[Constants.SS_USER];
        //    }

        //    int itemsPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["PIMRowsPerPage"]);
        //    atpTable.LoadRows();
        //    Session["FULL_TABLE"]   = Session["NEW_TABLE"] = atpTable.list_rows;
        //    Session["VWFULL_TABLE"] = atpTable.list_vwDashboardATP;

        //    atpTable.list_rows = atpTable.list_rows.Take(itemsPerPage).ToList();

        //    return Json(new { atpTable.list_rows, pageCount = atpTable.pageCount, rowCount = atpTable.rowCount });
        //}

        //[HttpPost]
        //public JsonResult PageTable(int pageNumber, string[] orderData)
        //{
        //    var listItems = Session["FULL_TABLE"];
        //    if (Session["NEW_TABLE"] != null)
        //        listItems = Session["NEW_TABLE"];

        //    atpTable.PageTable(listItems, pageNumber, orderData);

        //    return Json(new { atpTable.list_rows, pageCount = atpTable.pageCount, rowCount = atpTable.rowCount });
        //}


    }
}
