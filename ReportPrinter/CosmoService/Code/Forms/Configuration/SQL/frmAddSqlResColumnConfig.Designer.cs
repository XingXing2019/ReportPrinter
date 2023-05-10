namespace CosmoService.Code.Forms.Configuration.SQL
{
    partial class frmAddSqlResColumnConfig
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
            tbId = new System.Windows.Forms.TextBox();
            label15 = new System.Windows.Forms.Label();
            tbTitle = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            nudWidthRatio = new System.Windows.Forms.NumericUpDown();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            ecbPosition = new CustomControls.EnumComboBox();
            nudLeftRight = new System.Windows.Forms.NumericUpDown();
            panel1 = new System.Windows.Forms.Panel();
            rbRight = new System.Windows.Forms.RadioButton();
            rbLeft = new System.Windows.Forms.RadioButton();
            btnSave = new System.Windows.Forms.Button();
            epSqlResColumn = new System.Windows.Forms.ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)nudWidthRatio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLeftRight).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)epSqlResColumn).BeginInit();
            SuspendLayout();
            // 
            // tbId
            // 
            tbId.Location = new System.Drawing.Point(125, 15);
            tbId.Margin = new System.Windows.Forms.Padding(4);
            tbId.Name = "tbId";
            tbId.Size = new System.Drawing.Size(476, 35);
            tbId.TabIndex = 40;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(20, 18);
            label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(36, 30);
            label15.TabIndex = 39;
            label15.Text = "Id:";
            // 
            // tbTitle
            // 
            tbTitle.Location = new System.Drawing.Point(125, 58);
            tbTitle.Margin = new System.Windows.Forms.Padding(4);
            tbTitle.Name = "tbTitle";
            tbTitle.Size = new System.Drawing.Size(213, 35);
            tbTitle.TabIndex = 42;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(20, 61);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(57, 30);
            label1.TabIndex = 41;
            label1.Text = "Title:";
            // 
            // nudWidthRatio
            // 
            nudWidthRatio.DecimalPlaces = 1;
            nudWidthRatio.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudWidthRatio.Location = new System.Drawing.Point(487, 58);
            nudWidthRatio.Margin = new System.Windows.Forms.Padding(4);
            nudWidthRatio.Name = "nudWidthRatio";
            nudWidthRatio.Size = new System.Drawing.Size(114, 35);
            nudWidthRatio.TabIndex = 45;
            nudWidthRatio.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(346, 60);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(123, 30);
            label2.TabIndex = 46;
            label2.Text = "Width Ratio";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(20, 104);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(91, 30);
            label3.TabIndex = 43;
            label3.Text = "Position:";
            // 
            // ecbPosition
            // 
            ecbPosition.EnumType = null;
            ecbPosition.FormattingEnabled = true;
            ecbPosition.Location = new System.Drawing.Point(125, 101);
            ecbPosition.Margin = new System.Windows.Forms.Padding(4);
            ecbPosition.Name = "ecbPosition";
            ecbPosition.Size = new System.Drawing.Size(150, 38);
            ecbPosition.TabIndex = 44;
            // 
            // nudLeftRight
            // 
            nudLeftRight.DecimalPlaces = 1;
            nudLeftRight.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudLeftRight.Location = new System.Drawing.Point(487, 104);
            nudLeftRight.Margin = new System.Windows.Forms.Padding(4);
            nudLeftRight.Name = "nudLeftRight";
            nudLeftRight.Size = new System.Drawing.Size(114, 35);
            nudLeftRight.TabIndex = 48;
            // 
            // panel1
            // 
            panel1.Controls.Add(rbRight);
            panel1.Controls.Add(rbLeft);
            panel1.Location = new System.Drawing.Point(283, 100);
            panel1.Margin = new System.Windows.Forms.Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(196, 46);
            panel1.TabIndex = 47;
            // 
            // rbRight
            // 
            rbRight.AutoSize = true;
            rbRight.Location = new System.Drawing.Point(99, 4);
            rbRight.Margin = new System.Windows.Forms.Padding(4);
            rbRight.Name = "rbRight";
            rbRight.Size = new System.Drawing.Size(87, 34);
            rbRight.TabIndex = 17;
            rbRight.Text = "Right";
            rbRight.UseVisualStyleBackColor = true;
            // 
            // rbLeft
            // 
            rbLeft.AutoSize = true;
            rbLeft.Checked = true;
            rbLeft.Location = new System.Drawing.Point(8, 4);
            rbLeft.Margin = new System.Windows.Forms.Padding(4);
            rbLeft.Name = "rbLeft";
            rbLeft.Size = new System.Drawing.Size(73, 34);
            rbLeft.TabIndex = 16;
            rbLeft.TabStop = true;
            rbLeft.Text = "Left";
            rbLeft.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(477, 157);
            btnSave.Margin = new System.Windows.Forms.Padding(4);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(124, 40);
            btnSave.TabIndex = 49;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // epSqlResColumn
            // 
            epSqlResColumn.ContainerControl = this;
            // 
            // frmAddSqlResColumnConfig
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(633, 219);
            Controls.Add(btnSave);
            Controls.Add(nudLeftRight);
            Controls.Add(panel1);
            Controls.Add(nudWidthRatio);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(ecbPosition);
            Controls.Add(tbTitle);
            Controls.Add(label1);
            Controls.Add(tbId);
            Controls.Add(label15);
            Name = "frmAddSqlResColumnConfig";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Add SQL Result Column Config";
            ((System.ComponentModel.ISupportInitialize)nudWidthRatio).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLeftRight).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)epSqlResColumn).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox tbId;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudWidthRatio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private CustomControls.EnumComboBox ecbPosition;
        private System.Windows.Forms.NumericUpDown nudLeftRight;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbRight;
        private System.Windows.Forms.RadioButton rbLeft;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ErrorProvider epSqlResColumn;
    }
}