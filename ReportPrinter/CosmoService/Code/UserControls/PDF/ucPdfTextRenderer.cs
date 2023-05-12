using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ReportPrinterDatabase.Code.Helper;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfReprintMarkRenderer;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfTextRenderer;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.Enum;

namespace CosmoService.Code.UserControls.PDF
{
    public partial class ucPdfTextRenderer : UserControl, IPdfRendererUserControl
    {
        private PdfRendererManagerBase<PdfTextRendererModel> _manager;

        public bool ValidateInput()
        {
            epRendererInfo.Clear();
            var isValid = true;

            var type = (TextRendererType)ecbTextRendererType.SelectedItem;

            if (type == TextRendererType.Sql)
            {
                if (!ucSqlSelector.ValidateInput())
                {
                    isValid = false;
                }
            }
            else if (type == TextRendererType.Text)
            {
                if (string.IsNullOrEmpty(tbContent.Text.Trim()))
                {
                    epRendererInfo.SetError(tbContent, "Content is required");
                    isValid = false;
                }
            }

            return isValid;
        }

        public void Save(PdfRendererBaseModel rendererBase)
        {
            var renderer = PdfRendererHelper<PdfTextRendererModel>.CreatePdfRenderer(rendererBase);
            renderer.TextRendererType = (TextRendererType)ecbTextRendererType.SelectedItem;

            if (renderer.TextRendererType == TextRendererType.Sql)
            {
                renderer.SqlTemplateConfigSqlConfigId = ucSqlSelector.SelectedSql;
                renderer.SqlResColumn = ucSqlSelector.SqlResult;
                renderer.Title = string.IsNullOrEmpty(tbTitle.Text.Trim()) ? null : tbTitle.Text.Trim();
            }
            else if (renderer.TextRendererType == TextRendererType.Timestamp)
            {
                renderer.Title = string.IsNullOrEmpty(tbTitle.Text.Trim()) ? null : tbTitle.Text.Trim();
                renderer.Mask = string.IsNullOrEmpty(tbMask.Text.Trim()) ? null : tbMask.Text.Trim();
            }
            else if (renderer.TextRendererType == TextRendererType.Text)
            {
                renderer.Content = tbContent.Text.Trim();
            }

            _manager.Post(renderer);
        }

        public ucPdfTextRenderer()
        {
            InitializeComponent();
        }

        private void ucPdfTextRenderer_Load(object sender, EventArgs e)
        {
            if (AppConfig.Instance.DatabaseManagerType == DatabaseManagerType.EFCore)
                _manager = new PdfTextRendererEFCoreManager();
            else if (AppConfig.Instance.DatabaseManagerType == DatabaseManagerType.SP)
                _manager = new PdfTextRendererSPManager();

            SetupScreen();
            ucSqlSelector.Init(true);
        }


        private void ecbTextRendererType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var type = (TextRendererType)ecbTextRendererType.SelectedItem;

            if (type == TextRendererType.Sql)
            {
                ucSqlSelector.Visible = lblTitle.Visible = tbTitle.Visible = true;
                lblContent.Visible = tbContent.Visible = false;
                lblMask.Visible = tbMask.Visible = false;

                ucSqlSelector.Location = new Point(10, 130);
                gbRendererInfo.Height = 260;
                Height = 265;
            }
            else if (type == TextRendererType.Text)
            {
                ucSqlSelector.Visible = lblTitle.Visible = tbTitle.Visible = false;
                lblContent.Visible = tbContent.Visible = true;
                lblMask.Visible = tbMask.Visible = false;

                tbContent.Location = new Point(154, 75);
                lblContent.Location = new Point(18, 75);
                gbRendererInfo.Height = 235;
                Height = 240;
            }
            else if (type == TextRendererType.Timestamp)
            {
                ucSqlSelector.Visible = lblContent.Visible = tbContent.Visible = false;
                lblTitle.Visible = tbTitle.Visible = true;
                lblMask.Visible = tbMask.Visible = true;

                gbRendererInfo.Height = 195;
                Height = 200;
            }
        }

        #region Helper

        private void SetupScreen()
        {
            ecbTextRendererType.EnumType = typeof(TextRendererType);
        }

        #endregion
    }
}
