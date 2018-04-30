using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LusiadasDAL
{
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public partial class DBInternamentoContext : DbContext
    {
        //a escolha da BD ainda nao é dinamica porque há metodos que devem ser da BD escolhida e outros da BD do utilizador
        //ToDo : (string)HttpContext.Current.Session[Constants.SS_LOCAL_CONN]
        public DBInternamentoContext()
            : base("name=" + HttpContext.Current.Session["CONN"])
        {
            Database.SetInitializer<DBInternamentoContext>(null);
        }

        public virtual DbSet<VwInternamentos>   vwInternamentos { get; set; }
        public virtual DbSet<VwPisosIntern>     vwPisos         { get; set; }
        public virtual DbSet<VwValenciasFreq>   VwValenciasFreq { get; set; }
        public virtual DbSet<VwValenciasProdFreq> VwValenciasProdFreq { get; set; }
        public virtual DbSet<TblSDServ>         tblSdServ       { get; set; }
        public virtual DbSet<TblSDPessHosp>     tblSdPessHosp   { get; set; }
        public virtual DbSet<TblPessHospDet>    tblPessHospDet  { get; set; }


    }

}
