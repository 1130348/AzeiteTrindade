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

    public partial class DBDeslocacoesContext : DbContext
    {
        public DBDeslocacoesContext()
            : base("name=DBSolucaoWeb")
        {
            Database.SetInitializer<DBDeslocacoesContext>(null);
        }

        public virtual DbSet<VwDoentesPresentes>    vwDoentesPresentes  { get; set; }
        public virtual DbSet<TblDesloc>             tblDesloc           { get; set; }
        public virtual DbSet<TblDeslocProd>         tblDeslocProd       { get; set; }
        public virtual DbSet<VwDesloc>              vwDesloc            { get; set; }
        public virtual DbSet<VwDeslocProd>          vwDeslocProd        { get; set; }

    }

}
