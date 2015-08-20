using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMDAL
{
    public class DALAtPermanente
    {

        public List<VwDashboardATP> GetListATP()
        {
            try
            {
                DBAtPermanenteContext efATP = new DBAtPermanenteContext();
                return efATP.vwDashboardATP.ToList();
            }
            catch (Exception err)
            {

            }
            return null;
        }

    }
}
