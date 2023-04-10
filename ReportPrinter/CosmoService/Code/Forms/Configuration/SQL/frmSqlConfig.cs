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
            _sqlTemplateConfigManager = (ISqlTemplateConfigManager)ManagerFactory.CreateManager<SqlTemplateConfigModel>(typeof(ISqlTemplateConfigManager), AppConfig.Instance.DatabaseManagerType);

            Task.Run(RefreshSqlConfigDataGridView).Wait();
            Task.Run(RefreshSqlTemplateConfigDataGridView).Wait();
        }

        #region Sql Config

        private async void btnRefreshSqlConfig_Click(object sender, EventArgs e)
        {
            await RefreshSqlConfigDataGridView();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var frm = new frmUpsertSqlConfig(_sqlConfigManager);
            frm.ShowDialog();
            await RefreshSqlConfigDataGridView();
        }

        private async void btnModifySqlConfig_Click(object sender, EventArgs e)
        {
            if (!(dgvSqlConfigs.DataSource is List<SqlConfigData> configs) || configs.Count(x => x.IsSelected) != 1)
            {
                MessageBox.Show("Please select one sql config to modify", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var config = configs.Single(x => x.IsSelected);
            var frm = new frmUpsertSqlConfig(_sqlConfigManager, config);
            frm.ShowDialog();
            await RefreshSqlConfigDataGridView();
        }

        private async void btnDeleteSqlConfig_Click(object sender, EventArgs e)
        {
            if (!(dgvSqlConfigs.DataSource is List<SqlConfigData> configs) || configs.Count(x => x.IsSelected) == 0)
            {
                MessageBox.Show("Please select at least one sql config to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Do you want to delete selected sql config(s)?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var selectedConfigs = configs.Where(x => x.IsSelected).ToList();
                await _sqlConfigManager.Delete(selectedConfigs.Select(x => x.SqlConfigId).ToList());
                await RefreshSqlConfigDataGridView();
            }
        }

        private void dgvSqlConfigs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            if (dgvSqlConfigs.Columns[e.ColumnIndex] is DataGridViewLinkColumn)
            {
                var row = e.RowIndex;
                var query = ((SqlConfigData)dgvSqlConfigs.Rows[row].DataBoundItem).Query;
                var frm = new frmConfigPreview(query, "Query");
                frm.ShowDialog();
            }
        }

        #endregion


        #region Sql Template Config

        private async void btnRefreshSqlTemplate_Click(object sender, EventArgs e)
        {
            await RefreshSqlTemplateConfigDataGridView();
        }

        private async void btnAddSqlTemplate_Click(object sender, EventArgs e)
        {
            if (!(dgvSqlConfigs.DataSource is List<SqlConfigData> configs) || configs.Count(x => x.IsSelected) == 0)
            {
                MessageBox.Show("Please select at least one sql config to create sql template", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selectedSqlConfigs = configs.Where(x => x.IsSelected).ToList();
            var frm = new frmAddSqlTemplateConfig(selectedSqlConfigs, _sqlTemplateConfigManager);
            frm.ShowDialog();
            await RefreshSqlTemplateConfigDataGridView();
        }

        private void btnModifySqlTemplate_Click(object sender, EventArgs e)
        {

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

        #endregion

        #region Helper

        private async Task RefreshSqlConfigDataGridView()
        {
            var databaseIdPrefix = txtDatabaseIdPrefix.Text.Trim();
            var sqlConfigs = string.IsNullOrEmpty(databaseIdPrefix) ? await _sqlConfigManager.GetAll() : await _sqlConfigManager.GetAllByDatabaseIdPrefix(databaseIdPrefix);
            var data = sqlConfigs.Select(x => new SqlConfigData
            {
                SqlConfigId = x.SqlConfigId,
                Id = x.Id,
                DatabaseId = x.DatabaseId,
                Query = x.Query,
                SqlVariableConfigs = new List<SqlVariableConfigData>(x.SqlVariableConfigs.Select(y => new SqlVariableConfigData { Name = y.Name, })),
            }).ToList();

            dgvSqlConfigs.DataSource = data;
        }

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
