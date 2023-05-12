namespace CosmoService.Code.UserControls.PDF
{
    partial class ucPdfWaterMarkRenderer
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
            nudRotate = new System.Windows.Forms.NumericUpDown();
            label12 = new System.Windows.Forms.Label();
            ucSqlSelector = new SQL.ucSqlSelector();
            tbContent = new System.Windows.Forms.TextBox();
            lblContent = new System.Windows.Forms.Label();
            ecbWaterMarkType = new CustomControls.EnumComboBox();
            label4 = new System.Windows.Forms.Label();
            ecbWaterMarkLocation = new CustomControls.EnumComboBox();
            nudEndPage = new System.Windows.Forms.NumericUpDown();
            label3 = new System.Windows.Forms.Label();
            nudStartPage = new System.Windows.Forms.NumericUpDown();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            epRendererInfo = new System.Windows.Forms.ErrorProvider(components);
            gbRendererInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudRotate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudEndPage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStartPage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)epRendererInfo).BeginInit();
            SuspendLayout();
            // 
            // gbRendererInfo
            // 
            gbRendererInfo.Controls.Add(nudRotate);
            gbRendererInfo.Controls.Add(label12);
            gbRendererInfo.Controls.Add(ucSqlSelector);
            gbRendererInfo.Controls.Add(tbContent);
            gbRendererInfo.Controls.Add(lblContent);
            gbRendererInfo.Controls.Add(ecbWaterMarkType);
            gbRendererInfo.Controls.Add(label4);
            gbRendererInfo.Controls.Add(ecbWaterMarkLocation);
            gbRendererInfo.Controls.Add(nudEndPage);
            gbRendererInfo.Controls.Add(label3);
            gbRendererInfo.Controls.Add(nudStartPage);
            gbRendererInfo.Controls.Add(label1);
            gbRendererInfo.Controls.Add(label2);
            gbRendererInfo.Location = new System.Drawing.Point(3, 3);
            gbRendererInfo.Name = "gbRendererInfo";
            gbRendererInfo.Size = new System.Drawing.Size(457, 501);
            gbRendererInfo.TabIndex = 6;
            gbRendererInfo.TabStop = false;
            gbRendererInfo.Text = "Renderer Info";
            // 
            // nudRotate
            // 
            nudRotate.DecimalPlaces = 1;
            nudRotate.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudRotate.Location = new System.Drawing.Point(157, 122);
            nudRotate.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            nudRotate.Name = "nudRotate";
            nudRotate.Size = new System.Drawing.Size(111, 27);
            nudRotate.TabIndex = 42;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(18, 123);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(56, 20);
            label12.TabIndex = 43;
            label12.Text = "Rotate:";
            // 
            // ucSqlSelector
            // 
            ucSqlSelector.Location = new System.Drawing.Point(8, 179);
            ucSqlSelector.Margin = new System.Windows.Forms.Padding(4);
            ucSqlSelector.Name = "ucSqlSelector";
            ucSqlSelector.Size = new System.Drawing.Size(447, 108);
            ucSqlSelector.TabIndex = 41;
            // 
            // tbContent
            // 
            tbContent.Location = new System.Drawing.Point(157, 291);
            tbContent.Multiline = true;
            tbContent.Name = "tbContent";
            tbContent.Size = new System.Drawing.Size(279, 193);
            tbContent.TabIndex = 40;
            // 
            // lblContent
            // 
            lblContent.AutoSize = true;
            lblContent.Location = new System.Drawing.Point(21, 297);
            lblContent.Name = "lblContent";
            lblContent.Size = new System.Drawing.Size(64, 20);
            lblContent.TabIndex = 39;
            lblContent.Text = "Content:";
            // 
            // ecbWaterMarkType
            // 
            ecbWaterMarkType.EnumType = null;
            ecbWaterMarkType.FormattingEnabled = true;
            ecbWaterMarkType.Location = new System.Drawing.Point(157, 34);
            ecbWaterMarkType.Name = "ecbWaterMarkType";
            ecbWaterMarkType.Size = new System.Drawing.Size(279, 28);
            ecbWaterMarkType.TabIndex = 38;
            ecbWaterMarkType.SelectedIndexChanged += ecbWaterMarkType_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(18, 35);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(123, 20);
            label4.TabIndex = 37;
            label4.Text = "Water Mark Type:";
            // 
            // ecbWaterMarkLocation
            // 
            ecbWaterMarkLocation.EnumType = null;
            ecbWaterMarkLocation.FormattingEnabled = true;
            ecbWaterMarkLocation.Location = new System.Drawing.Point(157, 151);
            ecbWaterMarkLocation.Name = "ecbWaterMarkLocation";
            ecbWaterMarkLocation.Size = new System.Drawing.Size(279, 28);
            ecbWaterMarkLocation.TabIndex = 36;
            // 
            // nudEndPage
            // 
            nudEndPage.Location = new System.Drawing.Point(157, 93);
            nudEndPage.Maximum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            nudEndPage.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            nudEndPage.Name = "nudEndPage";
            nudEndPage.Size = new System.Drawing.Size(111, 27);
            nudEndPage.TabIndex = 35;
            nudEndPage.Value = new decimal(new int[] { 1, 0, 0, int.MinValue });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(18, 95);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(73, 20);
            label3.TabIndex = 34;
            label3.Text = "End Page:";
            // 
            // nudStartPage
            // 
            nudStartPage.Location = new System.Drawing.Point(157, 65);
            nudStartPage.Name = "nudStartPage";
            nudStartPage.Size = new System.Drawing.Size(111, 27);
            nudStartPage.TabIndex = 33;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(18, 153);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(69, 20);
            label1.TabIndex = 14;
            label1.Text = "Loaction:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(18, 66);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(79, 20);
            label2.TabIndex = 4;
            label2.Text = "Start Page:";
            // 
            // epRendererInfo
            // 
            epRendererInfo.ContainerControl = this;
            // 
            // ucPdfWaterMarkRenderer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gbRendererInfo);
            Margin = new System.Windows.Forms.Padding(2);
            Name = "ucPdfWaterMarkRenderer";
            Size = new System.Drawing.Size(465, 512);
            Load += ucPdfWaterMarkRenderer_Load;
            gbRendererInfo.ResumeLayout(false);
            gbRendererInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudRotate).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudEndPage).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStartPage).EndInit();
            ((System.ComponentModel.ISupportInitialize)epRendererInfo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox gbRendererInfo;
        private CustomControls.EnumComboBox ecbWaterMarkLocation;
        private System.Windows.Forms.NumericUpDown nudEndPage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudStartPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private CustomControls.EnumComboBox ecbWaterMarkType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ErrorProvider epRendererInfo;
        private SQL.ucSqlSelector ucSqlSelector;
        private System.Windows.Forms.TextBox tbContent;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.NumericUpDown nudRotate;
        private System.Windows.Forms.Label label12;
    }
}
