using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager;
using ReportPrinterDatabase.Code.Manager.ConfigManager.SqlConfigManager;
using ReportPrinterDatabase.Code.Manager.ConfigManager.SqlTemplateConfigManager;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.Winform.Configuration;

namespace CosmoService.Code.Forms.Configuration.SQL
{
    public partial class frmSqlConfig : Form
    {
        private readonly ISqlConfigManager _sqlConfigManager;
        private readonly ISqlTemplateConfigManager _sqlTemplateConfigManager;

        public frmSqlConfig()
        {
            InitializeComponent();

            _sqlConfigManager = (ISqlConfigManager)ManagerFactory.CreateManager<SqlConfig>(typeof(ISqlConfigManager), AppConfig.Instance.DatabaseManagerType);
            ucSqlConfig.Initialize(_sqlConfigManager, true, new HashSet<Guid>());

            _sqlTemplateConfigManager = (ISqlTemplateConfigManager)ManagerFactory.CreateManager<SqlTemplateConfigModel>(typeof(ISqlTemplateConfigManager), AppConfig.Instance.DatabaseManagerType);

            Task.Run(RefreshSqlTemplateConfigDataGridView).Wait();
        }

        private async void btnRefreshSqlTemplate_Click(object sender, EventArgs e)
        {
            await RefreshSqlTemplateConfigDataGridView();
        }

        private async void btnAddSqlTemplate_Click(object sender, EventArgs e)
        {
            var frm = new frmUpsertSqlTemplateConfig(_sqlTemplateConfigManager, _sqlConfigManager);
            frm.ShowDialog();
            await RefreshSqlTemplateConfigDataGridView();
        }

        private async void btnModifySqlTemplate_Click(object sender, EventArgs e)
        {
            if (!(dgvSqlTemplateConfigs.DataSource is List<SqlTemplateConfigData> configs) || configs.Count(x => x.IsSelected) != 1)
            {
                MessageBox.Show("Please select at least one sql template config to modify", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var config = configs.Single(x => x.IsSelected);
            var sqlTemplateConfig = new SqlTemplateConfigModel
            {
                SqlTemplateConfigId = config.SqlTemplateConfigId,
                Id = config.Id,
                SqlConfigs = config.SqlConfigs.Select(x => new SqlConfig
                {
                    SqlConfigId = x.SqlConfigId,
                    Id = x.Id,
                    DatabaseId = x.DatabaseId,
                    Query = x.Query
                }).ToList()
            };

            var frm = new frmUpsertSqlTemplateConfig(_sqlTemplateConfigManager, _sqlConfigManager, sqlTemplateConfig);
            frm.ShowDialog();
            await RefreshSqlTemplateConfigDataGridView();
        }

        private async void btnDeleteSqlTemplate_Click(object sender, EventArgs e)
        {
            if (!(dgvSqlTemplateConfigs.DataSource is List<SqlTemplateConfigData> configs) || configs.Count(x => x.IsSelected) == 0)
            {
                MessageBox.Show("Please select at least one sql template config to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Do you want to delete selected sql template config(s)?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var selectedConfigs = configs.Where(x => x.IsSelected).ToList();
                await _sqlTemplateConfigManager.Delete(selectedConfigs.Select(x => x.SqlTemplateConfigId).ToList());
                await RefreshSqlTemplateConfigDataGridView();
            }
        }


        #region Helper

        private async Task RefreshSqlTemplateConfigDataGridView()
        {
            var templateIdPrefix = txtTemplateIdPrefix.Text.Trim();
            var sqlTemplateConfigs = string.IsNullOrEmpty(templateIdPrefix) ? await _sqlTemplateConfigManager.GetAll() : await _sqlTemplateConfigManager.GetAllBySqlTemplateIdPrefix(templateIdPrefix);

            var data = sqlTemplateConfigs.Select(x => new SqlTemplateConfigData
            {
                SqlTemplateConfigId = x.SqlTemplateConfigId,
                Id = x.Id,
                SqlConfigs = x.SqlConfigs.Select(y => new SqlConfigData
                {
                    SqlConfigId = y.SqlConfigId,
                    Id = y.Id,
                    DatabaseId = y.DatabaseId,
                    Query = y.Query,
                    SqlVariableConfigs = y.SqlVariableConfigs.Select(z => new SqlVariableConfigData
                    {
                        Name = z.Name
                    }).ToList()
                }).ToList()
            }).ToList();

            dgvSqlTemplateConfigs.DataSource = data;
        }

        #endregion
    }
}
