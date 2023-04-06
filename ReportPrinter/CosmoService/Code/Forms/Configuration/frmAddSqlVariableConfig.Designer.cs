namespace CosmoService.Code.Forms.Configuration
{
    partial class frmAddSqlVariableConfig
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
            lblName = new System.Windows.Forms.Label();
            txtName = new System.Windows.Forms.TextBox();
            btnSave = new System.Windows.Forms.Button();
            epAddSqlVariableConfig = new System.Windows.Forms.ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)epAddSqlVariableConfig).BeginInit();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new System.Drawing.Point(22, 24);
            lblName.Name = "lblName";
            lblName.Size = new System.Drawing.Size(49, 20);
            lblName.TabIndex = 0;
            lblName.Text = "Name";
            // 
            // txtName
            // 
            txtName.Location = new System.Drawing.Point(95, 21);
            txtName.Name = "txtName";
            txtName.Size = new System.Drawing.Size(252, 27);
            txtName.TabIndex = 1;
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(276, 54);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(71, 28);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // epAddSqlVariableConfig
            // 
            epAddSqlVariableConfig.ContainerControl = this;
            // 
            // frmAddSqlVariableConfig
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(364, 93);
            Controls.Add(btnSave);
            Controls.Add(txtName);
            Controls.Add(lblName);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            Name = "frmAddSqlVariableConfig";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Add SQL Variable Config";
            ((System.ComponentModel.ISupportInitialize)epAddSqlVariableConfig).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ErrorProvider epAddSqlVariableConfig;
    }
}