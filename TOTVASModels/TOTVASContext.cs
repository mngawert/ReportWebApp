using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ReportWebApp.TOTVASModels
{
    public partial class TOTVASContext : DbContext
    {
        public TOTVASContext()
        {
        }

        public TOTVASContext(DbContextOptions<TOTVASContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DestnAddrMap> DestnAddrMap { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("name=TOT_VAS_Database");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DestnAddrMap>(entity =>
            {
                entity.HasKey(e => e.DestnAddrId);

                entity.Property(e => e.DestnAddrId).ValueGeneratedOnAdd();

                entity.Property(e => e.DestnAddrName).IsRequired();

                entity.Property(e => e.DestnAddrType).IsRequired();

                entity.Property(e => e.DestnAddrValue).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).ValueGeneratedOnAdd();

                entity.Property(e => e.Group).IsRequired();

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.UserName).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
