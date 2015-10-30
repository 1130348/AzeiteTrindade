using LusiadasDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LusiadasDAL
{
    public class DALMenu
    {
        public List<TblOptionsMenu> GetListOptions(List<TblMenu> listMenu)
        {
            try
            {
                DBMenuContext efMenu = new DBMenuContext();
                List<int> result = new List<int>();
                List<TblOptionsMenu> resultOptions = new List<TblOptionsMenu>();
                result = listMenu.Distinct().Select(q => q.OPTION_ID).ToList();
                foreach (int item in result)
                {
                    resultOptions = efMenu.tblOptionsMenu.Where(q => result.Contains(q.OPTION_ID)).ToList();
                }
                return resultOptions;
            }
            catch (Exception err)
            {

            }
            return null;
        }

        public List<TblMenu> GetMenus(List<string> groups)
        {
            List<int> listIDs = new List<int>();
            try
            {
                DBMenuContext efMenu = new DBMenuContext();
                List<int> listMenuGroup = new List<int>();

                //relaciona nomes dos grupos com respectivos IDs
                listIDs.AddRange(efMenu.tblAuthGroup.Where(q => groups.Contains(q.GROUP_NAME)).Select(s => s.GROUP_ID));

                //procura todos os menus que os grupos associados ao utilizador teem acesso
                listMenuGroup=efMenu.tblGroupMenu.Where(q=>listIDs.Contains(q.GROUP_ID)).Select(s=>s.MENU_ID).ToList();

                //retorna menus com IDs a que o utilizador tem direito
                return efMenu.tblMenu.Where(q => listMenuGroup.Contains(q.MENU_ID)).ToList();
            }
            catch (Exception err)
            {

            }
            return null;
        }
    }
}
