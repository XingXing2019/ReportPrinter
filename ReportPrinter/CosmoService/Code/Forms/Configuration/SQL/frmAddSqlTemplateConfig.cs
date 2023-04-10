using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager.ConfigManager.SqlTemplateConfigManager;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Winform.Configuration;

namespace CosmoService.Code.Forms.Configuration.SQL
{
    public partial class frmAddSqlTemplateConfig : Form
    {
        private readonly List<SqlConfigData> _sqlConfigs;
        private readonly ISqlTemplateConfigManager _manager;

        public frmAddSqlTemplateConfig(List<SqlConfigData> sqlConfigs, ISqlTemplateConfigManager manager)
        {
            _sqlConfigs = sqlConfigs;
            _manager = manager;
            InitializeComponent();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var templateId = txtTemplateId.Text.Trim();

            if (string.IsNullOrEmpty(templateId))
            {
                epAddSqlTemplateConfig.SetError(lblTemplateId, "Template Id is required");
                return;
            }

            var config = new SqlTemplateConfigModel
            {
                SqlTemplateConfigId = Guid.NewGuid(),
                Id = templateId,
                SqlConfigs = _sqlConfigs.Select(x => new SqlConfig { SqlConfigId = x.SqlConfigId }).ToList()
            };

            await _manager.Post(config);
            Close();
        }
    }
}
