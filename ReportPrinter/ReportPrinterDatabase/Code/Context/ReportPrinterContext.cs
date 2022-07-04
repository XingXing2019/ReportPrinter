using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using ReportPrinterDatabase.Code.Database;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterLibrary.Code.Config.Configuration;

#nullable disable

namespace ReportPrinterDatabase.Code.Context
{
    public partial class ReportPrinterContext : DbContext
    {
        private readonly string _connectionString;
        private readonly LoggerFactory _loggerFactory;

        public ReportPrinterContext()
        {
            var databaseId = AppConfig.Instance.TargetDatabase;
            if (!DatabaseManager.Instance.TryGetConnectionString(databaseId, out var connectionString))
            {
                throw new ApplicationException($"Database connection: {databaseId} does not exist");
            }

            _connectionString = connectionString;
            _loggerFactory = new LoggerFactory(new[] { new DebugLoggerProvider() });
        }

        public ReportPrinterContext(DbContextOptions<ReportPrinterContext> options)
            : base(options) { }

        public virtual DbSet<PrintReportMessage> PrintReportMessages { get; set; }
        public virtual DbSet<PrintReportSqlVariable> PrintReportSqlVariables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
                optionsBuilder.UseLoggerFactory(_loggerFactory);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrintReportMessage>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                    .HasName("PK_dbo.PrintReportMessage");

                entity.ToTable("PrintReportMessage");

                entity.Property(e => e.MessageId)
                    .ValueGeneratedNever()
                    .HasColumnName("PRM_MessageId");

                entity.Property(e => e.CompleteTime)
                    .HasColumnType("datetime")
                    .HasColumnName("PRM_CompleteTime");

                entity.Property(e => e.CorrelationId).HasColumnName("PRM_CorrelationId");

                entity.Property(e => e.HasReprintFlag).HasColumnName("PRM_HasReprintFlag");

                entity.Property(e => e.NumberOfCopy).HasColumnName("PRM_NumberOfCopy");

                entity.Property(e => e.PrinterId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PRM_PrinterId");

                entity.Property(e => e.PublishTime)
                    .HasColumnType("datetime")
                    .HasColumnName("PRM_PublishTime");

                entity.Property(e => e.ReceiveTime)
                    .HasColumnType("datetime")
                    .HasColumnName("PRM_ReceiveTime");

                entity.Property(e => e.ReportType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PRM_ReportType");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PRM_Status");

                entity.Property(e => e.TemplateId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PRM_TemplateId");
            });

            modelBuilder.Entity<PrintReportSqlVariable>(entity =>
            {
                entity.HasKey(e => e.SqlVariableId)
                    .HasName("PK_dbo.PrintReportSqlVariable");

                entity.ToTable("PrintReportSqlVariable");

                entity.Property(e => e.SqlVariableId)
                    .ValueGeneratedNever()
                    .HasColumnName("PRSV_SqlVariableId");

                entity.Property(e => e.MessageId).HasColumnName("PRSV_MessageId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PRSV_Name");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PRSV_Value");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.PrintReportSqlVariables)
                    .HasForeignKey(d => d.MessageId)
                    .HasConstraintName("FK_dbo.PrintReportMessage_dbo.PrintReportSqlVariable_MessageId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
