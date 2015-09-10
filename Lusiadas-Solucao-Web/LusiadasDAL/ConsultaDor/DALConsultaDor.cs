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

        public bool AddDador(string conn, TblConsultaDor dador)
        {
            try
            {
                DBConsultaDorContext ctxDador = new DBConsultaDorContext(conn);
                ctxDador.tblConsultaDor.Add(dador);
                ctxDador.SaveChanges();
                return true;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
