using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LusiadasDAL
{
    [Table("tblAuthGroup")]
    public class TblAuthGroup
    {
        [Key, Column(Order = 0)]
        public int GROUP_ID { get; set; }
        public string GROUP_NAME { get; set; }
    }
}
