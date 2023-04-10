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
            sqlConfigDataBindingSource = new System.Windows.Forms.BindingSource(components);
            groupBox2 = new System.Windows.Forms.GroupBox();
            label2 = new System.Windows.Forms.Label();
            btnModifySqlTemplate = new System.Windows.Forms.Button();
            txtTemplateIdPrefix = new System.Windows.Forms.TextBox();
            dgvSqlTemplateConfigs = new System.Windows.Forms.DataGridView();
            isSelectedDataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            idDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            sqlTemplateConfigDataBindingSource1 = new System.Windows.Forms.BindingSource(components);
            btnRefreshSqlTemplate = new System.Windows.Forms.Button();
            btnDeleteSqlTemplate = new System.Windows.Forms.Button();
            btnAddSqlTemplate = new System.Windows.Forms.Button();
            sqlTemplateConfigDataBindingSource = new System.Windows.Forms.BindingSource(components);
            ucSqlConfig = new UserControls.ucSqlConfig();
            ((System.ComponentModel.ISupportInitialize)sqlConfigDataBindingSource).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSqlTemplateConfigs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sqlTemplateConfigDataBindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sqlTemplateConfigDataBindingSource).BeginInit();
            SuspendLayout();
            // 
            // sqlConfigDataBindingSource
            // 
            sqlConfigDataBindingSource.DataSource = typeof(ReportPrinterLibrary.Code.Winform.Configuration.SqlConfigData);
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
            btnModifySqlTemplate.Location = new System.Drawing.Point(135, 750);
            btnModifySqlTemplate.Margin = new System.Windows.Forms.Padding(4);
            btnModifySqlTemplate.Name = "btnModifySqlTemplate";
            btnModifySqlTemplate.Size = new System.Drawing.Size(106, 40);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(10, 2, 10, 2);
            idDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            idDataGridViewTextBoxColumn1.HeaderText = "Template Id";
            idDataGridViewTextBoxColumn1.MinimumWidth = 9;
            idDataGridViewTextBoxColumn1.Name = "idDataGridViewTextBoxColumn1";
            // 
            // sqlTemplateConfigDataBindingSource1
            // 
            sqlTemplateConfigDataBindingSource1.DataSource = typeof(ReportPrinterLibrary.Code.Winform.Configuration.SqlTemplateConfigData);
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
            btnDeleteSqlTemplate.Location = new System.Drawing.Point(249, 750);
            btnDeleteSqlTemplate.Margin = new System.Windows.Forms.Padding(4);
            btnDeleteSqlTemplate.Name = "btnDeleteSqlTemplate";
            btnDeleteSqlTemplate.Size = new System.Drawing.Size(106, 40);
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
            btnAddSqlTemplate.Size = new System.Drawing.Size(106, 40);
            btnAddSqlTemplate.TabIndex = 11;
            btnAddSqlTemplate.Text = "Add";
            btnAddSqlTemplate.UseVisualStyleBackColor = true;
            btnAddSqlTemplate.Click += btnAddSqlTemplate_Click;
            // 
            // sqlTemplateConfigDataBindingSource
            // 
            sqlTemplateConfigDataBindingSource.DataSource = typeof(ReportPrinterLibrary.Code.Winform.Configuration.SqlTemplateConfigData);
            // 
            // ucSqlConfig
            // 
            ucSqlConfig.Location = new System.Drawing.Point(-3, 3);
            ucSqlConfig.Name = "ucSqlConfig";
            ucSqlConfig.Size = new System.Drawing.Size(733, 831);
            ucSqlConfig.TabIndex = 7;
            // 
            // frmSqlConfig
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1335, 860);
            Controls.Add(ucSqlConfig);
            Controls.Add(groupBox2);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            Margin = new System.Windows.Forms.Padding(4);
            Name = "frmSqlConfig";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Config SQL Template";
            ((System.ComponentModel.ISupportInitialize)sqlConfigDataBindingSource).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSqlTemplateConfigs).EndInit();
            ((System.ComponentModel.ISupportInitialize)sqlTemplateConfigDataBindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)sqlTemplateConfigDataBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.BindingSource sqlConfigDataBindingSource;
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
        private UserControls.ucSqlConfig ucSqlConfig;
    }
}