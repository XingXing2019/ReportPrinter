using System;
using System.Windows.Forms;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager;

namespace CosmoService.Code.UserControls
{
    public partial class ucPdfRenderer : UserControl
    {
        private IPdfRendererBaseManager _rendererBaseManager;

        public ucPdfRenderer()
        {
            InitializeComponent();
        }

        public void Initialize(IPdfRendererBaseManager rendererBaseManager)
        {
            _rendererBaseManager = rendererBaseManager;

        }

        private void btnAddSqlConfig_Click(object sender, EventArgs e)
        {

        }

        private void btnModifySqlConfig_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteSqlConfig_Click(object sender, EventArgs e)
        {

        }
    }
}
