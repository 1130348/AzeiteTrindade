using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LusiadasDAL
{
    [Table("tblMenu")]
    public class TblMenu
    {
        [Key, Column(Order = 0)]
        public int MENU_ID { get; set; }
        [Key, Column(Order = 1)]
        public int OPTION_ID { get; set; }
    }

    // Custom comparer for the Product class
    class MenuComparer : IEqualityComparer<TblMenu>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(TblMenu x, TblMenu y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x.MENU_ID == y.MENU_ID && x.OPTION_ID == y.OPTION_ID;
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(TblMenu menu)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(menu, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashMenuIDName = menu.MENU_ID == 0 ? 0 : menu.MENU_ID.GetHashCode();

            //Get hash code for the Code field.
            int hashOptionID = menu.OPTION_ID.GetHashCode();

            //Calculate the hash code for the product.
            return hashMenuIDName ^ hashOptionID;
        }

    }


}
