using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LusiadasDAL
{
    [Table("V_VALENCIAS_FREQ", Schema = "MEDICO")]
    public class VwValenciasFreq
    {
        [Key, Column(Order = 0)]
        public string COD_SERV { get; set; }
        public string COD_ESPEC { get; set; }
        public string DESCR_SERV { get; set; }
        public string LOCAL_SERV { get; set; }
        public int? LOT_POSS { get; set; }
        public int? LOT_APROV { get; set; }
        public string OBS { get; set; }
        public string USER_CRI { get; set; }
        public DateTime DT_CRI { get; set; }
        public string USER_ACT { get; set; }
        public Nullable<System.DateTime> DT_ACT { get; set; }
        public string FLG_ACTIVO { get; set; }
        public string PODE_CONS { get; set; }


    }
}
