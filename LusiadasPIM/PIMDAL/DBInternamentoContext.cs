using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMDAL
{
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public partial class DBInternamentoContext : DbContext
    {
        public DBInternamentoContext() : base("name=DBConnectionPIM")
        {
            Database.SetInitializer<DBInternamentoContext>(null);
        }

        public virtual DbSet<VwInternamentos>   vwInternamentos { get; set; }
        public virtual DbSet<VwPisosIntern>     vwPisos         { get; set; }
        public virtual DbSet<TblSDServ>         tblSdServ       { get; set; }
        public virtual DbSet<TblSDPessHosp>     tblSdPessHosp   { get; set; }


    }

}
