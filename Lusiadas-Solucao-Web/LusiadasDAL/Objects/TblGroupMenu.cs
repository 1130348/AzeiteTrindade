using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LusiadasDAL
{
    [Table("tblGroupMenu")]
    public class TblGroupMenu
    {
        [Key, Column(Order = 0)]
        public int GROUP_ID { get; set; }
        public int MENU_ID { get; set; }

    }
}
