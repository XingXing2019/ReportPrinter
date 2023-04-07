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
            dgvSqlConfigs = new System.Windows.Forms.DataGridView();
            btnRefresh = new System.Windows.Forms.Button();
            btnAdd = new System.Windows.Forms.Button();
            btnDelete = new System.Windows.Forms.Button();
            btnModify = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)dgvSqlConfigs).BeginInit();
            SuspendLayout();
            // 
            // dgvSqlConfigs
            // 
            dgvSqlConfigs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSqlConfigs.Location = new System.Drawing.Point(33, 112);
            dgvSqlConfigs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            dgvSqlConfigs.Name = "dgvSqlConfigs";
            dgvSqlConfigs.RowHeadersWidth = 51;
            dgvSqlConfigs.RowTemplate.Height = 29;
            dgvSqlConfigs.Size = new System.Drawing.Size(1026, 831);
            dgvSqlConfigs.TabIndex = 0;
            dgvSqlConfigs.CellContentClick += dgvSqlConfigs_CellContentClick;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new System.Drawing.Point(33, 48);
            btnRefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(142, 44);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new System.Drawing.Point(184, 48);
            btnAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new System.Drawing.Size(144, 44);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new System.Drawing.Point(490, 48);
            btnDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(144, 44);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnModify
            // 
            btnModify.Location = new System.Drawing.Point(338, 48);
            btnModify.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            btnModify.Name = "btnModify";
            btnModify.Size = new System.Drawing.Size(144, 44);
            btnModify.TabIndex = 4;
            btnModify.Text = "Modify";
            btnModify.UseVisualStyleBackColor = true;
            btnModify.Click += btnModify_Click;
            // 
            // frmSqlConfig
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1101, 962);
            Controls.Add(btnModify);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(btnRefresh);
            Controls.Add(dgvSqlConfigs);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            Name = "frmSqlConfig";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "SQL Config";
            ((System.ComponentModel.ISupportInitialize)dgvSqlConfigs).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSqlConfigs;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnModify;
    }
}