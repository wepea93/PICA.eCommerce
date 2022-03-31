using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Products.Core.Config;
using Products.Core.Objects.DbTypes;
using System.Reflection;

namespace Products.Infraestructure.Contexts.DbProduct
{
    public partial class DbProductContext2 : DbContext
    {
        private string _spCreateValue { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:DataBase1:StoreProcedures:SpCreateValue"].ToString();
        private string _spGetValue { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:DataBase1:StoreProcedures:SpGetValues"].ToString();

        public DbProductContext2(DbContextOptions<DbProductContext2> options) : base(options) { }

        public virtual DbSet<ProductEntity> ProductEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }      
    }
}
