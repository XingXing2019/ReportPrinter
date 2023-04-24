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

        public virtual DbSet<PdfAnnotationRenderer> PdfAnnotationRenderers { get; set; }
        public virtual DbSet<PdfBarcodeRenderer> PdfBarcodeRenderers { get; set; }
        public virtual DbSet<PdfImageRenderer> PdfImageRenderers { get; set; }
        public virtual DbSet<PdfPageNumberRenderer> PdfPageNumberRenderers { get; set; }
        public virtual DbSet<PdfRendererBase> PdfRendererBases { get; set; }
        public virtual DbSet<PdfReprintMarkRenderer> PdfReprintMarkRenderers { get; set; }
        public virtual DbSet<PdfTextRenderer> PdfTextRenderers { get; set; }
        public virtual DbSet<PdfWaterMarkRenderer> PdfWaterMarkRenderers { get; set; }
        public virtual DbSet<PrintReportMessage> PrintReportMessages { get; set; }
        public virtual DbSet<PrintReportSqlVariable> PrintReportSqlVariables { get; set; }
        public virtual DbSet<SqlConfig> SqlConfigs { get; set; }
        public virtual DbSet<SqlResColumnConfig> SqlResColumnConfigs { get; set; }
        public virtual DbSet<SqlTemplateConfig> SqlTemplateConfigs { get; set; }
        public virtual DbSet<SqlTemplateConfigSqlConfig> SqlTemplateConfigSqlConfigs { get; set; }
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

                entity.Property(e => e.SqlTemplateConfigSqlConfigId).HasColumnName("PAR_SqlTemplateConfigSqlConfigId");

                entity.Property(e => e.Title)
                    .IsUnicode(false)
                    .HasColumnName("PAR_Title");

                entity.HasOne(d => d.PdfRendererBase)
                    .WithMany(p => p.PdfAnnotationRenderers)
                    .HasForeignKey(d => d.PdfRendererBaseId)
                    .HasConstraintName("FK_PdfAnnotationRenderer_PdfRendererBase_PdfRendererBaseId");

                entity.HasOne(d => d.SqlTemplateConfigSqlConfig)
                    .WithMany(p => p.PdfAnnotationRenderers)
                    .HasForeignKey(d => d.SqlTemplateConfigSqlConfigId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PdfAnnotationRenderer_SqlTemplateConfigSqlConfig_SqlTemplateConfigSqlConfigId");
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

                entity.Property(e => e.SqlTemplateConfigSqlConfigId).HasColumnName("PBR_SqlTemplateConfigSqlConfigId");

                entity.HasOne(d => d.PdfRendererBase)
                    .WithMany(p => p.PdfBarcodeRenderers)
                    .HasForeignKey(d => d.PdfRendererBaseId)
                    .HasConstraintName("FK_PdfBarcodeRenderer_PdfRendererBase_PdfRendererBaseId");

                entity.HasOne(d => d.SqlTemplateConfigSqlConfig)
                    .WithMany(p => p.PdfBarcodeRenderers)
                    .HasForeignKey(d => d.SqlTemplateConfigSqlConfigId)
                    .HasConstraintName("FK_PdfBarcodeRenderer_SqlTemplateConfigSqlConfig_SqlTemplateConfigSqlConfigId");
            });

            modelBuilder.Entity<PdfImageRenderer>(entity =>
            {
                entity.HasKey(e => e.PdfImageRendererId)
                    .HasName("PK_dbo.PdfImageRenderer");

                entity.ToTable("PdfImageRenderer");

                entity.HasIndex(e => e.PdfRendererBaseId, "IX_PdfImageRenderer_PdfRendererBaseId");

                entity.Property(e => e.PdfImageRendererId)
                    .HasColumnName("PIR_PdfImageRendererId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ImageSource)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("PIR_ImageSource");

                entity.Property(e => e.PdfRendererBaseId).HasColumnName("PIR_PdfRendererBaseId");

                entity.Property(e => e.SourceType).HasColumnName("PIR_SourceType");

                entity.HasOne(d => d.PdfRendererBase)
                    .WithMany(p => p.PdfImageRenderers)
                    .HasForeignKey(d => d.PdfRendererBaseId)
                    .HasConstraintName("FK_PdfImageRenderer_PdfRendererBase_PdfRendererBaseId");
            });

            modelBuilder.Entity<PdfPageNumberRenderer>(entity =>
            {
                entity.HasKey(e => e.PdfPageNumberRendererId)
                    .HasName("PK_dbo.PdfPageNumberRenderer");

                entity.ToTable("PdfPageNumberRenderer");

                entity.HasIndex(e => e.PdfRendererBaseId, "IX_PdfPageNumberRenderer_PdfRendererBaseId");

                entity.Property(e => e.PdfPageNumberRendererId)
                    .HasColumnName("PPNR_PdfPageNumberRendererId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.EndPage).HasColumnName("PPNR_EndPage");

                entity.Property(e => e.PageNumberLocation).HasColumnName("PPNR_PageNumberLocation");

                entity.Property(e => e.PdfRendererBaseId).HasColumnName("PPNR_PdfRendererBaseId");

                entity.Property(e => e.StartPage).HasColumnName("PPNR_StartPage");

                entity.HasOne(d => d.PdfRendererBase)
                    .WithMany(p => p.PdfPageNumberRenderers)
                    .HasForeignKey(d => d.PdfRendererBaseId)
                    .HasConstraintName("FK_PdfPageNumberRenderer_PdfRendererBase_PdfRendererBaseId");
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

            modelBuilder.Entity<PdfReprintMarkRenderer>(entity =>
            {
                entity.HasKey(e => e.PdfReprintMarkRendererId)
                    .HasName("PK_dbo.PdfReprintMarkRenderer");

                entity.ToTable("PdfReprintMarkRenderer");

                entity.HasIndex(e => e.PdfRendererBaseId, "IX_PdfReprintMarkRenderer_PdfRendererBaseId");

                entity.Property(e => e.PdfReprintMarkRendererId)
                    .HasColumnName("PRMR_PdfReprintMarkRendererId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.BoardThickness).HasColumnName("PRMR_BoardThickness");

                entity.Property(e => e.Location).HasColumnName("PRMR_Location");

                entity.Property(e => e.PdfRendererBaseId).HasColumnName("PRMR_PdfRendererBaseId");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PRMR_Text");

                entity.HasOne(d => d.PdfRendererBase)
                    .WithMany(p => p.PdfReprintMarkRenderers)
                    .HasForeignKey(d => d.PdfRendererBaseId)
                    .HasConstraintName("FK_PdfReprintMarkRenderer_PdfRendererBase_PdfRendererBaseId");
            });

            modelBuilder.Entity<PdfTextRenderer>(entity =>
            {
                entity.HasKey(e => e.PdfTextRendererId)
                    .HasName("PK_dbo.PdfTextRenderer");

                entity.ToTable("PdfTextRenderer");

                entity.HasIndex(e => e.PdfRendererBaseId, "IX_PdfTextRenderer_PdfRendererBaseId");

                entity.Property(e => e.PdfTextRendererId)
                    .HasColumnName("PTR_PdfTextRendererId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Content)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PTR_Content");

                entity.Property(e => e.Mask)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PTR_Mask");

                entity.Property(e => e.PdfRendererBaseId).HasColumnName("PTR_PdfRendererBaseId");

                entity.Property(e => e.SqlTemplateConfigSqlConfigId).HasColumnName("PTR_SqlTemplateConfigSqlConfigId");

                entity.Property(e => e.TextRendererType).HasColumnName("PTR_TextRendererType");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PTR_Title");

                entity.HasOne(d => d.PdfRendererBase)
                    .WithMany(p => p.PdfTextRenderers)
                    .HasForeignKey(d => d.PdfRendererBaseId)
                    .HasConstraintName("FK_PdfTextRenderer_PdfRendererBase_PdfRendererBaseId");

                entity.HasOne(d => d.SqlTemplateConfigSqlConfig)
                    .WithMany(p => p.PdfTextRenderers)
                    .HasForeignKey(d => d.SqlTemplateConfigSqlConfigId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PdfTextRenderer_SqlTemplateConfigSqlConfig_SqlTemplateConfigSqlConfigId");
            });

            modelBuilder.Entity<PdfWaterMarkRenderer>(entity =>
            {
                entity.HasKey(e => e.PdfWaterMarkRendererId)
                    .HasName("PK_dbo.PdfWaterMarkRenderer");

                entity.ToTable("PdfWaterMarkRenderer");

                entity.HasIndex(e => e.PdfRendererBaseId, "IX_PdfWaterMarkRenderer_PdfRendererBaseId");

                entity.Property(e => e.PdfWaterMarkRendererId)
                    .HasColumnName("PWMR_PdfWaterMarkRendererId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Content)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PWMR_Content");

                entity.Property(e => e.EndPage).HasColumnName("PWMR_EndPage");

                entity.Property(e => e.Location).HasColumnName("PWMR_Location");

                entity.Property(e => e.PdfRendererBaseId).HasColumnName("PWMR_PdfRendererBaseId");

                entity.Property(e => e.Rotate).HasColumnName("PWMR_Rotate");

                entity.Property(e => e.SqlTemplateConfigSqlConfigId).HasColumnName("PWMR_SqlTemplateConfigSqlConfigId");

                entity.Property(e => e.StartPage).HasColumnName("PWMR_StartPage");

                entity.Property(e => e.WaterMarkType).HasColumnName("PWMR_WaterMarkType");

                entity.HasOne(d => d.PdfRendererBase)
                    .WithMany(p => p.PdfWaterMarkRenderers)
                    .HasForeignKey(d => d.PdfRendererBaseId)
                    .HasConstraintName("FK_PdfWaterMarkRenderer_PdfRendererBase_PdfRendererBaseId");

                entity.HasOne(d => d.SqlTemplateConfigSqlConfig)
                    .WithMany(p => p.PdfWaterMarkRenderers)
                    .HasForeignKey(d => d.SqlTemplateConfigSqlConfigId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PdfWaterMarkRenderer_SqlTemplateConfigSqlConfig_SqlTemplateConfigSqlConfigId");
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

            modelBuilder.Entity<SqlResColumnConfig>(entity =>
            {
                entity.HasKey(e => e.SqlResColumnConfigId)
                    .HasName("PK_dbo.SqlResColumnConfig");

                entity.ToTable("SqlResColumnConfig");

                entity.HasIndex(e => e.PdfRendererBaseId, "IX_SqlResColumnConfig_PdfRendererBaseId");

                entity.Property(e => e.SqlResColumnConfigId)
                    .HasColumnName("SRCC_SqlResColumnConfigId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SRCC_Name");

                entity.Property(e => e.PdfRendererBaseId).HasColumnName("SRCC_PdfRendererBaseId");

                entity.HasOne(d => d.PdfRendererBase)
                    .WithMany(p => p.SqlResColumnConfigs)
                    .HasForeignKey(d => d.PdfRendererBaseId)
                    .HasConstraintName("FK_SqlResColumnConfig_PdfRendererBase_PdfRendererBaseId");
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
                entity.HasKey(e => e.SqlTemplateConfigSqlConfigId)
                    .HasName("PK_dbo.SqlTemplateConfigSqlConfig");

                entity.ToTable("SqlTemplateConfigSqlConfig");

                entity.HasIndex(e => e.SqlConfigId, "IX_SqlTemplateConfigSqlConfig_SqlConfigId");

                entity.HasIndex(e => e.SqlTemplateConfigId, "IX_SqlTemplateConfigSqlConfig_SqlTemplateConfigId");

                entity.Property(e => e.SqlTemplateConfigSqlConfigId)
                    .HasColumnName("STCSC_SqlTemplateConfigSqlConfigId")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.SqlConfigId).HasColumnName("STCSC_SqlConfigId");

                entity.Property(e => e.SqlTemplateConfigId).HasColumnName("STCSC_SqlTemplateConfigId");

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
