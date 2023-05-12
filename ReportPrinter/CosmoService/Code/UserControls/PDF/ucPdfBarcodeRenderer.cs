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
            return ucSqlSelector.ValidateInput();
        }

        public void Save(PdfRendererBaseModel rendererBase)
        {
            var renderer = PdfRendererHelper<PdfBarcodeRendererModel>.CreatePdfRenderer(rendererBase);

            renderer.BarcodeFormat = (BarcodeFormat)ecbBarcodeFormat.SelectedItem;
            renderer.ShowBarcodeText = cbShowBarcodeText.Checked;
            renderer.SqlTemplateConfigSqlConfigId = ucSqlSelector.SelectedSql;
            renderer.SqlResColumn = ucSqlSelector.SqlResult;

            _manager.Post(renderer);
        }

        private void ucPdfBarcodeRenderer_Load(object sender, EventArgs e)
        {
            if (AppConfig.Instance.DatabaseManagerType == DatabaseManagerType.EFCore)
                _manager = new PdfBarcodeRendererEFCoreManager();
            else if (AppConfig.Instance.DatabaseManagerType == DatabaseManagerType.SP)
                _manager = new PdfBarcodeRendererSPManager();

            SetupScreen();
            ucSqlSelector.Init(true);
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
