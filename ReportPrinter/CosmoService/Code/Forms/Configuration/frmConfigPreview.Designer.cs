namespace CosmoService.Code.Forms.Configuration
{
    partial class frmConfigPreview
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
            btnPreview = new System.Windows.Forms.Button();
            txtPreview = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // btnPreview
            // 
            btnPreview.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnPreview.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnPreview.Location = new System.Drawing.Point(1268, 980);
            btnPreview.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            btnPreview.Name = "btnPreview";
            btnPreview.Size = new System.Drawing.Size(124, 40);
            btnPreview.TabIndex = 11;
            btnPreview.Text = "Close";
            btnPreview.UseVisualStyleBackColor = true;
            // 
            // txtPreview
            // 
            txtPreview.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtPreview.Location = new System.Drawing.Point(36, 38);
            txtPreview.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            txtPreview.Multiline = true;
            txtPreview.Name = "txtPreview";
            txtPreview.ReadOnly = true;
            txtPreview.Size = new System.Drawing.Size(1354, 920);
            txtPreview.TabIndex = 12;
            // 
            // frmConfigPreview
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1426, 1046);
            Controls.Add(txtPreview);
            Controls.Add(btnPreview);
            Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            Name = "frmConfigPreview";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.TextBox txtPreview;
    }
}