using System;
using System.Collections.Generic;
using eCommerce.Customers.Core.Config;
using eCommerce.Customers.Core.Objects.DbTypes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eCommerce.Customers.Infraestructure.Contexts.DbCustomers
{
    public partial class DbCustomerContext : DbContext
    {

        private string _spCreate { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:DataBase1:StoreProcedures:SpCreateCustomer"].ToString();
        private string _spUpdate { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:DataBase1:StoreProcedures:SpUpdateCustomer"].ToString();
        public DbCustomerContext()
        {
        }

        public DbCustomerContext(DbContextOptions<DbCustomerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuthenticationType> AuthenticationTypes { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; } = null!;
        public virtual DbSet<CustomerLogin> CustomerLogins { get; set; } = null!;
        public virtual DbSet<CustomerPasswordHistory> CustomerPasswordHistories { get; set; } = null!;
        public virtual DbSet<CustomerStatus> CustomerStatuses { get; set; } = null!;
        public virtual DbSet<CustomerToken> CustomerTokens { get; set; } = null!;
        public virtual DbSet<IdentificationType> IdentificationTypes { get; set; } = null!;
        public virtual DbSet<TokenType> TokenTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-5KI0UIFA;initial catalog=EcommerceCustomers;persist security info=True;user id=sa;password=Admin123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthenticationType>(entity =>
            {
                entity.ToTable("AuthenticationType");

                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.IdentTypeId).HasMaxLength(3);

                entity.Property(e => e.Identification).HasMaxLength(20);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.Property(e => e.Phone1).HasMaxLength(20);

                entity.Property(e => e.Phone2).HasMaxLength(20);

                entity.Property(e => e.SecondLastName).HasMaxLength(50);

                entity.Property(e => e.SecondName).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(3);

                entity.Property(e => e.UserName).HasMaxLength(250);

                entity.HasOne(d => d.IdentType)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.IdentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdentificationType_Client");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientStatus_Client");
            });

            modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.ToTable("CustomerAddress");

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.CustomertId).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.HasOne(d => d.Customert)
                    .WithMany(p => p.CustomerAddresses)
                    .HasForeignKey(d => d.CustomertId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientAdsress_Client");
            });

            modelBuilder.Entity<CustomerLogin>(entity =>
            {
                entity.ToTable("CustomerLogin");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasMaxLength(50);

                entity.Property(e => e.Ip)
                    .HasMaxLength(20)
                    .HasColumnName("IP");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerLogins)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientLogin_Client");
            });

            modelBuilder.Entity<CustomerPasswordHistory>(entity =>
            {
                entity.ToTable("CustomerPasswordHistory");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(250);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerPasswordHistories)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientPasswordHistory_Client");
            });

            modelBuilder.Entity<CustomerStatus>(entity =>
            {
                entity.HasKey(e => e.Status)
                    .HasName("PK_ClientStatus");

                entity.ToTable("CustomerStatus");

                entity.Property(e => e.Status).HasMaxLength(3);

                entity.Property(e => e.Description).HasMaxLength(50);
            });

            modelBuilder.Entity<CustomerToken>(entity =>
            {
                entity.ToTable("CustomerToken");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasMaxLength(50);

                entity.Property(e => e.ExpiredAt).HasColumnType("datetime");

                entity.Property(e => e.Token).HasMaxLength(150);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerTokens)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientToken_Client");
            });

            modelBuilder.Entity<IdentificationType>(entity =>
            {
                entity.ToTable("IdentificationType");

                entity.Property(e => e.Id).HasMaxLength(3);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TokenType>(entity =>
            {
                entity.ToTable("TokenType");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        public async Task<bool> SpCreateAsync(CustomerEntity customerEntity)
        {

            var result = await this.Database.ExecuteSqlRawAsync($"EXEC {_spCreate} @Id,@IdentTypeId,@Identification,@FirstName,@SecondName,@LastName,@SecondLastName,@Email,@Phone1,@Phone2,@UserName,@Password,@AutenticationTypeId,@Status,@CreatedAt",
                    new SqlParameter("@Id", customerEntity.Id),
                    new SqlParameter("@IdentTypeId", customerEntity.IdentTypeId),
                    new SqlParameter("@Identification", customerEntity.Identification),
                    new SqlParameter("@FirstName", customerEntity.FirstName),
                    new SqlParameter("@SecondName", customerEntity.SecondName),
                    new SqlParameter("@LastName", customerEntity.LastName),
                    new SqlParameter("@SecondLastName", customerEntity.SecondLastName),
                    new SqlParameter("@Email", customerEntity.Email),
                    new SqlParameter("@Phone1", customerEntity.Phone1),
                    new SqlParameter("@Phone2", customerEntity.Phone2),
                    new SqlParameter("@UserName", customerEntity.UserName),
                    new SqlParameter("@Password", customerEntity.Password),
                    new SqlParameter("@AutenticationTypeId", customerEntity.AutenticationTypeId),
                    new SqlParameter("@Status", customerEntity.Status),
                    new SqlParameter("@CreatedAt", customerEntity.CreatedAt)

                );

            return result > 0;
        }
        public async Task<bool> SpUpdateAsync(CustomerUpdateEntity customerEntity)
        {

            var result = await this.Database.ExecuteSqlRawAsync($"EXEC {_spUpdate} @Id,@IdentTypeId,@Identification,@FirstName,@SecondName,@LastName,@SecondLastName,@Email,@Phone1,@Phone2,@UserName,@Password,@AutenticationTypeId,@Status,@CreatedAt",
                    new SqlParameter("@Id", customerEntity.Id),
                    new SqlParameter("@IdentTypeId", customerEntity.IdentTypeId == null ? DBNull.Value : customerEntity.IdentTypeId),
                    new SqlParameter("@Identification", customerEntity.Identification == null ? DBNull.Value : customerEntity.Identification),
                    new SqlParameter("@FirstName", customerEntity.FirstName == null ? DBNull.Value : customerEntity.FirstName),
                    new SqlParameter("@SecondName", customerEntity.SecondName == null ? DBNull.Value : customerEntity.SecondName),
                    new SqlParameter("@LastName", customerEntity.LastName == null ? DBNull.Value : customerEntity.LastName),
                    new SqlParameter("@SecondLastName", customerEntity.SecondLastName == null ? DBNull.Value : customerEntity.SecondLastName),
                    new SqlParameter("@Email", customerEntity.Email == null ? DBNull.Value : customerEntity.Email),
                    new SqlParameter("@Phone1", customerEntity.Phone1 == null ? DBNull.Value : customerEntity.Phone1),
                    new SqlParameter("@Phone2", customerEntity.Phone2 == null ? DBNull.Value : customerEntity.Phone2),
                    new SqlParameter("@UserName", customerEntity.UserName == null ? DBNull.Value : customerEntity.UserName),
                    new SqlParameter("@Password", customerEntity.Password == null ? DBNull.Value : customerEntity.Password),
                    new SqlParameter("@AutenticationTypeId", customerEntity.AutenticationTypeId == null ? DBNull.Value : customerEntity.AutenticationTypeId),
                    new SqlParameter("@Status", customerEntity.Status == null ? DBNull.Value : customerEntity.Status),
                    new SqlParameter("@CreatedAt", customerEntity.CreatedAt == null ? DBNull.Value : customerEntity.CreatedAt)

                );

            return result > 0;
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
