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
using LusiadasSolucaoWeb.Models.Secret;

namespace LusiadasSolucaoWeb.Controllers
{
    public class SecretController : LDFTableController
    {
        #region Variables
        
        UserInfo                uinfo       = new UserInfo();
        
        #endregion

 

        public ActionResult Index()
        {

            try
            {


                foreach (Pcs str in Globals.listaUsers)
                {

                    if (str.getData().CompareTo(DateTime.Now.Subtract(new TimeSpan(0, 0, 5, 0))) < 0)
                    {
                        Globals.listaUsers.Remove(str);


                    }

                    if (Globals.listaUsers.Count < 1)
                    {

                        break;
                    }


                }



                if (Globals.listaUsers.Count > 25)
                {
                    Globals.listaUsers.RemoveRange(0, 20);
                }



            }
            catch (Exception e)
            {

            }

            return View(); 
            
        }

        


    }
}
