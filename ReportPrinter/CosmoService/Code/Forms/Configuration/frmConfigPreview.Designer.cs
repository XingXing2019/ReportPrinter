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
            btnPreview.Location = new System.Drawing.Point(845, 653);
            btnPreview.Name = "btnPreview";
            btnPreview.Size = new System.Drawing.Size(83, 27);
            btnPreview.TabIndex = 11;
            btnPreview.Text = "Close";
            btnPreview.UseVisualStyleBackColor = true;
            // 
            // txtPreview
            // 
            txtPreview.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtPreview.Location = new System.Drawing.Point(24, 25);
            txtPreview.Multiline = true;
            txtPreview.Name = "txtPreview";
            txtPreview.ReadOnly = true;
            txtPreview.Size = new System.Drawing.Size(904, 615);
            txtPreview.TabIndex = 12;
            // 
            // frmConfigPreview
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(951, 697);
            Controls.Add(txtPreview);
            Controls.Add(btnPreview);
            Name = "frmConfigPreview";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Config Preview";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.TextBox txtPreview;
    }
}