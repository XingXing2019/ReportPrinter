﻿using System;
using System.IO;
using System.Windows.Forms;
using CosmoService.Code.UserControls.SQL;
using ReportPrinterDatabase.Code.Helper;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfImageRenderer;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.Enum;

namespace CosmoService.Code.UserControls.PDF
{
    public partial class ucPdfImageRenderer : UserControl, IPdfRendererUserControl
    {
        private PdfRendererManagerBase<PdfImageRendererModel> _manager;

        public ucPdfImageRenderer()
        {
            InitializeComponent();
        }

        public bool ValidateInput()
        {
            epRendererInfo.Clear();
            var isValid = true;

            if (string.IsNullOrEmpty(tbImageSource.Text.Trim()))
            {
                epRendererInfo.SetError(tbImageSource, "Image source is required");
                isValid = false;
            }

            return isValid;
        }

        public void Save(PdfRendererBaseModel rendererBase)
        {
            var renderer = PdfRendererHelper<PdfImageRendererModel>.CreatePdfRenderer(rendererBase);

            renderer.SourceType = (SourceType)ecbSourceType.SelectedItem;
            renderer.ImageSource = tbImageSource.Text.Trim();

            _manager.Post(renderer);
        }

        public void ClearError()
        {
            epRendererInfo.Clear();
        }

        private void ucPdfImageRenderer_Load(object sender, EventArgs e)
        {
            if (AppConfig.Instance.DatabaseManagerType == DatabaseManagerType.EFCore)
                _manager = new PdfImageRendererEFCoreManager();
            else if (AppConfig.Instance.DatabaseManagerType == DatabaseManagerType.SP)
                _manager = new PdfImageRendererSPManager();

            SetupScreen();
        }

        #region Helper

        private void SetupScreen()
        {
            ecbSourceType.EnumType = typeof(SourceType);
            ecbSourceType.SelectedItem = SourceType.Local;
        }

        #endregion
    }
}
