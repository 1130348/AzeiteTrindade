using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PIMDAL;
using LusiadasSolucaoWeb.Models;
using System.Configuration;

namespace LusiadasSolucaoWeb.Controllers
{
    public class DeslocacoesController : Controller
    {

        #region Variables
        
        UserInfo                uinfo           = new UserInfo();
        ModelInfoAdTable        infADTable      = new ModelInfoAdTable();
        ModelDeslocProdTable    deslocProdTable = new ModelDeslocProdTable();
        
        #endregion

        #region Actions

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

            DALDeslocacoes dal = new DALDeslocacoes();

            ValenciaModel valencias = new ValenciaModel();
            valencias.LoadValencias(uinfo.listCodServ);
            Session["InfADValencias"] = valencias;

            ParameterModel paramProd = new ParameterModel();
            paramProd.FillParameters();
            Session["InfADDeslocProd"] = paramProd;

            Tuple<ModelInfoAdTable, ValenciaModel, ParameterModel> tp = new Tuple<ModelInfoAdTable, ValenciaModel, ParameterModel>(infADTable, valencias, paramProd);

            return View(tp);
        }

        #endregion

        #region Deslocacoes Table

        [HttpPost]
        public JsonResult LoadData()
        {
            if (Session[Constants.SS_USER] != null)
            {
                uinfo = (UserInfo)Session[Constants.SS_USER];
            }

            int itemsPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["PIMRowsPerPage"]);
            infADTable.LoadRows((ValenciaModel)Session["InfADValencias"]);
            Session["FULL_TABLE"] = Session["NEW_TABLE"] = infADTable.list_rows;
            Session["VWFULL_TABLE"] = infADTable.list_vwDoentesPresentes;

            infADTable.list_rows = infADTable.list_rows.Take(itemsPerPage).ToList();

            return Json(new { infADTable.list_rows, pageCount = infADTable.pageCount, rowCount = infADTable.rowCount });
        }

        [HttpPost]
        public JsonResult PaginationData(int pageNumber, string[] orderData)
        {
            var listItems = Session["FULL_TABLE"];
            if (Session["NEW_TABLE"] != null)
                listItems = Session["NEW_TABLE"];

            infADTable.PageTable(listItems, pageNumber, orderData);

            return Json(new { infADTable.list_rows, pageCount = infADTable.pageCount, rowCount = infADTable.rowCount });
        }

        [HttpPost]
        public JsonResult OrderData(string[] orderValues)
        {
            var listItems = Session["FULL_TABLE"];
            if (Session["NEW_TABLE"] != null)
                listItems = Session["NEW_TABLE"];

            infADTable.OrderTable(listItems, orderValues);

            return Json(new { infADTable.list_rows, pageCount = infADTable.pageCount, rowCount = infADTable.rowCount });
        }

        #endregion

        #region Filter Table

        [HttpGet]
        public JsonResult FilterData(string servicoCod, string ultLocalCod, string viewOnlocal)
        {
            bool onlyOnlocal = (Convert.ToInt32(viewOnlocal) == 1 ? true : false);
            UserInfo uinfo = Session[Constants.SS_USER] as UserInfo;

            Session["NEW_TABLE"] = infADTable.FilterData((ValenciaModel)Session["InfADValencias"], servicoCod, ultLocalCod, onlyOnlocal);
            Session["VWFULL_TABLE"] = infADTable.list_vwDoentesPresentes;

            return Json(new { infADTable.list_rows, pageCount = infADTable.pageCount, rowCount = infADTable.rowCount }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Ajax Methods

        [HttpPost]
        public JsonResult UpdateDeslocRow(string itemRow, string deslocCod)
        {
            UserInfo uinfo = Session[Constants.SS_USER] as UserInfo;
            string newRow = itemRow;

            var listItems = Session["VWFULL_TABLE"];

            bool res = infADTable.UpdateRow(uinfo, listItems, itemRow, deslocCod);

            return Json(new { res });
        }

        [HttpGet]
        public PartialViewResult ShowDeslocTimeLine(string tdoente, string doente)
        {
            DeslocHistModel myModal = new DeslocHistModel();
            myModal.LoadHistDesloc(tdoente, doente);


            return PartialView("_timeLine", myModal.listDesloc);
        }

        [HttpGet]
        public PartialViewResult ShowDeslocProd(string tdoente, string doente, string curserv, string ultlocal, string ncons, string tEpis, string epis)
        {
            Session["DeslocProd_TDOENTE"]  = tdoente;
            Session["DeslocProd_DOENTE"] = doente;

            ParameterModel paramProd = (ParameterModel)Session["InfADDeslocProd"];
            ValenciaModel valencias = (ValenciaModel)Session["InfADValencias"];

            deslocProdTable.tdoente = tdoente;
            deslocProdTable.doente  = doente;
            deslocProdTable.ncons   = ncons;
            deslocProdTable.tEpis   = tEpis;
            deslocProdTable.epis    = epis;

            Tuple<ModelDeslocProdTable, ParameterModel, ValenciaModel> tp 
                = new Tuple<ModelDeslocProdTable, ParameterModel, ValenciaModel>
                    (
                        deslocProdTable,
                        paramProd,
                        valencias
                    );

            return PartialView("_deslocProd", tp);
        }

        [HttpPost]
        public JsonResult AddDeslocProd(string tdoente, string doente, string ncons, string tEpis, string epis, string selProd, string selOrig, string selDest)
        {
            ModelDeslocProdTable prod = new ModelDeslocProdTable();
            UserInfo uinfo = Session[Constants.SS_USER] as UserInfo;
            bool res = prod.AddNewRow(uinfo, tdoente, doente, ncons, tEpis, epis, selProd, selOrig, selDest);

            return Json(res);
        }

        [HttpPost]
        public JsonResult UpdateDeslocProd(string itemRow, string selDest)
        {
            ModelDeslocProdTable prod = new ModelDeslocProdTable();
            UserInfo uinfo  = Session[Constants.SS_USER] as UserInfo;

            var listItems   = Session["DeslocProd_FULLVW"];
            bool res        = prod.UpdateRow(uinfo, listItems, itemRow, selDest);

            return Json(res);
        }


        #endregion

        #region DeslocProd Table

        [HttpPost]
        public JsonResult LoadDeslocProdTable()
        {
            if (Session[Constants.SS_USER] != null)
            {
                uinfo = (UserInfo)Session[Constants.SS_USER];
            }

            int itemsPerPage    = Convert.ToInt32(ConfigurationManager.AppSettings["PIMRowsPerPage"]);
            string tdoente      = (string)Session["DeslocProd_TDOENTE"];
            string doente       = (string)Session["DeslocProd_DOENTE"];


            deslocProdTable.LoadRows((ValenciaModel)Session["InfADValencias"], tdoente, doente);
            Session["DeslocProd_FULLTABLE"] = deslocProdTable.list_rows;
            Session["DeslocProd_FULLVW"]    = deslocProdTable.list_vwDeslocProd;

            deslocProdTable.list_rows = deslocProdTable.list_rows.Take(itemsPerPage).ToList();

            return Json(new { deslocProdTable.list_rows, pageCount = deslocProdTable.pageCount, rowCount = deslocProdTable.rowCount });
        }


        [HttpPost]
        public JsonResult PageDeslocProdTable(int pageNumber, string[] orderData)
        {
            var listItems = Session["DeslocProd_FULLTABLE"];

            deslocProdTable.PageTable(listItems, pageNumber, orderData);

            return Json(new { deslocProdTable.list_rows, pageCount = deslocProdTable.pageCount, rowCount = deslocProdTable.rowCount });
        }

        [HttpPost]
        public JsonResult OrderDeslocProdTable(string[] orderValues)
        {
            var listItems = Session["DeslocProd_FULLTABLE"];

            deslocProdTable.OrderTable(listItems, orderValues);

            return Json(new { deslocProdTable.list_rows, pageCount = deslocProdTable.pageCount, rowCount = deslocProdTable.rowCount });
        }

        #endregion

    }
}
