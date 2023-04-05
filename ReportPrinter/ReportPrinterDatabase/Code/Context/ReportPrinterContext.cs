using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
        public virtual DbSet<SqlConfig> SqlConfigs { get; set; }
        public virtual DbSet<SqlVariableConfig> SqlVariableConfigs { get; set; }

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
                    .HasColumnName("PRM_MessageId")
                    .HasDefaultValueSql("(newid())");

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

                entity.HasIndex(e => e.MessageId, "IX_PrintReportSqlVariable_MessageId");

                entity.Property(e => e.SqlVariableId)
                    .HasColumnName("PRSV_SqlVariableId")
                    .HasDefaultValueSql("(newid())");

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

            modelBuilder.Entity<SqlConfig>(entity =>
            {
                entity.HasKey(e => e.SqlConfigId)
                    .HasName("PK_dbo.SqlConfig");

                entity.ToTable("SqlConfig");

                entity.Property(e => e.SqlConfigId)
                    .HasColumnName("SC_SqlConfigId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DatabaseId)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SC_DatabaseId");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SC_Id");

                entity.Property(e => e.Query)
                    .IsRequired()
                    .HasColumnName("SC_Query");
            });

            modelBuilder.Entity<SqlVariableConfig>(entity =>
            {
                entity.HasKey(e => e.SqlVariableConfigId)
                    .HasName("PK_dbo.SqlVariableConfig");

                entity.ToTable("SqlVariableConfig");

                entity.HasIndex(e => e.SqlConfigId, "IX_SqlVariableConfig_SqlConfigId");

                entity.Property(e => e.SqlVariableConfigId)
                    .HasColumnName("SVC_SqlVariableConfigId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SVC_Name");

                entity.Property(e => e.SqlConfigId).HasColumnName("SVC_SqlConfigId");

                entity.HasOne(d => d.SqlConfig)
                    .WithMany(p => p.SqlVariableConfigs)
                    .HasForeignKey(d => d.SqlConfigId)
                    .HasConstraintName("FK_dbo.SqlConfig_dbo.SqlVariableConfig_SqlId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
