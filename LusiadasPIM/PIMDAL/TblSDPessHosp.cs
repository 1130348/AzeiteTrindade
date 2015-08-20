using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMDAL
{
    [Table("SD_PESS_HOSP", Schema = "GH")]
    public class TblSDPessHosp
    {
        [Key, Column(Order=0)]
        public string   N_MECAN     { get; set; }
        public string   NOME        { get; set; }
        public string   ABR         { get; set; }
        public string   COD_SERV    { get; set; }
        public string   TITULO      { get; set; }
        public string   FLAG_QUADRO { get; set; }
        public string   T_PESS_HOSP { get; set; }
        public string   USER_SYS    { get; set; }
        public string   FLG_ACTIVO  { get; set; }
        public string   USER_CRI    { get; set; }
        public Nullable<System.DateTime> DT_CRI { get; set; }
        public string   USER_ACT    { get; set; }
        public Nullable<System.DateTime> DT_ACT { get; set; }
        public string   CCUSTO      { get; set; }

    }
}
