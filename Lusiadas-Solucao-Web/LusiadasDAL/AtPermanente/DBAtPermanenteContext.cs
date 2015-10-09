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
    using System.Web;

    public partial class DBAtPermanenteContext : DbContext
    {
        public DBAtPermanenteContext()
            : base("name=" + HttpContext.Current.Session["CONN"])
        {
            Database.SetInitializer<DBAtPermanenteContext>(null);
        }

        public virtual DbSet<VwDashboardATP>    vwDashboardATP  { get; set; }


    }

}
