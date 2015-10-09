using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LusiadasDAL;

namespace LusiadasSolucaoWeb.Models
{
    public class AnestesistasModel
    {
        public List<TblPessHospDet> listAnestesistas = new List<TblPessHospDet>();

        public AnestesistasModel()
        {
            DALSDServ dal = new DALSDServ();
            listAnestesistas = dal.GetAnestesistas();
        }

    }
}