using CosmoService.Code.UserControls.SQL;

namespace CosmoService.Code.UserControls.PDF
{
    partial class ucPdfBarcodeRenderer
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
            label1 = new System.Windows.Forms.Label();
            cbShowBarcodeText = new System.Windows.Forms.CheckBox();
            ucSqlSelector = new ucSqlSelector();
            label2 = new System.Windows.Forms.Label();
            ecbBarcodeFormat = new CustomControls.EnumComboBox();
            epRendererInfo = new System.Windows.Forms.ErrorProvider(components);
            gbRendererInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)epRendererInfo).BeginInit();
            SuspendLayout();
            // 
            // gbRendererInfo
            // 
            gbRendererInfo.Controls.Add(label1);
            gbRendererInfo.Controls.Add(cbShowBarcodeText);
            gbRendererInfo.Controls.Add(ucSqlSelector);
            gbRendererInfo.Controls.Add(label2);
            gbRendererInfo.Controls.Add(ecbBarcodeFormat);
            gbRendererInfo.Location = new System.Drawing.Point(3, 3);
            gbRendererInfo.Name = "gbRendererInfo";
            gbRendererInfo.Size = new System.Drawing.Size(457, 211);
            gbRendererInfo.TabIndex = 4;
            gbRendererInfo.TabStop = false;
            gbRendererInfo.Text = "Renderer Info";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(17, 67);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(79, 20);
            label1.TabIndex = 14;
            label1.Text = "Show Text:";
            // 
            // cbShowBarcodeText
            // 
            cbShowBarcodeText.AutoSize = true;
            cbShowBarcodeText.Location = new System.Drawing.Point(155, 70);
            cbShowBarcodeText.Margin = new System.Windows.Forms.Padding(2);
            cbShowBarcodeText.Name = "cbShowBarcodeText";
            cbShowBarcodeText.Size = new System.Drawing.Size(18, 17);
            cbShowBarcodeText.TabIndex = 13;
            cbShowBarcodeText.UseVisualStyleBackColor = true;
            // 
            // ucSqlSelector
            // 
            ucSqlSelector.Location = new System.Drawing.Point(4, 89);
            ucSqlSelector.Margin = new System.Windows.Forms.Padding(4);
            ucSqlSelector.Name = "ucSqlSelector";
            ucSqlSelector.Size = new System.Drawing.Size(451, 106);
            ucSqlSelector.TabIndex = 12;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(17, 35);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(118, 20);
            label2.TabIndex = 4;
            label2.Text = "Barcode Format:";
            // 
            // ecbBarcodeFormat
            // 
            ecbBarcodeFormat.EnumType = null;
            ecbBarcodeFormat.FormattingEnabled = true;
            ecbBarcodeFormat.Location = new System.Drawing.Point(155, 32);
            ecbBarcodeFormat.Name = "ecbBarcodeFormat";
            ecbBarcodeFormat.Size = new System.Drawing.Size(279, 28);
            ecbBarcodeFormat.TabIndex = 5;
            // 
            // epRendererInfo
            // 
            epRendererInfo.ContainerControl = this;
            // 
            // ucPdfBarcodeRenderer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gbRendererInfo);
            Margin = new System.Windows.Forms.Padding(2);
            Name = "ucPdfBarcodeRenderer";
            Size = new System.Drawing.Size(467, 217);
            Load += ucPdfBarcodeRenderer_Load;
            gbRendererInfo.ResumeLayout(false);
            gbRendererInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)epRendererInfo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox gbRendererInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbShowBarcodeText;
        private ucSqlSelector ucSqlSelector;
        private System.Windows.Forms.Label label2;
        private CustomControls.EnumComboBox ecbBarcodeFormat;
        private System.Windows.Forms.ErrorProvider epRendererInfo;
    }
}
