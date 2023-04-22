﻿using NUnit.Framework;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using System.Threading.Tasks;
using System;
using PdfSharp.Pdf.Annotations;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfAnnotationRenderer;

namespace ReportPrinterUnitTest.ReportPrinterDatabase.Manager
{
    public class PdfAnnotationRendererManagerTest : PdfRendererManagerTestBase<PdfAnnotationRendererModel>
    {
        [Test]
        [TestCase(typeof(PdfAnnotationRendererEFCoreManager), true)]
        [TestCase(typeof(PdfAnnotationRendererEFCoreManager), false)]
        [TestCase(typeof(PdfAnnotationRendererSPManager), true)]
        [TestCase(typeof(PdfAnnotationRendererSPManager), false)]
        public async Task TesPdfAnnotationRendererEFCoreManager_Get(Type managerType, bool createNull)
        {
            try
            {
                var mgr = (PdfRendererManagerBase<PdfAnnotationRendererModel>)Activator.CreateInstance(managerType);

                var rendererBaseId = Guid.NewGuid();
                var rendererType = PdfRendererType.Annotation;

                var expectedRenderer = CreatePdfRendererBaseModel(rendererBaseId, rendererType, !createNull);

                expectedRenderer.AnnotationRendererType = AnnotationRendererType.Text;
                expectedRenderer.Title = createNull ? null : "Test Title 1";
                expectedRenderer.Icon = createNull ? null : (PdfTextAnnotationIcon?)PdfTextAnnotationIcon.Insert;
                expectedRenderer.SqlTemplateId = createNull ? null : "Test Sql Template 1";
                expectedRenderer.SqlId = createNull ? null : "Test Sql 1";
                expectedRenderer.SqlResColumn = createNull ? null : "Test Res Column 1";

                await mgr.Post(expectedRenderer);

                var actualRenderer = await mgr.Get(rendererBaseId);
                Assert.IsNotNull(actualRenderer);

                AssertHelper.AssertObject(expectedRenderer, actualRenderer);

                expectedRenderer = CreatePdfRendererBaseModel(rendererBaseId, rendererType, createNull);

                expectedRenderer.AnnotationRendererType = AnnotationRendererType.Sql;
                expectedRenderer.Title = createNull ? null : "Test Title 2";
                expectedRenderer.Icon = createNull ? null : (PdfTextAnnotationIcon?)PdfTextAnnotationIcon.Insert;
                expectedRenderer.SqlTemplateId = createNull ? null : "Test Sql Template 2";
                expectedRenderer.SqlId = createNull ? null : "Test Sql 2";
                expectedRenderer.SqlResColumn = createNull ? null : "Test Res Column 2";

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
    }
}