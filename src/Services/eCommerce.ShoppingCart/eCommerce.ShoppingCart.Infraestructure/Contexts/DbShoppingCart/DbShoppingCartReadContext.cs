using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using eCommerce.ShoppingCart.Core.Config;
using eCommerce.ShoppingCart.Core.Objects.DbTypes;

namespace eCommerce.ShoppingCart.Infraestructure.Contexts
{
    public partial class DbShoppingCartReadContext : DbContext
    {
        private string _spGetShoppingCartByUser { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:EcommerceCoreRead:StoreProcedures:SpGetShoppingCart"].ToString();

        public DbShoppingCartReadContext(DbContextOptions<DbShoppingCartReadContext> options) : base(options) { }

        public virtual DbSet<ShoppingCartEntity> ShoppingCartEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async Task<IList<ShoppingCartEntity>?> SpGetShoppingCartByUserAsync(string userId)
        {
            var result = await this.Set<ShoppingCartEntity>().FromSqlInterpolated($"{_spGetShoppingCartByUser} {userId} ").ToListAsync();
            return result != null && result.Any() ? result.ToList() : null;
        }

    }
}
