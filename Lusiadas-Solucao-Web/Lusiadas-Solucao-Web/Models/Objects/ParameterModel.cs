using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LDF.ParameterManager;
using System.Configuration;
using System.ComponentModel.DataAnnotations.Schema;

namespace LusiadasSolucaoWeb.Models
{
    public class ParameterModel
    {
        public List<Produtos> list_prods = new List<Produtos>();
        public List<Produtos> list_defs = new List<Produtos>();
        public List<string> list_destDoentes = new List<string>();
        public List<string> list_destProd = new List<string>();

        private string schema = "MEDICO";
        private string tabela = "TAB_CAMPODEF";
        private string condition = "";
  


        public void FillParameters()
        {
            list_prods = ParameterManager.GetParametersFrom<Produtos>("name="+(string)HttpContext.Current.Session[Constants.SS_LOCAL_CONN], schema, tabela, "CODIGO, NOME_CAMPO, VALOR, CODIGO_PAI", "CODIGO='PRODUTO_DESLOC'", condition);
        }

        public void FillDefinitions()
        {
            list_defs = ParameterManager.GetParametersFrom<Produtos>("name=" + (string)HttpContext.Current.Session[Constants.SS_LOCAL_CONN], schema, tabela, "CODIGO, NOME_CAMPO, VALOR, CODIGO_PAI", "CODIGO='HISTORICO'", condition);
        }

        public void FillDestDoente()
        {
            List<Produtos> list_destTmp = new List<Produtos>();
            list_destTmp = ParameterManager.GetParametersFrom<Produtos>("name=" + (string)HttpContext.Current.Session[Constants.SS_LOCAL_CONN], schema, tabela, "VALOR", "CODIGO='DEST_DOENTE'", condition);
            list_destDoentes = list_destTmp.Select(s => s.VALOR).ToList();
        }

        public void FillDestProd()
        {
            List<Produtos> list_destTmp = new List<Produtos>();
            list_destTmp = ParameterManager.GetParametersFrom<Produtos>("name=" + (string)HttpContext.Current.Session[Constants.SS_LOCAL_CONN], schema, tabela, "VALOR", "CODIGO='DEST_PROD'", condition);

            list_destProd=list_destTmp.Select(s => s.VALOR).ToList();
        }

    }

    public class Produtos
    {
        public string CODIGO { get; set; }
        public string NOME_CAMPO { get; set; }
        public string VALOR { get; set; }
        public string CODIGO_PAI { get; set; }
    }
}