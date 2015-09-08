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
    public class VDeslocProd : LDFTableOracleObject
    {

        [Key, Column(Order = 2)]
        [Description("Data")]
        [LDFTableOrder]
        public DateTime DT_DESL { get; set; }

        [Description("Produto")]
        [LDFTableOrder]
        public string PRODUTO_DESCR { get; set; }

        [Description("Origem")]
        public string DESCR_SERV_ORIG { get; set; }



        [Key, Column(Order = 0)]
        [LDFTableIsVisible(false)]
        public string T_DOENTE { get; set; }
        [Key, Column(Order = 1)]
        [LDFTableIsVisible(false)]
        public string DOENTE { get; set; }
        [LDFTableIsVisible(false)]
        public string T_EPISODIO { get; set; }
        [LDFTableIsVisible(false)]
        public string EPISODIO { get; set; }
        [LDFTableIsVisible(false)]
        public string PRODUTO { get; set; }

        [LDFTableIsVisible(false)]
        public string COD_SERV_ORIG { get; set; }

        [LDFTableIsVisible(false)]
        public string COD_SERV { get; set; }
        [LDFTableIsVisible(false)]
        public string DESCR_SERV { get; set; }
        [LDFTableIsVisible(false)]
        public string CONTACTO_SERV { get; set; }

        [LDFTableIsVisible(false)]
        public string FLG_POS_FINAL { get; set; }

        [LDFTableIsVisible(false)]
        public string USER_CRI { get; set; }
        [Key, Column(Order = 3)]
        [LDFTableIsVisible(false)]
        public DateTime DT_CRI { get; set; }
        [LDFTableIsVisible(false)]
        public string TITULO { get; set; }
        [LDFTableIsVisible(false)]
        public string ABR { get; set; }
        [LDFTableIsVisible(false)]
        public string NOME { get; set; }

    }
}
