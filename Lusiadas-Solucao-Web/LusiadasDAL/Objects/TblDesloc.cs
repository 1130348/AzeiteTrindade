using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LusiadasDAL
{
    [Table("DESLOC", Schema = "MEDICO")]
    public class TblDesloc
    {
        [Key, Column(Order=0)]
        public string   T_DOENTE { get; set; }
        public string   DOENTE {get;set;}
        public string   N_CONS {get;set;}
        public string   T_EPISODIO {get;set;}
        public string   EPISODIO {get;set;}
        public string   COD_SERV {get;set;}
        public DateTime DT_DESL {get;set;}
        public string   USER_CRI {get;set;}
        public DateTime DT_CRI { get; set; }
        public char     FLG_POS_FINAL   { get; set; }

    }
}
