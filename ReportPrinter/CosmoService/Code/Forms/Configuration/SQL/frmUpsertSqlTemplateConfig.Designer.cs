namespace CosmoService.Code.Forms.Configuration.SQL
{
    partial class frmUpsertSqlTemplateConfig
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
            btnSave = new System.Windows.Forms.Button();
            txtTemplateId = new System.Windows.Forms.TextBox();
            lblTemplateId = new System.Windows.Forms.Label();
            epAddSqlTemplateConfig = new System.Windows.Forms.ErrorProvider(components);
            ucSqlConfig = new UserControls.ucSqlConfig();
            lblSqlConfigError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)epAddSqlTemplateConfig).BeginInit();
            SuspendLayout();
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(556, 22);
            btnSave.Margin = new System.Windows.Forms.Padding(4);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(106, 42);
            btnSave.TabIndex = 5;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // txtTemplateId
            // 
            txtTemplateId.Location = new System.Drawing.Point(194, 27);
            txtTemplateId.Margin = new System.Windows.Forms.Padding(4);
            txtTemplateId.Name = "txtTemplateId";
            txtTemplateId.Size = new System.Drawing.Size(323, 35);
            txtTemplateId.TabIndex = 4;
            // 
            // lblTemplateId
            // 
            lblTemplateId.AutoSize = true;
            lblTemplateId.Location = new System.Drawing.Point(32, 28);
            lblTemplateId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblTemplateId.Name = "lblTemplateId";
            lblTemplateId.Size = new System.Drawing.Size(126, 30);
            lblTemplateId.TabIndex = 3;
            lblTemplateId.Text = "Template Id:";
            // 
            // epAddSqlTemplateConfig
            // 
            epAddSqlTemplateConfig.ContainerControl = this;
            // 
            // ucSqlConfig
            // 
            ucSqlConfig.Location = new System.Drawing.Point(24, 81);
            ucSqlConfig.Name = "ucSqlConfig";
            ucSqlConfig.Size = new System.Drawing.Size(717, 828);
            ucSqlConfig.TabIndex = 6;
            // 
            // lblSqlConfigError
            // 
            lblSqlConfigError.AutoSize = true;
            lblSqlConfigError.Location = new System.Drawing.Point(152, 85);
            lblSqlConfigError.Name = "lblSqlConfigError";
            lblSqlConfigError.Size = new System.Drawing.Size(0, 30);
            lblSqlConfigError.TabIndex = 7;
            // 
            // frmAddSqlTemplateConfig
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(764, 847);
            Controls.Add(lblSqlConfigError);
            Controls.Add(ucSqlConfig);
            Controls.Add(btnSave);
            Controls.Add(txtTemplateId);
            Controls.Add(lblTemplateId);
            Name = "frmAddSqlTemplateConfig";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Add Sql Template Config";
            ((System.ComponentModel.ISupportInitialize)epAddSqlTemplateConfig).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtTemplateId;
        private System.Windows.Forms.Label lblTemplateId;
        private System.Windows.Forms.ErrorProvider epAddSqlTemplateConfig;
        private UserControls.ucSqlConfig ucSqlConfig;
        private System.Windows.Forms.Label lblSqlConfigError;
    }
}