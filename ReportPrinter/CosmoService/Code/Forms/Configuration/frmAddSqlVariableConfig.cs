using ReportPrinterLibrary.Code.Winform.Configuration;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace CosmoService.Code.Forms.Configuration
{
    public partial class frmAddSqlVariableConfig : Form
    {
        private readonly BindingList<SqlVariableConfigData> _sqlVariableConfigs;
        public frmAddSqlVariableConfig(BindingList<SqlVariableConfigData> sqlVariableConfigs)
        {
            InitializeComponent();
            _sqlVariableConfigs = sqlVariableConfigs;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var name = txtName.Text;

            if (string.IsNullOrEmpty(name))
            {
                epAddSqlVariableConfig.SetError(lblName, "Name is required");
                return;
            }

            _sqlVariableConfigs.Add(new SqlVariableConfigData { Name = name });
            Close();
        }
    }
}
