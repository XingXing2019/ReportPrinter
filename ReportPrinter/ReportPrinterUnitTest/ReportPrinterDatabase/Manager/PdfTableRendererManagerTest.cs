using System;
using System.Collections.Generic;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager;
using System.Threading.Tasks;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using NUnit.Framework;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfTableRenderer;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Manager
{
    public class PdfTableRendererManagerTest : PdfRendererManagerTestBase<PdfTableRendererModel>
    {
        private PdfTableRendererModel _subTableRenderer;

        [Test]
        [TestCase(typeof(PdfTableRendererEFCoreManager), true)]
        [TestCase(typeof(PdfTableRendererEFCoreManager), false)]
        [TestCase(typeof(PdfTableRendererSPManager), false)]
        [TestCase(typeof(PdfTableRendererSPManager), false)]
        public async Task TestPdfTableRendererManager_Get(Type managerType, bool createNull)
        {
            _subTableRenderer = await PostPdfTableRenderer(managerType);
            await DoTest(managerType, PdfRendererType.Barcode, createNull);
        }


        protected override void AssignPostProperties(PdfTableRendererModel expectedRenderer, bool createNull, Guid sqlInfoId)
        {
            expectedRenderer.BoardThickness = createNull ? null : (double?)1.7;
            expectedRenderer.LineSpace = createNull ? null : (double?)2.5;
            expectedRenderer.TitleHorizontalAlignment = createNull ? null : (HorizontalAlignment?)HorizontalAlignment.Right;
            expectedRenderer.HideTitle = createNull ? null : (bool?)false;
            expectedRenderer.Space = createNull ? null : (double?)1.8;
            expectedRenderer.TitleColor = createNull ? null : (XKnownColor?)XKnownColor.BurlyWood;
            expectedRenderer.TitleColorOpacity = createNull ? null : (double?)0.67;
            expectedRenderer.SqlTemplateConfigSqlConfigId = sqlInfoId;
            expectedRenderer.SqlTemplateId = "Test Sql Template 1";
            expectedRenderer.SqlId = "Test Sql 1";
            expectedRenderer.SqlResColumns = new List<SqlResColumnModel>
            {
                new SqlResColumnModel
                {
                    PdfRendererBaseId = expectedRenderer.PdfRendererBaseId,
                    Id = "Test Res Column a",
                    Title = "Test Res Column Title a",
                    WidthRatio = 1.3,
                    Position = Position.Relative,
                    Left = 2.9,
                    Right = 1.2
                }
            };
            expectedRenderer.SqlVariable = "Test Sql Variable a";
            expectedRenderer.SubPdfTableRendererId = _subTableRenderer.PdfRendererBaseId;
            expectedRenderer.SubPdfTableRenderer = _subTableRenderer;
        }

        protected override void AssignPutProperties(PdfTableRendererModel expectedRenderer, bool createNull, Guid sqlInfoId)
        {
            expectedRenderer.BoardThickness = createNull ? null : (double?)2.6;
            expectedRenderer.LineSpace = createNull ? null : (double?)1.2;
            expectedRenderer.TitleHorizontalAlignment = createNull ? null : (HorizontalAlignment?)HorizontalAlignment.Center;
            expectedRenderer.HideTitle = createNull ? null : (bool?)true;
            expectedRenderer.Space = createNull ? null : (double?)2.8;
            expectedRenderer.TitleColor = createNull ? null : (XKnownColor?)XKnownColor.Blue;
            expectedRenderer.TitleColorOpacity = createNull ? null : (double?)0.85;
            expectedRenderer.SqlTemplateConfigSqlConfigId = sqlInfoId;
            expectedRenderer.SqlTemplateId = "Test Sql Template 2";
            expectedRenderer.SqlId = "Test Sql 2";
            expectedRenderer.SqlResColumns = new List<SqlResColumnModel>
            {
                new SqlResColumnModel
                {
                    PdfRendererBaseId = expectedRenderer.PdfRendererBaseId,
                    Id = "Test Res Column A",
                    Title = "Test Res Column Title A",
                    WidthRatio = 1.4,
                    Position = Position.Static,
                    Left = 2.5,
                    Right = 1.3
                }
            };
            expectedRenderer.SqlVariable = "Test Sql Variable A";
            expectedRenderer.SubPdfTableRendererId = null;
            expectedRenderer.SubPdfTableRenderer = null;
        }


        #region Helper


        private async Task<PdfTableRendererModel> PostPdfTableRenderer(Type managerType)
        {
            var sqlInfoIds = await PostSqlInfo();
            var mgr = (PdfRendererManagerBase<PdfTableRendererModel>)Activator.CreateInstance(managerType);

            var rendererBase1Id = Guid.NewGuid();
            var renderer1 = CreatePdfRendererBaseModel(rendererBase1Id, PdfRendererType.Table, false);

            renderer1.BoardThickness = 1.5;
            renderer1.LineSpace = 2.5;
            renderer1.TitleHorizontalAlignment = HorizontalAlignment.Center;
            renderer1.HideTitle = true;
            renderer1.Space = 1.4;
            renderer1.TitleColor = XKnownColor.AliceBlue;
            renderer1.TitleColorOpacity = 0.5;
            renderer1.SqlTemplateConfigSqlConfigId = sqlInfoIds[2];
            renderer1.SqlTemplateId = "Test Sql Template 3";
            renderer1.SqlId = "Test Sql 3";
            renderer1.SqlResColumns = new List<SqlResColumnModel>
            {
                new SqlResColumnModel
                {
                    PdfRendererBaseId = renderer1.PdfRendererBaseId,
                    Id = "Test Res Column 1",
                    Title = "Test Res Column Title 1",
                    WidthRatio = 2.5,
                    Position = Position.Relative,
                    Left = 1.9,
                    Right = 0.2
                }
            };
            renderer1.SqlVariable = "Test Sql Variable 1";

            await mgr.Post(renderer1);

            var rendererBase2Id = Guid.NewGuid();
            var renderer2 = CreatePdfRendererBaseModel(rendererBase2Id, PdfRendererType.Table, true);

            renderer2.BoardThickness = 2.6;
            renderer2.LineSpace = 1.5;
            renderer2.TitleHorizontalAlignment = HorizontalAlignment.Right;
            renderer2.HideTitle = false;
            renderer2.Space = 1.9;
            renderer2.TitleColor = XKnownColor.Yellow;
            renderer2.TitleColorOpacity = 0.9;
            renderer2.SqlTemplateConfigSqlConfigId = sqlInfoIds[3];
            renderer2.SqlTemplateId = "Test Sql Template 4";
            renderer2.SqlId = "Test Sql 4";
            renderer2.SqlResColumns = new List<SqlResColumnModel>
            {
                new SqlResColumnModel
                {
                    PdfRendererBaseId = renderer2.PdfRendererBaseId,
                    Id = "Test Res Column A",
                    Title = "Test Res Column Title A",
                    WidthRatio = 2.1,
                    Position = Position.Static,
                    Left = 1.1,
                    Right = 2.2
                }
            };
            renderer2.SqlVariable = "Test Sql Variable A";
            renderer2.SubPdfTableRendererId = rendererBase1Id;

            await mgr.Post(renderer2);

            renderer2.SubPdfTableRenderer = renderer1;
            return renderer2;
        }

        #endregion
    }
}