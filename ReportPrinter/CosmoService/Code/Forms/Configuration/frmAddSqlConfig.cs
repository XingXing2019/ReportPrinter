﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager.ConfigManager.SqlConfigManager;
using ReportPrinterLibrary.Code.Winform.Configuration;

namespace CosmoService.Code.Forms.Configuration
{
    public partial class frmAddSqlConfig : Form
    {
        private readonly ISqlConfigManager _manager;
        private readonly BindingList<SqlVariableConfigData> _sqlVariableConfigs;

        public frmAddSqlConfig(ISqlConfigManager manager)
        {
            InitializeComponent();

            _manager = manager;
            _sqlVariableConfigs = new BindingList<SqlVariableConfigData>();
            SetupDataGridView();
            ToggleDeleteButton();
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

            selectedConfigs.ForEach(x => _sqlVariableConfigs.Remove(x));
            ToggleDeleteButton();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var isValidInput = true;

            var id = txtId.Text.Trim();
            if (string.IsNullOrEmpty(id))
            {
                epAddSqlConfig.SetError(lblId, "Id is required");
                isValidInput = false;
            }

            var databaseId = txtDatabaseId.Text.Trim();
            if (string.IsNullOrEmpty(databaseId))
            {
                epAddSqlConfig.SetError(lblDatabaseId, "Database Id is required");
                isValidInput = false;
            }

            var query = txtQuery.Text.Trim();
            if (string.IsNullOrEmpty(query))
            {
                epAddSqlConfig.SetError(lblQuery, "Query is required");
                isValidInput = false;
            }

            if (!isValidInput)
                return;

            var sqlConfig = CreateSqlConfig(id, databaseId, query);
            _manager.Post(sqlConfig);
            Close();
        }


        #region Helper

        private void SetupDataGridView()
        {
            dgvSqlVariables.DataSource = _sqlVariableConfigs;
            var width = dgvSqlVariables.Width * 0.9;
            dgvSqlVariables.Columns[0].Width = (int)(width * 0.2);
            dgvSqlVariables.Columns[1].Width = (int)(width * 0.8);
        }

        private void ToggleDeleteButton()
        {
            btnDelete.Enabled = _sqlVariableConfigs.Count > 0;
        }

        private SqlConfig CreateSqlConfig(string id, string databaseId, string query)
        {
            var sqlConfig = new SqlConfig
            {
                SqlConfigId = Guid.NewGuid(),
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

        #endregion
    }
}
