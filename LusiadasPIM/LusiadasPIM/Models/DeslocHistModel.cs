using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PIMDAL;

namespace LusiadasSolucaoWeb.Models
{
    public class DeslocHistModel
    {
        public List<Deslocacao> listDesloc = new List<Deslocacao>();

        public void LoadHistDesloc(string tdoente, string doente)
        {
            DALDeslocacoes dal = new DALDeslocacoes();
            List<VwDesloc> listItems = dal.GetHistDesloc(tdoente, doente);

            listDesloc = (from vw in listItems  
                          select new Deslocacao
                          {
                              T_EPISODIO    = vw.T_EPISODIO,
                              EPISODIO      = vw.EPISODIO,
                              DT_DESLOC     = vw.DT_DESL,
                              COD_SERV      = vw.COD_SERV,
                              DESCR_SERV    = vw.DESCR_SERV,
                              CONTACTO_SERV = vw.CONTACTO_SERV,
                              NOME_USER     = String.Format("{0} {1}", vw.TITULO, vw.ABR)

                          }).ToList();
        }
    }


    public class Deslocacao
    {
        public string T_EPISODIO { get; set; }
        public string EPISODIO { get; set; }
        public DateTime DT_DESLOC { get; set; }
        public string COD_SERV { get; set; }
        public string DESCR_SERV { get; set; }
        public string CONTACTO_SERV { get; set; }
        public string NOME_USER { get; set; }

    }
}