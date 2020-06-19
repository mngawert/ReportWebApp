using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ReportWebApp.IVRModels
{
    public partial class TOT_IVR_CDRContext : DbContext
    {
        public TOT_IVR_CDRContext()
        {
        }

        public TOT_IVR_CDRContext(DbContextOptions<TOT_IVR_CDRContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DestnAddrMap> DestnAddrMap { get; set; }
        public virtual DbSet<TransCdr01> TransCdr01 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("name=TOT_IVR_CDR_Database");
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

            modelBuilder.Entity<TransCdr01>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("trans_cdr_01");

                entity.HasIndex(e => e.AppCause)
                    .HasName("appcause");

                entity.HasIndex(e => e.ReceiptTimestamp)
                    .HasName("recp_time");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("service_id_idx");

                entity.HasIndex(e => e.SessionId)
                    .HasName("sess_id");

                entity.Property(e => e.AmountDeducted)
                    .HasColumnName("amount_deducted")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AppCause)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AppInfo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AppKey)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AppRequest)
                    .HasColumnName("APP_REQUEST")
                    .HasColumnType("timestamp(3)")
                    .HasDefaultValueSql("'0000-00-00 00:00:00.000'");

                entity.Property(e => e.AppResponse)
                    .HasColumnName("APP_RESPONSE")
                    .HasColumnType("timestamp(3)")
                    .HasDefaultValueSql("'0000-00-00 00:00:00.000'");

                entity.Property(e => e.Cgi).HasColumnType("bigint(25)");

                entity.Property(e => e.ChargeStatus)
                    .HasColumnName("charge_status")
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.DataCodingScheme)
                    .HasColumnName("data_coding_scheme")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DataLength)
                    .HasColumnName("data_length")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DeliveryTime)
                    .HasColumnName("delivery_time")
                    .HasColumnType("timestamp(3)")
                    .HasDefaultValueSql("'0000-00-00 00:00:00.000'");

                entity.Property(e => e.DestinationAddress)
                    .HasColumnName("destination_address")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FeatureId)
                    .HasColumnName("feature_id")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.InitiationDirection)
                    .HasColumnName("initiation_direction")
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.InterfaceId)
                    .HasColumnName("interface_id")
                    .HasColumnType("int(7)");

                entity.Property(e => e.MenuLevel)
                    .HasColumnName("menu_level")
                    .HasColumnType("int(4)");

                entity.Property(e => e.MessageStatus)
                    .HasColumnName("message_status")
                    .HasColumnType("int(4)");

                entity.Property(e => e.MessageType)
                    .HasColumnName("message_type")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'11'");

                entity.Property(e => e.MonthIndex)
                    .HasColumnName("month_index")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NodeId)
                    .HasColumnName("node_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.OriginationAddress)
                    .HasColumnName("origination_address")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiptTimestamp)
                    .HasColumnName("receipt_timestamp")
                    .HasColumnType("timestamp(3)")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP(3)'");

                entity.Property(e => e.SequenceNo)
                    .HasColumnName("SEQUENCE_NO")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ServerId)
                    .HasColumnName("Server_Id")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceId)
                    .HasColumnName("service_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SessionId)
                    .HasColumnName("session_id")
                    .HasColumnType("bigint(25)");

                entity.Property(e => e.SubscriberType)
                    .HasColumnName("subscriber_type")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Taskid)
                    .HasColumnName("taskid")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionId)
                    .HasColumnName("transaction_id")
                    .HasColumnType("bigint(25)");

                entity.Property(e => e.TraversalPath)
                    .HasColumnName("traversal_path")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UserData)
                    .HasColumnName("user_data")
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.Vlr)
                    .HasColumnName("vlr")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
