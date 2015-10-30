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

    public partial class DBMenuContext : DbContext
    {
        //a escolha da BD nesta classe nao ´uma opção visto que as tabelas se encontram na BD do SQL Server
        public DBMenuContext()
            : base("name=DBGeneral")
        {
            Database.SetInitializer<DBMenuContext>(null);
        }

        //ToDo: adicionar BdSet das classes que vao ser criadas com base nas tabelas

        public virtual DbSet<TblMenu> tblMenu { get; set; }
        public virtual DbSet<TblOptionsMenu> tblOptionsMenu { get; set; }
        public virtual DbSet<TblGroupMenu> tblGroupMenu { get; set; }
        public virtual DbSet<TblAuthGroup> tblAuthGroup { get; set; }


    }

}

