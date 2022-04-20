using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BlazorAppEC.Server.Models
{
    public partial class BlazorECContext : DbContext
    {
        public BlazorECContext()
        {
        }

        public BlazorECContext(DbContextOptions<BlazorECContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Manufacture> Manufactures { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//             if (!optionsBuilder.IsConfigured)
//             {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                 optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=BlazorEC;User ID=SA;Password=Tuan2903");
//             }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Slug).HasDefaultValueSql("('NULL')");
            });

            modelBuilder.Entity<Manufacture>(entity =>
            {
                entity.HasKey(e => e.ManufactureId)
                    .HasName("PK__manufact__142795FEFB6DB61C");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DiscountId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_dc");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_user_id");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("PK__order_de__022945F68DAD3584");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_od_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_p_id");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.CreateAt).HasDefaultValueSql("('NULL')");

                entity.Property(e => e.Image).HasDefaultValueSql("('NULL')");

                entity.Property(e => e.Price).HasDefaultValueSql("('NULL')");

                entity.Property(e => e.Sold).HasDefaultValueSql("('0')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
