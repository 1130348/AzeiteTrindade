using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LusiadasDAL;
using LusiadasSolucaoWeb.Models;
using System.Configuration;
using LDFHelper.Helpers;
using Newtonsoft.Json;

namespace LusiadasSolucaoWeb.Controllers
{
    public class DeslocacoesController : LDFTableController
    {
        #region Variables
        
        UserInfo                uinfo       = new UserInfo();
        
        #endregion

        #region Actions

        public ActionResult Index()
        {
            DALDeslocacoes dal = new DALDeslocacoes();

            PisosModel pisos = new PisosModel();
            pisos.LoadPisos();

            ValenciaModel valencias = new ValenciaModel();
            valencias.LoadValencias(uinfo.listCodServ);
            //Pesquisar como fazer union das duas listas
            

            Session["InfADValencias"] = valencias;

            ParameterModel paramProd = new ParameterModel();
            paramProd.FillParameters();
            Session["InfADDeslocProd"] = paramProd;

            DeslocacoesModel    infADTable      = new DeslocacoesModel();
            Tuple<DeslocacoesModel, ValenciaModel, ParameterModel, PisosModel> tp = new Tuple<DeslocacoesModel, ValenciaModel, ParameterModel, PisosModel>(infADTable, valencias, paramProd, pisos);

            return View(tp);
        }

        #endregion

        #region Filter Table

        [HttpGet]
        public JsonResult FilterData(string servicoCod, string ultLocalCod, string viewOnlocal, string pisoCod)
        {
            bool onlyOnlocal = (Convert.ToInt32(viewOnlocal) == 1 ? true : false);
            UserInfo uinfo = Session[Constants.SS_USER] as UserInfo;

            List<LDFTableField> listFields = new List<LDFTableField>();
            listFields.Add(new LDFTableField("COD_SERV", servicoCod));
            listFields.Add(new LDFTableField("U_LOCAL", ultLocalCod));
            listFields.Add(new LDFTableField("PISO", pisoCod));
            if (onlyOnlocal)
                listFields.Add(new LDFTableField("U_LOCAL", "IS NOT NULL"));

            DeslocacoesModel deslocModel = new DeslocacoesModel();
            _model = deslocModel;
            _modelName = "LusiadasSolucaoWeb.Models.DeslocacoesModel";

            return LoadLDFTable(1, null, JsonConvert.SerializeObject(listFields));
        }


        #endregion

        #region Ajax Methods

        [HttpPost]
        public JsonResult UpdateDeslocRow(string itemRow, string deslocCod)
        {
            UserInfo uinfo = Session[Constants.SS_USER] as UserInfo;
            DeslocacoesModel infADTable = new DeslocacoesModel();
            bool res = infADTable.UpdateRow(uinfo, itemRow, deslocCod);
            return Json(false);
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
            Session["DeslocProd_TDOENTE"] = tdoente;
            Session["DeslocProd_DOENTE"] = doente;

            ParameterModel paramProd = (ParameterModel)Session["InfADDeslocProd"];
            ValenciaModel valencias = (ValenciaModel)Session["InfADValencias"];

            DeslocProdModel deslocProdTable = new DeslocProdModel();
            deslocProdTable.tdoente = tdoente;
            deslocProdTable.doente = doente;
            deslocProdTable.serv = curserv;

            Session["DeslocProd_NCONS"] = deslocProdTable.ncons = ncons;
            Session["DeslocProd_TEPIS"] = deslocProdTable.tEpis = tEpis;
            Session["DeslocProd_EPIS"] = deslocProdTable.epis = epis;

            Tuple<DeslocProdModel, ParameterModel, ValenciaModel> tp
                = new Tuple<DeslocProdModel, ParameterModel, ValenciaModel>
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
            DeslocProdModel prod = new DeslocProdModel();
            UserInfo uinfo = Session[Constants.SS_USER] as UserInfo;
            bool res = prod.AddNewRow(uinfo, tdoente, doente, ncons, tEpis, epis, selProd, selOrig, selDest);

            return Json(res);
        }

        [HttpPost]
        public JsonResult UpdateDeslocProd(string itemRow, string selDest)
        {
            DeslocProdModel prod = new DeslocProdModel();
            UserInfo uinfo = Session[Constants.SS_USER] as UserInfo;

            bool res = prod.UpdateRow(uinfo, itemRow, selDest);

            return Json(res);
        }


        #endregion

        //#region DeslocProd Table

        //[HttpPost]
        //public JsonResult LoadDeslocProdTable()
        //{
        //    if (Session[Constants.SS_USER] != null)
        //    {
        //        uinfo = (UserInfo)Session[Constants.SS_USER];
        //    }

        //    int itemsPerPage    = Convert.ToInt32(ConfigurationManager.AppSettings["PIMRowsPerPage"]);
        //    string tdoente      = (string)Session["DeslocProd_TDOENTE"];
        //    string doente       = (string)Session["DeslocProd_DOENTE"];


        //    deslocProdTable.LoadRows((ValenciaModel)Session["InfADValencias"], tdoente, doente);
        //    Session["DeslocProd_FULLTABLE"] = deslocProdTable.list_rows;
        //    Session["DeslocProd_FULLVW"]    = deslocProdTable.list_vwDeslocProd;

        //    deslocProdTable.list_rows = deslocProdTable.list_rows.Take(itemsPerPage).ToList();

        //    return Json(new { deslocProdTable.list_rows, pageCount = deslocProdTable.pageCount, rowCount = deslocProdTable.rowCount });
        //}


        //[HttpPost]
        //public JsonResult PageDeslocProdTable(int pageNumber, string[] orderData)
        //{
        //    var listItems = Session["DeslocProd_FULLTABLE"];

        //    deslocProdTable.PageTable(listItems, pageNumber, orderData);

        //    return Json(new { deslocProdTable.list_rows, pageCount = deslocProdTable.pageCount, rowCount = deslocProdTable.rowCount });
        //}

        //[HttpPost]
        //public JsonResult OrderDeslocProdTable(string[] orderValues)
        //{
        //    var listItems = Session["DeslocProd_FULLTABLE"];

        //    deslocProdTable.OrderTable(listItems, orderValues);

        //    return Json(new { deslocProdTable.list_rows, pageCount = deslocProdTable.pageCount, rowCount = deslocProdTable.rowCount });
        //}

        //#endregion

    }
}
