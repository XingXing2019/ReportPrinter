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
            gbRendererInfo.Location = new System.Drawing.Point(3, 3);
            gbRendererInfo.Name = "gbRendererInfo";
            gbRendererInfo.Size = new System.Drawing.Size(457, 173);
            gbRendererInfo.TabIndex = 5;
            gbRendererInfo.TabStop = false;
            gbRendererInfo.Text = "Renderer Info";
            // 
            // tbImageSource
            // 
            tbImageSource.Location = new System.Drawing.Point(155, 65);
            tbImageSource.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            tbImageSource.Multiline = true;
            tbImageSource.Name = "tbImageSource";
            tbImageSource.Size = new System.Drawing.Size(279, 95);
            tbImageSource.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(17, 65);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(103, 20);
            label1.TabIndex = 14;
            label1.Text = "Image Source:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(17, 36);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(89, 20);
            label2.TabIndex = 4;
            label2.Text = "Image Type:";
            // 
            // ecbSourceType
            // 
            ecbSourceType.EnumType = null;
            ecbSourceType.FormattingEnabled = true;
            ecbSourceType.Location = new System.Drawing.Point(155, 32);
            ecbSourceType.Name = "ecbSourceType";
            ecbSourceType.Size = new System.Drawing.Size(279, 28);
            ecbSourceType.TabIndex = 5;
            // 
            // epRendererInfo
            // 
            epRendererInfo.ContainerControl = this;
            // 
            // ucPdfImageRenderer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gbRendererInfo);
            Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            Name = "ucPdfImageRenderer";
            Size = new System.Drawing.Size(464, 181);
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
