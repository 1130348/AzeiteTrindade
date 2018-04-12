using LDFHelper.Helpers;
using LusiadasSolucaoWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LusiadasSolucaoWeb.Controllers
{
    public class ATPController : LDFTableController
    {
        UserInfo uinfo = new UserInfo();

        //[AuthenticationHandler(Auth.VER_PAGINA, Constants.SS_AUTH)]
        //[LogActionHandler(0, "Index ATP")]
        //[LogErrorHandler(Constants.WEB_GENERIC_ERROR)]
        public ActionResult Index()
        {
            if (Session[Constants.SS_USER]!=null)
            {
                ATPModel atpTable = new ATPModel();
                return View(atpTable);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        public ActionResult Dash()
        {
            ATPModel atpTable = new ATPModel();
            return View(atpTable);
        }

        [HttpGet]
        public JsonResult LoadTable()
        {
            //if (Session[Constants.SS_USER] != null)
            //{
            //    uinfo = (UserInfo)Session[Constants.SS_USER];
            //}

            //int itemsPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["PIMRowsPerPage"]);


            //atpTable.LoadRows();
            //Session["FULL_TABLE"] = Session["NEW_TABLE"] = atpTable.list_rows;
            //Session["VWFULL_TABLE"] = atpTable.list_vwDashboardATP;

            //atpTable.list_rows = atpTable.list_rows.Take(itemsPerPage).ToList();

            //return Json(new { atpTable.list_rows, pageCount = atpTable.pageCount, rowCount = atpTable.rowCount });

            
            UserInfo uinfo = Session[Constants.SS_USER] as UserInfo;

            List<LDFTableField> listFields = new List<LDFTableField>();


            //Adicionar Filtros CodigoDoente
            //listFields.Add(new LDFTableField("DOENTE", "563388"));

            ATPModel atpModel = new ATPModel();
            _model = atpModel;
            _modelName = "LusiadasSolucaoWeb.Models.ATPModel";

            JsonResult res=LoadLDFTable(1, null, JsonConvert.SerializeObject(listFields));

            return res;

        }


        public JsonResult LoadTableDash(int pagen)
        {
            //if (Session[Constants.SS_USER] != null)
            //{
            //    uinfo = (UserInfo)Session[Constants.SS_USER];
            //}

            //int itemsPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["PIMRowsPerPage"]);


            //atpTable.LoadRows();
            //Session["FULL_TABLE"] = Session["NEW_TABLE"] = atpTable.list_rows;
            //Session["VWFULL_TABLE"] = atpTable.list_vwDashboardATP;

            //atpTable.list_rows = atpTable.list_rows.Take(itemsPerPage).ToList();

            //return Json(new { atpTable.list_rows, pageCount = atpTable.pageCount, rowCount = atpTable.rowCount });


            UserInfo uinfo = Session[Constants.SS_USER] as UserInfo;

            List<LDFTableField> listFields = new List<LDFTableField>();


            //Adicionar Filtros CodigoDoente
            //listFields.Add(new LDFTableField("DOENTE", "563388"));

           

            ATPModel atpModel = new ATPModel();
            _model = atpModel;
            _modelName = "LusiadasSolucaoWeb.Models.ATPModel";

            JsonResult res = LoadLDFTableDash(pagen, null, JsonConvert.SerializeObject(listFields));

            return res;

        }

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
