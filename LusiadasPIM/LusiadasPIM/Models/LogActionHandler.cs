using LDF.ErrorLogging.Logging;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace LusiadasSolucaoWeb.Models
{
    
    public class LogActionHandler : ActionFilterAttribute
    {
        private int _level              = 0;
        private string _description     = "";
        private List<string> parameters = new List<string>();

        public LogActionHandler() { }

        public LogActionHandler(int level, string description)
        {
            _level          = level;
            _description    = description;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            foreach (var parameter in filterContext.ActionParameters)
                parameters.Add(string.Format("{0}: {1}", parameter.Key, parameter.Value));

            Log("OnActionExecuting", filterContext.RouteData);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("OnResultExecuting", filterContext.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log("OnResultExecuted", filterContext.RouteData);
        }

        private void Log(string methodName, RouteData routeData)
        {
            var controllerName  = routeData.Values["controller"];
            var actionName      = routeData.Values["action"];
            string message      = string.Format("{0} controller: {1} action: {2}", methodName, controllerName, actionName);

            if (_level == 0)
            {
                if (String.IsNullOrEmpty(_description))
                    _description = message;

                Logger.LogInformation(_description);
            }

        }
    }
}