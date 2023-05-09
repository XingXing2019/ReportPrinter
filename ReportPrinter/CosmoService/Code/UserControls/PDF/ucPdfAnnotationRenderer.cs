using System;
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

            var annotationRendererType = (AnnotationRendererType)ecbAnnotationRendererType.SelectedValue;
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
            var renderer = PdfRendererHelper<PdfAnnotationRendererModel>.CreatePdfRenderer(rendererBase);

            renderer.AnnotationRendererType = (AnnotationRendererType)ecbAnnotationRendererType.SelectedValue;
            renderer.Title = string.IsNullOrEmpty(tbTitle.Text.Trim()) ? null : tbTitle.Text.Trim();
            renderer.Icon = (PdfTextAnnotationIcon)ecbIcon.SelectedValue;

            if (renderer.AnnotationRendererType == AnnotationRendererType.Sql)
                renderer.SqlTemplateConfigSqlConfigId = ucSqlSelector.GetSelectedSql();
            else
                renderer.Content = tbContent.Text.Trim();

            _manager.Post(renderer);
        }
        
        private void ecbAnnotationRendererType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var type = (AnnotationRendererType)ecbAnnotationRendererType.SelectedValue;
            if (type == AnnotationRendererType.Sql)
            {
                ucSqlSelector.Visible = true;
                lblContent.Visible = tbContent.Visible = false;
                gbRendererInfo.Height = 325;
                Height = 330;
            }
            else
            {
                ucSqlSelector.Visible = false;
                lblContent.Visible = tbContent.Visible = true;

                tbContent.Location = new Point(232, 215);
                lblContent.Location = new Point(24, 215);
                gbRendererInfo.Height = 525;
                Height = 530;
            }
        }

        private void ucPdfAnnotationRenderer_Load(object sender, EventArgs e)
        {
            SetupScreen();

            if (AppConfig.Instance.DatabaseManagerType == DatabaseManagerType.EFCore)
                _manager = new PdfAnnotationRendererEFCoreManager();
            else if (AppConfig.Instance.DatabaseManagerType == DatabaseManagerType.SP)
                _manager = new PdfAnnotationRendererSPManager();

            ucSqlSelector.Init();
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
