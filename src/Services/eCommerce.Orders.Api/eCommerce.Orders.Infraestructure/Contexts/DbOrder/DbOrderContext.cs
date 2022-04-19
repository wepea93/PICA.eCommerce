using System;
using System.Collections.Generic;
using eCommerce.Orders.Core.Config;
using eCommerce.Orders.Core.Objects.DbTypes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eCommerce.Orders.Infraestructure.Contexts.DbOrder
{
    public partial class DbOrderContext : DbContext
    {
        private string _spCreate { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:DataBase1:StoreProcedures:SpCreateOrder"].ToString();
        private string _spCreateDetail { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:DataBase1:StoreProcedures:SpCreateDetailOrder"].ToString();
        private string _spGetByIdOrCustomer { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:DataBase1:StoreProcedures:SpGetOrder"].ToString();
        private string _spGetDetailById { get; set; } = AppConfiguration.Configuration["AppConfiguration:DataBases:DataBase1:StoreProcedures:SpGetDetailOrder"].ToString();

        public DbOrderContext()
        {
        }

        public DbOrderContext(DbContextOptions<DbOrderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EOrder> EOrders { get; set; } = null!;
        public virtual DbSet<EOrderDetail> EOrderDetails { get; set; } = null!;
        public virtual DbSet<EOrderState> EOrderStates { get; set; } = null!;
        public virtual DbSet<OrderEntity> OrderEntity { get; set; } = null!;
        public virtual DbSet<OrderDetailEntity> OrderDetailEntity { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-5KI0UIFA;initial catalog=Ecommerce;persist security info=True;user id=sa;password=Admin123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EOrder>(entity =>
            {
                entity.ToTable("E_order");

                entity.HasIndex(e => e.OrderNumber, "UQ__E_order__DF49821C049DFBAA")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comment)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("COMMENT");

                entity.Property(e => e.CustomerId).HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.DateRequired)
                    .HasColumnType("datetime")
                    .HasColumnName("DATE_REQUIRED");

                entity.Property(e => e.DateShipped)
                    .HasColumnType("datetime")
                    .HasColumnName("DATE_SHIPPED");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("ORDER_DATE");

                entity.Property(e => e.OrderNumber).HasColumnName("ORDER_NUMBER");

                entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.EOrders)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__E_order__STATUS___282DF8C2");
            });

            modelBuilder.Entity<EOrderDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("E_order_detail");

                entity.Property(e => e.OrderId).HasColumnName("ORDER_ID");

                entity.Property(e => e.OrderLine).HasColumnName("ORDER_LINE");

                entity.Property(e => e.PriceEach)
                    .HasColumnType("money")
                    .HasColumnName("PRICE_EACH");

                entity.Property(e => e.ProductId).HasColumnName("PRODUCT_ID");

                entity.Property(e => e.QuantityOrdered).HasColumnName("QUANTITY_ORDERED");

                entity.HasOne(d => d.Order)
                    .WithMany()
                    .HasPrincipalKey(p => p.OrderNumber)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__E_order_d__ORDER__2A164134");
            });

            modelBuilder.Entity<EOrderState>(entity =>
            {
                entity.ToTable("E_order_state");

                entity.HasIndex(e => e.Id, "UQ__E_order___3214EC2698560F68")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.State)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("STATE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public async Task<bool> SpCreateAsync(OrderEntity orderEntity, List<OrderDetailEntity> orderDetailEntity)
        {
            var resultDetail = 0;
            var result = await this.Database.ExecuteSqlRawAsync($"EXEC {_spCreate} @ORDER_NUMBER, @DATE_REQUIRED,@ORDER_DATE,@STATUS_ID,@COMMENT,@CUSTOMER_ID",
                new SqlParameter("@ORDER_NUMBER", orderEntity.OrderID),
                new SqlParameter("@DATE_REQUIRED", orderEntity.DateRequiered),
                new SqlParameter("@ORDER_DATE", orderEntity.OrderDate),
                new SqlParameter("@STATUS_ID", orderEntity.Status),
                new SqlParameter("@COMMENT", orderEntity.Comment),
                new SqlParameter("@CUSTOMER_ID", orderEntity.Customer)
                );
            if (result > 0)
            {

                foreach (var item in orderDetailEntity)
                {
                    resultDetail = await this.Database.ExecuteSqlRawAsync($"EXEC {_spCreateDetail} @ORDER_NUMBER,@PRODUCT_ID,@QUANTITY_ORDERED,@PRICE_EACH,@ORDER_LINE",
                                     new SqlParameter("@ORDER_NUMBER", orderEntity.OrderID),
                                     new SqlParameter("@PRODUCT_ID", item.ProductID),
                                     new SqlParameter("@QUANTITY_ORDERED", item.QuantityOrdered),
                                     new SqlParameter("@PRICE_EACH", item.PriceEach),
                                     new SqlParameter("@ORDER_LINE", item.OrderLine)
                                     );
                }

            }
            return resultDetail > 0;
        }

        public async Task<IList<OrderEntity>> SpGetOrderByIdOrCustomer(string ID,string customer)
        {
            var gResult = await this.Set<OrderEntity>().FromSqlInterpolated($"EXEC {_spGetByIdOrCustomer} @ORDER_NUMBER={ID},@CUSTOMER_ID={customer}").ToListAsync();
            return gResult != null && gResult.Any() ? gResult.ToList() : null;
        }
        public async Task<IList<OrderDetailEntity>> SpGetOrderDetailById(string ID)
        {
            var gResultDetail = await this.Set<OrderDetailEntity>().FromSqlInterpolated($"EXEC {_spGetDetailById} @ORDER_NUMBER={ID}").ToListAsync();
            return gResultDetail != null && gResultDetail.Any() ? gResultDetail.ToList() : null;
        }
    }
}
