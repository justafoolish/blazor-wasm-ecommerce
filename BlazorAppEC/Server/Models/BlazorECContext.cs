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
        public virtual DbSet<ReceivedNote> ReceivedNotes { get; set; }
        public virtual DbSet<ReceivedNoteDetail> ReceivedNoteDetails { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Slug).HasDefaultValueSql("('NULL')");
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

                entity.Property(e => e.Description).HasDefaultValueSql("('NULL')");

                entity.Property(e => e.Image).HasDefaultValueSql("('NULL')");

                entity.Property(e => e.Price).HasDefaultValueSql("('NULL')");

                entity.Property(e => e.Sold).HasDefaultValueSql("('0')");
            });

            modelBuilder.Entity<ReceivedNote>(entity =>
            {
                entity.HasKey(e => e.ReceiveNoteId)
                    .HasName("PK__received__5F76C5E57F6AE548");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.ReceivedNotes)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("fk_sup_note");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ReceivedNotes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_rn");
            });

            modelBuilder.Entity<ReceivedNoteDetail>(entity =>
            {
                entity.HasKey(e => new { e.ReceivedNoteId, e.ProductId })
                    .HasName("PK__received__F8801DB6737CB634");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ReceivedNoteDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_product_goods");

                entity.HasOne(d => d.ReceivedNote)
                    .WithMany(p => p.ReceivedNoteDetails)
                    .HasForeignKey(d => d.ReceivedNoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reveived_note");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
