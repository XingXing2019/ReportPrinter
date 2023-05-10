namespace CosmoService.Code.UserControls.PDF
{
    partial class ucPdfTextRenderer
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
            tbMask = new System.Windows.Forms.TextBox();
            lblMask = new System.Windows.Forms.Label();
            tbTitle = new System.Windows.Forms.TextBox();
            lblTitle = new System.Windows.Forms.Label();
            ucSqlSelector = new SQL.ucSqlSelector();
            tbContent = new System.Windows.Forms.TextBox();
            lblContent = new System.Windows.Forms.Label();
            ecbTextRendererType = new CustomControls.EnumComboBox();
            label4 = new System.Windows.Forms.Label();
            epRendererInfo = new System.Windows.Forms.ErrorProvider(components);
            gbRendererInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)epRendererInfo).BeginInit();
            SuspendLayout();
            // 
            // gbRendererInfo
            // 
            gbRendererInfo.Controls.Add(tbMask);
            gbRendererInfo.Controls.Add(lblMask);
            gbRendererInfo.Controls.Add(tbTitle);
            gbRendererInfo.Controls.Add(lblTitle);
            gbRendererInfo.Controls.Add(ucSqlSelector);
            gbRendererInfo.Controls.Add(tbContent);
            gbRendererInfo.Controls.Add(lblContent);
            gbRendererInfo.Controls.Add(ecbTextRendererType);
            gbRendererInfo.Controls.Add(label4);
            gbRendererInfo.Location = new System.Drawing.Point(4, 4);
            gbRendererInfo.Margin = new System.Windows.Forms.Padding(4);
            gbRendererInfo.Name = "gbRendererInfo";
            gbRendererInfo.Padding = new System.Windows.Forms.Padding(4);
            gbRendererInfo.Size = new System.Drawing.Size(685, 545);
            gbRendererInfo.TabIndex = 7;
            gbRendererInfo.TabStop = false;
            gbRendererInfo.Text = "Renderer Info";
            // 
            // tbMask
            // 
            tbMask.Location = new System.Drawing.Point(231, 142);
            tbMask.Margin = new System.Windows.Forms.Padding(4);
            tbMask.Name = "tbMask";
            tbMask.Size = new System.Drawing.Size(420, 35);
            tbMask.TabIndex = 45;
            // 
            // lblMask
            // 
            lblMask.AutoSize = true;
            lblMask.Location = new System.Drawing.Point(28, 147);
            lblMask.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblMask.Name = "lblMask";
            lblMask.Size = new System.Drawing.Size(67, 30);
            lblMask.TabIndex = 44;
            lblMask.Text = "Mask:";
            // 
            // tbTitle
            // 
            tbTitle.Location = new System.Drawing.Point(231, 99);
            tbTitle.Margin = new System.Windows.Forms.Padding(4);
            tbTitle.Name = "tbTitle";
            tbTitle.Size = new System.Drawing.Size(420, 35);
            tbTitle.TabIndex = 43;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new System.Drawing.Point(28, 104);
            lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(57, 30);
            lblTitle.TabIndex = 42;
            lblTitle.Text = "Title:";
            // 
            // ucSqlSelector
            // 
            ucSqlSelector.Location = new System.Drawing.Point(10, 180);
            ucSqlSelector.Margin = new System.Windows.Forms.Padding(6);
            ucSqlSelector.Name = "ucSqlSelector";
            ucSqlSelector.Size = new System.Drawing.Size(644, 111);
            ucSqlSelector.TabIndex = 41;
            // 
            // tbContent
            // 
            tbContent.Location = new System.Drawing.Point(233, 307);
            tbContent.Margin = new System.Windows.Forms.Padding(4);
            tbContent.Multiline = true;
            tbContent.Name = "tbContent";
            tbContent.Size = new System.Drawing.Size(418, 213);
            tbContent.TabIndex = 40;
            // 
            // lblContent
            // 
            lblContent.AutoSize = true;
            lblContent.Location = new System.Drawing.Point(28, 317);
            lblContent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblContent.Name = "lblContent";
            lblContent.Size = new System.Drawing.Size(92, 30);
            lblContent.TabIndex = 39;
            lblContent.Text = "Content:";
            // 
            // ecbTextRendererType
            // 
            ecbTextRendererType.EnumType = null;
            ecbTextRendererType.FormattingEnabled = true;
            ecbTextRendererType.Location = new System.Drawing.Point(231, 51);
            ecbTextRendererType.Margin = new System.Windows.Forms.Padding(4);
            ecbTextRendererType.Name = "ecbTextRendererType";
            ecbTextRendererType.Size = new System.Drawing.Size(420, 38);
            ecbTextRendererType.TabIndex = 38;
            ecbTextRendererType.SelectedIndexChanged += ecbTextRendererType_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(28, 53);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(193, 30);
            label4.TabIndex = 37;
            label4.Text = "Text Renderer Type:";
            // 
            // epRendererInfo
            // 
            epRendererInfo.ContainerControl = this;
            // 
            // ucPdfTextRenderer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gbRendererInfo);
            Name = "ucPdfTextRenderer";
            Size = new System.Drawing.Size(696, 552);
            Load += ucPdfTextRenderer_Load;
            gbRendererInfo.ResumeLayout(false);
            gbRendererInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)epRendererInfo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox gbRendererInfo;
        private SQL.ucSqlSelector ucSqlSelector;
        private System.Windows.Forms.TextBox tbContent;
        private System.Windows.Forms.Label lblContent;
        private CustomControls.EnumComboBox ecbTextRendererType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ErrorProvider epRendererInfo;
        private System.Windows.Forms.TextBox tbMask;
        private System.Windows.Forms.Label lblMask;
    }
}
