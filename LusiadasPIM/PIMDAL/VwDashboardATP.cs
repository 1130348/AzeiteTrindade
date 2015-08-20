using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMDAL
{
    [Table("V_DASHBOARD_ATP", Schema = "MEDICO")]
    public class VwDashboardATP
    {
        [Key, Column(Order=0)]
        public string T_DOENTE  { get; set; }
        [Key, Column(Order = 1)]

        public string DOENTE    { get; set; }
        public string NOME      { get; set; }
        public System.Nullable<DateTime> DT_NASC { get; set; }
        public string SEXO                  { get; set; }
        public DateTime DT_CONS             { get; set; }
        public DateTime HR_CONS             { get; set; }
        public string N_CONS                { get; set; }
        public string TRIAGEM               { get; set; }
        public string NOTA_MEDICA           { get; set; }
        public string BOX_APLICAVEL         { get; set; }
        public string BOX_PRESENTE          { get; set; }
        public string CADEIRAO_PRESENTE     { get; set; }
        public string PENSO_APLICAVEL       { get; set; }
        public string PENSO_REGISTO         { get; set; }
        public string ANALISES_SOLICITADAS  { get; set; }
        public string COLHEITA_OK           { get; set; }
        public string ANALISES_OK           { get; set; }

        public int? MEDICACAO_PRESCRITA_PCE         { get; set; }
        public int? MEDICACAO_PRESCRITA_GLINTT      { get; set; }
        public int? MEDICACAO_ADMINISTRADA_AMB      { get; set; }
        public int? MEDICACAO_ADMINISTRADA_GLINTT   { get; set; }
        public int? IMAGIOLOGIA_REQUESITADOS        { get; set; }
        public int? IMAGIOLOGIA_REALIZADOS          { get; set; }
        public int? ECG_REQUESITADOS                { get; set; }
        public int? ECG_REALIZADOS                  { get; set; }
        public int? CEXTERNA_REQUISITADOS           { get; set; }
        public int? CEXTERNA_REALIZADOS             { get; set; }
    }
}
