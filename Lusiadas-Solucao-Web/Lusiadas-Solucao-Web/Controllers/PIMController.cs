using LusiadasSolucaoWeb.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Collections;
using LDFHelper.Helpers;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LusiadasSolucaoWeb.Controllers
{
    public class PIMController : LDFTableController
    {
        public ActionResult Index()
        {
            UserInfo uinfo = Session[Constants.SS_USER] as UserInfo;

            PisosModel pisos = new PisosModel();
            pisos.LoadPisos();
            ValenciaModel valencias = new ValenciaModel();
            valencias.LoadValencias(uinfo.listCodServ);
            PIMModel pimTable = new PIMModel();
            Tuple<PIMModel, PisosModel, ValenciaModel> tp = new Tuple<PIMModel, PisosModel, ValenciaModel>(pimTable, pisos, valencias);

            return View(tp);
        }

        #region Filter Table

        [HttpGet]
        public JsonResult FilterData(string valenciaCod, string pisoCod, string viewMine)
        {
            UserInfo uinfo = Session[Constants.SS_USER] as UserInfo;

            List<LDFTableField> listFields = new List<LDFTableField>();
            listFields.Add(new LDFTableField("COD_SERV_VALENCIA", valenciaCod));
            listFields.Add(new LDFTableField("COD_SERV", pisoCod));
            bool onlyMine = (Convert.ToInt32(viewMine) == 1 ? true : false);

            if (onlyMine && !String.IsNullOrEmpty(uinfo.numMecan))
                listFields.Add(new LDFTableField("N_MECAN", uinfo.numMecan));
            
            PIMModel pimTable = new PIMModel();
            _model = pimTable;
            _modelName = "LusiadasSolucaoWeb.Models.PIMModel";
            return LoadLDFTable(1, null, JsonConvert.SerializeObject(listFields));
        }

        #endregion

    }
}
