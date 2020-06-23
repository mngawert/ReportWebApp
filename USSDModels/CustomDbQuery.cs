﻿using Microsoft.EntityFrameworkCore;
using ReportWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.USSDModels
{
    public partial class TOT_USSD_CDRContext : DbContext
    {

        public DbSet<Report1ViewModel> Report1ViewModel { get; set; }
        public DbSet<MngmtReportViewModel> MngmtReportViewModel { get; set; }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report1ViewModel>().HasNoKey();
            modelBuilder.Entity<MngmtReportViewModel>().HasNoKey();
        }
    }
}
