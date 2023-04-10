namespace CosmoService.Code.Forms.Configuration.SQL
{
    partial class frmSqlConfig
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

        #region Windows Form Designer generated code

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            sqlConfigDataBindingSource = new System.Windows.Forms.BindingSource(components);
            groupBox1 = new System.Windows.Forms.GroupBox();
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
            groupBox2 = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            btnModifySqlTemplate = new System.Windows.Forms.Button();
            txtTemplateIdPrefix = new System.Windows.Forms.TextBox();
            dgvSqlTemplateConfigs = new System.Windows.Forms.DataGridView();
            btnRefreshSqlTemplate = new System.Windows.Forms.Button();
            btnDeleteSqlTemplate = new System.Windows.Forms.Button();
            btnAddSqlTemplate = new System.Windows.Forms.Button();
            sqlTemplateConfigDataBindingSource = new System.Windows.Forms.BindingSource(components);
            sqlTemplateConfigDataBindingSource1 = new System.Windows.Forms.BindingSource(components);
            isSelectedDataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            idDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)sqlConfigDataBindingSource).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSqlConfigs).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSqlTemplateConfigs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sqlTemplateConfigDataBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sqlTemplateConfigDataBindingSource1).BeginInit();
            SuspendLayout();
            // 
            // sqlConfigDataBindingSource
            // 
            sqlConfigDataBindingSource.DataSource = typeof(ReportPrinterLibrary.Code.Winform.Configuration.SqlConfigData);
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(txtDatabaseIdPrefix);
            groupBox1.Controls.Add(btnModifySqlConfig);
            groupBox1.Controls.Add(btnDeleteSqlConfig);
            groupBox1.Controls.Add(btnAddSqlConfig);
            groupBox1.Controls.Add(btnRefreshSqlConfig);
            groupBox1.Controls.Add(dgvSqlConfigs);
            groupBox1.Location = new System.Drawing.Point(26, 21);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(704, 812);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "SQL Config";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(23, 52);
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
            btnModifySqlConfig.Location = new System.Drawing.Point(161, 750);
            btnModifySqlConfig.Margin = new System.Windows.Forms.Padding(4);
            btnModifySqlConfig.Name = "btnModifySqlConfig";
            btnModifySqlConfig.Size = new System.Drawing.Size(130, 40);
            btnModifySqlConfig.TabIndex = 8;
            btnModifySqlConfig.Text = "Modify";
            btnModifySqlConfig.UseVisualStyleBackColor = true;
            btnModifySqlConfig.Click += btnModifySqlConfig_Click;
            // 
            // btnDeleteSqlConfig
            // 
            btnDeleteSqlConfig.Location = new System.Drawing.Point(308, 750);
            btnDeleteSqlConfig.Margin = new System.Windows.Forms.Padding(4);
            btnDeleteSqlConfig.Name = "btnDeleteSqlConfig";
            btnDeleteSqlConfig.Size = new System.Drawing.Size(130, 40);
            btnDeleteSqlConfig.TabIndex = 7;
            btnDeleteSqlConfig.Text = "Delete";
            btnDeleteSqlConfig.UseVisualStyleBackColor = true;
            btnDeleteSqlConfig.Click += btnDeleteSqlConfig_Click;
            // 
            // btnAddSqlConfig
            // 
            btnAddSqlConfig.Location = new System.Drawing.Point(23, 750);
            btnAddSqlConfig.Margin = new System.Windows.Forms.Padding(4);
            btnAddSqlConfig.Name = "btnAddSqlConfig";
            btnAddSqlConfig.Size = new System.Drawing.Size(130, 40);
            btnAddSqlConfig.TabIndex = 6;
            btnAddSqlConfig.Text = "Add";
            btnAddSqlConfig.UseVisualStyleBackColor = true;
            btnAddSqlConfig.Click += btnAdd_Click;
            // 
            // btnRefreshSqlConfig
            // 
            btnRefreshSqlConfig.Location = new System.Drawing.Point(422, 52);
            btnRefreshSqlConfig.Margin = new System.Windows.Forms.Padding(4);
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
            dgvSqlConfigs.Location = new System.Drawing.Point(23, 108);
            dgvSqlConfigs.Margin = new System.Windows.Forms.Padding(4);
            dgvSqlConfigs.Name = "dgvSqlConfigs";
            dgvSqlConfigs.RowHeadersVisible = false;
            dgvSqlConfigs.RowHeadersWidth = 51;
            dgvSqlConfigs.RowTemplate.Height = 29;
            dgvSqlConfigs.Size = new System.Drawing.Size(660, 617);
            dgvSqlConfigs.TabIndex = 1;
            dgvSqlConfigs.CellContentClick += dgvSqlConfigs_CellContentClick;
            // 
            // isSelectedDataGridViewCheckBoxColumn
            // 
            isSelectedDataGridViewCheckBoxColumn.DataPropertyName = "IsSelected";
            isSelectedDataGridViewCheckBoxColumn.HeaderText = " ";
            isSelectedDataGridViewCheckBoxColumn.MinimumWidth = 9;
            isSelectedDataGridViewCheckBoxColumn.Name = "isSelectedDataGridViewCheckBoxColumn";
            isSelectedDataGridViewCheckBoxColumn.Width = 60;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
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
            databaseIdDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
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
            // groupBox2
            // 
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(btnModifySqlTemplate);
            groupBox2.Controls.Add(txtTemplateIdPrefix);
            groupBox2.Controls.Add(dgvSqlTemplateConfigs);
            groupBox2.Controls.Add(btnRefreshSqlTemplate);
            groupBox2.Controls.Add(btnDeleteSqlTemplate);
            groupBox2.Controls.Add(btnAddSqlTemplate);
            groupBox2.Location = new System.Drawing.Point(736, 21);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(577, 812);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "SQL Template Config";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(18, 61);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(126, 30);
            label2.TabIndex = 13;
            label2.Text = "Template Id:";
            // 
            // btnModifySqlTemplate
            // 
            btnModifySqlTemplate.Location = new System.Drawing.Point(159, 750);
            btnModifySqlTemplate.Margin = new System.Windows.Forms.Padding(4);
            btnModifySqlTemplate.Name = "btnModifySqlTemplate";
            btnModifySqlTemplate.Size = new System.Drawing.Size(130, 40);
            btnModifySqlTemplate.TabIndex = 13;
            btnModifySqlTemplate.Text = "Modify";
            btnModifySqlTemplate.UseVisualStyleBackColor = true;
            btnModifySqlTemplate.Click += btnModifySqlTemplate_Click;
            // 
            // txtTemplateIdPrefix
            // 
            txtTemplateIdPrefix.Location = new System.Drawing.Point(161, 61);
            txtTemplateIdPrefix.Name = "txtTemplateIdPrefix";
            txtTemplateIdPrefix.Size = new System.Drawing.Size(224, 35);
            txtTemplateIdPrefix.TabIndex = 12;
            // 
            // dgvSqlTemplateConfigs
            // 
            dgvSqlTemplateConfigs.AllowUserToAddRows = false;
            dgvSqlTemplateConfigs.AllowUserToDeleteRows = false;
            dgvSqlTemplateConfigs.AutoGenerateColumns = false;
            dgvSqlTemplateConfigs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSqlTemplateConfigs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { isSelectedDataGridViewCheckBoxColumn1, idDataGridViewTextBoxColumn1 });
            dgvSqlTemplateConfigs.DataSource = sqlTemplateConfigDataBindingSource1;
            dgvSqlTemplateConfigs.Location = new System.Drawing.Point(21, 108);
            dgvSqlTemplateConfigs.Name = "dgvSqlTemplateConfigs";
            dgvSqlTemplateConfigs.RowHeadersVisible = false;
            dgvSqlTemplateConfigs.RowHeadersWidth = 72;
            dgvSqlTemplateConfigs.RowTemplate.Height = 37;
            dgvSqlTemplateConfigs.Size = new System.Drawing.Size(524, 617);
            dgvSqlTemplateConfigs.TabIndex = 0;
            // 
            // btnRefreshSqlTemplate
            // 
            btnRefreshSqlTemplate.Location = new System.Drawing.Point(417, 61);
            btnRefreshSqlTemplate.Margin = new System.Windows.Forms.Padding(4);
            btnRefreshSqlTemplate.Name = "btnRefreshSqlTemplate";
            btnRefreshSqlTemplate.Size = new System.Drawing.Size(128, 40);
            btnRefreshSqlTemplate.TabIndex = 11;
            btnRefreshSqlTemplate.Text = "Refresh";
            btnRefreshSqlTemplate.UseVisualStyleBackColor = true;
            btnRefreshSqlTemplate.Click += btnRefreshSqlTemplate_Click;
            // 
            // btnDeleteSqlTemplate
            // 
            btnDeleteSqlTemplate.Location = new System.Drawing.Point(306, 750);
            btnDeleteSqlTemplate.Margin = new System.Windows.Forms.Padding(4);
            btnDeleteSqlTemplate.Name = "btnDeleteSqlTemplate";
            btnDeleteSqlTemplate.Size = new System.Drawing.Size(130, 40);
            btnDeleteSqlTemplate.TabIndex = 12;
            btnDeleteSqlTemplate.Text = "Delete";
            btnDeleteSqlTemplate.UseVisualStyleBackColor = true;
            btnDeleteSqlTemplate.Click += btnDeleteSqlTemplate_Click;
            // 
            // btnAddSqlTemplate
            // 
            btnAddSqlTemplate.Location = new System.Drawing.Point(21, 750);
            btnAddSqlTemplate.Margin = new System.Windows.Forms.Padding(4);
            btnAddSqlTemplate.Name = "btnAddSqlTemplate";
            btnAddSqlTemplate.Size = new System.Drawing.Size(130, 40);
            btnAddSqlTemplate.TabIndex = 11;
            btnAddSqlTemplate.Text = "Add";
            btnAddSqlTemplate.UseVisualStyleBackColor = true;
            btnAddSqlTemplate.Click += btnAddSqlTemplate_Click;
            // 
            // sqlTemplateConfigDataBindingSource
            // 
            sqlTemplateConfigDataBindingSource.DataSource = typeof(ReportPrinterLibrary.Code.Winform.Configuration.SqlTemplateConfigData);
            // 
            // sqlTemplateConfigDataBindingSource1
            // 
            sqlTemplateConfigDataBindingSource1.DataSource = typeof(ReportPrinterLibrary.Code.Winform.Configuration.SqlTemplateConfigData);
            // 
            // isSelectedDataGridViewCheckBoxColumn1
            // 
            isSelectedDataGridViewCheckBoxColumn1.DataPropertyName = "IsSelected";
            isSelectedDataGridViewCheckBoxColumn1.HeaderText = " ";
            isSelectedDataGridViewCheckBoxColumn1.MinimumWidth = 9;
            isSelectedDataGridViewCheckBoxColumn1.Name = "isSelectedDataGridViewCheckBoxColumn1";
            isSelectedDataGridViewCheckBoxColumn1.Width = 60;
            // 
            // idDataGridViewTextBoxColumn1
            // 
            idDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            idDataGridViewTextBoxColumn1.DataPropertyName = "Id";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(10, 2, 10, 2);
            idDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle4;
            idDataGridViewTextBoxColumn1.HeaderText = "Template Id";
            idDataGridViewTextBoxColumn1.MinimumWidth = 9;
            idDataGridViewTextBoxColumn1.Name = "idDataGridViewTextBoxColumn1";
            // 
            // frmSqlConfig
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1335, 860);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            Margin = new System.Windows.Forms.Padding(4);
            Name = "frmSqlConfig";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Config SQL Template";
            ((System.ComponentModel.ISupportInitialize)sqlConfigDataBindingSource).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSqlConfigs).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSqlTemplateConfigs).EndInit();
            ((System.ComponentModel.ISupportInitialize)sqlTemplateConfigDataBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)sqlTemplateConfigDataBindingSource1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.BindingSource sqlConfigDataBindingSource;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDatabaseIdPrefix;
        private System.Windows.Forms.Button btnModifySqlConfig;
        private System.Windows.Forms.Button btnDeleteSqlConfig;
        private System.Windows.Forms.Button btnAddSqlConfig;
        private System.Windows.Forms.Button btnRefreshSqlConfig;
        private System.Windows.Forms.DataGridView dgvSqlConfigs;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isSelectedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn databaseIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewLinkColumn Query;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvSqlTemplateConfigs;
        private System.Windows.Forms.BindingSource sqlTemplateConfigDataBindingSource;
        private System.Windows.Forms.Button btnModifySqlTemplate;
        private System.Windows.Forms.Button btnDeleteSqlTemplate;
        private System.Windows.Forms.Button btnAddSqlTemplate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTemplateIdPrefix;
        private System.Windows.Forms.Button btnRefreshSqlTemplate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isSelectedDataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn1;
        private System.Windows.Forms.BindingSource sqlTemplateConfigDataBindingSource1;
    }
}