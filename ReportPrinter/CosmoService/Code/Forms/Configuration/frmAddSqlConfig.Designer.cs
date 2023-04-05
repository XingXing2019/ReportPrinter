namespace CosmoService.Code.Forms.Configuration
{
    partial class frmAddSqlConfig
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox2 = new System.Windows.Forms.GroupBox();
            label3 = new System.Windows.Forms.Label();
            textBox3 = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            textBox2 = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            btnDelete = new System.Windows.Forms.Button();
            btnAdd = new System.Windows.Forms.Button();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            btnPreview = new System.Windows.Forms.Button();
            btnSave = new System.Windows.Forms.Button();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(textBox3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(textBox2);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(textBox1);
            groupBox2.Location = new System.Drawing.Point(17, 24);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(533, 388);
            groupBox2.TabIndex = 8;
            groupBox2.TabStop = false;
            groupBox2.Text = "Parameters";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(8, 75);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(51, 20);
            label3.TabIndex = 13;
            label3.Text = "Query:";
            // 
            // textBox3
            // 
            textBox3.Location = new System.Drawing.Point(74, 72);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            textBox3.Size = new System.Drawing.Size(445, 298);
            textBox3.TabIndex = 12;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(253, 37);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(92, 20);
            label2.TabIndex = 11;
            label2.Text = "Database Id:";
            // 
            // textBox2
            // 
            textBox2.Location = new System.Drawing.Point(359, 34);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(160, 27);
            textBox2.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(8, 37);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(25, 20);
            label1.TabIndex = 9;
            label1.Text = "Id:";
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(74, 34);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(160, 27);
            textBox1.TabIndex = 8;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnDelete);
            groupBox1.Controls.Add(btnAdd);
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Location = new System.Drawing.Point(556, 24);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(251, 388);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "Variables";
            // 
            // btnDelete
            // 
            btnDelete.Location = new System.Drawing.Point(105, 34);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(83, 27);
            btnDelete.TabIndex = 13;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new System.Drawing.Point(16, 34);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new System.Drawing.Size(83, 27);
            btnAdd.TabIndex = 12;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new System.Drawing.Point(16, 72);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new System.Drawing.Size(221, 298);
            dataGridView1.TabIndex = 0;
            // 
            // btnPreview
            // 
            btnPreview.Location = new System.Drawing.Point(17, 423);
            btnPreview.Name = "btnPreview";
            btnPreview.Size = new System.Drawing.Size(83, 27);
            btnPreview.TabIndex = 10;
            btnPreview.Text = "Preview";
            btnPreview.UseVisualStyleBackColor = true;
            btnPreview.Click += btnPreview_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(106, 423);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(83, 27);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // frmAddSqlConfig
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(823, 467);
            Controls.Add(btnSave);
            Controls.Add(btnPreview);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Name = "frmAddSqlConfig";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "frmAddSqlConfig";
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
    }
}