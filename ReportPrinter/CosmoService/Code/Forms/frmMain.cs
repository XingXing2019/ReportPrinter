using System;
using System.Windows.Forms;
using CosmoService.Code.Forms.Configuration;
using CosmoService.Code.Forms.Configuration.PDF;
using CosmoService.Code.Forms.Configuration.SQL;
using CosmoService.Code.Forms.Message;
using ReportPrinterLibrary.Code.Config.Configuration;

namespace CosmoService.Code.Forms
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void publishMessage_Click(object sender, EventArgs e)
        {
            var frm = new frmPublishPrintReport();
            frm.ShowDialog();
        }

        private void sqlConfiguration_Click(object sender, EventArgs e)
        {
            var frm = new frmSqlConfig();
            frm.ShowDialog();
        }

        private void pdfConfiguration_Click(object sender, EventArgs e)
        {
            var frm = new frmPdfConfig();
            frm.ShowDialog();
        }
    }
}
