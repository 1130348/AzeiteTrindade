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
using System.ComponentModel;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace LusiadasSolucaoWeb.Controllers
{
    public class SecretController : LDFTableController
    {
        #region Variables
        
        UserInfo                uinfo       = new UserInfo();
        
        #endregion

 

        public ActionResult Index()
        {
           

            return View(); 
            
        }

        


    }
}
