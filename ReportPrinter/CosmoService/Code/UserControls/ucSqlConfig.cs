using CosmoService.Code.Forms.Configuration;
using CosmoService.Code.Forms.Configuration.SQL;
using ReportPrinterDatabase.Code.Manager.ConfigManager.SqlConfigManager;
using ReportPrinterLibrary.Code.Winform.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CosmoService.Code.UserControls
{
    public partial class ucSqlConfig : UserControl
    {
        private ISqlConfigManager _sqlConfigManager;

        public ucSqlConfig()
        {
            InitializeComponent();
        }

        public void Initialize(ISqlConfigManager sqlConfigManager, bool allowEdit)
        {
            _sqlConfigManager = sqlConfigManager;

            btnAddSqlConfig.Visible = allowEdit;
            btnModifySqlConfig.Visible = allowEdit;
            btnDeleteSqlConfig.Visible = allowEdit;

            Task.Run(RefreshSqlConfigDataGridView).Wait();
        }

        private async void btnRefreshSqlConfig_Click(object sender, EventArgs e)
        {
            await RefreshSqlConfigDataGridView();
        }

        private async void btnAddSqlConfig_Click(object sender, EventArgs e)
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

        #endregion
    }
}
