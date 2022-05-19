using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using eCommerce.Products.Availability.Core.Config;
using eCommerce.Products.Availability.Core.Objects.DbTypes;
using System.Data;

namespace eCommerce.Products.Availability.Infraestructure.Contexts.DbProduct
{
    public partial class DbProductContext : DbContext
    {
        private string _spGetProduct { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:DbProduct:StoreProcedures:SpGetProduct"].ToString();
        private string _spUpdateProduct { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:DbProduct:StoreProcedures:SpUpdateProduct"].ToString();
        private string _spUpdateProductStock { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:DbProduct:StoreProcedures:SpUpdateProductStock"].ToString();

        public DbProductContext(DbContextOptions<DbProductContext> options) : base(options) { }

        public virtual DbSet<ProductEntity> ProductEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async Task<ProductEntity?> SpGetProductAsync(long productId)
        {
            var result = await this.Set<ProductEntity>().FromSqlInterpolated($"{_spGetProduct} {productId}").ToListAsync();
            return result != null && result.Any() ? result.FirstOrDefault() : null;
        }

        public async Task<bool> SpUpdateProductAsync(ProductEntity productEntity)
        {
            var result = await this.Database.ExecuteSqlRawAsync($"EXEC {_spUpdateProduct} @productId , @price, @stock",
                new SqlParameter("@productId ", productEntity.Id),
                new SqlParameter("@price", productEntity.Price),
                new SqlParameter("@stock", productEntity.Stock));

            return result > 0;
        }

        public async Task<bool> SpUpdateProductStockAsync(DataTable productDataTable)
        {
            var result = await this.Database.ExecuteSqlRawAsync($"EXEC {_spUpdateProductStock} @dataTable",
                new SqlParameter("@dataTable", productDataTable)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.T_Product"
                });

            return result > 0;
        }

    }
}
