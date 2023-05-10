﻿using ReportPrinterLibrary.Code.Winform.Configuration.SQL;

namespace CosmoService.Code.UserControls.SQL
{
    partial class ucSqlConfig
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            gbSqlConfig = new System.Windows.Forms.GroupBox();
            label1 = new System.Windows.Forms.Label();
            txtDatabaseIdPrefix = new System.Windows.Forms.TextBox();
            btnModifySqlConfig = new System.Windows.Forms.Button();
            btnDeleteSqlConfig = new System.Windows.Forms.Button();
            btnAddSqlConfig = new System.Windows.Forms.Button();
            btnRefreshSqlConfig = new System.Windows.Forms.Button();
            dgvSqlConfigs = new System.Windows.Forms.DataGridView();
            isSelectedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            databaseIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Query = new System.Windows.Forms.DataGridViewLinkColumn();
            sqlConfigDataBindingSource = new System.Windows.Forms.BindingSource(components);
            gbSqlConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSqlConfigs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sqlConfigDataBindingSource).BeginInit();
            SuspendLayout();
            // 
            // gbSqlConfig
            // 
            gbSqlConfig.Controls.Add(label1);
            gbSqlConfig.Controls.Add(txtDatabaseIdPrefix);
            gbSqlConfig.Controls.Add(btnModifySqlConfig);
            gbSqlConfig.Controls.Add(btnDeleteSqlConfig);
            gbSqlConfig.Controls.Add(btnAddSqlConfig);
            gbSqlConfig.Controls.Add(btnRefreshSqlConfig);
            gbSqlConfig.Controls.Add(dgvSqlConfigs);
            gbSqlConfig.Location = new System.Drawing.Point(3, 3);
            gbSqlConfig.Name = "gbSqlConfig";
            gbSqlConfig.Size = new System.Drawing.Size(771, 812);
            gbSqlConfig.TabIndex = 6;
            gbSqlConfig.TabStop = false;
            gbSqlConfig.Text = "SQL Config";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(22, 52);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(129, 30);
            label1.TabIndex = 10;
            label1.Text = "Database Id:";
            // 
            // txtDatabaseIdPrefix
            // 
            txtDatabaseIdPrefix.Location = new System.Drawing.Point(166, 52);
            txtDatabaseIdPrefix.Name = "txtDatabaseIdPrefix";
            txtDatabaseIdPrefix.Size = new System.Drawing.Size(224, 35);
            txtDatabaseIdPrefix.TabIndex = 9;
            // 
            // btnModifySqlConfig
            // 
            btnModifySqlConfig.Location = new System.Drawing.Point(136, 750);
            btnModifySqlConfig.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            btnModifySqlConfig.Name = "btnModifySqlConfig";
            btnModifySqlConfig.Size = new System.Drawing.Size(106, 40);
            btnModifySqlConfig.TabIndex = 8;
            btnModifySqlConfig.Text = "Modify";
            btnModifySqlConfig.UseVisualStyleBackColor = true;
            btnModifySqlConfig.Click += btnModifySqlConfig_Click;
            // 
            // btnDeleteSqlConfig
            // 
            btnDeleteSqlConfig.Location = new System.Drawing.Point(250, 750);
            btnDeleteSqlConfig.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            btnDeleteSqlConfig.Name = "btnDeleteSqlConfig";
            btnDeleteSqlConfig.Size = new System.Drawing.Size(106, 40);
            btnDeleteSqlConfig.TabIndex = 7;
            btnDeleteSqlConfig.Text = "Delete";
            btnDeleteSqlConfig.UseVisualStyleBackColor = true;
            btnDeleteSqlConfig.Click += btnDeleteSqlConfig_Click;
            // 
            // btnAddSqlConfig
            // 
            btnAddSqlConfig.Location = new System.Drawing.Point(22, 750);
            btnAddSqlConfig.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            btnAddSqlConfig.Name = "btnAddSqlConfig";
            btnAddSqlConfig.Size = new System.Drawing.Size(106, 40);
            btnAddSqlConfig.TabIndex = 6;
            btnAddSqlConfig.Text = "Add";
            btnAddSqlConfig.UseVisualStyleBackColor = true;
            btnAddSqlConfig.Click += btnAddSqlConfig_Click;
            // 
            // btnRefreshSqlConfig
            // 
            btnRefreshSqlConfig.Location = new System.Drawing.Point(422, 52);
            btnRefreshSqlConfig.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            btnRefreshSqlConfig.Name = "btnRefreshSqlConfig";
            btnRefreshSqlConfig.Size = new System.Drawing.Size(128, 40);
            btnRefreshSqlConfig.TabIndex = 5;
            btnRefreshSqlConfig.Text = "Refresh";
            btnRefreshSqlConfig.UseVisualStyleBackColor = true;
            btnRefreshSqlConfig.Click += btnRefreshSqlConfig_Click;
            // 
            // dgvSqlConfigs
            // 
            dgvSqlConfigs.AllowUserToAddRows = false;
            dgvSqlConfigs.AllowUserToDeleteRows = false;
            dgvSqlConfigs.AutoGenerateColumns = false;
            dgvSqlConfigs.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dgvSqlConfigs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSqlConfigs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { isSelectedDataGridViewCheckBoxColumn, idDataGridViewTextBoxColumn, databaseIdDataGridViewTextBoxColumn, Query });
            dgvSqlConfigs.DataSource = sqlConfigDataBindingSource;
            dgvSqlConfigs.Location = new System.Drawing.Point(22, 108);
            dgvSqlConfigs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            dgvSqlConfigs.Name = "dgvSqlConfigs";
            dgvSqlConfigs.RowHeadersVisible = false;
            dgvSqlConfigs.RowHeadersWidth = 51;
            dgvSqlConfigs.RowTemplate.Height = 29;
            dgvSqlConfigs.Size = new System.Drawing.Size(729, 616);
            dgvSqlConfigs.TabIndex = 1;
            dgvSqlConfigs.CellContentClick += dgvSqlConfigs_CellContentClick;
            // 
            // isSelectedDataGridViewCheckBoxColumn
            // 
            isSelectedDataGridViewCheckBoxColumn.DataPropertyName = "IsSelected";
            isSelectedDataGridViewCheckBoxColumn.HeaderText = " ";
            isSelectedDataGridViewCheckBoxColumn.MinimumWidth = 9;
            isSelectedDataGridViewCheckBoxColumn.Name = "isSelectedDataGridViewCheckBoxColumn";
            isSelectedDataGridViewCheckBoxColumn.Width = 40;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(10, 2, 10, 2);
            idDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.MinimumWidth = 9;
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            idDataGridViewTextBoxColumn.Width = 72;
            // 
            // databaseIdDataGridViewTextBoxColumn
            // 
            databaseIdDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            databaseIdDataGridViewTextBoxColumn.DataPropertyName = "DatabaseId";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 2, 10, 2);
            databaseIdDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            databaseIdDataGridViewTextBoxColumn.HeaderText = "Database Id";
            databaseIdDataGridViewTextBoxColumn.MinimumWidth = 9;
            databaseIdDataGridViewTextBoxColumn.Name = "databaseIdDataGridViewTextBoxColumn";
            databaseIdDataGridViewTextBoxColumn.Width = 165;
            // 
            // Query
            // 
            Query.ActiveLinkColor = System.Drawing.Color.White;
            Query.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(10, 2, 10, 2);
            Query.DefaultCellStyle = dataGridViewCellStyle3;
            Query.HeaderText = "Query";
            Query.MinimumWidth = 9;
            Query.Name = "Query";
            Query.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            Query.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            Query.Text = "View";
            Query.UseColumnTextForLinkValue = true;
            // 
            // sqlConfigDataBindingSource
            // 
            sqlConfigDataBindingSource.DataSource = typeof(SqlConfigData);
            // 
            // ucSqlConfig
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gbSqlConfig);
            Name = "ucSqlConfig";
            Size = new System.Drawing.Size(780, 818);
            gbSqlConfig.ResumeLayout(false);
            gbSqlConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSqlConfigs).EndInit();
            ((System.ComponentModel.ISupportInitialize)sqlConfigDataBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox gbSqlConfig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDatabaseIdPrefix;
        private System.Windows.Forms.Button btnModifySqlConfig;
        private System.Windows.Forms.Button btnDeleteSqlConfig;
        private System.Windows.Forms.Button btnAddSqlConfig;
        private System.Windows.Forms.Button btnRefreshSqlConfig;
        private System.Windows.Forms.DataGridView dgvSqlConfigs;
        private System.Windows.Forms.BindingSource sqlConfigDataBindingSource;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isSelectedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn databaseIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewLinkColumn Query;
    }
}
