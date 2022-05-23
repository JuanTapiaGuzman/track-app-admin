using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using track_app_admin.Models;

#nullable disable

namespace track_app_admin.Data
{
    public partial class SercommContext : DbContext
    {
        public SercommContext()
        {
        }

        public SercommContext(DbContextOptions<SercommContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SercommInventoryCategory> Sercomminventorycategories { get; set; }
        public virtual DbSet<SercommInventoryEntry> Sercomminventoryentries { get; set; }
        public virtual DbSet<SercommInventoryEntryStatus> Sercomminventoryentrystatus { get; set; }
        public virtual DbSet<SercommInventoryItem> Sercomminventoryitems { get; set; }
        public virtual DbSet<SercommInventoryItemEntry> Sercomminventoryitementries { get; set; }
        public DbSet<AppFile> File { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=127.0.0.1;uid=root;pwd=admin;database=sercomm");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SercommInventoryCategory>(entity =>
            {
                entity.ToTable("sercomminventorycategory");

                entity.Property(e => e.CreationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<SercommInventoryEntry>(entity =>
            {
                entity.ToTable("sercomminventoryentry");

                entity.Property(e => e.CreationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<SercommInventoryEntryStatus>(entity =>
            {
                entity.ToTable("sercomminventoryentrystatus");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<SercommInventoryItem>(entity =>
            {
                entity.ToTable("sercomminventoryitem");

                entity.HasIndex(e => e.SercommInventoryCategoryId, "SercommInventoryCategoryId");

                entity.Property(e => e.CreationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Make).HasMaxLength(50);

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.SercommInventoryCategory)
                    .WithMany(p => p.Sercomminventoryitems)
                    .HasForeignKey(d => d.SercommInventoryCategoryId)
                    .HasConstraintName("sercomminventoryitem_ibfk_1");
            });

            modelBuilder.Entity<SercommInventoryItemEntry>(entity =>
            {
                entity.HasKey(e => new { e.SercommInventoryEntryId, e.SercommInventoryItemId })
                    .HasName("PRIMARY");

                entity.ToTable("sercomminventoryitementry");

                entity.HasIndex(e => e.SercommInventoryItemId, "SercommInventoryItemId");

                entity.Property(e => e.CreationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.SercommInventoryEntry)
                    .WithMany(p => p.Sercomminventoryitementries)
                    .HasForeignKey(d => d.SercommInventoryEntryId)
                    .HasConstraintName("sercomminventoryitementry_ibfk_2");

                entity.HasOne(d => d.SercommInventoryItem)
                    .WithMany(p => p.Sercomminventoryitementries)
                    .HasForeignKey(d => d.SercommInventoryItemId)
                    .HasConstraintName("sercomminventoryitementry_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
