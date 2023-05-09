namespace CosmoService.Code.UserControls.PDF
{
    partial class ucPdfReprintMarkRenderer
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
            ecbReprintMarkLocation = new CustomControls.EnumComboBox();
            label3 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            nudBoardThickness = new System.Windows.Forms.NumericUpDown();
            tbText = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            epRendererInfo = new System.Windows.Forms.ErrorProvider(components);
            gbRendererInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudBoardThickness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)epRendererInfo).BeginInit();
            SuspendLayout();
            // 
            // gbRendererInfo
            // 
            gbRendererInfo.Controls.Add(ecbReprintMarkLocation);
            gbRendererInfo.Controls.Add(label3);
            gbRendererInfo.Controls.Add(label1);
            gbRendererInfo.Controls.Add(nudBoardThickness);
            gbRendererInfo.Controls.Add(tbText);
            gbRendererInfo.Controls.Add(label2);
            gbRendererInfo.Location = new System.Drawing.Point(4, 4);
            gbRendererInfo.Margin = new System.Windows.Forms.Padding(4);
            gbRendererInfo.Name = "gbRendererInfo";
            gbRendererInfo.Padding = new System.Windows.Forms.Padding(4);
            gbRendererInfo.Size = new System.Drawing.Size(685, 211);
            gbRendererInfo.TabIndex = 5;
            gbRendererInfo.TabStop = false;
            gbRendererInfo.Text = "Renderer Info";
            // 
            // ecbReprintMarkLocation
            // 
            ecbReprintMarkLocation.EnumType = null;
            ecbReprintMarkLocation.FormattingEnabled = true;
            ecbReprintMarkLocation.Location = new System.Drawing.Point(283, 150);
            ecbReprintMarkLocation.Margin = new System.Windows.Forms.Padding(4);
            ecbReprintMarkLocation.Name = "ecbReprintMarkLocation";
            ecbReprintMarkLocation.Size = new System.Drawing.Size(365, 38);
            ecbReprintMarkLocation.TabIndex = 38;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(26, 153);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(222, 30);
            label3.TabIndex = 37;
            label3.Text = "Reprint Mark Loaction:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(26, 103);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(167, 30);
            label1.TabIndex = 32;
            label1.Text = "Board Thickness:";
            // 
            // nudBoardThickness
            // 
            nudBoardThickness.DecimalPlaces = 1;
            nudBoardThickness.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudBoardThickness.Location = new System.Drawing.Point(283, 98);
            nudBoardThickness.Margin = new System.Windows.Forms.Padding(4);
            nudBoardThickness.Name = "nudBoardThickness";
            nudBoardThickness.Size = new System.Drawing.Size(365, 35);
            nudBoardThickness.TabIndex = 31;
            nudBoardThickness.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // tbText
            // 
            tbText.Location = new System.Drawing.Point(283, 48);
            tbText.Name = "tbText";
            tbText.Size = new System.Drawing.Size(365, 35);
            tbText.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(26, 54);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(55, 30);
            label2.TabIndex = 4;
            label2.Text = "Text:";
            // 
            // epRendererInfo
            // 
            epRendererInfo.ContainerControl = this;
            // 
            // ucPdfReprintMarkRenderer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gbRendererInfo);
            Name = "ucPdfReprintMarkRenderer";
            Size = new System.Drawing.Size(698, 222);
            Load += ucPdfReprintMarkRenderer_Load;
            gbRendererInfo.ResumeLayout(false);
            gbRendererInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudBoardThickness).EndInit();
            ((System.ComponentModel.ISupportInitialize)epRendererInfo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox gbRendererInfo;
        private System.Windows.Forms.TextBox tbText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudBoardThickness;
        private CustomControls.EnumComboBox ecbReprintMarkLocation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ErrorProvider epRendererInfo;
    }
}
