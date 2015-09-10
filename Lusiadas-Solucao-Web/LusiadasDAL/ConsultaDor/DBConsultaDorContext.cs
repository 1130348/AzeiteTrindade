using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LusiadasDAL
{
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    public partial class DBConsultaDorContext : DbContext
    {
        public DBConsultaDorContext(string conn) : base(conn)
        {
            Database.SetInitializer<DBConsultaDorContext>(null);
        }

        public virtual DbSet<TblConsultaDor>    tblConsultaDor { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblConsultaDor>().Ignore(t => t.ROWID);
            modelBuilder.Entity<TblConsultaDor>().Ignore(t => t.ZTOTAL);
            base.OnModelCreating(modelBuilder);
        }

    }

}
