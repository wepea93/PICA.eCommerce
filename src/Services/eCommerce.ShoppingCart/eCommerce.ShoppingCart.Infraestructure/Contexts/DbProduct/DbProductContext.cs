using eCommerce.ShoppingCart.Core.Config;
using eCommerce.ShoppingCart.Core.Objects.DbTypes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace eCommerce.ShoppingCart.Infraestructure.Contexts.DbProduct
{
    public class DbProductContext : DbContext
    {
        private string _spGetProducts { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:EcommerceProducts:StoreProcedures:SpGetProducts"].ToString();

        public DbProductContext(DbContextOptions<DbProductContext> options) : base(options) { }

        public virtual DbSet<ProductEntity> ProducEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async Task<IList<ProductEntity>?> SpGetProductsAsync(DataTable productDataTable)
        {
            var result = await this.Set<ProductEntity>().FromSqlRaw($"EXEC {_spGetProducts} @dataTable",
                new SqlParameter("@dataTable", productDataTable)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.T_Product"
                }).ToListAsync();

            return result != null && result.Any() ? result.ToList() : null;
        }
    }
}
