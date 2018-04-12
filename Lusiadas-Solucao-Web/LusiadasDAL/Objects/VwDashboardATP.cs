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
    [Table("V_DASH_DESLOC_ATP_V3", Schema = "MEDICO")]
    public class VwDashboardATP : LDFTableOracleObject
    {
        [Key, Column(Order=0)] [LDFTableOrder] [Description("Doente")] public string DOENTE   { get; set; }
        [LDFTableOrder] [Description("Nome")] public string NOME      { get; set; }
        //[Description("Hora")] public DateTime HR_CONS                       { get; set; }
        //[Description("Triagem")] public string TRIAGEM                      { get; set; }
        //[Description("Nota Méd.")] public string NOTA_MEDICA              { get; set; }
      
        [LDFTableIsVisible(false)] [Description("Cadeirão")] public string CADEIRAO_PRESENTE           { get; set; }
        [LDFTableOrder] [Description("Medico")] public string MEDICO_NOME { get; set; }
        [LDFTableOrder] [Description("Dt. Admissao")] public DateTime DT_CONS { get; set; }
        [LDFTableOrder] [Description("Triagem")] public string COR_TRIAGEM { get; set; }
        [LDFTableOrder] [Description("Local")] public string LOCALIZACAO { get; set; }
        [LDFTableOrder] [Description("Anotação")] public System.Nullable<DateTime> DT_NOTA_MEDICA { get; set; }
        [LDFTableIsVisible(false)] public string PENSO_APLICAVEL                { get; set; }
        [LDFTableOrder] [Description("Análises")] public int? ANALISES_OK                 { get; set; }
        [LDFTableOrder] [Description("Prescrição")] public int? MEDICACAO_PRESCRITA_PCE      { get; set; }
        [LDFTableOrder] [Description("Imagiologia")] public int? IMAGIOLOGIA_REQUESITADOS   { get; set; }
        [LDFTableOrder] [Description("ECG")] public int? ECG_REQUESITADOS                   { get; set; }
        [LDFTableOrder] [Description("Consulta Ext.")] public string CEXTERNA_REQUISITADOS { get; set; }

        
        [Key, Column(Order = 1)] [LDFTableIsVisible(false)] public string T_DOENTE     { get; set; }
        [LDFTableIsVisible(false)] public System.Nullable<DateTime> DT_NASC { get; set; }
        [LDFTableIsVisible(false)] public string SEXO { get; set; }
        
        [LDFTableIsVisible(false)] public string N_CONS { get; set; }

        
        [LDFTableIsVisible(false)] public string OBS_CONS { get; set; }
        [LDFTableIsVisible(false)] public string COD_SERV { get; set; }
        [LDFTableIsVisible(false)] public string MEDICO_ID { get; set; }
       
      
        [LDFTableIsVisible(false)] public System.Nullable<DateTime> DT_TRIAGEM { get; set; }
        
        [LDFTableIsVisible(false)] public string BOX_PRESENTE { get; set; }
        [LDFTableIsVisible(false)] public string PENSO_REGISTO                  { get; set; }
        [LDFTableIsVisible(false)] public string ANALISES_SOLICITADAS           { get; set; }

        [LDFTableIsVisible(false)] public int? ENTREGUE_LAB                   { get; set; }
        [LDFTableIsVisible(false)] public string COLHEITA_OK                    { get; set; }

        [LDFTableIsVisible(false)] public int? MEDICACAO_PRESCRITA_GLINTT       { get; set; }
        [LDFTableIsVisible(false)] public int? MEDICACAO_ADMINISTRADA_AMB       { get; set; }
        [LDFTableIsVisible(false)] public int? MEDICACAO_ADMINISTRADA_GLINTT    { get; set; }
        [LDFTableIsVisible(false)] public int? IMAGIOLOGIA_REALIZADOS           { get; set; }
        [LDFTableIsVisible(false)] public int? IMAG_DESLOC { get; set; }
        [LDFTableIsVisible(false)] public string IMAG_GRUPO_ATO { get; set; }
        [LDFTableIsVisible(false)] public int? ECG_REALIZADOS                   { get; set; }
        [LDFTableIsVisible(false)] public string CEXTERNA_REALIZADOS              { get; set; }
        [LDFTableIsVisible(false)] public string CEXTERNA_DESLOC{ get; set; }
    }
}
