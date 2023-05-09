namespace CosmoService.Code.UserControls.PDF
{
    partial class ucPdfImageRenderer
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
            components = new System.ComponentModel.Container();
            gbRendererInfo = new System.Windows.Forms.GroupBox();
            tbImageSource = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            ecbSourceType = new CustomControls.EnumComboBox();
            epRendererInfo = new System.Windows.Forms.ErrorProvider(components);
            gbRendererInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)epRendererInfo).BeginInit();
            SuspendLayout();
            // 
            // gbRendererInfo
            // 
            gbRendererInfo.Controls.Add(tbImageSource);
            gbRendererInfo.Controls.Add(label1);
            gbRendererInfo.Controls.Add(label2);
            gbRendererInfo.Controls.Add(ecbSourceType);
            gbRendererInfo.Location = new System.Drawing.Point(4, 4);
            gbRendererInfo.Margin = new System.Windows.Forms.Padding(4);
            gbRendererInfo.Name = "gbRendererInfo";
            gbRendererInfo.Padding = new System.Windows.Forms.Padding(4);
            gbRendererInfo.Size = new System.Drawing.Size(685, 259);
            gbRendererInfo.TabIndex = 5;
            gbRendererInfo.TabStop = false;
            gbRendererInfo.Text = "Renderer Info";
            // 
            // tbImageSource
            // 
            tbImageSource.Location = new System.Drawing.Point(232, 98);
            tbImageSource.Multiline = true;
            tbImageSource.Name = "tbImageSource";
            tbImageSource.Size = new System.Drawing.Size(416, 140);
            tbImageSource.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(26, 98);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(145, 30);
            label1.TabIndex = 14;
            label1.Text = "Image Source:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(26, 54);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(194, 30);
            label2.TabIndex = 4;
            label2.Text = "Image Source Type:";
            // 
            // ecbSourceType
            // 
            ecbSourceType.EnumType = null;
            ecbSourceType.FormattingEnabled = true;
            ecbSourceType.Location = new System.Drawing.Point(232, 48);
            ecbSourceType.Margin = new System.Windows.Forms.Padding(4);
            ecbSourceType.Name = "ecbSourceType";
            ecbSourceType.Size = new System.Drawing.Size(416, 38);
            ecbSourceType.TabIndex = 5;
            // 
            // epRendererInfo
            // 
            epRendererInfo.ContainerControl = this;
            // 
            // ucPdfImageRenderer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gbRendererInfo);
            Name = "ucPdfImageRenderer";
            Size = new System.Drawing.Size(696, 271);
            Load += ucPdfImageRenderer_Load;
            gbRendererInfo.ResumeLayout(false);
            gbRendererInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)epRendererInfo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox gbRendererInfo;
        private System.Windows.Forms.TextBox tbImageSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private CustomControls.EnumComboBox ecbSourceType;
        private System.Windows.Forms.ErrorProvider epRendererInfo;
    }
}
