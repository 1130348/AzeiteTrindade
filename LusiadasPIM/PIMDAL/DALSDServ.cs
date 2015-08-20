using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMDAL
{
    public class DALSDServ
    {

        public List<TblSDServ> GetValencia()
        {
            try
            {
                DBInternamentoContext efInt = new DBInternamentoContext();
                return efInt.tblSdServ.Where(q => (q.FLG_ACTIVO == null || q.FLG_ACTIVO == "S") && q.PODE_CONS == "S").OrderBy(q => q.DESCR_SERV).ToList();
            }
            catch (Exception err)
            {

            }
            return null;
        }

        public List<TblSDPessHosp> GetInfoPessHosp(string user)
        {
            try
            {
                DBInternamentoContext efInt = new DBInternamentoContext();
                return efInt.tblSdPessHosp.Where(q => q.USER_SYS.ToUpper() == user.ToUpper()).ToList();
            }
            catch (Exception err)
            {
                throw err;
            }
            return null;
        }

    }
}
