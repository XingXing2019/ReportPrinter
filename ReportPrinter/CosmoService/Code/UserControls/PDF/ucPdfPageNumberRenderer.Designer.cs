namespace CosmoService.Code.UserControls.PDF
{
    partial class ucPdfPageNumberRenderer
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
            epRendererInfo = new System.Windows.Forms.ErrorProvider(components);
            gbRendererInfo = new System.Windows.Forms.GroupBox();
            ecbPageNumberLocation = new CustomControls.EnumComboBox();
            nudEndPage = new System.Windows.Forms.NumericUpDown();
            label3 = new System.Windows.Forms.Label();
            nudStartPage = new System.Windows.Forms.NumericUpDown();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)epRendererInfo).BeginInit();
            gbRendererInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudEndPage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStartPage).BeginInit();
            SuspendLayout();
            // 
            // epRendererInfo
            // 
            epRendererInfo.ContainerControl = this;
            // 
            // gbRendererInfo
            // 
            gbRendererInfo.Controls.Add(ecbPageNumberLocation);
            gbRendererInfo.Controls.Add(nudEndPage);
            gbRendererInfo.Controls.Add(label3);
            gbRendererInfo.Controls.Add(nudStartPage);
            gbRendererInfo.Controls.Add(label1);
            gbRendererInfo.Controls.Add(label2);
            gbRendererInfo.Location = new System.Drawing.Point(3, 3);
            gbRendererInfo.Name = "gbRendererInfo";
            gbRendererInfo.Size = new System.Drawing.Size(457, 131);
            gbRendererInfo.TabIndex = 5;
            gbRendererInfo.TabStop = false;
            gbRendererInfo.Text = "Renderer Info";
            // 
            // ecbPageNumberLocation
            // 
            ecbPageNumberLocation.EnumType = null;
            ecbPageNumberLocation.FormattingEnabled = true;
            ecbPageNumberLocation.Location = new System.Drawing.Point(187, 92);
            ecbPageNumberLocation.Name = "ecbPageNumberLocation";
            ecbPageNumberLocation.Size = new System.Drawing.Size(256, 28);
            ecbPageNumberLocation.TabIndex = 36;
            // 
            // nudEndPage
            // 
            nudEndPage.Location = new System.Drawing.Point(187, 61);
            nudEndPage.Maximum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            nudEndPage.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            nudEndPage.Name = "nudEndPage";
            nudEndPage.Size = new System.Drawing.Size(255, 27);
            nudEndPage.TabIndex = 35;
            nudEndPage.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(17, 65);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(73, 20);
            label3.TabIndex = 34;
            label3.Text = "End Page:";
            // 
            // nudStartPage
            // 
            nudStartPage.Location = new System.Drawing.Point(187, 33);
            nudStartPage.Name = "nudStartPage";
            nudStartPage.Size = new System.Drawing.Size(255, 27);
            nudStartPage.TabIndex = 33;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(18, 95);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(163, 20);
            label1.TabIndex = 14;
            label1.Text = "Page Number Loaction:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(17, 36);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(79, 20);
            label2.TabIndex = 4;
            label2.Text = "Start Page:";
            // 
            // ucPdfPageNumberRenderer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gbRendererInfo);
            Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            Name = "ucPdfPageNumberRenderer";
            Size = new System.Drawing.Size(465, 139);
            Load += ucPdfPageNumberRenderer_Load;
            ((System.ComponentModel.ISupportInitialize)epRendererInfo).EndInit();
            gbRendererInfo.ResumeLayout(false);
            gbRendererInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudEndPage).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStartPage).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ErrorProvider epRendererInfo;
        private System.Windows.Forms.GroupBox gbRendererInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudStartPage;
        private System.Windows.Forms.NumericUpDown nudEndPage;
        private System.Windows.Forms.Label label3;
        private CustomControls.EnumComboBox ecbPageNumberLocation;
    }
}
