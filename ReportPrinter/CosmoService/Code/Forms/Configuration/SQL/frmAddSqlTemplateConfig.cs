using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager.ConfigManager.SqlConfigManager;
using ReportPrinterDatabase.Code.Manager.ConfigManager.SqlTemplateConfigManager;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Winform.Configuration;

namespace CosmoService.Code.Forms.Configuration.SQL
{
    public partial class frmAddSqlTemplateConfig : Form
    {
        private readonly ISqlTemplateConfigManager _sqlTemplateConfigManager;

        public frmAddSqlTemplateConfig(ISqlTemplateConfigManager sqlTemplateConfigManager, ISqlConfigManager sqlConfigManager)
        {
            InitializeComponent();

            ucSqlConfig.Initialize(sqlConfigManager, false);
            _sqlTemplateConfigManager = sqlTemplateConfigManager;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out var templateId, out var selectedSqlConfigs))
                return;

            var config = new SqlTemplateConfigModel
            {
                SqlTemplateConfigId = Guid.NewGuid(),
                Id = templateId,
                SqlConfigs = selectedSqlConfigs.Select(x => new SqlConfig { SqlConfigId = x.SqlConfigId }).ToList()
            };

            await _sqlTemplateConfigManager.Post(config);
            Close();
        }


        #region Helper

        private bool ValidateInput(out string templateId, out List<SqlConfigData> selectedSqlConfigs)
        {
            epAddSqlTemplateConfig.Clear();
            templateId = txtTemplateId.Text.Trim();
            var isValid = true;

            if (string.IsNullOrEmpty(templateId))
            {
                epAddSqlTemplateConfig.SetError(lblTemplateId, "Template Id is required");
                isValid = false;
            }

            selectedSqlConfigs = ucSqlConfig.GetSelectedSqlConfigIds();
            if (selectedSqlConfigs.Count == 0)
            {
                epAddSqlTemplateConfig.SetError(lblSqlConfigError, "Please select at least one sql configs");
                isValid = false;
            }

            return isValid;
        }

        #endregion
    }
}
