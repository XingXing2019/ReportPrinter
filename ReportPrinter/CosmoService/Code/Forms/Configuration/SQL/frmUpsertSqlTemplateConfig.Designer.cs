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
            btnPreview = new System.Windows.Forms.Button();
            btnGenerate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)epAddSqlTemplateConfig).BeginInit();
            SuspendLayout();
            // 
            // btnSave
            // 
            btnSave.Enabled = false;
            btnSave.Location = new System.Drawing.Point(437, 559);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(83, 27);
            btnSave.TabIndex = 5;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // txtTemplateId
            // 
            txtTemplateId.Location = new System.Drawing.Point(129, 18);
            txtTemplateId.Name = "txtTemplateId";
            txtTemplateId.Size = new System.Drawing.Size(217, 27);
            txtTemplateId.TabIndex = 4;
            // 
            // lblTemplateId
            // 
            lblTemplateId.AutoSize = true;
            lblTemplateId.Location = new System.Drawing.Point(21, 19);
            lblTemplateId.Name = "lblTemplateId";
            lblTemplateId.Size = new System.Drawing.Size(91, 20);
            lblTemplateId.TabIndex = 3;
            lblTemplateId.Text = "Template Id:";
            // 
            // epAddSqlTemplateConfig
            // 
            epAddSqlTemplateConfig.ContainerControl = this;
            // 
            // ucSqlConfig
            // 
            ucSqlConfig.Location = new System.Drawing.Point(16, 54);
            ucSqlConfig.Margin = new System.Windows.Forms.Padding(1);
            ucSqlConfig.Name = "ucSqlConfig";
            ucSqlConfig.Size = new System.Drawing.Size(529, 500);
            ucSqlConfig.TabIndex = 6;
            // 
            // lblSqlConfigError
            // 
            lblSqlConfigError.AutoSize = true;
            lblSqlConfigError.Location = new System.Drawing.Point(101, 57);
            lblSqlConfigError.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            lblSqlConfigError.Name = "lblSqlConfigError";
            lblSqlConfigError.Size = new System.Drawing.Size(0, 20);
            lblSqlConfigError.TabIndex = 7;
            // 
            // btnPreview
            // 
            btnPreview.Location = new System.Drawing.Point(259, 559);
            btnPreview.Name = "btnPreview";
            btnPreview.Size = new System.Drawing.Size(83, 27);
            btnPreview.TabIndex = 11;
            btnPreview.Text = "Preview";
            btnPreview.UseVisualStyleBackColor = true;
            btnPreview.Click += btnPreview_Click;
            // 
            // btnGenerate
            // 
            btnGenerate.Enabled = false;
            btnGenerate.Location = new System.Drawing.Point(348, 559);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new System.Drawing.Size(83, 27);
            btnGenerate.TabIndex = 12;
            btnGenerate.Text = "Generate";
            btnGenerate.UseVisualStyleBackColor = true;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // frmUpsertSqlTemplateConfig
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(548, 604);
            Controls.Add(btnGenerate);
            Controls.Add(btnPreview);
            Controls.Add(lblSqlConfigError);
            Controls.Add(ucSqlConfig);
            Controls.Add(btnSave);
            Controls.Add(txtTemplateId);
            Controls.Add(lblTemplateId);
            Margin = new System.Windows.Forms.Padding(2);
            Name = "frmUpsertSqlTemplateConfig";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnGenerate;
    }
}