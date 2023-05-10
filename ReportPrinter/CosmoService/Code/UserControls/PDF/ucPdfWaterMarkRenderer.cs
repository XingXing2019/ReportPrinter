using ReportPrinterLibrary.Code.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfAnnotationRenderer;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfWaterMarkRenderer;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterDatabase.Code.Helper;

namespace CosmoService.Code.UserControls.PDF
{
    public partial class ucPdfWaterMarkRenderer : UserControl, IPdfRendererUserControl
    {
        private PdfRendererManagerBase<PdfWaterMarkRendererModel> _manager;

        public ucPdfWaterMarkRenderer()
        {
            InitializeComponent();
        }

        public bool ValidateInput()
        {
            epRendererInfo.Clear();
            var isValid = true;

            var waterMarkRendererType = (WaterMarkRendererType)ecbWaterMarkType.SelectedItem;
            if (waterMarkRendererType == WaterMarkRendererType.Text)
            {
                if (string.IsNullOrEmpty(tbContent.Text))
                {
                    epRendererInfo.SetError(tbContent, "Content is required");
                    isValid = false;
                }
            }
            else
            {
                if (ucSqlSelector.GetSelectedSql() == Guid.Empty)
                {
                    epRendererInfo.SetError(ucSqlSelector, "Sql is required");
                    isValid = false;
                }
            }

            return isValid;
        }

        public void Save(PdfRendererBaseModel rendererBase)
        {
            var renderer = PdfRendererHelper<PdfWaterMarkRendererModel>.CreatePdfRenderer(rendererBase);

            renderer.WaterMarkType = (WaterMarkRendererType)ecbWaterMarkType.SelectedItem;
            renderer.Location = (Location)ecbWaterMarkLocation.SelectedItem;
            renderer.StartPage = int.Parse(nudStartPage.Text);
            renderer.EndPage = int.Parse(nudEndPage.Text);
            renderer.Rotate = double.Parse(nudRotate.Text);

            if (renderer.WaterMarkType == WaterMarkRendererType.Sql)
                renderer.SqlTemplateConfigSqlConfigId = ucSqlSelector.GetSelectedSql();
            else
                renderer.Content = tbContent.Text.Trim();

            _manager.Post(renderer);
        }


        private void ecbWaterMarkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var type = (WaterMarkRendererType)ecbWaterMarkType.SelectedItem;
            if (type == WaterMarkRendererType.Sql)
            {
                ucSqlSelector.Visible = true;
                lblContent.Visible = tbContent.Visible = false;
                gbRendererInfo.Height = 385;
                Height = 390;
            }
            else
            {
                ucSqlSelector.Visible = false;
                lblContent.Visible = tbContent.Visible = true;

                tbContent.Location = new Point(235, 280);
                lblContent.Location = new Point(27, 290);
                gbRendererInfo.Height = 595;
                Height = 600;
            }
        }

        private void ucPdfWaterMarkRenderer_Load(object sender, EventArgs e)
        {
            if (AppConfig.Instance.DatabaseManagerType == DatabaseManagerType.EFCore)
                _manager = new PdfWaterMarkRendererEFCoreManager();
            else if (AppConfig.Instance.DatabaseManagerType == DatabaseManagerType.SP)
                _manager = new PdfWaterMarkRendererSPManager();

            SetupScreen();
            ucSqlSelector.Init();
        }


        #region Helper

        private void SetupScreen()
        {
            ecbWaterMarkType.EnumType = typeof(WaterMarkRendererType);
            ecbWaterMarkLocation.EnumType = typeof(Location);
        }

        #endregion
    }
}
