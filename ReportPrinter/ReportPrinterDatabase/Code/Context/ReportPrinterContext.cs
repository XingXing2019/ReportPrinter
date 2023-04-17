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
        public virtual DbSet<SqlTemplateConfig> SqlTemplateConfigs { get; set; }
        public virtual DbSet<SqlTemplateConfigSqlConfig> SqlTemplateConfigSqlConfigs { get; set; }
        public virtual DbSet<SqlVariableConfig> SqlVariableConfigs { get; set; }
        public virtual DbSet<PdfRendererBase> PdfRendererBases { get; set; }
        public virtual DbSet<PdfAnnotationRenderer> PdfAnnotationRenderers { get; set; }
        public virtual DbSet<PdfBarcodeRenderer> PdfBarcodeRenderers { get; set; }


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
            modelBuilder.Entity<PdfAnnotationRenderer>(entity =>
            {
                entity.HasKey(e => e.PdfAnnotationRendererId)
                    .HasName("PK_dbo.PdfAnnotationRenderer");

                entity.ToTable("PdfAnnotationRenderer");

                entity.HasIndex(e => e.PdfRendererBaseId, "IX_PdfAnnotationRenderer_PdfRendererBaseId");

                entity.Property(e => e.PdfAnnotationRendererId)
                    .HasColumnName("PAR_PdfAnnotationRendererId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AnnotationRendererType).HasColumnName("PAR_AnnotationRendererType");

                entity.Property(e => e.Content)
                    .IsUnicode(false)
                    .HasColumnName("PAR_Content");

                entity.Property(e => e.Icon).HasColumnName("PAR_Icon");

                entity.Property(e => e.PdfRendererBaseId).HasColumnName("PAR_PdfRendererBaseId");

                entity.Property(e => e.SqlId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PAR_SqlId");

                entity.Property(e => e.SqlResColumn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PAR_SqlResColumn");

                entity.Property(e => e.SqlTemplateId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PAR_SqlTemplateId");

                entity.Property(e => e.Title)
                    .IsUnicode(false)
                    .HasColumnName("PAR_Title");

                entity.HasOne(d => d.PdfRendererBase)
                    .WithMany(p => p.PdfAnnotationRenderers)
                    .HasForeignKey(d => d.PdfRendererBaseId)
                    .HasConstraintName("FK_PdfAnnotationRenderer_PdfRendererBase_PdfRendererBaseId");
            });

            modelBuilder.Entity<PdfBarcodeRenderer>(entity =>
            {
                entity.HasKey(e => e.PdfBarcodeRendererId)
                    .HasName("PK_dbo.PdfBarcodeRenderer");

                entity.ToTable("PdfBarcodeRenderer");

                entity.HasIndex(e => e.PdfRendererBaseId, "IX_PdfBarcodeRenderer_PdfRendererBaseId");

                entity.Property(e => e.PdfBarcodeRendererId)
                    .HasColumnName("PBR_PdfBarcodeRendererId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.BarcodeFormat).HasColumnName("PBR_BarcodeFormat");

                entity.Property(e => e.PdfRendererBaseId).HasColumnName("PBR_PdfRendererBaseId");

                entity.Property(e => e.ShowBarcodeText).HasColumnName("PBR_ShowBarcodeText");

                entity.Property(e => e.SqlId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PBR_SqlId");

                entity.Property(e => e.SqlResColumn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PBR_SqlResColumn");

                entity.Property(e => e.SqlTemplateId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PBR_SqlTemplateId");

                entity.HasOne(d => d.PdfRendererBase)
                    .WithMany(p => p.PdfBarcodeRenderers)
                    .HasForeignKey(d => d.PdfRendererBaseId)
                    .HasConstraintName("FK_PdfBarcodeRenderer_PdfRendererBase_PdfRendererBaseId");
            });

            modelBuilder.Entity<PdfRendererBase>(entity =>
            {
                entity.HasKey(e => e.PdfRendererBaseId)
                    .HasName("PK_dbo.PdfRendererBase");

                entity.ToTable("PdfRendererBase");

                entity.HasIndex(e => e.Id, "IX_PdfRendererBase_Id");

                entity.Property(e => e.PdfRendererBaseId)
                    .HasColumnName("PRB_PdfRendererBaseId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.BackgroundColor).HasColumnName("PRB_BackgroundColor");

                entity.Property(e => e.Bottom).HasColumnName("PRB_Bottom");

                entity.Property(e => e.BrushColor).HasColumnName("PRB_BrushColor");

                entity.Property(e => e.Column).HasColumnName("PRB_Column");

                entity.Property(e => e.ColumnSpan).HasColumnName("PRB_ColumnSpan");

                entity.Property(e => e.FontFamily)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRB_FontFamily");

                entity.Property(e => e.FontSize).HasColumnName("PRB_FontSize");

                entity.Property(e => e.FontStyle).HasColumnName("PRB_FontStyle");

                entity.Property(e => e.HorizontalAlignment).HasColumnName("PRB_HorizontalAlignment");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRB_Id");

                entity.Property(e => e.Left).HasColumnName("PRB_Left");

                entity.Property(e => e.Margin)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PRB_Margin");

                entity.Property(e => e.Opacity).HasColumnName("PRB_Opacity");

                entity.Property(e => e.Padding)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PRB_Padding");

                entity.Property(e => e.Position).HasColumnName("PRB_Position");

                entity.Property(e => e.RendererType).HasColumnName("PRB_RendererType");

                entity.Property(e => e.Right).HasColumnName("PRB_Right");

                entity.Property(e => e.Row).HasColumnName("PRB_Row");

                entity.Property(e => e.RowSpan).HasColumnName("PRB_RowSpan");

                entity.Property(e => e.Top).HasColumnName("PRB_Top");

                entity.Property(e => e.VerticalAlignment).HasColumnName("PRB_VerticalAlignment");
            });

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
                    .HasConstraintName("FK_PrintReportSqlVariable_PrintReportMessage_MessageId");
            });

            modelBuilder.Entity<SqlConfig>(entity =>
            {
                entity.HasKey(e => e.SqlConfigId)
                    .HasName("PK_dbo.SqlConfig");

                entity.ToTable("SqlConfig");

                entity.HasIndex(e => e.DatabaseId, "IX_SqlConfig_DatabaseId");

                entity.Property(e => e.SqlConfigId)
                    .HasColumnName("SC_SqlConfigId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DatabaseId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("SC_DatabaseId");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SC_Id");

                entity.Property(e => e.Query)
                    .IsRequired()
                    .HasColumnName("SC_Query");
            });

            modelBuilder.Entity<SqlTemplateConfig>(entity =>
            {
                entity.HasKey(e => e.SqlTemplateConfigId)
                    .HasName("PK_dbo.SqlTemplateConfig");

                entity.ToTable("SqlTemplateConfig");

                entity.HasIndex(e => e.Id, "IX_SqlTemplateConfig_Id");

                entity.Property(e => e.SqlTemplateConfigId)
                    .HasColumnName("STC_SqlTemplateConfigId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STC_Id");
            });

            modelBuilder.Entity<SqlTemplateConfigSqlConfig>(entity =>
            {
                entity.HasKey(e => new { StcscSqlTemplateConfigId = e.SqlTemplateConfigId, StcscSqlConfigId = e.SqlConfigId })
                    .HasName("PK_dbo.SqlTemplateConfigSqlConfig");

                entity.ToTable("SqlTemplateConfigSqlConfig");

                entity.Property(e => e.SqlTemplateConfigId).HasColumnName("STCSC_SqlTemplateConfigId");

                entity.Property(e => e.SqlConfigId).HasColumnName("STCSC_SqlConfigId");

                entity.HasOne(d => d.SqlConfig)
                    .WithMany(p => p.SqlTemplateConfigSqlConfigs)
                    .HasForeignKey(d => d.SqlConfigId)
                    .HasConstraintName("FK_SqlTemplateConfigSqlConfig_SqlConfig_SqlConfigId");

                entity.HasOne(d => d.SqlTemplateConfig)
                    .WithMany(p => p.SqlTemplateConfigSqlConfigs)
                    .HasForeignKey(d => d.SqlTemplateConfigId)
                    .HasConstraintName("FK_SqlTemplateConfigSqlConfig_SqlTemplateConfig_SqlTemplateConfigId");
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
                    .HasConstraintName("FK_SqlVariableConfig_SqlConfig_SqlConfigId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
