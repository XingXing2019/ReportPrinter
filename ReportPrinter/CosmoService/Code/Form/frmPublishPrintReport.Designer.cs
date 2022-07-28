namespace CosmoService.Code.Form
{
    partial class frmPublishPrintReport
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSend = new System.Windows.Forms.Button();
            this.rdbPDF = new System.Windows.Forms.RadioButton();
            this.rdbLabel = new System.Windows.Forms.RadioButton();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(463, 54);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(131, 40);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // rdbPDF
            // 
            this.rdbPDF.AutoSize = true;
            this.rdbPDF.Checked = false;
            this.rdbPDF.Location = new System.Drawing.Point(76, 57);
            this.rdbPDF.Name = "rdbPDF";
            this.rdbPDF.Size = new System.Drawing.Size(75, 34);
            this.rdbPDF.TabIndex = 1;
            this.rdbPDF.TabStop = true;
            this.rdbPDF.Text = "PDF";
            this.rdbPDF.UseVisualStyleBackColor = true;
            // 
            // rdbLabel
            // 
            this.rdbLabel.AutoSize = true;
            this.rdbLabel.Checked = true;
            this.rdbLabel.Location = new System.Drawing.Point(271, 57);
            this.rdbLabel.Name = "rdbLabel";
            this.rdbLabel.Size = new System.Drawing.Size(87, 34);
            this.rdbLabel.TabIndex = 2;
            this.rdbLabel.Text = "Label";
            this.rdbLabel.UseVisualStyleBackColor = true;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(62, 118);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(910, 489);
            this.txtMessage.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 638);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.rdbLabel);
            this.Controls.Add(this.rdbPDF);
            this.Controls.Add(this.btnSend);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.RadioButton rdbPDF;
        private System.Windows.Forms.RadioButton rdbLabel;
        private System.Windows.Forms.TextBox txtMessage;
    }
}
