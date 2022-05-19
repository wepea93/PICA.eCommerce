using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using eCommerce.Products.Reports.Core.Config;
using eCommerce.Products.Reports.Core.Objects.DbTypes;
using System.Data;

namespace eCommerce.Products.Reports.Infraestructure.Contexts.DbProducts
{
    public partial class DbProductContext : DbContext
    {
        private string _spGetProductsById { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:DbProduct:StoreProcedures:SpGetProductsById"].ToString();

        public DbProductContext(DbContextOptions<DbProductContext> options) : base(options) { }

        public virtual DbSet<ProductEntity> ProductEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async Task<IList<ProductEntity>?> SpGetProductsByIdAsync(DataTable productDataTable)
        {
            var result = await this.Set<ProductEntity>().FromSqlRaw($"EXEC {_spGetProductsById} @dataTable",
                new SqlParameter("@dataTable", productDataTable)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.T_Product"
                }).ToListAsync();

            return result != null && result.Any() ? result.ToList() : null;
        }

    }
}
