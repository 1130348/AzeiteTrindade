using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LusiadasDAL;

namespace LusiadasSolucaoWeb.Models
{
    public class ValenciaModel
    {
        public List<Valencia> listValencias = new List<Valencia>();

        internal void LoadValencias(List<string> listCods)
        {
            DALSDServ dal = new DALSDServ();
            List<TblSDServ> listItems = dal.GetValencia();

            listValencias = (from item in listItems
                             select new Valencia
                             {
                                DESCR_SERV  = item.DESCR_SERV,
                                COD_SERV    = item.COD_SERV,
                                ISMINE      = ( listCods.Contains(item.COD_SERV) ? true : false)
                            }).ToList();
        }
    }

    public class Valencia
    {
        public string   DESCR_SERV  { get; set; }
        public string   COD_SERV    { get; set; }
        public bool     ISMINE      { get; set; }
    }

}