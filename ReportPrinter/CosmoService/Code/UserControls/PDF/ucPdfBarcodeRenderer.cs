using System;
using System.Windows.Forms;
using ReportPrinterDatabase.Code.Helper;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfBarcodeRenderer;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.Enum;

namespace CosmoService.Code.UserControls.PDF
{
    public partial class ucPdfBarcodeRenderer : UserControl, IPdfRendererUserControl
    {
        private PdfRendererManagerBase<PdfBarcodeRendererModel> _manager;

        public ucPdfBarcodeRenderer()
        {
            InitializeComponent();
        }

        public bool ValidateInput()
        {
            epRendererInfo.Clear();
            var isValid = true;

            if (ucSqlSelector.GetSelectedSql() == Guid.Empty)
            {
                epRendererInfo.SetError(ucSqlSelector, "Sql is required");
                isValid = false;
            }

            return isValid;
        }

        public void Save(PdfRendererBaseModel rendererBase)
        {
            var renderer = PdfRendererHelper<PdfBarcodeRendererModel>.CreatePdfRenderer(rendererBase);

            renderer.BarcodeFormat = (BarcodeFormat)ecbBarcodeFormat.SelectedValue;
            renderer.ShowBarcodeText = cbShowBarcodeText.Checked;
            renderer.SqlTemplateConfigSqlConfigId = ucSqlSelector.GetSelectedSql();

            _manager.Post(renderer);
        }

        private void ucPdfBarcodeRenderer_Load(object sender, EventArgs e)
        {
            SetupScreen();

            if (AppConfig.Instance.DatabaseManagerType == DatabaseManagerType.EFCore)
                _manager = new PdfBarcodeRendererEFCoreManager();
            else if (AppConfig.Instance.DatabaseManagerType == DatabaseManagerType.SP)
                _manager = new PdfBarcodeRendererSPManager();

            ucSqlSelector.Init();
        }

        #region Helper

        private void SetupScreen()
        {
            ecbBarcodeFormat.EnumType = typeof(BarcodeFormat);
            ecbBarcodeFormat.SelectedItem = BarcodeFormat.CODE_128;
        }

        #endregion
    }
}
