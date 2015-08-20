using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMDAL
{
    [Table("SD_SERV", Schema = "GH")]
    public class TblSDServ
    {
        [Key, Column(Order=0)]
        public string COD_SERV { get; set; }
        public string COD_ESPEC { get; set; }
        public string DESCR_SERV { get; set; }
        public string LOCAL_SERV { get; set; }
        public int? LOT_POSS { get; set; }
        public int? LOT_APROV { get; set; }
        public string OBS { get; set; }
        public string USER_CRI { get; set; }
        public DateTime DT_CRI { get; set; }
        public string USER_ACT { get; set; }
        public Nullable<System.DateTime> DT_ACT { get; set; }
        public string PODE_REQ { get; set; }
        public string PODE_INT { get; set; }
        public string PODE_CONS { get; set; }
        public string PODE_REQ_CONS { get; set; }
        public string FLAG_PRIM_VEZ { get; set; }
        public string FLG_PAGA_TAXA { get; set; }
        public int? COD_CONTROLO_FA { get; set; }
        public string ABRV_SERV { get; set; }
        public string COD_SERV_PAI { get; set; }
        public string FLAG_ARQ { get; set; }
        public string PODE_LOG { get; set; }
        public string PODE_EXEC_MCDTS { get; set; }
        public int? COD_SERV_FA { get; set; }
        public string MATERNIDADE { get; set; }
        public string PODE_AMB { get; set; }
        public string SERV_URG { get; set; }
        public string FLAG_HDIA { get; set; }
        public string FLAG_BLOCO { get; set; }
        public int? COD_RUBR { get; set; }
        public int? LOT_HOMENS { get; set; }
        public int? LOT_MULHERES { get; set; }
        public string FLAG_UCI {get;set;}
        public string T_SEGMENTO {get;set;}
        public string FLAG_MCDTS {get;set;}
        public string FLAG_AG_AMB_INT {get;set;}
        public string FLAG_TIPO_SERV {get;set;}
        public string FLAG_LIB_CAMA {get;set;}
        public string FLAG_MED_CIR {get;set;}
        public string FLAG_LANCA_PEDIDO {get;set;}
        public string CONSENTIMENTO_AUTO {get;set;} 
        public string EMAIL {get;set;}
        public string FLG_ACTIVO {get;set;}
        public string TELEFONES {get;set;}
        public string N_FAX {get;set;}
        public string LOCALIDADE {get;set;}
        public string COD_POSTAL {get;set;}
        public string MORADA {get;set;}
        public int? N_SEQ_POSTAL { get; set; }
        public string N_CONTRIB {get;set;}
        public string TIPO_SERVICO {get;set;}
        public int? ID_ESPECIALIDADE {get;set;}
        public string FLAG_FARM {get;set;}
        public string FLAG_FIN {get;set;}
        public string FLAG_REF {get;set;}
        public string N_MECAN_RESP {get;set;}
        public string PODE_EXEC_MCDTS_BLOCO {get;set;}
        public string FUSAO_UNIDADE {get;set;}
        public string FLG_OBRIG_GAB {get;set;}
        public string FLG_OBRIG_SERV_REQ {get;set;}
        public string PODE_REQ_AMB {get;set;}
        public int? SERV_ID {get;set;}

    }
}
