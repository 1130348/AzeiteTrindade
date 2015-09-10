using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LusiadasDAL
{
    [Table("V_PISOS_INTERN", Schema = "MEDICO")]
    public class VwPisosIntern
    {
        public string DESCR_SERV { get; set; }
        [Key, Column(Order = 0)]
        public string COD_SERV { get; set; }
    }
}
