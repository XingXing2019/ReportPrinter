using System;
using System.ComponentModel;
using System.Windows.Forms;
using ReportPrinterLibrary.Code.Winform.Configuration.SQL;

namespace CosmoService.Code.Forms.Configuration.SQL
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
            var name = txtName.Text.Trim();

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
