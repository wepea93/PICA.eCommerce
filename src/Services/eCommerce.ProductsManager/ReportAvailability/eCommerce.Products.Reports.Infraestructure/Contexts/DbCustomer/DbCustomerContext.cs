using eCommerce.Products.Reports.Core.Config;
using eCommerce.Products.Reports.Core.Objects.DbTypes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace eCommerce.Products.Reports.Infraestructure.Contexts.DbCustomer
{
    public class DbCustomerContext : DbContext
    {
        private string _spGetCustomersById { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:DbCustomer:StoreProcedures:SpGetCustomersById"].ToString();

        public DbCustomerContext(DbContextOptions<DbCustomerContext> options) : base(options) { }

        public virtual DbSet<CustomerEntity> CustomerEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async Task<IList<CustomerEntity>?> SpGetCustomersByIdAsync(DataTable productDataTable)
        {
            var result = await this.Set<CustomerEntity>().FromSqlRaw($"EXEC {_spGetCustomersById} @dataTable",
                new SqlParameter("@dataTable", productDataTable)
                {
                    SqlDbType = SqlDbType.Structured,
                    TypeName = "dbo.T_Customer"
                }).ToListAsync();

            return result != null && result.Any() ? result.ToList() : null;
        }
    }
}
