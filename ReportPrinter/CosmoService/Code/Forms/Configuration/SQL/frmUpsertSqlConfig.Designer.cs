namespace CosmoService.Code.Forms.Configuration.SQL
{
    partial class frmUpsertSqlConfig
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
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            groupBox2 = new System.Windows.Forms.GroupBox();
            lblQuery = new System.Windows.Forms.Label();
            txtQuery = new System.Windows.Forms.TextBox();
            lblDatabaseId = new System.Windows.Forms.Label();
            txtDatabaseId = new System.Windows.Forms.TextBox();
            lblId = new System.Windows.Forms.Label();
            txtId = new System.Windows.Forms.TextBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            btnDelete = new System.Windows.Forms.Button();
            btnAdd = new System.Windows.Forms.Button();
            dgvSqlVariables = new System.Windows.Forms.DataGridView();
            isSelectedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            sqlVariableConfigDataBindingSource = new System.Windows.Forms.BindingSource(components);
            btnPreview = new System.Windows.Forms.Button();
            btnSave = new System.Windows.Forms.Button();
            epAddSqlConfig = new System.Windows.Forms.ErrorProvider(components);
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSqlVariables).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sqlVariableConfigDataBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)epAddSqlConfig).BeginInit();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lblQuery);
            groupBox2.Controls.Add(txtQuery);
            groupBox2.Controls.Add(lblDatabaseId);
            groupBox2.Controls.Add(txtDatabaseId);
            groupBox2.Controls.Add(lblId);
            groupBox2.Controls.Add(txtId);
            groupBox2.Location = new System.Drawing.Point(17, 24);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(563, 387);
            groupBox2.TabIndex = 8;
            groupBox2.TabStop = false;
            groupBox2.Text = "Parameters";
            // 
            // lblQuery
            // 
            lblQuery.AutoSize = true;
            lblQuery.Location = new System.Drawing.Point(8, 75);
            lblQuery.Name = "lblQuery";
            lblQuery.Size = new System.Drawing.Size(51, 20);
            lblQuery.TabIndex = 13;
            lblQuery.Text = "Query:";
            // 
            // txtQuery
            // 
            txtQuery.Location = new System.Drawing.Point(85, 72);
            txtQuery.Multiline = true;
            txtQuery.Name = "txtQuery";
            txtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtQuery.Size = new System.Drawing.Size(459, 304);
            txtQuery.TabIndex = 12;
            // 
            // lblDatabaseId
            // 
            lblDatabaseId.AutoSize = true;
            lblDatabaseId.Location = new System.Drawing.Point(264, 37);
            lblDatabaseId.Name = "lblDatabaseId";
            lblDatabaseId.Size = new System.Drawing.Size(92, 20);
            lblDatabaseId.TabIndex = 11;
            lblDatabaseId.Text = "Database Id:";
            // 
            // txtDatabaseId
            // 
            txtDatabaseId.Location = new System.Drawing.Point(378, 34);
            txtDatabaseId.Name = "txtDatabaseId";
            txtDatabaseId.Size = new System.Drawing.Size(166, 27);
            txtDatabaseId.TabIndex = 10;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Location = new System.Drawing.Point(8, 37);
            lblId.Name = "lblId";
            lblId.Size = new System.Drawing.Size(25, 20);
            lblId.TabIndex = 9;
            lblId.Text = "Id:";
            // 
            // txtId
            // 
            txtId.Location = new System.Drawing.Point(85, 34);
            txtId.Name = "txtId";
            txtId.Size = new System.Drawing.Size(166, 27);
            txtId.TabIndex = 8;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnDelete);
            groupBox1.Controls.Add(btnAdd);
            groupBox1.Controls.Add(dgvSqlVariables);
            groupBox1.Location = new System.Drawing.Point(586, 24);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(223, 333);
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
            // dgvSqlVariables
            // 
            dgvSqlVariables.AllowUserToAddRows = false;
            dgvSqlVariables.AllowUserToDeleteRows = false;
            dgvSqlVariables.AllowUserToResizeColumns = false;
            dgvSqlVariables.AllowUserToResizeRows = false;
            dgvSqlVariables.AutoGenerateColumns = false;
            dgvSqlVariables.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dgvSqlVariables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSqlVariables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { isSelectedDataGridViewCheckBoxColumn, nameDataGridViewTextBoxColumn });
            dgvSqlVariables.DataSource = sqlVariableConfigDataBindingSource;
            dgvSqlVariables.Location = new System.Drawing.Point(16, 72);
            dgvSqlVariables.Name = "dgvSqlVariables";
            dgvSqlVariables.RowHeadersVisible = false;
            dgvSqlVariables.RowHeadersWidth = 51;
            dgvSqlVariables.RowTemplate.Height = 29;
            dgvSqlVariables.Size = new System.Drawing.Size(196, 245);
            dgvSqlVariables.TabIndex = 0;
            // 
            // isSelectedDataGridViewCheckBoxColumn
            // 
            isSelectedDataGridViewCheckBoxColumn.DataPropertyName = "IsSelected";
            isSelectedDataGridViewCheckBoxColumn.HeaderText = " ";
            isSelectedDataGridViewCheckBoxColumn.MinimumWidth = 9;
            isSelectedDataGridViewCheckBoxColumn.Name = "isSelectedDataGridViewCheckBoxColumn";
            isSelectedDataGridViewCheckBoxColumn.Width = 40;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(10, 2, 10, 2);
            nameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            nameDataGridViewTextBoxColumn.HeaderText = "Name";
            nameDataGridViewTextBoxColumn.MinimumWidth = 9;
            nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // sqlVariableConfigDataBindingSource
            // 
            sqlVariableConfigDataBindingSource.DataSource = typeof(ReportPrinterLibrary.Code.Winform.Configuration.SqlVariableConfigData);
            // 
            // btnPreview
            // 
            btnPreview.Location = new System.Drawing.Point(634, 372);
            btnPreview.Name = "btnPreview";
            btnPreview.Size = new System.Drawing.Size(83, 27);
            btnPreview.TabIndex = 10;
            btnPreview.Text = "Preview";
            btnPreview.UseVisualStyleBackColor = true;
            btnPreview.Click += btnPreview_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(726, 372);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(83, 27);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // epAddSqlConfig
            // 
            epAddSqlConfig.ContainerControl = this;
            // 
            // frmUpsertSqlConfig
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(821, 419);
            Controls.Add(btnSave);
            Controls.Add(btnPreview);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            Name = "frmUpsertSqlConfig";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSqlVariables).EndInit();
            ((System.ComponentModel.ISupportInitialize)sqlVariableConfigDataBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)epAddSqlConfig).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblQuery;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.Label lblDatabaseId;
        private System.Windows.Forms.TextBox txtDatabaseId;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvSqlVariables;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ErrorProvider epAddSqlConfig;
        private System.Windows.Forms.BindingSource sqlVariableConfigDataBindingSource;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isSelectedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
    }
}