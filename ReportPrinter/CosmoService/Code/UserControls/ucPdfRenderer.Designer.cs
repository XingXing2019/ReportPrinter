namespace CosmoService.Code.UserControls
{
    partial class ucPdfRenderer
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
            gbSqlConfig = new System.Windows.Forms.GroupBox();
            label1 = new System.Windows.Forms.Label();
            txtDatabaseIdPrefix = new System.Windows.Forms.TextBox();
            btnModifySqlConfig = new System.Windows.Forms.Button();
            btnDeleteSqlConfig = new System.Windows.Forms.Button();
            btnAddSqlConfig = new System.Windows.Forms.Button();
            btnRefreshSqlConfig = new System.Windows.Forms.Button();
            dgvSqlConfigs = new System.Windows.Forms.DataGridView();
            gbSqlConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSqlConfigs).BeginInit();
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
            gbSqlConfig.TabIndex = 7;
            gbSqlConfig.TabStop = false;
            gbSqlConfig.Text = "Pdf Renderer";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(22, 52);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(125, 30);
            label1.TabIndex = 10;
            label1.Text = "Renderer Id:";
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
            btnModifySqlConfig.Margin = new System.Windows.Forms.Padding(4);
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
            btnDeleteSqlConfig.Margin = new System.Windows.Forms.Padding(4);
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
            btnAddSqlConfig.Margin = new System.Windows.Forms.Padding(4);
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
            btnRefreshSqlConfig.Margin = new System.Windows.Forms.Padding(4);
            btnRefreshSqlConfig.Name = "btnRefreshSqlConfig";
            btnRefreshSqlConfig.Size = new System.Drawing.Size(128, 40);
            btnRefreshSqlConfig.TabIndex = 5;
            btnRefreshSqlConfig.Text = "Refresh";
            btnRefreshSqlConfig.UseVisualStyleBackColor = true;
            // 
            // dgvSqlConfigs
            // 
            dgvSqlConfigs.AllowUserToAddRows = false;
            dgvSqlConfigs.AllowUserToDeleteRows = false;
            dgvSqlConfigs.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dgvSqlConfigs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSqlConfigs.Location = new System.Drawing.Point(22, 108);
            dgvSqlConfigs.Margin = new System.Windows.Forms.Padding(4);
            dgvSqlConfigs.Name = "dgvSqlConfigs";
            dgvSqlConfigs.RowHeadersVisible = false;
            dgvSqlConfigs.RowHeadersWidth = 51;
            dgvSqlConfigs.RowTemplate.Height = 29;
            dgvSqlConfigs.Size = new System.Drawing.Size(729, 616);
            dgvSqlConfigs.TabIndex = 1;
            // 
            // ucPdfRenderer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gbSqlConfig);
            Name = "ucPdfRenderer";
            Size = new System.Drawing.Size(781, 817);
            gbSqlConfig.ResumeLayout(false);
            gbSqlConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSqlConfigs).EndInit();
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
    }
}
