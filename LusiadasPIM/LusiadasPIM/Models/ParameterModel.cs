using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LDF.ParameterManager;
using System.Configuration;

namespace LusiadasSolucaoWeb.Models
{
    public class ParameterModel
    {
        public List<Produtos> list_prods = new List<Produtos>();
        public void FillParameters()
        {
            list_prods = ParameterManager.GetParametersFrom<Produtos>("name=DBConnectionPIM", "MEDICO", "TAB_CAMPODEF", "CODIGO, NOME_CAMPO, VALOR", "CODIGO='PRODUTO_DESLOC'");
        }
    }


    public class Produtos
    {
        public string CODIGO { get; set; }
        public string NOME_CAMPO { get; set; }
        public string VALOR { get; set; }
    }
}