using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager.ConfigManager.SqlConfigManager;
using ReportPrinterLibrary.Code.Winform.Configuration;
using ReportPrinterLibrary.Code.Winform.Helper;

namespace CosmoService.Code.Forms.Configuration.SQL
{
    public partial class frmUpsertSqlConfig : Form
    {
        private readonly ISqlConfigManager _manager;
        private readonly bool _isEdit;
        private readonly Guid? _sqlConfigId;
        private BindingList<SqlVariableConfigData> _sqlVariableConfigs;

        public frmUpsertSqlConfig(ISqlConfigManager manager)
        {
            InitializeComponent();

            _manager = manager;
            _isEdit = false;
            SetupScreen(_isEdit);
        }

        public frmUpsertSqlConfig(ISqlConfigManager manager, SqlConfigData config)
        {
            InitializeComponent();

            _manager = manager;
            _isEdit = true;
            _sqlConfigId = config.SqlConfigId;
            SetupScreen(_isEdit, config);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var frm = new frmAddSqlVariableConfig(_sqlVariableConfigs);
            frm.ShowDialog();
            ToggleDeleteButton();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedConfigs = ((BindingList<SqlVariableConfigData>)dgvSqlVariables.DataSource).Where(x => x.IsSelected).ToList();

            if (selectedConfigs.Count == 0)
            {
                epAddSqlConfig.SetError(btnDelete, "Please select one config");
                return;
            }

            if (MessageBox.Show("Do you want to delete selected sql variable configs?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                selectedConfigs.ForEach(x => _sqlVariableConfigs.Remove(x));
                ToggleDeleteButton();
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out var id, out var databaseId, out var query))
                return;

            var sqlConfig = CreateSqlConfigData(id, databaseId, query);
            var preview = ConfigPreviewHelper.GeneratePreview(sqlConfig);
            var frm = new frmConfigPreview(preview);
            frm.ShowDialog();
            btnSave.Enabled = true;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out var id, out var databaseId, out var query))
                return;
            
            var sqlConfig = CreateSqlConfig(id, databaseId, query);

            if (_isEdit)
                await _manager.PutSqlConfig(sqlConfig);
            else
                await _manager.Post(sqlConfig);
            
            Close();
        }


        #region Helper

        private void SetupScreen(bool isEdit, SqlConfigData config = null)
        {
            Text = isEdit ? "Edit SQL Config" : "Add SQL Config";

            txtId.Text = config == null ? string.Empty : config.Id;
            txtDatabaseId.Text = config == null ? string.Empty : config.DatabaseId;
            txtQuery.Text = config == null ? string.Empty : config.Query;

            var sqlVariableConfigs = config == null ? new List<SqlVariableConfigData>() : config.SqlVariableConfigs.Select(x => new SqlVariableConfigData { Name = x.Name, }).ToList();
            _sqlVariableConfigs = new BindingList<SqlVariableConfigData>(sqlVariableConfigs);
            dgvSqlVariables.DataSource = _sqlVariableConfigs;
            
            btnSave.Enabled = false;
            ToggleDeleteButton();
        }

        private void ToggleDeleteButton()
        {
            btnDelete.Enabled = _sqlVariableConfigs.Count > 0;
        }

        private bool ValidateInput(out string id, out string databaseId, out string query)
        {
            var isValidInput = true;

            id = txtId.Text.Trim();
            if (string.IsNullOrEmpty(id))
            {
                epAddSqlConfig.SetError(lblId, "Id is required");
                isValidInput = false;
            }

            databaseId = txtDatabaseId.Text.Trim();
            if (string.IsNullOrEmpty(databaseId))
            {
                epAddSqlConfig.SetError(lblDatabaseId, "Database Id is required");
                isValidInput = false;
            }

            query = txtQuery.Text.Trim();
            if (string.IsNullOrEmpty(query))
            {
                epAddSqlConfig.SetError(lblQuery, "Query is required");
                isValidInput = false;
            }

            return isValidInput;
        }

        private SqlConfig CreateSqlConfig(string id, string databaseId, string query)
        {
            var sqlConfig = new SqlConfig
            {
                SqlConfigId = _sqlConfigId ?? Guid.NewGuid(),
                Id = id,
                DatabaseId = databaseId,
                Query = query,
            };

            foreach (var config in _sqlVariableConfigs)
            {
                sqlConfig.SqlVariableConfigs.Add(new SqlVariableConfig
                {
                    SqlConfigId = sqlConfig.SqlConfigId,
                    Name = config.Name
                });
            }

            return sqlConfig;
        }

        private SqlConfigData CreateSqlConfigData(string id, string databaseId, string query)
        {
            var sqlConfig = new SqlConfigData
            {
                Id = id,
                DatabaseId = databaseId,
                Query = $"\r\n{query}\r\n"
            };

            foreach (var config in _sqlVariableConfigs)
            {
                sqlConfig.SqlVariableConfigs.Add(new SqlVariableConfigData { Name = config.Name });
            }

            return sqlConfig;
        }

        #endregion
    }
}
