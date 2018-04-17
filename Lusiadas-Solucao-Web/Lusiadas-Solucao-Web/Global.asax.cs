using LDF.Authentication;
using LDF.ErrorLogging;
using LDF.ErrorLogging.ErrorHandling;
using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;

namespace LusiadasSolucaoWeb
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LogErrorHandler());
            filters.Add(new AuthenticationHandler());
            filters.Add(new LogActionHandler());
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception err = Server.GetLastError();

            string app = ConfigurationManager.AppSettings[LDFErrorLoggingKeys.APP];
            string user = (Session[LDFErrorLoggingKeys.USER] != null ? (string)Session[LDFErrorLoggingKeys.USER] : "");
            string machine = HttpContext.Current.Request.UserHostAddress;
            string browser = HttpContext.Current.Request.UserAgent;
            string message = (ConfigurationManager.AppSettings["ErrorMessage"] != null ? ConfigurationManager.AppSettings["ErrorMessage"] : err.Message);

            if (Request.AcceptTypes.ToList().Count(q => q.ToLower().Contains("json")) > 0)
            {
                Response.Write("{ \"header\" : { \"status\" : false, \"message\" : \"" + message + "\"} }");
                ErrorHandler.ProcessError(Server.GetLastError(), machine, app, user, browser);
                Context.ClearError();
            }
            else
            {
                string page = "General";

                //ErrorHandler.ProcessError(Server.GetLastError(), machine, app, user, browser);
                if (err is HttpException)
                {
                    HttpException httpEx = (HttpException)err;
                    if (httpEx.GetHttpCode() == 404)
                        page = "~/Views/Error/Error404.cshtml";
                }
                Context.ClearError();
                
               Response.Redirect(page, true);
            }
        }

    }
}