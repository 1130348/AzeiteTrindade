using LDFHelper.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace LusiadasSolucaoWeb.Controllers
{
    public class LDFTableController : LayoutController
    {
        public List<LDFTableModel> _listModels = new List<LDFTableModel>();
        public LDFTableModel       _model      = null;
        public string _modelName = "";

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (requestContext.RouteData.Values.Count(q => q.Key == "id") == 1)
                _modelName = requestContext.RouteData.Values.First(q => q.Key == "id").Value.ToString();
        }

        [HttpGet]
        public JsonResult LoadLDFTable(int pageNumber = 1, string[] orderData = null, string filterData = "")
        {
 
            _model                  = GetSessionModel();
            Type thisType           = _model.GetType();
            try
            {
                _model.LoadTableData(pageNumber, orderData, filterData);
               
                    MethodInfo theMethod = thisType.GetMethod("LDFTableTreatData");
                    if (theMethod != null)
                        theMethod.Invoke(_model, null);  
                
            }catch (Exception err)
            {
                err.InnerException.ToString();
            }

            return Json(_model.GetJSON(), JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult LoadLDFTableDash(int pageNumber, string[] orderData = null, string filterData = "")
        {

            _model = GetSessionModel();
            Type thisType = _model.GetType();
            try
            {      
                _model.LoadTableData(pageNumber,orderData, filterData);

                MethodInfo theMethod = thisType.GetMethod("LDFTableTreatData");
                if (theMethod != null)
                    theMethod.Invoke(_model, null);

            }
            catch (Exception err)
            {
                err.InnerException.ToString();
            }

            return Json(_model.GetJSON(), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult EditLDFTable(string rowID, string fields)
        {
            bool res                    = false;
            _model                      = GetSessionModel();
            
            Type thisType = _model.GetType();
            
            MethodInfo theMethod = thisType.GetMethod("LDFTableUpdateTreatData");
            object[] funcParams = new object[] { rowID, fields };
            if (theMethod != null)
                if (theMethod.ReturnType==(typeof(string)))        
                    fields = theMethod.Invoke(_model, funcParams).ToString();
                else                                                          
                    theMethod.Invoke(_model, funcParams).ToString();

            theMethod        = thisType.GetMethod("LDFSaveData").MakeGenericMethod(_model.objType);
            funcParams = new object[] { rowID, fields,  Activator.CreateInstance(_model.objType)};
            res = (bool)theMethod.Invoke(_model, funcParams);

            return Json(_model.GetJSON(res));
        }

        [HttpGet]
        public JsonResult FilterLDFTable(List<LDFTableField> listFilters)
        {
            _model                  = GetSessionModel();
            Type thisType           = _model.GetType();
            MethodInfo theMethod = thisType.GetMethod("FilterData").MakeGenericMethod(_model.objType);
            theMethod.Invoke(_model, new object[] { listFilters });

            theMethod = thisType.GetMethod("LDFTableTreatData");
            if (theMethod != null)
                theMethod.Invoke(_model, null);
            
            return Json(_model.GetJSON(),
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddLDFTable(int pageNumber = 1, string[] orderData = null, string filterData = "", string insertData = "")
        {
            _model = GetSessionModel();
            Type thisType = _model.GetType();

            MethodInfo theMethod    = thisType.GetMethod("LDFInsertData").MakeGenericMethod(_model.objType);
            object[] funcParams     = new object[] { Activator.CreateInstance(_model.objType), insertData };
            bool res                = (bool)theMethod.Invoke(_model, funcParams);

            _model.LoadTableData(pageNumber, orderData, filterData);

            theMethod = thisType.GetMethod("LDFTableTreatData");
            if (theMethod != null)
                theMethod.Invoke(_model, null);

            return Json(_model.GetJSON());
        }

        [HttpPost]
        public JsonResult RemoveRow(string rowID)
        {
            _model                      = GetSessionModel();

            return Json(
                new { _model.list_rows, pageCount = _model.pageCount, rowCount = _model.rowCount },
                JsonRequestBehavior.AllowGet);
        }

        #region Aux Methods

        private LDFTableModel GetSessionModel()
        {
            try
            {
                var obj = Activator.CreateInstance(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, _modelName.Replace("_", "."));

            return (LDFTableModel)obj.Unwrap();
            }
            catch(Exception err)
            {
                string str = err.Message;
            }
            return null;
        }
       
        #endregion

    }
}