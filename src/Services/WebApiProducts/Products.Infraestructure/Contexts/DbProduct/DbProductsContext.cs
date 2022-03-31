using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Products.Core.Config;

namespace Products.Infraestructure.Contexts.DbProduct
{
    public partial class DbProductsContext : DbContext
    {
        public DbProductsContext()
        {
        }

        public DbProductsContext(DbContextOptions<DbProductsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<ProductDetail> ProductDetails { get; set; } = null!;
        public virtual DbSet<ProductProvider> ProductProviders { get; set; } = null!;
        public virtual DbSet<ProductReview> ProductReviews { get; set; } = null!;
        public virtual DbSet<ProductSpecification> ProductSpecifications { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(AppConfiguration.Configuration["AppConfiguration:DataBases:DbProducts:ConnectionString"].ToString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Image).HasMaxLength(2000);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.ProductCategoy)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductCategoyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_ProductCategory");

                entity.HasOne(d => d.ProductProvider)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_ProductProvider");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory");

                entity.Property(e => e.Category).HasMaxLength(50);
            });

            modelBuilder.Entity<ProductDetail>(entity =>
            {
                entity.ToTable("ProductDetail");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductDetail_Product");
            });

            modelBuilder.Entity<ProductProvider>(entity =>
            {
                entity.ToTable("ProductProvider");

                entity.Property(e => e.Provider).HasMaxLength(50);
            });

            modelBuilder.Entity<ProductReview>(entity =>
            {
                entity.ToTable("ProductReview");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Review).HasMaxLength(500);

                entity.Property(e => e.UserId).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductReviews)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductReview_Product");
            });

            modelBuilder.Entity<ProductSpecification>(entity =>
            {
                entity.ToTable("ProductSpecification");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Value).HasMaxLength(250);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductSpecifications)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductEspecification_Product");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
