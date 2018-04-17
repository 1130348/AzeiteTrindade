using LDFHelper.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LusiadasSolucaoWeb.Models
{
    public class VDoentesPresentes : LDFTableOracleObject
    {
        [Description("Doente")] [LDFTableJoin("DOENTE")] [LDFTableOrder] [LDFTableKey] public string T_DOENTE { get; set; }
        [Key, Column(Order = 0)] [LDFTableIsVisible(false)] public string DOENTE        { get; set; }
        [Description("Nome")] [LDFTableOrder] public string NOME    { get; set; }
        [LDFTableIsVisible(false)] public Nullable<System.DateTime> DT_CONS { get; set; }
        [Description("Episódio")] public string T_EPISODIO { get; set; }
        [Description("Data Consulta")] public string HR_CONS { get; set; }
        [Description("Local Origem")] public string DESCR_SERV { get; set; }
        [LDFTableIsVisible(false)] public int? PROD { get; set; }

        [LDFTableIsVisible(false)] public int? TPROD { get; set; }



        [LDFTableIsVisible(false)] public string COD_SERV { get; set; }
        [LDFTableIsVisible(false)] public string MEDICO { get; set; }
        [LDFTableIsVisible(false)] public string FLAG_ESTADO { get; set; }
        [LDFTableIsVisible(false)] public string FLAG_URGENTE { get; set; }
        [LDFTableIsVisible(false)] public string N_CONS { get; set; }
        [Key, Column(Order = 1)] [LDFTableIsVisible(false)] public string EPISODIO { get; set; }
        [LDFTableIsVisible(false)] public string U_LOCAL { get; set; }
        [LDFTableIsVisible(false)] public string U_LOCAL_DESCR { get; set; }

        


    }
}
