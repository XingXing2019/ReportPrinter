using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager.ConfigManager.SqlConfigManager;
using ReportPrinterDatabase.Code.Manager.ConfigManager.SqlTemplateConfigManager;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Winform.Configuration;
using ReportPrinterLibrary.Code.Winform.Helper;

namespace CosmoService.Code.Forms.Configuration.SQL
{
    public partial class frmUpsertSqlTemplateConfig : Form
    {
        private readonly ISqlTemplateConfigManager _sqlTemplateConfigManager;
        private readonly bool _isEdit;
        private readonly Guid? _sqlTemplateConfigId;

        public frmUpsertSqlTemplateConfig(ISqlTemplateConfigManager sqlTemplateConfigManager, ISqlConfigManager sqlConfigManager)
        {
            InitializeComponent();

            ucSqlConfig.Initialize(sqlConfigManager, false, new HashSet<Guid>());
            _sqlTemplateConfigManager = sqlTemplateConfigManager;
            _isEdit = false;
            Text = "Add Sql Template Config";
        }

        public frmUpsertSqlTemplateConfig(ISqlTemplateConfigManager sqlTemplateConfigManager, ISqlConfigManager sqlConfigManager, SqlTemplateConfigModel config)
        {
            InitializeComponent();
            Text = "Edit Sql Template Config";

            var selectedSqlConfigs = new HashSet<Guid>(config.SqlConfigs.Select(x => x.SqlConfigId));
            ucSqlConfig.Initialize(sqlConfigManager, false, selectedSqlConfigs);
            _sqlTemplateConfigManager = sqlTemplateConfigManager;
            _isEdit = true;
            _sqlTemplateConfigId = config.SqlTemplateConfigId;

            txtTemplateId.Text = config.Id;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out var templateId, out var selectedSqlConfigs))
                return;

            var sqlTemplateConfig = CreateSqlTemplateConfigModel(templateId, selectedSqlConfigs);

            if (_isEdit)
                await _sqlTemplateConfigManager.PutSqlTemplateConfig(sqlTemplateConfig);
            else
                await _sqlTemplateConfigManager.Post(sqlTemplateConfig);
            Close();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out var templateId, out var selectedSqlConfigs))
                return;

            var sqlTemplateConfig = CreateSqlTemplateConfigData(templateId, selectedSqlConfigs);
            var preview = ConfigPreviewHelper.GeneratePreview(sqlTemplateConfig);
            var frm = new frmConfigPreview(preview);
            frm.ShowDialog();
            btnSave.Enabled = true;
            btnGenerate.Enabled = true;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out var templateId, out var selectedSqlConfigs))
                return;

            var sqlTemplateConfig = CreateSqlTemplateConfigData(templateId, selectedSqlConfigs);
            var frm = new frmGenerateSqlTemplateConfig(sqlTemplateConfig);
            frm.ShowDialog();
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

            selectedSqlConfigs = ucSqlConfig.GetSelectedSqlConfigs();
            if (selectedSqlConfigs.Count == 0)
            {
                epAddSqlTemplateConfig.SetError(lblSqlConfigError, "Please select at least one sql configs");
                isValid = false;
            }

            return isValid;
        }

        private SqlTemplateConfigModel CreateSqlTemplateConfigModel(string templateId, List<SqlConfigData> selectedSqlConfigs)
        {
            var sqlTemplateConfigModel = new SqlTemplateConfigModel
            {
                SqlTemplateConfigId = _sqlTemplateConfigId ?? Guid.NewGuid(),
                Id = templateId,
                SqlConfigs = selectedSqlConfigs.Select(x => new SqlConfig
                {
                    SqlConfigId = x.SqlConfigId,
                    Id = x.Id,
                    DatabaseId = x.DatabaseId,
                    Query = x.Query,
                    SqlVariableConfigs = x.SqlVariableConfigs.Select(y => new SqlVariableConfig
                    {
                        Name = y.Name
                    }).ToList()
                }).ToList()
            };

            return sqlTemplateConfigModel;
        }

        private SqlTemplateConfigData CreateSqlTemplateConfigData(string templateId, List<SqlConfigData> selectedSqlConfigs)
        {
            return new SqlTemplateConfigData
            {
                Id = templateId,
                SqlConfigs = selectedSqlConfigs
            };
        }

        #endregion
    }
}
