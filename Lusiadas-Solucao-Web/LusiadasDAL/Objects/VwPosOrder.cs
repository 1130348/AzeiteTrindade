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
    [Table("V_POS_OPER", Schema = "MEDICO")]
    public class VwPosOrder : LDFTableOracleObject
    {
        [Key, Column(Order=0)] [Description("Doente")] [LDFTableJoin("DOENTE")] public string T_DOENTE      { get; set; }
        [Description("Nome")]               public string   NOME            { get; set; }
        [Description("Quarto")]             public string   SALA            { get; set; }
        [Description("Data Cirurgia")]      public DateTime DT_OPER         { get; set; }
        [Description("Especialidade")]      public string   ESPECIALIDADE   { get; set; }
        [Description("Cirurgião")]          public string   CIRURGIAO       { get; set; }
        [Description("Cirurgia")]           public string   CIRURGIA        { get; set; }
        [Description("Anestesista")]        public string   ANESTESISTA     { get; set; }
        [Description("Tipo Anestesia")]     public string   TIPO_ANEST      { get; set; }
        [Description("Prot. Analgesia")]    public string   PROT_ANALGESIA  { get; set; }
        [Description("Data Visita")]        public System.Nullable<DateTime> DATA_VISITA    { get; set; }

        [Key, Column(Order = 1)] [LDFTableIsVisible(false)] public string DOENTE    { get; set; }
        [LDFTableIsVisible(false)] public string    COD_SERV        { get; set; }
        [LDFTableIsVisible(false)] public string    CAMA            { get; set; }
        [LDFTableIsVisible(false)] public DateTime  DT_INT          { get; set; }
        [LDFTableIsVisible(false)] public string    N_INT           { get; set; }
        [LDFTableIsVisible(false)] public string    N_REG_OPER      { get; set; }
        [LDFTableIsVisible(false)] public string    PAT_ASSOCIADAS      { get; set; }

    }
}
