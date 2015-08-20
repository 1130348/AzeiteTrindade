using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LDF.ErrorLogging.Logging;
using System.Net;
using LDF.Authentication;
using System.ComponentModel;

namespace LusiadasSolucaoWeb.Models
{
    public class AuthenticationHandler : AuthorizeAttribute, IAuthorizationFilter
    {
        public string _action = "";

        public AuthenticationHandler(string action)
        {
            _action = action;
        }

        public AuthenticationHandler() { }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            LDFAuthentication auth = new LDFAuthentication();
            if (HttpContext.Current.Session[Constants.SS_AUTH] != null)
                auth = (LDFAuthentication)HttpContext.Current.Session[Constants.SS_AUTH];
            else {
                filterContext.Result = CreateActionResult(filterContext, 401);
            }

            if (!auth.HasPermission(Convert.ToInt32(Enum.Parse(typeof(Teste), _action))))
            {
                filterContext.Result = CreateActionResult(filterContext, 401);
            }

            return;
        }

        protected virtual ActionResult CreateActionResult(AuthorizationContext filterContext, int statusCode)
        {
            var ctx = new ControllerContext(filterContext.RequestContext, filterContext.Controller);
            var statusCodeName = ((HttpStatusCode)statusCode).ToString();

            var viewName = SelectFirstView(ctx,
                                           String.Format("~/Views/Error/{0}.cshtml", statusCodeName),
                                           "~/Views/Error/General.cshtml",
                                           "NoAuthorization",
                                           "Error");

            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            var result = new ViewResult
            {
                ViewName = viewName,
                ViewData = new ViewDataDictionary<string>("NO AUTHORIZATION"),
            };
            result.ViewBag.StatusCode = statusCode;
            return result;
        }

        protected string SelectFirstView(ControllerContext ctx, params string[] viewNames)
        {
            return viewNames.First(view => ViewExists(ctx, view));
        }

        protected bool ViewExists(ControllerContext ctx, string name)
        {
            var result = ViewEngines.Engines.FindView(ctx, name, null);
            return result.View != null;
        }
    }

    public enum Teste
    {
        [Description("VIEW_ATP")]
        VIEW_ATP = 1
    }
}