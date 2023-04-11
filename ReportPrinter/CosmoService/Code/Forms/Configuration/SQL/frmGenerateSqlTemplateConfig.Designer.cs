namespace CosmoService.Code.Forms.Configuration.SQL
{
    partial class frmGenerateSqlTemplateConfig
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
            txtOutputPath = new System.Windows.Forms.TextBox();
            btnSelectPath = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            btnSave = new System.Windows.Forms.Button();
            sfdSqlTemplateConfig = new System.Windows.Forms.SaveFileDialog();
            SuspendLayout();
            // 
            // txtOutputPath
            // 
            txtOutputPath.Location = new System.Drawing.Point(109, 23);
            txtOutputPath.Name = "txtOutputPath";
            txtOutputPath.ReadOnly = true;
            txtOutputPath.Size = new System.Drawing.Size(336, 27);
            txtOutputPath.TabIndex = 0;
            // 
            // btnSelectPath
            // 
            btnSelectPath.Location = new System.Drawing.Point(451, 23);
            btnSelectPath.Name = "btnSelectPath";
            btnSelectPath.Size = new System.Drawing.Size(41, 27);
            btnSelectPath.TabIndex = 1;
            btnSelectPath.Text = "...";
            btnSelectPath.UseVisualStyleBackColor = true;
            btnSelectPath.Click += btnSelectPath_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 26);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(90, 20);
            label1.TabIndex = 2;
            label1.Text = "Output Path:";
            // 
            // btnSave
            // 
            btnSave.Enabled = false;
            btnSave.Location = new System.Drawing.Point(409, 60);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(83, 27);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // frmGenerateSqlTemplateConfig
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(512, 99);
            Controls.Add(btnSave);
            Controls.Add(label1);
            Controls.Add(btnSelectPath);
            Controls.Add(txtOutputPath);
            Name = "frmGenerateSqlTemplateConfig";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Generate Sql Template Config";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.SaveFileDialog sfdSqlTemplateConfig;
    }
}