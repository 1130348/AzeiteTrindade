using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMDAL
{
    [Table("V_DESLOC", Schema = "MEDICO")]
    public class VwDesloc
    {
        [Key, Column(Order=0)]
        public string   T_DOENTE        { get; set; }
        [Key, Column(Order = 1)]
        public string   DOENTE          { get; set; }
        public string   N_CONS          { get; set; }
        [Key, Column(Order = 2)]
        public string   T_EPISODIO      { get; set; }
        [Key, Column(Order = 3)]
        public string   EPISODIO        { get; set; }
        [Key, Column(Order = 4)]
        public string   COD_SERV        { get; set; }
        public string   DESCR_SERV      { get; set; }
        public string   CONTACTO_SERV   { get; set; }
        [Key, Column(Order = 5)]
        public DateTime DT_DESL         { get; set; }
        public string   USER_CRI        { get; set; }
        [Key, Column(Order = 6)]
        public DateTime DT_CRI          { get; set; }
        public char     FLG_POS_FINAL   { get; set; }
        public string   TITULO          { get; set; }
        public string   ABR             { get; set; }
        public string   NOME            { get; set; }


    }
}
