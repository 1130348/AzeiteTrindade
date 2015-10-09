using LusiadasSolucaoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace LusiadasSolucaoWeb
{
    public class CostumFilterAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            LoginModel userTmp = new LoginModel();
            UserInfo userLogged = new UserInfo();
            
            userLogged = (UserInfo)filterContext.HttpContext.Session.Contents[Constants.SS_USER];
            if (userLogged.catProfissional != "MED")
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary 
                { 
                    { "controller", "Home" }, 
                    { "action", "Index" } 
                });

            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            LoginModel userTmp = new LoginModel();
            UserInfo userLogged = new UserInfo();
            userTmp.UserName = user.ToString();
            userLogged = userTmp.GetUserInfo();
            if (userLogged.getcatProfissional() == "MED")
            {
                ViewResult result = new ViewResult();

                result.ViewName = "SecurityError";
                result.ViewBag.ErrorMessage = "You are not authorized to use this page. Please contact administrator!";
                filterContext.Result = result;

            }
        }


    }
}