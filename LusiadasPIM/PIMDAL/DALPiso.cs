using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMDAL
{
    public class DALPiso
    {

        public List<VwPisosIntern> GetListPisos()
        {
            try
            {
                DBInternamentoContext efInt = new DBInternamentoContext();
                return efInt.vwPisos.ToList();

            }
            catch (Exception err)
            {

            }
            return null;
        }

    }
}
