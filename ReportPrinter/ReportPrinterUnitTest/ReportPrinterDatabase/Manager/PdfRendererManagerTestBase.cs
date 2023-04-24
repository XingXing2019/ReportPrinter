using PdfSharp.Drawing;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using System;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager;
using NUnit.Framework;
using XFontStyle = ReportPrinterLibrary.Code.Enum.XFontStyle;
using XKnownColor = ReportPrinterLibrary.Code.Enum.XKnownColor;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager.ConfigManager.SqlTemplateConfigManager;
using System.Collections.Generic;
using ReportPrinterDatabase.Code.Executor;
using ReportPrinterDatabase.Code.Manager.ConfigManager.SqlConfigManager;
using ReportPrinterUnitTest.StoredProcedure;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Manager
{
    public abstract class PdfRendererManagerTestBase<T, E> : DatabaseTestBase<PdfRendererBaseModel> where T : PdfRendererBaseModel
    {
        private readonly ISqlConfigManager _sqlConfigMgr;
        private readonly ISqlTemplateConfigManager _sqlTemplateConfigMgr;

        protected PdfRendererManagerTestBase()
        {
            Manager = new PdfRendererBaseEFCoreManager();
            _sqlConfigMgr = new SqlConfigEFCoreManager();
            _sqlTemplateConfigMgr = new SqlTemplateConfigEFCoreManager();
        }

        [TearDown]
        public new void TearDown()
        {
            Manager.DeleteAll().Wait();
            _sqlConfigMgr.DeleteAll().Wait();
            _sqlTemplateConfigMgr.DeleteAll().Wait();
        }

        protected async Task DoTest(Type managerType, bool createNull)
        {
            try
            {
                var sqlInfoIds = await PostSqlInfo();
                var mgr = (PdfRendererManagerBase<T, E>)Activator.CreateInstance(managerType);

                var rendererBaseId = Guid.NewGuid();
                var rendererType = PdfRendererType.Barcode;

                var expectedRenderer = CreatePdfRendererBaseModel(rendererBaseId, rendererType, createNull);
                AssignPostProperties(expectedRenderer, createNull, sqlInfoIds[0]);

                await mgr.Post(expectedRenderer);

                var actualRenderer = await mgr.Get(rendererBaseId);
                Assert.IsNotNull(actualRenderer);
                AssertHelper.AssertObject(expectedRenderer, actualRenderer);

                UpdatePdfRendererBaseModel(expectedRenderer, createNull);
                AssignPutProperties(expectedRenderer, createNull, sqlInfoIds[1]);

                
                await mgr.Put(expectedRenderer);

                actualRenderer = await mgr.Get(rendererBaseId);
                Assert.IsNotNull(actualRenderer);
                AssertHelper.AssertObject(expectedRenderer, actualRenderer);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        protected abstract void AssignPostProperties(T expectedRenderer, bool createNull, Guid sqlInfoId);
        protected abstract void AssignPutProperties(T expectedRenderer, bool createNull, Guid sqlInfoId);


        private T CreatePdfRendererBaseModel(Guid rendererBaseId, PdfRendererType rendererType, bool createNull)
        {
            var renderer = Activator.CreateInstance<T>();

            renderer.PdfRendererBaseId = rendererBaseId;
            renderer.Id = "Test Pdf Renderer 1";
            renderer.RendererType = rendererType;
            renderer.Margin = createNull ? null : "10, 5, 10, 5";
            renderer.Padding = createNull ? null : "5, 10, 5, 10";
            renderer.HorizontalAlignment = createNull ? null : (HorizontalAlignment?)HorizontalAlignment.Left;
            renderer.VerticalAlignment = createNull ? null : (VerticalAlignment?)VerticalAlignment.Center;
            renderer.Position = createNull ? null : (Position?)Position.Static;
            renderer.Left = createNull ? null : (double?)10.5;
            renderer.Right = createNull ? null : (double?)5.5;
            renderer.Top = createNull ? null : (double?)20.5;
            renderer.Bottom = createNull ? null : (double?)15.5;
            renderer.FontSize = createNull ? null : (double?)6.5;
            renderer.FontFamily = createNull ? null : "Time New Roman";
            renderer.FontStyle = createNull ? null : (XFontStyle?)XFontStyle.Strikeout;
            renderer.Opacity = createNull ? null : (double?)0.9;
            renderer.BrushColor = createNull ? null : (XKnownColor?)XKnownColor.YellowGreen;
            renderer.BackgroundColor = createNull ? null : (XKnownColor?)XKnownColor.Brown;
            renderer.Row = 2;
            renderer.Column = 4;
            renderer.RowSpan = createNull ? null : (int?)5;
            renderer.ColumnSpan = createNull ? null : (int?)8;

            return renderer;
        }

        private void UpdatePdfRendererBaseModel(T renderer, bool createNull)
        {
            renderer.Id = "Test Pdf Renderer 2";
            renderer.Margin = createNull ? null : "15, 0, 15, 0";
            renderer.Padding = createNull ? null : "10, 5, 10, 5";
            renderer.HorizontalAlignment = createNull ? null : (HorizontalAlignment?)HorizontalAlignment.Right;
            renderer.VerticalAlignment = createNull ? null : (VerticalAlignment?)VerticalAlignment.Bottom;
            renderer.Position = createNull ? null : (Position?)Position.Relative;
            renderer.Left = createNull ? null : (double?)25.4;
            renderer.Right = createNull ? null : (double?)8.1;
            renderer.Top = createNull ? null : (double?)21.6;
            renderer.Bottom = createNull ? null : (double?)65.5;
            renderer.FontSize = createNull ? null : (double?)48.1;
            renderer.FontFamily = createNull ? null : "Caril";
            renderer.FontStyle = createNull ? null : (XFontStyle?)XFontStyle.Italic;
            renderer.Opacity = createNull ? null : (double?)0.75;
            renderer.BrushColor = createNull ? null : (XKnownColor?)XKnownColor.Yellow;
            renderer.BackgroundColor = createNull ? null : (XKnownColor?)XKnownColor.Black;
            renderer.Row = 3;
            renderer.Column = 5;
            renderer.RowSpan = createNull ? null : (int?)9;
            renderer.ColumnSpan = createNull ? null : (int?)1;
        }

        private async Task<List<Guid>> PostSqlInfo()
        {
            var sqlInfoIds = new List<Guid>();
            var executor = new StoredProcedureExecutor();

            for (int i = 0; i < 2; i++)
            {
                var sqlConfigId = Guid.NewGuid();
                var id = $"Test Sql {i + 1}";
                var databaseId = $"Test DB {i + 1}";
                var query = $"Test Query {i + 1}";
                var sqlVariableNames = new List<string> { $"Variable {i + 1}" };

                var sqlConfig = CreateSqlConfig(sqlConfigId, id, databaseId, query, sqlVariableNames);
                await _sqlConfigMgr.Post(sqlConfig);

                var sqlTemplateConfigId = Guid.NewGuid();
                var sqlTemplateId = $"Test Sql Template {i + 1}";
                var expectedSqlTemplateConfig = CreateSqlTemplateConfig(sqlTemplateConfigId, new List<SqlConfig> { sqlConfig }, sqlTemplateId);

                await _sqlTemplateConfigMgr.Post(expectedSqlTemplateConfig);

                var sqlTemplateConfigSqlConfig = await executor.ExecuteQueryOneAsync<SqlTemplateConfigSqlConfig>(new GetSqlTemplateConfigSqlConfig(sqlConfigId, sqlTemplateConfigId));

                sqlInfoIds.Add(sqlTemplateConfigSqlConfig.SqlTemplateConfigSqlConfigId);
            }

            return sqlInfoIds;
        }
    }
}