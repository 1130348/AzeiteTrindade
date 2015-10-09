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

        public void FillParameters()
        {
            list_prods = ParameterManager.GetParametersFrom<Produtos>("name="+(string)HttpContext.Current.Session[Constants.SS_LOCAL_CONN], "MEDICO", "TAB_CAMPODEF", "CODIGO, NOME_CAMPO, VALOR", "CODIGO='PRODUTO_DESLOC'", "");
        }

        public void FillDefinitions()
        {
            list_defs = ParameterManager.GetParametersFrom<Produtos>("name=" + (string)HttpContext.Current.Session[Constants.SS_LOCAL_CONN], "MEDICO", "TAB_CAMPODEF", "CODIGO, NOME_CAMPO, VALOR, CODIGO_PAI", "CODIGO='HISTORICO'", "");
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