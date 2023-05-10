using System;
using System.Windows.Forms;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfReprintMarkRenderer;
using ReportPrinterDatabase.Code.Helper;

namespace CosmoService.Code.UserControls.PDF
{
    public partial class ucPdfReprintMarkRenderer : UserControl, IPdfRendererUserControl
    {
        private PdfRendererManagerBase<PdfReprintMarkRendererModel> _manager;

        public ucPdfReprintMarkRenderer()
        {
            InitializeComponent();
        }

        public bool ValidateInput()
        {
            epRendererInfo.Clear();
            var isValid = true;

            if (string.IsNullOrEmpty(tbText.Text.Trim()))
            {
                epRendererInfo.SetError(tbText, "Text is required");
                isValid = false;
            }

            if (nudBoardThickness.Value == 0)
            {
                epRendererInfo.SetError(nudBoardThickness, "Board thickness cannot be 0");
                isValid = false;
            }

            return isValid;
        }

        public void Save(PdfRendererBaseModel rendererBase)
        {
            var renderer = PdfRendererHelper<PdfReprintMarkRendererModel>.CreatePdfRenderer(rendererBase);

            renderer.Text = tbText.Text.Trim();
            renderer.BoardThickness = double.Parse(nudBoardThickness.Text);
            renderer.Location = (Location)ecbReprintMarkLocation.SelectedItem;

            _manager.Post(renderer);
        }

        private void ucPdfReprintMarkRenderer_Load(object sender, EventArgs e)
        {
            if (AppConfig.Instance.DatabaseManagerType == DatabaseManagerType.EFCore)
                _manager = new PdfReprintMarkRendererEFCoreManager();
            else if (AppConfig.Instance.DatabaseManagerType == DatabaseManagerType.SP)
                _manager = new PdfReprintMarkRendererSPManager();

            SetupScreen();
        }

        #region Helper

        private void SetupScreen()
        {
            ecbReprintMarkLocation.EnumType = typeof(Location);
        }

        #endregion
    }
}
