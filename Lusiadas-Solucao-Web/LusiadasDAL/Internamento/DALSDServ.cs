using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LusiadasDAL
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
                throw err;
            }
        }

        public List<VwValenciasFreq> GetValenciaParametrizadas()
        {
            try
            {
                DBInternamentoContext efInt = new DBInternamentoContext();

                return efInt.VwValenciasFreq.ToList();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<VwValenciasProdFreq> GetValenciaParametrizadasProdutos()
        {
            try
            {
                DBInternamentoContext efInt = new DBInternamentoContext();

                return efInt.VwValenciasProdFreq.ToList();
            }
            catch (Exception err)
            {
                throw err;
            }
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
        }

        
        public TblSDPessHosp GetInfoPessHospNmecan(string n_mecan)
        {
            try
            {
                TblSDPessHosp res = new TblSDPessHosp();
                DBInternamentoContext efInt = new DBInternamentoContext();
                if (n_mecan != "")
                {
                    res= efInt.tblSdPessHosp.Where(q => q.N_MECAN.ToUpper() == n_mecan.ToUpper()).First();
                }
                return res;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<TblSDPessHosp> GetEnfermeiros()
        {
            try
            {
                DBInternamentoContext efInt = new DBInternamentoContext();
                return efInt.tblSdPessHosp.Where(q => q.FLG_ACTIVO == "S" && q.T_PESS_HOSP == "ENF").ToList();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<TblPessHospDet> GetAnestesistas()
        {
            try
            {
                var codAnestesista = "2101";
                DBInternamentoContext efInt = new DBInternamentoContext();
                return efInt.tblPessHospDet.Where(q => q.COD_SERV == codAnestesista).ToList();
            }
            catch (Exception err)
            {
                throw err;
            }
        }

    }
}
