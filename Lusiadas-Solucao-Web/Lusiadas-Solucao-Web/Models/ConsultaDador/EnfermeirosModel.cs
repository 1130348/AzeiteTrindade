using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LusiadasDAL;

namespace LusiadasSolucaoWeb.Models
{
    public class EnfermeirosModel
    {
        public List<TblSDPessHosp> listEnfermeiros = new List<TblSDPessHosp>();

        public EnfermeirosModel()
        {
            DALSDServ dal = new DALSDServ();
            listEnfermeiros = dal.GetEnfermeiros();
        }

    }
}