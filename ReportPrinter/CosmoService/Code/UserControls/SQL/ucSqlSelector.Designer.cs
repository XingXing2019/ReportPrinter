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
            cbSqlTemplate = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            cbSql = new System.Windows.Forms.ComboBox();
            SuspendLayout();
            // 
            // cbSqlTemplate
            // 
            cbSqlTemplate.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            cbSqlTemplate.FormattingEnabled = true;
            cbSqlTemplate.Location = new System.Drawing.Point(222, 14);
            cbSqlTemplate.Margin = new System.Windows.Forms.Padding(4);
            cbSqlTemplate.Name = "cbSqlTemplate";
            cbSqlTemplate.Size = new System.Drawing.Size(420, 38);
            cbSqlTemplate.TabIndex = 0;
            cbSqlTemplate.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(18, 18);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(169, 30);
            label2.TabIndex = 5;
            label2.Text = "SQL Template Id:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(18, 69);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(79, 30);
            label1.TabIndex = 7;
            label1.Text = "SQL Id:";
            // 
            // cbSql
            // 
            cbSql.FormattingEnabled = true;
            cbSql.Location = new System.Drawing.Point(222, 64);
            cbSql.Margin = new System.Windows.Forms.Padding(4);
            cbSql.Name = "cbSql";
            cbSql.Size = new System.Drawing.Size(420, 38);
            cbSql.TabIndex = 6;
            // 
            // ucSqlSelector
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(cbSql);
            Controls.Add(label2);
            Controls.Add(cbSqlTemplate);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "ucSqlSelector";
            Size = new System.Drawing.Size(680, 117);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox cbSqlTemplate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSql;
    }
}
