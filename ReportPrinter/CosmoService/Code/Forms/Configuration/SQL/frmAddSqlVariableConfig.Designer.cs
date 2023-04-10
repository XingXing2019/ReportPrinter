namespace CosmoService.Code.Forms.Configuration.SQL
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
            lblName.Location = new System.Drawing.Point(33, 33);
            lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblName.Name = "lblName";
            lblName.Size = new System.Drawing.Size(74, 30);
            lblName.TabIndex = 0;
            lblName.Text = "Name:";
            // 
            // txtName
            // 
            txtName.Location = new System.Drawing.Point(115, 32);
            txtName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            txtName.Name = "txtName";
            txtName.Size = new System.Drawing.Size(403, 35);
            txtName.TabIndex = 1;
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(414, 81);
            btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(106, 42);
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
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(546, 140);
            Controls.Add(btnSave);
            Controls.Add(txtName);
            Controls.Add(lblName);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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