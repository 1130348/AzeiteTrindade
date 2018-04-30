﻿using LDFHelper.Helpers;
using LusiadasSolucaoWeb.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.Mvc;

namespace LusiadasSolucaoWeb.Controllers
{
    public class ATPDashController : LDFTableController
    {
        UserInfo uinfo = new UserInfo();

        //[AuthenticationHandler(Auth.VER_PAGINA, Constants.SS_AUTH)]
        //[LogActionHandler(0, "Index ATP")]
        //[LogErrorHandler(Constants.WEB_GENERIC_ERROR)]
        //[Route("/Dash", Name = "DashBoard")]
     

        /*public ActionResult Dashboard()
        {
           
            ConfigurationManager.AppSettings["TableRowsPerPage"] = 20.ToString();
            ATPModel atpTable = new ATPModel(20);
            return View(atpTable);

        }*/

        //[Route("/Dash/{num}", Name = "DashBoard")]
        public ActionResult Dash(int num)
        {
            Session.Remove("Tabela");


            Session["Tabela"] = num;
            if (num==null||num<=0)
            {


                ATPDashModel atpTable = new ATPDashModel(20);
                return View(atpTable);
            }
            else
            {
                
                ATPDashModel atpTable = new ATPDashModel(num);
                return View(atpTable);
            }
            
            
        }

       

        [HttpGet]
        public string GetDados()
        {
            
            GetData();

            return Globals.nDoentes+";"+Globals.semTriagem+";"+Globals.semNota;

        }


        public JsonResult LoadTableDash(int pagen)
        {
            //if (Session[Constants.SS_USER] != null)
            //{
            //    uinfo = (UserInfo)Session[Constants.SS_USER];
            //}

            //int itemsPerPage = Convert.ToInt32(ConfigurationManager.AppSettings["PIMRowsPerPage"]);


            //atpTable.LoadRows();
            //Session["FULL_TABLE"] = Session["NEW_TABLE"] = atpTable.list_rows;
            //Session["VWFULL_TABLE"] = atpTable.list_vwDashboardATP;

            //atpTable.list_rows = atpTable.list_rows.Take(itemsPerPage).ToList();

            //return Json(new { atpTable.list_rows, pageCount = atpTable.pageCount, rowCount = atpTable.rowCount });


            UserInfo uinfo = Session[Constants.SS_USER] as UserInfo;

            List<LDFTableField> listFields = new List<LDFTableField>();


            //Adicionar Filtros CodigoDoente
            //listFields.Add(new LDFTableField("DOENTE", "563388"));



            ATPDashModel atpModel = new ATPDashModel((int)Session["Tabela"]);
            _model = atpModel;
            _modelName = "LusiadasSolucaoWeb.Models.ATPDashModel";

            JsonResult res = LoadLDFTableDash(pagen, null, JsonConvert.SerializeObject(listFields));

            return res;

        }

        public void GetData()
        {

            try
            {

                //get data 
                string oradb = "User Id=medico;Password=medico;Data Source=BDHLUQL2";
                OracleConnection conn = new Oracle.ManagedDataAccess.Client.OracleConnection(oradb);  // C#
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select count(*) from MEDICO.V_DASH_DESLOC_ATP_V3";
                cmd.CommandType = CommandType.Text;
                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {

                    while (dr.Read())
                    {
                        Globals.nDoentes = dr["COUNT(*)"].ToString();

                    }

                }
                else
                {
                    Globals.nDoentes = "Sem Dados";
                }

                OracleCommand cmd2 = new OracleCommand();
                cmd2.Connection = conn;
                cmd2.CommandText = "select count(*) from MEDICO.V_DASH_DESLOC_ATP_V3 where COR_TRIAGEM IS NULL";
                cmd2.CommandType = CommandType.Text;
                OracleDataReader dr2 = cmd2.ExecuteReader();

                if (dr2.HasRows)
                {

                    while (dr2.Read())
                    {
                        Globals.semTriagem = dr2["COUNT(*)"].ToString();

                    }

                }
                else
                {
                    Globals.semTriagem = "Sem Dados";
                }

                OracleCommand cmd3 = new OracleCommand();
                cmd3.Connection = conn;
                cmd3.CommandText = "select count(*) from MEDICO.V_DASH_DESLOC_ATP_V3 where DT_NOTA_MEDICA IS NULL";
                cmd3.CommandType = CommandType.Text;
                OracleDataReader dr3 = cmd3.ExecuteReader();

                if (dr3.HasRows)
                {

                    while (dr3.Read())
                    {
                        Globals.semNota = dr3["COUNT(*)"].ToString();

                    }

                }
                else
                {
                    Globals.semNota = "Sem Dados";
                }





                conn.Dispose();

            }
            catch (Exception err)
            {
                err.InnerException.ToString();
                string var=err.InnerException.ToString();
                Globals.nDoentes = "Sem Dados";
                Globals.semNota = "Sem Dados";
                Globals.semTriagem = "Sem Dados";

            }


        }

        //[HttpPost]
        //public JsonResult PageTable(int pageNumber, string[] orderData)
        //{
        //    var listItems = Session["FULL_TABLE"];
        //    if (Session["NEW_TABLE"] != null)
        //        listItems = Session["NEW_TABLE"];

        //    atpTable.PageTable(listItems, pageNumber, orderData);

        //    return Json(new { atpTable.list_rows, pageCount = atpTable.pageCount, rowCount = atpTable.rowCount });
        //}


    }
}