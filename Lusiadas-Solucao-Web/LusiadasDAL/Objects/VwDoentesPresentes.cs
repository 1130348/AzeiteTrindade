using LDFHelper.Helpers;
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
    [Table("V_DOENTES_PRESENTES_V2", Schema = "MEDICO")]
    public class VwDoentesPresentes
    {
        public string T_DOENTE { get; set; }
        [Key, Column(Order = 0)]  public string DOENTE        { get; set; }
        public string NOME    { get; set; }
        public Nullable<System.DateTime> DT_CONS { get; set; }
        public string HR_CONS { get; set; }
        public string COD_SERV { get; set; }
        public string DESCR_SERV { get; set; }
        public string MEDICO { get; set; }
        public string FLAG_ESTADO { get; set; }
        public string FLAG_URGENTE { get; set; }
        public string N_CONS { get; set; }
        [Key, Column(Order = 1)]  public string EPISODIO { get; set; }
        public string T_EPISODIO { get; set; }
        public string U_LOCAL { get; set; }
        public string U_LOCAL_DESCR { get; set; }
    }
}
