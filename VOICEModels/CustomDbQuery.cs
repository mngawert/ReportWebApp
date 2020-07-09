using Microsoft.EntityFrameworkCore;
using ReportWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.VOICEModels
{
    public partial class TOT_VOICE_CDRContext : DbContext
    {

        public DbSet<Report1ViewModel> Report1ViewModel { get; set; }
        public DbSet<MngmtReportViewModel> MngmtReportViewModel { get; set; }
        public DbSet<DashboardReport1ViewModel> DashboardReport1ViewModel { get; set; }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report1ViewModel>().HasNoKey();
            modelBuilder.Entity<MngmtReportViewModel>().HasNoKey();
            modelBuilder.Entity<DashboardReport1ViewModel>().HasNoKey();
        }
    }
}
