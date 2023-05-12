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
            gbRendererInfo.Location = new System.Drawing.Point(3, 3);
            gbRendererInfo.Name = "gbRendererInfo";
            gbRendererInfo.Size = new System.Drawing.Size(457, 397);
            gbRendererInfo.TabIndex = 7;
            gbRendererInfo.TabStop = false;
            gbRendererInfo.Text = "Renderer Info";
            // 
            // tbMask
            // 
            tbMask.Location = new System.Drawing.Point(154, 95);
            tbMask.Name = "tbMask";
            tbMask.Size = new System.Drawing.Size(281, 27);
            tbMask.TabIndex = 45;
            // 
            // lblMask
            // 
            lblMask.AutoSize = true;
            lblMask.Location = new System.Drawing.Point(19, 98);
            lblMask.Name = "lblMask";
            lblMask.Size = new System.Drawing.Size(46, 20);
            lblMask.TabIndex = 44;
            lblMask.Text = "Mask:";
            // 
            // tbTitle
            // 
            tbTitle.Location = new System.Drawing.Point(154, 66);
            tbTitle.Name = "tbTitle";
            tbTitle.Size = new System.Drawing.Size(281, 27);
            tbTitle.TabIndex = 43;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new System.Drawing.Point(19, 69);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(41, 20);
            lblTitle.TabIndex = 42;
            lblTitle.Text = "Title:";
            // 
            // ucSqlSelector
            // 
            ucSqlSelector.Location = new System.Drawing.Point(6, 120);
            ucSqlSelector.Margin = new System.Windows.Forms.Padding(4);
            ucSqlSelector.Name = "ucSqlSelector";
            ucSqlSelector.Size = new System.Drawing.Size(449, 107);
            ucSqlSelector.TabIndex = 41;
            // 
            // tbContent
            // 
            tbContent.Location = new System.Drawing.Point(153, 234);
            tbContent.Multiline = true;
            tbContent.Name = "tbContent";
            tbContent.Size = new System.Drawing.Size(282, 143);
            tbContent.TabIndex = 40;
            // 
            // lblContent
            // 
            lblContent.AutoSize = true;
            lblContent.Location = new System.Drawing.Point(18, 240);
            lblContent.Name = "lblContent";
            lblContent.Size = new System.Drawing.Size(64, 20);
            lblContent.TabIndex = 39;
            lblContent.Text = "Content:";
            // 
            // ecbTextRendererType
            // 
            ecbTextRendererType.EnumType = null;
            ecbTextRendererType.FormattingEnabled = true;
            ecbTextRendererType.Location = new System.Drawing.Point(154, 34);
            ecbTextRendererType.Name = "ecbTextRendererType";
            ecbTextRendererType.Size = new System.Drawing.Size(281, 28);
            ecbTextRendererType.TabIndex = 38;
            ecbTextRendererType.SelectedIndexChanged += ecbTextRendererType_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(19, 37);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(74, 20);
            label4.TabIndex = 37;
            label4.Text = "Text Type:";
            // 
            // epRendererInfo
            // 
            epRendererInfo.ContainerControl = this;
            // 
            // ucPdfTextRenderer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gbRendererInfo);
            Margin = new System.Windows.Forms.Padding(2);
            Name = "ucPdfTextRenderer";
            Size = new System.Drawing.Size(464, 404);
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
