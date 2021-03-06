﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LusiadasDAL
{
    [Table("DESLOC_PROD_V2", Schema = "MEDICO")]
    public class TblDeslocProd
    {
        [Key, Column(Order=0)]
        public string   T_DOENTE        { get; set; }
        [Key, Column(Order = 1)]
        public string   DOENTE          { get; set; }
        public string   PRODUTO         { get; set; }
        public string   N_CONS          { get; set; }
        public string   T_EPISODIO      { get; set; }
        public string   EPISODIO        { get; set; }
        public string   COD_SERV_ORIG   { get; set; }
        public string   COD_SERV        { get; set; }
        [Key, Column(Order = 2)]
        public DateTime DT_DESL         { get; set; }
        public string   USER_CRI        { get; set; }
        [Key, Column(Order = 3)]
        public DateTime DT_CRI          { get; set; }
    }
}
