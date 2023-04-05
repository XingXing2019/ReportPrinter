namespace CosmoService.Code.Forms.Configuration
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
            dgvSqlConfigs.Location = new System.Drawing.Point(22, 75);
            dgvSqlConfigs.Name = "dgvSqlConfigs";
            dgvSqlConfigs.RowHeadersWidth = 51;
            dgvSqlConfigs.RowTemplate.Height = 29;
            dgvSqlConfigs.Size = new System.Drawing.Size(684, 554);
            dgvSqlConfigs.TabIndex = 0;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new System.Drawing.Point(22, 32);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(95, 29);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new System.Drawing.Point(123, 32);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new System.Drawing.Size(96, 29);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new System.Drawing.Point(327, 32);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(96, 29);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnModify
            // 
            btnModify.Location = new System.Drawing.Point(225, 32);
            btnModify.Name = "btnModify";
            btnModify.Size = new System.Drawing.Size(96, 29);
            btnModify.TabIndex = 4;
            btnModify.Text = "Modify";
            btnModify.UseVisualStyleBackColor = true;
            btnModify.Click += btnModify_Click;
            // 
            // frmSqlConfig
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(734, 641);
            Controls.Add(btnModify);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(btnRefresh);
            Controls.Add(dgvSqlConfigs);
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