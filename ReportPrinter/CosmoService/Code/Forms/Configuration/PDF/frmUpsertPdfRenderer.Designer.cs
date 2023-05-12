using CosmoService.Code.UserControls.PDF;

namespace CosmoService.Code.Forms.Configuration.PDF
{
    partial class frmUpsertPdfRenderer
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
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox6 = new System.Windows.Forms.GroupBox();
            nudColumnSpan = new System.Windows.Forms.NumericUpDown();
            label20 = new System.Windows.Forms.Label();
            nudRowSpan = new System.Windows.Forms.NumericUpDown();
            label19 = new System.Windows.Forms.Label();
            nudColumn = new System.Windows.Forms.NumericUpDown();
            label18 = new System.Windows.Forms.Label();
            nudRow = new System.Windows.Forms.NumericUpDown();
            label17 = new System.Windows.Forms.Label();
            groupBox5 = new System.Windows.Forms.GroupBox();
            tbBackgroundColor = new System.Windows.Forms.TextBox();
            label16 = new System.Windows.Forms.Label();
            tbBrushColor = new System.Windows.Forms.TextBox();
            nudOpacity = new System.Windows.Forms.NumericUpDown();
            label13 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            groupBox4 = new System.Windows.Forms.GroupBox();
            tbFontFamily = new System.Windows.Forms.TextBox();
            nudFontSize = new System.Windows.Forms.NumericUpDown();
            label12 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            ecbFontStyle = new CustomControls.EnumComboBox();
            label11 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            groupBox3 = new System.Windows.Forms.GroupBox();
            nudTopBottom = new System.Windows.Forms.NumericUpDown();
            tbPadding = new System.Windows.Forms.MaskedTextBox();
            nudLeftRight = new System.Windows.Forms.NumericUpDown();
            label6 = new System.Windows.Forms.Label();
            tbMargin = new System.Windows.Forms.MaskedTextBox();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            panel2 = new System.Windows.Forms.Panel();
            rbBottom = new System.Windows.Forms.RadioButton();
            rbTop = new System.Windows.Forms.RadioButton();
            ecbHAlignment = new CustomControls.EnumComboBox();
            panel1 = new System.Windows.Forms.Panel();
            rbRight = new System.Windows.Forms.RadioButton();
            rbLeft = new System.Windows.Forms.RadioButton();
            ecbVAlignment = new CustomControls.EnumComboBox();
            ecbPosition = new CustomControls.EnumComboBox();
            label9 = new System.Windows.Forms.Label();
            tbId = new System.Windows.Forms.TextBox();
            ecbRendererType = new CustomControls.EnumComboBox();
            label1 = new System.Windows.Forms.Label();
            btnSave = new System.Windows.Forms.Button();
            epPdfConfig = new System.Windows.Forms.ErrorProvider(components);
            ucPdfAnnotationRenderer = new ucPdfAnnotationRenderer();
            ucPdfBarcodeRenderer = new ucPdfBarcodeRenderer();
            ucPdfImageRenderer = new ucPdfImageRenderer();
            ucPdfPageNumberRenderer = new ucPdfPageNumberRenderer();
            ucPdfReprintMarkRenderer = new ucPdfReprintMarkRenderer();
            ucPdfWaterMarkRenderer = new ucPdfWaterMarkRenderer();
            ucPdfTextRenderer = new ucPdfTextRenderer();
            ucPdfTableRenderer = new ucPdfTableRenderer();
            groupBox1.SuspendLayout();
            groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudColumnSpan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRowSpan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudColumn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRow).BeginInit();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudOpacity).BeginInit();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudFontSize).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudTopBottom).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLeftRight).BeginInit();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)epPdfConfig).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(groupBox6);
            groupBox1.Controls.Add(groupBox5);
            groupBox1.Controls.Add(groupBox4);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(groupBox3);
            groupBox1.Controls.Add(tbId);
            groupBox1.Controls.Add(ecbRendererType);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new System.Drawing.Point(12, 24);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(556, 601);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Basic Info";
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(nudColumnSpan);
            groupBox6.Controls.Add(label20);
            groupBox6.Controls.Add(nudRowSpan);
            groupBox6.Controls.Add(label19);
            groupBox6.Controls.Add(nudColumn);
            groupBox6.Controls.Add(label18);
            groupBox6.Controls.Add(nudRow);
            groupBox6.Controls.Add(label17);
            groupBox6.Location = new System.Drawing.Point(17, 468);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new System.Drawing.Size(522, 114);
            groupBox6.TabIndex = 32;
            groupBox6.TabStop = false;
            groupBox6.Text = "Location";
            // 
            // nudColumnSpan
            // 
            nudColumnSpan.Location = new System.Drawing.Point(379, 68);
            nudColumnSpan.Name = "nudColumnSpan";
            nudColumnSpan.Size = new System.Drawing.Size(128, 27);
            nudColumnSpan.TabIndex = 38;
            nudColumnSpan.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new System.Drawing.Point(274, 70);
            label20.Name = "label20";
            label20.Size = new System.Drawing.Size(100, 20);
            label20.TabIndex = 39;
            label20.Text = "Column Span:";
            // 
            // nudRowSpan
            // 
            nudRowSpan.Location = new System.Drawing.Point(115, 66);
            nudRowSpan.Name = "nudRowSpan";
            nudRowSpan.Size = new System.Drawing.Size(128, 27);
            nudRowSpan.TabIndex = 36;
            nudRowSpan.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new System.Drawing.Point(10, 73);
            label19.Name = "label19";
            label19.Size = new System.Drawing.Size(75, 20);
            label19.TabIndex = 37;
            label19.Text = "Row Span";
            // 
            // nudColumn
            // 
            nudColumn.Location = new System.Drawing.Point(379, 35);
            nudColumn.Name = "nudColumn";
            nudColumn.Size = new System.Drawing.Size(128, 27);
            nudColumn.TabIndex = 34;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new System.Drawing.Point(274, 37);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(63, 20);
            label18.TabIndex = 35;
            label18.Text = "Column:";
            // 
            // nudRow
            // 
            nudRow.Location = new System.Drawing.Point(115, 33);
            nudRow.Name = "nudRow";
            nudRow.Size = new System.Drawing.Size(128, 27);
            nudRow.TabIndex = 32;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new System.Drawing.Point(10, 40);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(41, 20);
            label17.TabIndex = 33;
            label17.Text = "Row:";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(tbBackgroundColor);
            groupBox5.Controls.Add(label16);
            groupBox5.Controls.Add(tbBrushColor);
            groupBox5.Controls.Add(nudOpacity);
            groupBox5.Controls.Add(label13);
            groupBox5.Controls.Add(label14);
            groupBox5.Controls.Add(label15);
            groupBox5.Location = new System.Drawing.Point(281, 311);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new System.Drawing.Size(258, 141);
            groupBox5.TabIndex = 30;
            groupBox5.TabStop = false;
            groupBox5.Text = "Color";
            // 
            // tbBackgroundColor
            // 
            tbBackgroundColor.Location = new System.Drawing.Point(115, 93);
            tbBackgroundColor.Name = "tbBackgroundColor";
            tbBackgroundColor.Size = new System.Drawing.Size(128, 27);
            tbBackgroundColor.TabIndex = 31;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new System.Drawing.Point(10, 92);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(88, 20);
            label16.TabIndex = 30;
            label16.Text = "Background";
            // 
            // tbBrushColor
            // 
            tbBrushColor.Location = new System.Drawing.Point(115, 59);
            tbBrushColor.Name = "tbBrushColor";
            tbBrushColor.Size = new System.Drawing.Size(128, 27);
            tbBrushColor.TabIndex = 29;
            // 
            // nudOpacity
            // 
            nudOpacity.DecimalPlaces = 1;
            nudOpacity.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudOpacity.Location = new System.Drawing.Point(115, 26);
            nudOpacity.Name = "nudOpacity";
            nudOpacity.Size = new System.Drawing.Size(128, 27);
            nudOpacity.TabIndex = 27;
            nudOpacity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(10, 30);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(63, 20);
            label13.TabIndex = 28;
            label13.Text = "Opacity:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(10, 112);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(48, 20);
            label14.TabIndex = 24;
            label14.Text = "Color:";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(10, 62);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(88, 20);
            label15.TabIndex = 26;
            label15.Text = "Brush Color:";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(tbFontFamily);
            groupBox4.Controls.Add(nudFontSize);
            groupBox4.Controls.Add(label12);
            groupBox4.Controls.Add(label10);
            groupBox4.Controls.Add(ecbFontStyle);
            groupBox4.Controls.Add(label11);
            groupBox4.Location = new System.Drawing.Point(17, 311);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(258, 141);
            groupBox4.TabIndex = 4;
            groupBox4.TabStop = false;
            groupBox4.Text = "Font";
            // 
            // tbFontFamily
            // 
            tbFontFamily.Location = new System.Drawing.Point(115, 59);
            tbFontFamily.Name = "tbFontFamily";
            tbFontFamily.Size = new System.Drawing.Size(128, 27);
            tbFontFamily.TabIndex = 29;
            // 
            // nudFontSize
            // 
            nudFontSize.DecimalPlaces = 1;
            nudFontSize.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudFontSize.Location = new System.Drawing.Point(115, 26);
            nudFontSize.Name = "nudFontSize";
            nudFontSize.Size = new System.Drawing.Size(128, 27);
            nudFontSize.TabIndex = 27;
            nudFontSize.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(10, 30);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(72, 20);
            label12.TabIndex = 28;
            label12.Text = "Font Size:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(10, 93);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(77, 20);
            label10.TabIndex = 24;
            label10.Text = "Font Style:";
            // 
            // ecbFontStyle
            // 
            ecbFontStyle.EnumType = null;
            ecbFontStyle.FormattingEnabled = true;
            ecbFontStyle.Location = new System.Drawing.Point(115, 92);
            ecbFontStyle.Name = "ecbFontStyle";
            ecbFontStyle.Size = new System.Drawing.Size(128, 28);
            ecbFontStyle.TabIndex = 25;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(10, 62);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(87, 20);
            label11.TabIndex = 26;
            label11.Text = "Font Family:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(262, 34);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(107, 20);
            label2.TabIndex = 2;
            label2.Text = "Renderer Type:";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(nudTopBottom);
            groupBox3.Controls.Add(tbPadding);
            groupBox3.Controls.Add(nudLeftRight);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(tbMargin);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(label3);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(panel2);
            groupBox3.Controls.Add(ecbHAlignment);
            groupBox3.Controls.Add(panel1);
            groupBox3.Controls.Add(ecbVAlignment);
            groupBox3.Controls.Add(ecbPosition);
            groupBox3.Controls.Add(label9);
            groupBox3.Location = new System.Drawing.Point(17, 81);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(522, 213);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Layout";
            // 
            // nudTopBottom
            // 
            nudTopBottom.DecimalPlaces = 1;
            nudTopBottom.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudTopBottom.Location = new System.Drawing.Point(431, 169);
            nudTopBottom.Name = "nudTopBottom";
            nudTopBottom.Size = new System.Drawing.Size(76, 27);
            nudTopBottom.TabIndex = 31;
            // 
            // tbPadding
            // 
            tbPadding.Location = new System.Drawing.Point(379, 28);
            tbPadding.Mask = "00 00 00 00";
            tbPadding.Name = "tbPadding";
            tbPadding.Size = new System.Drawing.Size(128, 27);
            tbPadding.TabIndex = 11;
            // 
            // nudLeftRight
            // 
            nudLeftRight.DecimalPlaces = 1;
            nudLeftRight.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudLeftRight.Location = new System.Drawing.Point(431, 136);
            nudLeftRight.Name = "nudLeftRight";
            nudLeftRight.Size = new System.Drawing.Size(76, 27);
            nudLeftRight.TabIndex = 30;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(292, 65);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(58, 20);
            label6.TabIndex = 7;
            label6.Text = "Vertical";
            // 
            // tbMargin
            // 
            tbMargin.Location = new System.Drawing.Point(108, 32);
            tbMargin.Mask = "00 00 00 00";
            tbMargin.Name = "tbMargin";
            tbMargin.Size = new System.Drawing.Size(128, 27);
            tbMargin.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(292, 85);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(81, 20);
            label5.TabIndex = 9;
            label5.Text = "Alignment:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(13, 85);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(81, 20);
            label4.TabIndex = 6;
            label4.Text = "Alignment:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(13, 35);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(59, 20);
            label7.TabIndex = 12;
            label7.Text = "Margin:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(13, 65);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(79, 20);
            label3.TabIndex = 4;
            label3.Text = "Horizontal";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(292, 35);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(66, 20);
            label8.TabIndex = 13;
            label8.Text = "Padding:";
            // 
            // panel2
            // 
            panel2.Controls.Add(rbBottom);
            panel2.Controls.Add(rbTop);
            panel2.Location = new System.Drawing.Point(272, 166);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(153, 31);
            panel2.TabIndex = 23;
            // 
            // rbBottom
            // 
            rbBottom.AutoSize = true;
            rbBottom.Location = new System.Drawing.Point(66, 3);
            rbBottom.Name = "rbBottom";
            rbBottom.Size = new System.Drawing.Size(80, 24);
            rbBottom.TabIndex = 17;
            rbBottom.Text = "Bottom";
            rbBottom.UseVisualStyleBackColor = true;
            // 
            // rbTop
            // 
            rbTop.AutoSize = true;
            rbTop.Checked = true;
            rbTop.Location = new System.Drawing.Point(5, 3);
            rbTop.Name = "rbTop";
            rbTop.Size = new System.Drawing.Size(55, 24);
            rbTop.TabIndex = 16;
            rbTop.TabStop = true;
            rbTop.Text = "Top";
            rbTop.UseVisualStyleBackColor = true;
            // 
            // ecbHAlignment
            // 
            ecbHAlignment.EnumType = null;
            ecbHAlignment.FormattingEnabled = true;
            ecbHAlignment.Location = new System.Drawing.Point(108, 65);
            ecbHAlignment.Name = "ecbHAlignment";
            ecbHAlignment.Size = new System.Drawing.Size(128, 28);
            ecbHAlignment.TabIndex = 5;
            // 
            // panel1
            // 
            panel1.Controls.Add(rbRight);
            panel1.Controls.Add(rbLeft);
            panel1.Location = new System.Drawing.Point(272, 133);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(153, 31);
            panel1.TabIndex = 22;
            // 
            // rbRight
            // 
            rbRight.AutoSize = true;
            rbRight.Location = new System.Drawing.Point(66, 3);
            rbRight.Name = "rbRight";
            rbRight.Size = new System.Drawing.Size(65, 24);
            rbRight.TabIndex = 17;
            rbRight.Text = "Right";
            rbRight.UseVisualStyleBackColor = true;
            // 
            // rbLeft
            // 
            rbLeft.AutoSize = true;
            rbLeft.Checked = true;
            rbLeft.Location = new System.Drawing.Point(5, 3);
            rbLeft.Name = "rbLeft";
            rbLeft.Size = new System.Drawing.Size(55, 24);
            rbLeft.TabIndex = 16;
            rbLeft.TabStop = true;
            rbLeft.Text = "Left";
            rbLeft.UseVisualStyleBackColor = true;
            // 
            // ecbVAlignment
            // 
            ecbVAlignment.EnumType = null;
            ecbVAlignment.FormattingEnabled = true;
            ecbVAlignment.Location = new System.Drawing.Point(379, 65);
            ecbVAlignment.Name = "ecbVAlignment";
            ecbVAlignment.Size = new System.Drawing.Size(128, 28);
            ecbVAlignment.TabIndex = 8;
            // 
            // ecbPosition
            // 
            ecbPosition.EnumType = null;
            ecbPosition.FormattingEnabled = true;
            ecbPosition.Location = new System.Drawing.Point(108, 135);
            ecbPosition.Name = "ecbPosition";
            ecbPosition.Size = new System.Drawing.Size(128, 28);
            ecbPosition.TabIndex = 15;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(13, 139);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(64, 20);
            label9.TabIndex = 14;
            label9.Text = "Position:";
            // 
            // tbId
            // 
            tbId.Location = new System.Drawing.Point(59, 31);
            tbId.Name = "tbId";
            tbId.Size = new System.Drawing.Size(143, 27);
            tbId.TabIndex = 1;
            // 
            // ecbRendererType
            // 
            ecbRendererType.EnumType = null;
            ecbRendererType.FormattingEnabled = true;
            ecbRendererType.Location = new System.Drawing.Point(375, 31);
            ecbRendererType.Name = "ecbRendererType";
            ecbRendererType.Size = new System.Drawing.Size(149, 28);
            ecbRendererType.TabIndex = 3;
            ecbRendererType.SelectedIndexChanged += ecbRendererType_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(17, 34);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(25, 20);
            label1.TabIndex = 0;
            label1.Text = "Id:";
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(938, 596);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(94, 29);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // epPdfConfig
            // 
            epPdfConfig.ContainerControl = this;
            // 
            // ucPdfAnnotationRenderer
            // 
            ucPdfAnnotationRenderer.Location = new System.Drawing.Point(573, 21);
            ucPdfAnnotationRenderer.Name = "ucPdfAnnotationRenderer";
            ucPdfAnnotationRenderer.Size = new System.Drawing.Size(465, 353);
            ucPdfAnnotationRenderer.TabIndex = 2;
            // 
            // ucPdfBarcodeRenderer
            // 
            ucPdfBarcodeRenderer.Location = new System.Drawing.Point(573, 21);
            ucPdfBarcodeRenderer.Margin = new System.Windows.Forms.Padding(1);
            ucPdfBarcodeRenderer.Name = "ucPdfBarcodeRenderer";
            ucPdfBarcodeRenderer.Size = new System.Drawing.Size(465, 227);
            ucPdfBarcodeRenderer.TabIndex = 3;
            // 
            // ucPdfImageRenderer
            // 
            ucPdfImageRenderer.Location = new System.Drawing.Point(573, 21);
            ucPdfImageRenderer.Margin = new System.Windows.Forms.Padding(1);
            ucPdfImageRenderer.Name = "ucPdfImageRenderer";
            ucPdfImageRenderer.Size = new System.Drawing.Size(466, 185);
            ucPdfImageRenderer.TabIndex = 33;
            // 
            // ucPdfPageNumberRenderer
            // 
            ucPdfPageNumberRenderer.Location = new System.Drawing.Point(573, 21);
            ucPdfPageNumberRenderer.Margin = new System.Windows.Forms.Padding(1);
            ucPdfPageNumberRenderer.Name = "ucPdfPageNumberRenderer";
            ucPdfPageNumberRenderer.Size = new System.Drawing.Size(466, 141);
            ucPdfPageNumberRenderer.TabIndex = 34;
            // 
            // ucPdfReprintMarkRenderer
            // 
            ucPdfReprintMarkRenderer.Location = new System.Drawing.Point(573, 21);
            ucPdfReprintMarkRenderer.Margin = new System.Windows.Forms.Padding(1);
            ucPdfReprintMarkRenderer.Name = "ucPdfReprintMarkRenderer";
            ucPdfReprintMarkRenderer.Size = new System.Drawing.Size(465, 151);
            ucPdfReprintMarkRenderer.TabIndex = 35;
            // 
            // ucPdfWaterMarkRenderer
            // 
            ucPdfWaterMarkRenderer.Location = new System.Drawing.Point(573, 21);
            ucPdfWaterMarkRenderer.Margin = new System.Windows.Forms.Padding(1);
            ucPdfWaterMarkRenderer.Name = "ucPdfWaterMarkRenderer";
            ucPdfWaterMarkRenderer.Size = new System.Drawing.Size(467, 400);
            ucPdfWaterMarkRenderer.TabIndex = 36;
            // 
            // ucPdfTextRenderer
            // 
            ucPdfTextRenderer.Location = new System.Drawing.Point(573, 21);
            ucPdfTextRenderer.Margin = new System.Windows.Forms.Padding(1);
            ucPdfTextRenderer.Name = "ucPdfTextRenderer";
            ucPdfTextRenderer.Size = new System.Drawing.Size(466, 227);
            ucPdfTextRenderer.TabIndex = 37;
            // 
            // ucPdfTableRenderer
            // 
            ucPdfTableRenderer.Location = new System.Drawing.Point(572, 21);
            ucPdfTableRenderer.Margin = new System.Windows.Forms.Padding(1);
            ucPdfTableRenderer.Name = "ucPdfTableRenderer";
            ucPdfTableRenderer.Size = new System.Drawing.Size(467, 559);
            ucPdfTableRenderer.TabIndex = 38;
            // 
            // frmUpsertPdfRenderer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1049, 644);
            Controls.Add(ucPdfTableRenderer);
            Controls.Add(ucPdfTextRenderer);
            Controls.Add(ucPdfWaterMarkRenderer);
            Controls.Add(ucPdfReprintMarkRenderer);
            Controls.Add(ucPdfPageNumberRenderer);
            Controls.Add(ucPdfImageRenderer);
            Controls.Add(ucPdfBarcodeRenderer);
            Controls.Add(ucPdfAnnotationRenderer);
            Controls.Add(btnSave);
            Controls.Add(groupBox1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            Name = "frmUpsertPdfRenderer";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudColumnSpan).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRowSpan).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudColumn).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRow).EndInit();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudOpacity).EndInit();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudFontSize).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudTopBottom).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLeftRight).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)epPdfConfig).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private CustomControls.EnumComboBox ecbRendererType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private CustomControls.EnumComboBox ecbHAlignment;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private CustomControls.EnumComboBox ecbVAlignment;
        private System.Windows.Forms.MaskedTextBox tbMargin;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox tbPadding;
        private CustomControls.EnumComboBox ecbPosition;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rbRight;
        private System.Windows.Forms.RadioButton rbLeft;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbBottom;
        private System.Windows.Forms.RadioButton rbTop;
        private CustomControls.EnumComboBox ecbFontStyle;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudFontSize;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbFontFamily;
        private System.Windows.Forms.NumericUpDown nudTopBottom;
        private System.Windows.Forms.NumericUpDown nudLeftRight;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox tbBackgroundColor;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbBrushColor;
        private System.Windows.Forms.NumericUpDown nudOpacity;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NumericUpDown nudColumnSpan;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.NumericUpDown nudRowSpan;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown nudColumn;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown nudRow;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ErrorProvider epPdfConfig;
        private ucPdfAnnotationRenderer ucPdfAnnotationRenderer;
        private ucPdfBarcodeRenderer ucPdfBarcodeRenderer;
        private ucPdfImageRenderer ucPdfImageRenderer;
        private ucPdfPageNumberRenderer ucPdfPageNumberRenderer;
        private ucPdfReprintMarkRenderer ucPdfReprintMarkRenderer;
        private ucPdfWaterMarkRenderer ucPdfWaterMarkRenderer;
        private ucPdfTextRenderer ucPdfTextRenderer;
        private ucPdfTableRenderer ucPdfTableRenderer;
    }
}