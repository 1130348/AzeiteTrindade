using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LusiadasDAL
{
    public class DALConsultaDor
    {

        public bool AddDaDor(string conn, TblConsultaDor daDor)
        {
            try
            {
                DBConsultaDorContext ctxDaDor = new DBConsultaDorContext(conn);
                ctxDaDor.tblConsultaDor.Add(daDor);
                ctxDaDor.SaveChanges();
                return true;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
