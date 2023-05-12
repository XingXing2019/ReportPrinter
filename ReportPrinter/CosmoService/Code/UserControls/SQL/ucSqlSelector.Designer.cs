namespace CosmoService.Code.UserControls.SQL
{
    partial class ucSqlSelector
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
            cbSqlTemplate = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            cbSql = new System.Windows.Forms.ComboBox();
            tbSqlRes = new System.Windows.Forms.TextBox();
            lblSqlRes = new System.Windows.Forms.Label();
            epSqlSelector = new System.Windows.Forms.ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)epSqlSelector).BeginInit();
            SuspendLayout();
            // 
            // cbSqlTemplate
            // 
            cbSqlTemplate.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            cbSqlTemplate.FormattingEnabled = true;
            cbSqlTemplate.Location = new System.Drawing.Point(148, 9);
            cbSqlTemplate.Name = "cbSqlTemplate";
            cbSqlTemplate.Size = new System.Drawing.Size(281, 28);
            cbSqlTemplate.TabIndex = 0;
            cbSqlTemplate.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 12);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(121, 20);
            label2.TabIndex = 5;
            label2.Text = "SQL Template Id:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 46);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(55, 20);
            label1.TabIndex = 7;
            label1.Text = "SQL Id:";
            // 
            // cbSql
            // 
            cbSql.FormattingEnabled = true;
            cbSql.Location = new System.Drawing.Point(148, 43);
            cbSql.Name = "cbSql";
            cbSql.Size = new System.Drawing.Size(281, 28);
            cbSql.TabIndex = 6;
            // 
            // tbSqlRes
            // 
            tbSqlRes.Location = new System.Drawing.Point(148, 77);
            tbSqlRes.Name = "tbSqlRes";
            tbSqlRes.Size = new System.Drawing.Size(281, 27);
            tbSqlRes.TabIndex = 9;
            // 
            // lblSqlRes
            // 
            lblSqlRes.AutoSize = true;
            lblSqlRes.Location = new System.Drawing.Point(12, 80);
            lblSqlRes.Name = "lblSqlRes";
            lblSqlRes.Size = new System.Drawing.Size(82, 20);
            lblSqlRes.TabIndex = 8;
            lblSqlRes.Text = "SQL Result:";
            // 
            // epSqlSelector
            // 
            epSqlSelector.ContainerControl = this;
            // 
            // ucSqlSelector
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tbSqlRes);
            Controls.Add(lblSqlRes);
            Controls.Add(label1);
            Controls.Add(cbSql);
            Controls.Add(label2);
            Controls.Add(cbSqlTemplate);
            Name = "ucSqlSelector";
            Size = new System.Drawing.Size(453, 113);
            ((System.ComponentModel.ISupportInitialize)epSqlSelector).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox cbSqlTemplate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSql;
        private System.Windows.Forms.TextBox tbSqlRes;
        private System.Windows.Forms.Label lblSqlRes;
        private System.Windows.Forms.ErrorProvider epSqlSelector;
    }
}
