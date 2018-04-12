using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LusiadasDAL
{
    public class DALDeslocacoes
    {

        #region Doentes Presentes

        public List<VwDoentesPresentes> GetListDoentesPresentes()
        {
            try
            {
                DBDeslocacoesContext efInt = new DBDeslocacoesContext();
                return efInt.vwDoentesPresentes.OrderBy(q => q.DESCR_SERV).ToList();
            }
            catch (Exception err)
            {
                string str = err.Message;
            }
            return null;
        }

        public List<VwDoentesPresentes> GetCondListDoentesPresentes(string servicoCod, string ultLocalCod, bool viewOnlocal)
        {
            try
            {
                DBDeslocacoesContext efInt = new DBDeslocacoesContext();
                List<VwDoentesPresentes> listVw = efInt.vwDoentesPresentes.OrderBy(q => q.DESCR_SERV).ToList();

                if (!String.IsNullOrEmpty(servicoCod))
                    listVw = listVw.Where(q => q.COD_SERV == servicoCod).ToList();
                if (!String.IsNullOrEmpty(ultLocalCod))
                    listVw = listVw.Where(q => q.U_LOCAL == ultLocalCod).ToList();
                if (viewOnlocal)
                    listVw = listVw.Where(q => q.U_LOCAL != null).ToList();
                return listVw;
            }
            catch (Exception err)
            {
                string str = err.Message;
            }
            return null;
        }

        #endregion

        #region Desloc

        public bool InsertDoenteDesloc(TblDesloc desloc)
        {
            bool res = false;

            try
            {
                DBDeslocacoesContext efInt = new DBDeslocacoesContext();
                efInt.tblDesloc.Add(desloc);
                efInt.SaveChanges();
                res = true;

            } catch (Exception err)
            {
            }

            return res;
        }

        public List<VwDesloc> GetHistDesloc(string tdoente, string doente)
        {
            try
            {
                DBDeslocacoesContext efInt = new DBDeslocacoesContext();
                return efInt.vwDesloc.Where(q => q.T_DOENTE == tdoente && q.DOENTE == doente).ToList();
            }
            catch (Exception err)
            {
                string str = err.Message;
            }
            return null;
        }

        public List<VwDeslocProd> ListDeslocProd(string tdoente, string doente)
        {
            try
            {
                DBDeslocacoesContext efInt = new DBDeslocacoesContext();
                return efInt.vwDeslocProd.Where(q => q.T_DOENTE == tdoente && q.DOENTE == doente).ToList();
            }
            catch (Exception err)
            {
                string str = err.Message;
            }
            return null;
        }

        public bool InsertDeslocProd(TblDeslocProd deslocprod)
        {
            bool res = false;

            try
            {
                DBDeslocacoesContext efInt = new DBDeslocacoesContext();
                efInt.tblDeslocProd.Add(deslocprod);
                efInt.SaveChanges();
                res = true;

            }
            catch (Exception err)
            {
            }

            return res;
        }

        public VwDoentesPresentes GetDeslocUser(string tdoente, string doente)
        {
            try
            {
                DBDeslocacoesContext efInt = new DBDeslocacoesContext();
                List<VwDoentesPresentes> list = efInt.vwDoentesPresentes.Where(q => q.T_DOENTE == tdoente && q.DOENTE == doente).OrderByDescending(q => q.DT_CONS).ToList();
                return list.First();
            }
            catch
            {
                return null;
            }
        }

        #endregion


        public VwDeslocProd GetDeslocProdUser(string tdoente, string doente)
        {
            try
            {
                DBDeslocacoesContext efInt = new DBDeslocacoesContext();
                List<VwDeslocProd> list = efInt.vwDeslocProd.Where(q => q.T_DOENTE == tdoente && q.DOENTE == doente).OrderByDescending(q => q.DT_CRI).ToList();
                return list.First();
            }
            catch
            {
                return null;
            }
        }

        
    }
}
