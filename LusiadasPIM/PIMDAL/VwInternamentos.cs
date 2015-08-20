using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMDAL
{
    [Table("V_INTERNAMENTOS", Schema = "MEDICO")]
    public class VwInternamentos
    {
        [Key, Column(Order = 0)]
        public string T_DOENTE { get; set; }
        [Key, Column(Order = 1)]
        public string DOENTE { get; set; }
        public string NOME { get; set; }
        [Key, Column(Order = 2)]
        public string N_INT { get; set; }
        [Key, Column(Order = 3)]
        public string COD_SERV { get; set; }
        public string CAMA { get; set; }
        public string ENFERMARIA { get; set; }
        public string SALA { get; set; }
        public string COD_SERV_VALENCIA { get; set; }
        [Key, Column(Order = 4)]
        public System.DateTime DT_INT { get; set; }
        public string HORA_INT { get; set; }
        public Nullable<System.DateTime> DT_ALTA { get; set; }
        public string HR_ALTA { get; set; }
        public Nullable<System.DateTime> DT_OPER { get; set; }
        public string HR_INI { get; set; }
        public string DURACAO { get; set; }
        public string DESCR_SERV { get; set; }
        public string T_EPISODIO { get; set; }
        [Key, Column(Order=5)]
        public string EPISODIO { get; set; }
        public string N_MECAN { get; set; }
        public string NOME_MEDICO { get; set; }
        public string DESC_OPER { get; set; }
        public string T_EPISODIO_PAI { get; set; }
        public string EPISODIO_PAI { get; set; }
        public string TRI_NUM { get; set; }
        public string MOT_INTERN { get; set; }
    }
}
