using System;
using System.IO;
using System.Windows.Forms;
using ReportPrinterLibrary.Code.Helper;
using ReportPrinterLibrary.Code.Winform.Configuration.SQL;

namespace CosmoService.Code.Forms.Configuration.SQL
{
    public partial class frmGenerateSqlTemplateConfig : Form
    {
        private readonly SqlTemplateConfigData _sqlTemplateConfig;

        public frmGenerateSqlTemplateConfig(SqlTemplateConfigData sqlTemplateConfig)
        {
            InitializeComponent();
            ToggleSaveButton();
            _sqlTemplateConfig = sqlTemplateConfig;
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            sfdSqlTemplateConfig.Filter = "XML-File | *.xml";
            if (sfdSqlTemplateConfig.ShowDialog() == DialogResult.OK)
            {
                txtOutputPath.Text = sfdSqlTemplateConfig.FileName;
            }

            ToggleSaveButton();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var outputPath = txtOutputPath.Text;
            var dir = Path.GetDirectoryName(outputPath);

            if (!Directory.Exists(dir))
            {
                MessageBox.Show("Invalid output path", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOutputPath.Text = string.Empty;
                return;
            }

            var sqlTemplate = ConfigPreviewHelper.GeneratePreview(_sqlTemplateConfig);

            File.WriteAllText(outputPath, sqlTemplate);
            Close();
        }

        #region Helper

        private void ToggleSaveButton()
        {
            var dir = Path.GetDirectoryName(txtOutputPath.Text);
            btnSave.Enabled = Directory.Exists(dir);
        }

        #endregion

    }
}
