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
    [Table("CONSULTA_DOR", Schema = "MEDICO")]
    public class TblConsultaDor : LDFTableOracleObject
    {
        [Key, Column(Order=0)] [LDFTableIsVisible(false)] [LDFTableKey]
        public string NUM_CONS_DOR { get; set; }
        [Key, Column(Order = 1)]    [LDFTableIsVisible(false)]
        public string T_DOENTE { get; set; }
        [Key, Column(Order = 2)]    [LDFTableIsVisible(false)]
        public string DOENTE { get; set; }
        
        [LDFTableIsVisible(false)]      public string N_INT                         { get; set; }
        [LDFTableIsVisible(false)]      public string N_REG_OPER                    { get; set; }
        [LDFTableIsVisible(false)]      public string BAL_INFUSOR                   { get; set; }
        [LDFTableIsVisible(false)]      public string BAL_INFUSOR_SIT               { get; set; }
        [Description("SOS")]            public string SOS                           { get; set; }
        [Description("Dor")]            public string DOR                           { get; set; }
        [Description("Nausea")]         public string NAUSEA                        { get; set; }
        [Description("Vómitos")]        public string VOMITOS                       { get; set; }
        [Description("Insónia")]        public string INSONIA                       { get; set; }
        [Description("Cefaleia")]       public string CEFALEIA                      { get; set; }
        [LDFTableIsVisible(false)]      public string OUTROS_SINT                   { get; set; }
        [LDFTableIsVisible(false)]      public string ALT_TERAP                     { get; set; }
        [Description("Observações")]    public string OBS                           { get; set; }
        [LDFTableIsVisible(false)]      public string ENF_RESP                      { get; set; }
        [LDFTableIsVisible(false)]      public string ENF                           { get; set; }
        [LDFTableIsVisible(false)]      public string ANEST_VISITA                  { get; set; }
        [LDFTableIsVisible(false)]      public DateTime DT_CRI                      { get; set; }
        [LDFTableIsVisible(false)]      public string USER_CRI                      { get; set; }
        [LDFTableIsVisible(false)]      public System.Nullable<DateTime> DT_ACT     { get; set; }
        [LDFTableIsVisible(false)]      public string USER_ACT                      { get; set; }
        [LDFTableIsVisible(false)]      public System.Nullable<DateTime> DT_ANUL    { get; set; }
        [LDFTableIsVisible(false)]      public string USER_ANUL                     { get; set; }
        [LDFTableIsVisible(false)]      public string FLAG_ESTADO                   { get; set; }








    }
}
