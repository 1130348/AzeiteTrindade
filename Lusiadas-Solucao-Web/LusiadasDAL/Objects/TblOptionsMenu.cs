using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LusiadasDAL
{
    [Table("tblOptionsMenu")]
    public class TblOptionsMenu
    {
        [Key, Column(Order = 0)]
        public int OPTION_ID { get; set; }
        public string DESCR { get; set; }
        public string ICON { get; set; }
        public int PARENT { get; set; }
        public int ORDER_MENU { get; set; }
        public string ACCAO { get; set; }
        public string CONTROLLER { get; set; }
    }
}
