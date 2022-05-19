using eCommerce.Products.Reports.Core.Config;
using eCommerce.Products.Reports.Core.Objects.DbTypes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace eCommerce.Products.Reports.Infraestructure.Contexts.DbCore
{
    public class DbCoreContext : DbContext
    {
        private string _spGetUsersByProductsId { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:DbCore:StoreProcedures:SpGetUsersByProductsId"].ToString();

        public DbCoreContext(DbContextOptions<DbCoreContext> options) : base(options) { }

        public virtual DbSet<CustomerEntity> CustomerEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async Task<IList<CustomerByProductEntity>?> SpGetUsersByProductsIdAsync(DataTable productDataTable)
        {
            var result = await this.Set<CustomerByProductEntity>().FromSqlRaw($"EXEC {_spGetUsersByProductsId} @dataTable",
                new SqlParameter("@dataTable", productDataTable)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.T_Product"
                }).ToListAsync();

            return result != null && result.Any() ? result.ToList() : null;
        }
    }
}
