using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LDFHelper.Helpers;

namespace LusiadasDAL
{
    [Table("V_DASHBOARD_ATP", Schema = "MEDICO")]
    public class VwDashboardATP : LDFTableOracleObject
    {
        [Key, Column(Order=0)] [Description("Doente")] [LDFTableJoin("DOENTE")] public string T_DOENTE   { get; set; }
        [Description("Nome")] public string NOME      { get; set; }
        [Description("Hora")] public DateTime HR_CONS                       { get; set; }
        [Description("Triagem")] public string TRIAGEM                      { get; set; }
        [Description("Nota Méd.")] public string NOTA_MEDICA              { get; set; }
        [Description("Box")] public string BOX_APLICAVEL                    { get; set; }
        [Description("Cadeirão")] public string CADEIRAO_PRESENTE           { get; set; }
        [Description("Penso")] public string PENSO_APLICAVEL                { get; set; }
        [Description("Análises")] public string ANALISES_OK                 { get; set; }
        [Description("Medicação")] public int? MEDICACAO_PRESCRITA_PCE      { get; set; }
        [Description("Imagiologia")] public int? IMAGIOLOGIA_REQUESITADOS   { get; set; }
        [Description("ECG")] public int? ECG_REQUESITADOS                   { get; set; }
        [Description("Consulta Ext.")] public int? CEXTERNA_REQUISITADOS { get; set; }

        
        [Key, Column(Order = 1)] [LDFTableIsVisible(false)] public string DOENTE     { get; set; }
        [LDFTableIsVisible(false)] public System.Nullable<DateTime> DT_NASC { get; set; }
        [LDFTableIsVisible(false)] public string SEXO { get; set; }
        [LDFTableIsVisible(false)] public DateTime DT_CONS { get; set; }
        [LDFTableIsVisible(false)] public string N_CONS { get; set; }
        [LDFTableIsVisible(false)] public string BOX_PRESENTE { get; set; }
        [LDFTableIsVisible(false)] public string PENSO_REGISTO                  { get; set; }
        [LDFTableIsVisible(false)] public string ANALISES_SOLICITADAS           { get; set; }
        [LDFTableIsVisible(false)] public string COLHEITA_OK                    { get; set; }

        [LDFTableIsVisible(false)] public int? MEDICACAO_PRESCRITA_GLINTT       { get; set; }
        [LDFTableIsVisible(false)] public int? MEDICACAO_ADMINISTRADA_AMB       { get; set; }
        [LDFTableIsVisible(false)] public int? MEDICACAO_ADMINISTRADA_GLINTT    { get; set; }
        [LDFTableIsVisible(false)] public int? IMAGIOLOGIA_REALIZADOS           { get; set; }
        [LDFTableIsVisible(false)] public int? ECG_REALIZADOS                   { get; set; }
        [LDFTableIsVisible(false)] public int? CEXTERNA_REALIZADOS              { get; set; }
    }
}
