namespace CosmoService.Code.UserControls.PDF
{
    partial class ucPdfTableRenderer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            gbRendererInfo = new System.Windows.Forms.GroupBox();
            btnDelete = new System.Windows.Forms.Button();
            btnAdd = new System.Windows.Forms.Button();
            lblSqlresColumns = new System.Windows.Forms.Label();
            dgvSqlResultColumns = new System.Windows.Forms.DataGridView();
            isSelectedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            sqlResColumnDataBindingSource = new System.Windows.Forms.BindingSource(components);
            label6 = new System.Windows.Forms.Label();
            cbSubTable = new System.Windows.Forms.ComboBox();
            nudTitleOpacity = new System.Windows.Forms.NumericUpDown();
            label5 = new System.Windows.Forms.Label();
            tbTitleColor = new System.Windows.Forms.TextBox();
            label15 = new System.Windows.Forms.Label();
            nudSpace = new System.Windows.Forms.NumericUpDown();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            cbHideTitle = new System.Windows.Forms.CheckBox();
            nudLineSpace = new System.Windows.Forms.NumericUpDown();
            label1 = new System.Windows.Forms.Label();
            nudBoardThickness = new System.Windows.Forms.NumericUpDown();
            label12 = new System.Windows.Forms.Label();
            ucSqlSelector = new SQL.ucSqlSelector();
            label2 = new System.Windows.Forms.Label();
            ecbTitleHAlignment = new CustomControls.EnumComboBox();
            epRendererInfo = new System.Windows.Forms.ErrorProvider(components);
            gbRendererInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSqlResultColumns).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sqlResColumnDataBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudTitleOpacity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSpace).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLineSpace).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudBoardThickness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)epRendererInfo).BeginInit();
            SuspendLayout();
            // 
            // gbRendererInfo
            // 
            gbRendererInfo.Controls.Add(btnDelete);
            gbRendererInfo.Controls.Add(btnAdd);
            gbRendererInfo.Controls.Add(lblSqlresColumns);
            gbRendererInfo.Controls.Add(dgvSqlResultColumns);
            gbRendererInfo.Controls.Add(label6);
            gbRendererInfo.Controls.Add(cbSubTable);
            gbRendererInfo.Controls.Add(nudTitleOpacity);
            gbRendererInfo.Controls.Add(label5);
            gbRendererInfo.Controls.Add(tbTitleColor);
            gbRendererInfo.Controls.Add(label15);
            gbRendererInfo.Controls.Add(nudSpace);
            gbRendererInfo.Controls.Add(label4);
            gbRendererInfo.Controls.Add(label3);
            gbRendererInfo.Controls.Add(cbHideTitle);
            gbRendererInfo.Controls.Add(nudLineSpace);
            gbRendererInfo.Controls.Add(label1);
            gbRendererInfo.Controls.Add(nudBoardThickness);
            gbRendererInfo.Controls.Add(label12);
            gbRendererInfo.Controls.Add(ucSqlSelector);
            gbRendererInfo.Controls.Add(label2);
            gbRendererInfo.Controls.Add(ecbTitleHAlignment);
            gbRendererInfo.Location = new System.Drawing.Point(4, 4);
            gbRendererInfo.Margin = new System.Windows.Forms.Padding(4);
            gbRendererInfo.Name = "gbRendererInfo";
            gbRendererInfo.Padding = new System.Windows.Forms.Padding(4);
            gbRendererInfo.Size = new System.Drawing.Size(685, 778);
            gbRendererInfo.TabIndex = 5;
            gbRendererInfo.TabStop = false;
            gbRendererInfo.Text = "Renderer Info";
            // 
            // btnDelete
            // 
            btnDelete.Location = new System.Drawing.Point(531, 378);
            btnDelete.Margin = new System.Windows.Forms.Padding(4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(124, 40);
            btnDelete.TabIndex = 46;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new System.Drawing.Point(397, 378);
            btnAdd.Margin = new System.Windows.Forms.Padding(4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new System.Drawing.Size(124, 40);
            btnAdd.TabIndex = 45;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // lblSqlresColumns
            // 
            lblSqlresColumns.AutoSize = true;
            lblSqlresColumns.Location = new System.Drawing.Point(27, 378);
            lblSqlresColumns.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblSqlresColumns.Name = "lblSqlresColumns";
            lblSqlresColumns.Size = new System.Drawing.Size(204, 30);
            lblSqlresColumns.TabIndex = 44;
            lblSqlresColumns.Text = "SQL Result Columns:";
            // 
            // dgvSqlResultColumns
            // 
            dgvSqlResultColumns.AllowUserToAddRows = false;
            dgvSqlResultColumns.AllowUserToDeleteRows = false;
            dgvSqlResultColumns.AutoGenerateColumns = false;
            dgvSqlResultColumns.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            dgvSqlResultColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSqlResultColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { isSelectedDataGridViewCheckBoxColumn, idDataGridViewTextBoxColumn, titleDataGridViewTextBoxColumn });
            dgvSqlResultColumns.DataSource = sqlResColumnDataBindingSource;
            dgvSqlResultColumns.Location = new System.Drawing.Point(29, 428);
            dgvSqlResultColumns.Name = "dgvSqlResultColumns";
            dgvSqlResultColumns.RowHeadersVisible = false;
            dgvSqlResultColumns.RowHeadersWidth = 72;
            dgvSqlResultColumns.RowTemplate.Height = 37;
            dgvSqlResultColumns.Size = new System.Drawing.Size(624, 332);
            dgvSqlResultColumns.TabIndex = 43;
            // 
            // isSelectedDataGridViewCheckBoxColumn
            // 
            isSelectedDataGridViewCheckBoxColumn.DataPropertyName = "IsSelected";
            isSelectedDataGridViewCheckBoxColumn.HeaderText = "";
            isSelectedDataGridViewCheckBoxColumn.MinimumWidth = 9;
            isSelectedDataGridViewCheckBoxColumn.Name = "isSelectedDataGridViewCheckBoxColumn";
            isSelectedDataGridViewCheckBoxColumn.Width = 40;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(10, 2, 10, 2);
            idDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.MinimumWidth = 9;
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            idDataGridViewTextBoxColumn.Width = 250;
            // 
            // titleDataGridViewTextBoxColumn
            // 
            titleDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(10, 2, 10, 2);
            titleDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            titleDataGridViewTextBoxColumn.HeaderText = "Title";
            titleDataGridViewTextBoxColumn.MinimumWidth = 9;
            titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            // 
            // sqlResColumnDataBindingSource
            // 
            sqlResColumnDataBindingSource.DataSource = typeof(ReportPrinterLibrary.Code.Winform.Configuration.SQL.SqlResColumnData);
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(29, 329);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(107, 30);
            label6.TabIndex = 42;
            label6.Text = "Sub Table:";
            // 
            // cbSubTable
            // 
            cbSubTable.FormattingEnabled = true;
            cbSubTable.Location = new System.Drawing.Point(233, 324);
            cbSubTable.Margin = new System.Windows.Forms.Padding(4);
            cbSubTable.Name = "cbSubTable";
            cbSubTable.Size = new System.Drawing.Size(420, 38);
            cbSubTable.TabIndex = 41;
            // 
            // nudTitleOpacity
            // 
            nudTitleOpacity.DecimalPlaces = 1;
            nudTitleOpacity.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudTitleOpacity.Location = new System.Drawing.Point(519, 175);
            nudTitleOpacity.Margin = new System.Windows.Forms.Padding(4);
            nudTitleOpacity.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            nudTitleOpacity.Name = "nudTitleOpacity";
            nudTitleOpacity.Size = new System.Drawing.Size(134, 35);
            nudTitleOpacity.TabIndex = 39;
            nudTitleOpacity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(387, 175);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(134, 30);
            label5.TabIndex = 40;
            label5.Text = "Title Opacity:";
            // 
            // tbTitleColor
            // 
            tbTitleColor.Location = new System.Drawing.Point(233, 175);
            tbTitleColor.Margin = new System.Windows.Forms.Padding(4);
            tbTitleColor.Name = "tbTitleColor";
            tbTitleColor.Size = new System.Drawing.Size(134, 35);
            tbTitleColor.TabIndex = 38;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(27, 180);
            label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(112, 30);
            label15.TabIndex = 37;
            label15.Text = "Title Color:";
            // 
            // nudSpace
            // 
            nudSpace.DecimalPlaces = 1;
            nudSpace.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudSpace.Location = new System.Drawing.Point(233, 86);
            nudSpace.Margin = new System.Windows.Forms.Padding(4);
            nudSpace.Name = "nudSpace";
            nudSpace.Size = new System.Drawing.Size(134, 35);
            nudSpace.TabIndex = 35;
            nudSpace.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(27, 88);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(73, 30);
            label4.TabIndex = 36;
            label4.Text = "Space:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(387, 88);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(106, 30);
            label3.TabIndex = 34;
            label3.Text = "Hide Title:";
            // 
            // cbHideTitle
            // 
            cbHideTitle.AutoSize = true;
            cbHideTitle.Location = new System.Drawing.Point(631, 94);
            cbHideTitle.Name = "cbHideTitle";
            cbHideTitle.Size = new System.Drawing.Size(22, 21);
            cbHideTitle.TabIndex = 33;
            cbHideTitle.UseVisualStyleBackColor = true;
            // 
            // nudLineSpace
            // 
            nudLineSpace.DecimalPlaces = 1;
            nudLineSpace.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudLineSpace.Location = new System.Drawing.Point(519, 47);
            nudLineSpace.Margin = new System.Windows.Forms.Padding(4);
            nudLineSpace.Name = "nudLineSpace";
            nudLineSpace.Size = new System.Drawing.Size(134, 35);
            nudLineSpace.TabIndex = 31;
            nudLineSpace.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(387, 47);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(117, 30);
            label1.TabIndex = 32;
            label1.Text = "Line Space:";
            // 
            // nudBoardThickness
            // 
            nudBoardThickness.DecimalPlaces = 1;
            nudBoardThickness.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudBoardThickness.Location = new System.Drawing.Point(233, 43);
            nudBoardThickness.Margin = new System.Windows.Forms.Padding(4);
            nudBoardThickness.Name = "nudBoardThickness";
            nudBoardThickness.Size = new System.Drawing.Size(134, 35);
            nudBoardThickness.TabIndex = 29;
            nudBoardThickness.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(27, 45);
            label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(167, 30);
            label12.TabIndex = 30;
            label12.Text = "Board Thickness:";
            // 
            // ucSqlSelector
            // 
            ucSqlSelector.Location = new System.Drawing.Point(10, 211);
            ucSqlSelector.Margin = new System.Windows.Forms.Padding(6);
            ucSqlSelector.Name = "ucSqlSelector";
            ucSqlSelector.Size = new System.Drawing.Size(648, 118);
            ucSqlSelector.TabIndex = 12;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(27, 132);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(159, 30);
            label2.TabIndex = 4;
            label2.Text = "Title Alignment:";
            // 
            // ecbTitleHAlignment
            // 
            ecbTitleHAlignment.EnumType = null;
            ecbTitleHAlignment.FormattingEnabled = true;
            ecbTitleHAlignment.Location = new System.Drawing.Point(233, 129);
            ecbTitleHAlignment.Margin = new System.Windows.Forms.Padding(4);
            ecbTitleHAlignment.Name = "ecbTitleHAlignment";
            ecbTitleHAlignment.Size = new System.Drawing.Size(420, 38);
            ecbTitleHAlignment.TabIndex = 5;
            // 
            // epRendererInfo
            // 
            epRendererInfo.ContainerControl = this;
            // 
            // ucPdfTableRenderer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gbRendererInfo);
            Name = "ucPdfTableRenderer";
            Size = new System.Drawing.Size(698, 791);
            Load += ucPdfTableRenderer_Load;
            gbRendererInfo.ResumeLayout(false);
            gbRendererInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSqlResultColumns).EndInit();
            ((System.ComponentModel.ISupportInitialize)sqlResColumnDataBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudTitleOpacity).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSpace).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLineSpace).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudBoardThickness).EndInit();
            ((System.ComponentModel.ISupportInitialize)epRendererInfo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox gbRendererInfo;
        private SQL.ucSqlSelector ucSqlSelector;
        private System.Windows.Forms.Label label2;
        private CustomControls.EnumComboBox ecbTitleHAlignment;
        private System.Windows.Forms.NumericUpDown nudBoardThickness;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown nudLineSpace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider epRendererInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbHideTitle;
        private System.Windows.Forms.NumericUpDown nudSpace;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudTitleOpacity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbTitleColor;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbSubTable;
        private System.Windows.Forms.Label lblSqlresColumns;
        private System.Windows.Forms.DataGridView dgvSqlResultColumns;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.BindingSource sqlResColumnDataBindingSource;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isSelectedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
    }
}
