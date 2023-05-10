using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CosmoService.Code.Forms.Configuration;
using CosmoService.Code.Forms.Configuration.SQL;
using ReportPrinterDatabase.Code.Manager.ConfigManager.SqlConfigManager;
using ReportPrinterLibrary.Code.Winform.Configuration;
using ReportPrinterLibrary.Code.Winform.Configuration.SQL;

namespace CosmoService.Code.UserControls.SQL
{
    public partial class ucSqlConfig : UserControl
    {
        private ISqlConfigManager _sqlConfigManager;

        public ucSqlConfig()
        {
            InitializeComponent();
        }

        public void Initialize(ISqlConfigManager sqlConfigManager, bool allowEdit, HashSet<Guid> selectedSqlConfigs)
        {
            _sqlConfigManager = sqlConfigManager;

            btnAddSqlConfig.Visible = allowEdit;
            btnModifySqlConfig.Visible = allowEdit;
            btnDeleteSqlConfig.Visible = allowEdit;

            if (!allowEdit)
            {
                gbSqlConfig.Height = 494;
            }

            Task.Run(() => RefreshSqlConfigDataGridView(selectedSqlConfigs)).Wait();
        }

        public List<SqlConfigData> GetSelectedSqlConfigs()
        {
            if (!(dgvSqlConfigs.DataSource is List<SqlConfigData> configs))
            {
                return new List<SqlConfigData>();
            }

            return configs.Where(x => x.IsSelected).ToList();
        }

        private async void btnRefreshSqlConfig_Click(object sender, EventArgs e)
        {
            await RefreshSqlConfigDataGridView(new HashSet<Guid>());
        }

        private async void btnAddSqlConfig_Click(object sender, EventArgs e)
        {
            var frm = new frmUpsertSqlConfig(_sqlConfigManager);
            frm.ShowDialog();
            await RefreshSqlConfigDataGridView(new HashSet<Guid>());
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
            await RefreshSqlConfigDataGridView(new HashSet<Guid>());
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
                await RefreshSqlConfigDataGridView(new HashSet<Guid>());
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

        private async Task RefreshSqlConfigDataGridView(HashSet<Guid> selectedSqlConfigs)
        {
            var databaseIdPrefix = txtDatabaseIdPrefix.Text.Trim();
            var sqlConfigs = string.IsNullOrEmpty(databaseIdPrefix) ? await _sqlConfigManager.GetAll() : await _sqlConfigManager.GetAllByDatabaseIdPrefix(databaseIdPrefix);
            var data = sqlConfigs.Select(x => new SqlConfigData
            {
                IsSelected = selectedSqlConfigs.Contains(x.SqlConfigId),
                SqlConfigId = x.SqlConfigId,
                Id = x.Id,
                DatabaseId = x.DatabaseId,
                Query = x.Query,
                SqlVariableConfigs = x.SqlVariableConfigs.Select(y => new SqlVariableConfigData
                {
                    Name = y.Name
                }).ToList(),
            }).ToList();

            dgvSqlConfigs.DataSource = data;
        }

        #endregion
    }
}
