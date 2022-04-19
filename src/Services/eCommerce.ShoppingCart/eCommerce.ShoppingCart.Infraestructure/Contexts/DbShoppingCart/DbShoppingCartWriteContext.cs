using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using eCommerce.ShoppingCart.Core.Config;
using eCommerce.ShoppingCart.Core.Objects.DbTypes;
using System.Data;

namespace eCommerce.ShoppingCart.Infraestructure.Contexts
{
    public partial class DbShoppingCartWriteContext : DbContext
    {
        private string _spCreateShoppingCart { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:EcommerceCoreWrite:StoreProcedures:SpCreateShoppingCart"].ToString();
        private string _spUpdateShoppingCart { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:EcommerceCoreWrite:StoreProcedures:SpUpdateShoppingCart"].ToString();
        private string _spDeleteShoppingCartItem { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:EcommerceCoreWrite:StoreProcedures:SpDeleteShoppingCartItem"].ToString();
        private string _spDeleteShoppingCartByUser { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:EcommerceCoreWrite:StoreProcedures:SpDeleteShoppingCartByUser"].ToString();


        public DbShoppingCartWriteContext(DbContextOptions<DbShoppingCartWriteContext> options) : base(options) { }

        public virtual DbSet<ShoppingCartEntity> ShoppingCartEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async Task<bool> SpCreateShoppingCartAsync(DataTable shoppingCartDataTable)
        {
            var result = await this.Database.ExecuteSqlRawAsync($"EXEC {_spCreateShoppingCart} @dataTable",
                new SqlParameter("@dataTable", shoppingCartDataTable) 
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.T_ShoppingCart"
                });

            return result > 0;
        }

        public async Task<bool> SpUpdateShoppingCartAsync(ShoppingCartEntity shoppingCartEntity)
        {
            var result = await this.Database.ExecuteSqlRawAsync($"EXEC {_spUpdateShoppingCart} @customerId, @productId, @initialPrice, @quantity",
                new SqlParameter("@customerId", shoppingCartEntity.CustomerId),
                new SqlParameter("@productId", shoppingCartEntity.ProductId),
                new SqlParameter("@initialPrice", shoppingCartEntity.InitialPrice),
                new SqlParameter("@quantity", shoppingCartEntity.Quantity));

            return result > 0;
        }

        public async Task<bool> SpDeleteShoppingCartItemAsync(string customerId, long productId)
        {
            var result = await this.Database.ExecuteSqlRawAsync($"EXEC {_spDeleteShoppingCartItem} @customerId, @productId",
                new SqlParameter("@customerId", customerId),
                new SqlParameter("@productId", productId));

            return result > 0;
        }

        public async Task<bool> SpDeleteShoppingCartByUserAsync(string customerId)
        {
            var result = await this.Database.ExecuteSqlRawAsync($"EXEC {_spDeleteShoppingCartByUser} @customerId",
                new SqlParameter("@customerId", customerId));

            return result > 0;
        }

    }
}
