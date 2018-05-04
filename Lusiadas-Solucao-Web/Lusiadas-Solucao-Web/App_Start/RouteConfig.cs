using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LusiadasSolucaoWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            

            routes.MapRoute("", "Dash/{hosp}/{num}",
                 new { controller = "ATPDash", action = "Dash", hosp = UrlParameter.Optional, num = UrlParameter.Optional });

            routes.MapRoute("", "Dashboard/{hosp}",
                 new { controller = "ATP", action = "Dash", hosp = UrlParameter.Optional });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

          
        }
    }
}