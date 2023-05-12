﻿using System;
using System.Drawing;
using System.Windows.Forms;
using ReportPrinterDatabase.Code.Helper;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfAnnotationRenderer;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.Enum;

namespace CosmoService.Code.UserControls.PDF
{
    public partial class ucPdfAnnotationRenderer : UserControl, IPdfRendererUserControl
    {
        private PdfRendererManagerBase<PdfAnnotationRendererModel> _manager;

        public ucPdfAnnotationRenderer()
        {
            InitializeComponent();
        }

        public bool ValidateInput()
        {
            epRendererInfo.Clear();
            var isValid = true;

            var annotationRendererType = (AnnotationRendererType)ecbAnnotationRendererType.SelectedItem;
            if (annotationRendererType == AnnotationRendererType.Text)
            {
                if (string.IsNullOrEmpty(tbContent.Text))
                {
                    epRendererInfo.SetError(tbContent, "Content is required");
                    isValid = false;
                }
            }
            else
            {
                if (!ucSqlSelector.ValidateInput())
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        public void Save(PdfRendererBaseModel rendererBase)
        {
            var renderer = PdfRendererHelper<PdfAnnotationRendererModel>.CreatePdfRenderer(rendererBase);

            renderer.AnnotationRendererType = (AnnotationRendererType)ecbAnnotationRendererType.SelectedItem;
            renderer.Title = string.IsNullOrEmpty(tbTitle.Text.Trim()) ? null : tbTitle.Text.Trim();
            renderer.Icon = (PdfTextAnnotationIcon)ecbIcon.SelectedItem;

            if (renderer.AnnotationRendererType == AnnotationRendererType.Sql)
            {
                renderer.SqlTemplateConfigSqlConfigId = ucSqlSelector.SelectedSql;
                renderer.SqlResColumn = ucSqlSelector.SqlResult;
            }
            else
                renderer.Content = tbContent.Text.Trim();

            _manager.Post(renderer);
        }

        private void ecbAnnotationRendererType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var type = (AnnotationRendererType)ecbAnnotationRendererType.SelectedItem;
            if (type == AnnotationRendererType.Sql)
            {
                ucSqlSelector.Visible = true;
                lblContent.Visible = tbContent.Visible = false;
                gbRendererInfo.Height = 255;
                Height = 260;
            }
            else
            {
                ucSqlSelector.Visible = false;
                lblContent.Visible = tbContent.Visible = true;

                tbContent.Location = new Point(155, 145);
                lblContent.Location = new Point(16, 145);
                gbRendererInfo.Height = 355;
                Height = 360;
            }
        }

        private void ucPdfAnnotationRenderer_Load(object sender, EventArgs e)
        {
            if (AppConfig.Instance.DatabaseManagerType == DatabaseManagerType.EFCore)
                _manager = new PdfAnnotationRendererEFCoreManager();
            else if (AppConfig.Instance.DatabaseManagerType == DatabaseManagerType.SP)
                _manager = new PdfAnnotationRendererSPManager();

            SetupScreen();
            ucSqlSelector.Init(true);
        }


        #region Helper

        private void SetupScreen()
        {
            ecbAnnotationRendererType.EnumType = typeof(AnnotationRendererType);
            ecbAnnotationRendererType.SelectedItem = AnnotationRendererType.Text;
            ecbIcon.EnumType = typeof(PdfTextAnnotationIcon);
        }

        #endregion
    }
}
