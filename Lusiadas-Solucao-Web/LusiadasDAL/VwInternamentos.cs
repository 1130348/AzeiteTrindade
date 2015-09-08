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
    [Table("V_INTERNAMENTOS", Schema = "MEDICO")]
    public class VwInternamentos : LDFTableOracleObject
    {
        [Key, Column(Order = 0)] [Description("Código")] [LDFTableOrder] [LDFTableJoin("DOENTE")]
        public string T_DOENTE  { get; set; }
        [Description("Doente")] [LDFTableOrder]
        public string NOME      { get; set; }
        [Key, Column(Order = 3)] [Description("Piso")]
        public string COD_SERV  { get; set; }
        [Description("Cama")]       public string SALA          { get; set; }

        [Description("Valência")]   public string DESCR_SERV    { get; set; }
        [Description("Médico")]     public string NOME_MEDICO   { get; set; }
        [Key, Column(Order = 4)]
        [LDFTableOrder] [Description("Data Internamento")]      public System.DateTime           DT_INT  { get; set; }
        [LDFTableOrder] [Description("Data Cirurgia")]  public Nullable<System.DateTime> DT_OPER { get; set; }
        [Description("Duração")]    public string DURACAO       { get; set; }



        [Key, Column(Order = 1)] [LDFTableIsVisible(false)]
        public string DOENTE { get; set; }

        [LDFTableIsVisible(false)]
        public string COD_SERV_VALENCIA { get; set; }
        [LDFTableIsVisible(false)]
        public string CAMA { get; set; }
        [LDFTableIsVisible(false)]
        public Nullable<System.DateTime> DT_ALTA { get; set; }
        [Key, Column(Order = 5)]
        [LDFTableIsVisible(false)]
        public string EPISODIO { get; set; }
        [Key, Column(Order = 2)]
        [LDFTableIsVisible(false)] public string N_INT { get; set; }

        [LDFTableIsVisible(false)] public string HORA_INT           { get; set; }
        [LDFTableIsVisible(false)] public string HR_INI             { get; set; }
        [LDFTableIsVisible(false)] public string HR_ALTA            { get; set; }
        [LDFTableIsVisible(false)] public string T_EPISODIO         { get; set; }
        [LDFTableIsVisible(false)] public string ENFERMARIA         { get; set; }
        [LDFTableIsVisible(false)] public string N_MECAN            { get; set; }
        [LDFTableIsVisible(false)] public string DESC_OPER          { get; set; }
        [LDFTableIsVisible(false)] public string T_EPISODIO_PAI     { get; set; }
        [LDFTableIsVisible(false)] public string EPISODIO_PAI       { get; set; }
        [LDFTableIsVisible(false)] public string TRI_NUM            { get; set; }
        [LDFTableIsVisible(false)] public string MOT_INTERN         { get; set; }
    }
}

