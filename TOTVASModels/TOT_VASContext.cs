using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ReportWebApp.TOTVASModels
{
    public partial class TOT_VASContext : DbContext
    {
        public TOT_VASContext()
        {
        }

        public TOT_VASContext(DbContextOptions<TOT_VASContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DestnAddrMap> DestnAddrMap { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("name=TOT_VAS_Database");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DestnAddrMap>(entity =>
            {
                entity.HasKey(e => e.DestnAddrId)
                    .HasName("PRIMARY");

                entity.ToTable("destn_addr_map");

                entity.Property(e => e.DestnAddrId)
                    .HasColumnName("destn_addr_id")
                    .HasColumnType("bigint(25)");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("timestamp(3)");

                entity.Property(e => e.DestnAddrName)
                    .IsRequired()
                    .HasColumnName("destn_addr_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.DestnAddrStatus)
                    .HasColumnName("destn_addr_status")
                    .HasColumnType("int(4)");

                entity.Property(e => e.DestnAddrType)
                    .IsRequired()
                    .HasColumnName("destn_addr_type")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DestnAddrValue)
                    .IsRequired()
                    .HasColumnName("destn_addr_value")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasColumnName("updated_by")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("timestamp(3)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
