using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ReportWebApp.VOICEModels
{
    public partial class TOT_VOICE_CDRContext : DbContext
    {
        public TOT_VOICE_CDRContext()
        {
        }

        public TOT_VOICE_CDRContext(DbContextOptions<TOT_VOICE_CDRContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("name=TOT_VOICE_CDR_Database");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
