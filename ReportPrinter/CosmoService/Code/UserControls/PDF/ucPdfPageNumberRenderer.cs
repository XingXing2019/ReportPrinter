using System;
using System.Windows.Forms;
using CosmoService.Code.UserControls.SQL;
using ReportPrinterDatabase.Code.Helper;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfPageNumberRenderer;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.Enum;

namespace CosmoService.Code.UserControls.PDF
{
    public partial class ucPdfPageNumberRenderer : UserControl, IPdfRendererUserControl
    {
        private PdfRendererManagerBase<PdfPageNumberRendererModel> _manager;
        public ucPdfPageNumberRenderer()
        {
            InitializeComponent();
        }

        public bool ValidateInput()
        {
            return true;
        }

        public void Save(PdfRendererBaseModel rendererBase)
        {
            var renderer = PdfRendererHelper<PdfPageNumberRendererModel>.CreatePdfRenderer(rendererBase);

            renderer.StartPage = int.Parse(nudStartPage.Text);
            renderer.EndPage = int.Parse(nudEndPage.Text);
            renderer.PageNumberLocation = (Location)ecbPageNumberLocation.SelectedItem;

            _manager.Post(renderer);
        }

        private void ucPdfPageNumberRenderer_Load(object sender, EventArgs e)
        {
            if (AppConfig.Instance.DatabaseManagerType == DatabaseManagerType.EFCore)
                _manager = new PdfPageNumberRendererEFCoreManager();
            else if (AppConfig.Instance.DatabaseManagerType == DatabaseManagerType.SP)
                _manager = new PdfPageNumberRendererSPManager();

            SetupScreen();
        }

        public void ClearError()
        {
            epRendererInfo.Clear();
        }

        #region Helper

        private void SetupScreen()
        {
            ecbPageNumberLocation.EnumType = typeof(Location);
        }

        #endregion
    }
}
