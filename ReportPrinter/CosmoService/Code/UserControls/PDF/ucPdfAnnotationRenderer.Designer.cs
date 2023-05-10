using CosmoService.Code.UserControls.SQL;

namespace CosmoService.Code.UserControls.PDF
{
    partial class ucPdfAnnotationRenderer
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
            ucSqlSelector = new ucSqlSelector();
            tbContent = new System.Windows.Forms.TextBox();
            lblContent = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            ecbIcon = new CustomControls.EnumComboBox();
            tbTitle = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            ecbAnnotationRendererType = new CustomControls.EnumComboBox();
            epRendererInfo = new System.Windows.Forms.ErrorProvider(components);
            gbRendererInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)epRendererInfo).BeginInit();
            SuspendLayout();
            // 
            // gbRendererInfo
            // 
            gbRendererInfo.Controls.Add(ucSqlSelector);
            gbRendererInfo.Controls.Add(tbContent);
            gbRendererInfo.Controls.Add(lblContent);
            gbRendererInfo.Controls.Add(label3);
            gbRendererInfo.Controls.Add(ecbIcon);
            gbRendererInfo.Controls.Add(tbTitle);
            gbRendererInfo.Controls.Add(label1);
            gbRendererInfo.Controls.Add(label2);
            gbRendererInfo.Controls.Add(ecbAnnotationRendererType);
            gbRendererInfo.Location = new System.Drawing.Point(4, 4);
            gbRendererInfo.Margin = new System.Windows.Forms.Padding(4);
            gbRendererInfo.Name = "gbRendererInfo";
            gbRendererInfo.Padding = new System.Windows.Forms.Padding(4);
            gbRendererInfo.Size = new System.Drawing.Size(685, 634);
            gbRendererInfo.TabIndex = 3;
            gbRendererInfo.TabStop = false;
            gbRendererInfo.Text = "Renderer Info";
            // 
            // ucSqlSelector
            // 
            ucSqlSelector.Location = new System.Drawing.Point(9, 200);
            ucSqlSelector.Margin = new System.Windows.Forms.Padding(6);
            ucSqlSelector.Name = "ucSqlSelector";
            ucSqlSelector.Size = new System.Drawing.Size(646, 118);
            ucSqlSelector.TabIndex = 12;
            // 
            // tbContent
            // 
            tbContent.Location = new System.Drawing.Point(232, 324);
            tbContent.Margin = new System.Windows.Forms.Padding(4);
            tbContent.Multiline = true;
            tbContent.Name = "tbContent";
            tbContent.Size = new System.Drawing.Size(416, 288);
            tbContent.TabIndex = 11;
            // 
            // lblContent
            // 
            lblContent.AutoSize = true;
            lblContent.Location = new System.Drawing.Point(24, 328);
            lblContent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblContent.Name = "lblContent";
            lblContent.Size = new System.Drawing.Size(92, 30);
            lblContent.TabIndex = 10;
            lblContent.Text = "Content:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(24, 153);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(58, 30);
            label3.TabIndex = 8;
            label3.Text = "Icon:";
            // 
            // ecbIcon
            // 
            ecbIcon.EnumType = null;
            ecbIcon.FormattingEnabled = true;
            ecbIcon.Location = new System.Drawing.Point(232, 148);
            ecbIcon.Margin = new System.Windows.Forms.Padding(4);
            ecbIcon.Name = "ecbIcon";
            ecbIcon.Size = new System.Drawing.Size(416, 38);
            ecbIcon.TabIndex = 9;
            // 
            // tbTitle
            // 
            tbTitle.Location = new System.Drawing.Point(232, 99);
            tbTitle.Margin = new System.Windows.Forms.Padding(4);
            tbTitle.Name = "tbTitle";
            tbTitle.Size = new System.Drawing.Size(416, 35);
            tbTitle.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(24, 104);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(57, 30);
            label1.TabIndex = 6;
            label1.Text = "Title:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(24, 52);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(61, 30);
            label2.TabIndex = 4;
            label2.Text = "Type:";
            // 
            // ecbAnnotationRendererType
            // 
            ecbAnnotationRendererType.EnumType = null;
            ecbAnnotationRendererType.FormattingEnabled = true;
            ecbAnnotationRendererType.Location = new System.Drawing.Point(232, 48);
            ecbAnnotationRendererType.Margin = new System.Windows.Forms.Padding(4);
            ecbAnnotationRendererType.Name = "ecbAnnotationRendererType";
            ecbAnnotationRendererType.Size = new System.Drawing.Size(416, 38);
            ecbAnnotationRendererType.TabIndex = 5;
            ecbAnnotationRendererType.SelectedIndexChanged += ecbAnnotationRendererType_SelectedIndexChanged;
            // 
            // epRendererInfo
            // 
            epRendererInfo.ContainerControl = this;
            // 
            // ucPdfAnnotationRenderer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gbRendererInfo);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "ucPdfAnnotationRenderer";
            Size = new System.Drawing.Size(697, 645);
            Load += ucPdfAnnotationRenderer_Load;
            gbRendererInfo.ResumeLayout(false);
            gbRendererInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)epRendererInfo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox gbRendererInfo;
        private System.Windows.Forms.Label label2;
        private CustomControls.EnumComboBox ecbAnnotationRendererType;
        private System.Windows.Forms.Label label3;
        private CustomControls.EnumComboBox ecbIcon;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbContent;
        private System.Windows.Forms.Label lblContent;
        private ucSqlSelector ucSqlSelector;
        private System.Windows.Forms.ErrorProvider epRendererInfo;
    }
}
