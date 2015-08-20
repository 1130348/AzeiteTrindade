using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PIMDAL;

namespace LusiadasSolucaoWeb.Models
{
    public class PisosModel
    {
        public List<Piso> listPisos = new List<Piso>();

        internal void LoadPisos()
        {
            DALPiso dal = new DALPiso();
            List<VwPisosIntern> listItems = dal.GetListPisos();
            
            listPisos = (from item in listItems
                        select new Piso {
                            DESCR_SERV  = String.Format("{0} ({1})", item.DESCR_SERV, item.COD_SERV),
                            COD_SERV    = item.COD_SERV
                        }).ToList();
        }
    }

    public class Piso
    {
        public string DESCR_SERV { get; set; }
        public string COD_SERV { get; set; }
    }

}