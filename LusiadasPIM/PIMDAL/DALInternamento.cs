using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMDAL
{
    public class DALInternamento
    {

        public List<VwInternamentos> GetListInternamento()
        {
            try
            {
                DBInternamentoContext efInt = new DBInternamentoContext();
                return efInt.vwInternamentos.Where(q => q.DT_ALTA == null).ToList();
            }
            catch (Exception err)
            {

            }
            return null;
        }

    }
}
