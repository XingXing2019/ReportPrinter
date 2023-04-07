namespace CosmoService.Code.Forms.Message
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
            btnSend = new System.Windows.Forms.Button();
            rdbPDF = new System.Windows.Forms.RadioButton();
            rdbLabel = new System.Windows.Forms.RadioButton();
            txtMessage = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // btnSend
            // 
            btnSend.Location = new System.Drawing.Point(309, 36);
            btnSend.Margin = new System.Windows.Forms.Padding(2);
            btnSend.Name = "btnSend";
            btnSend.Size = new System.Drawing.Size(87, 27);
            btnSend.TabIndex = 0;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // rdbPDF
            // 
            rdbPDF.AutoSize = true;
            rdbPDF.Location = new System.Drawing.Point(51, 38);
            rdbPDF.Margin = new System.Windows.Forms.Padding(2);
            rdbPDF.Name = "rdbPDF";
            rdbPDF.Size = new System.Drawing.Size(56, 24);
            rdbPDF.TabIndex = 1;
            rdbPDF.TabStop = true;
            rdbPDF.Text = "PDF";
            rdbPDF.UseVisualStyleBackColor = true;
            // 
            // rdbLabel
            // 
            rdbLabel.AutoSize = true;
            rdbLabel.Checked = true;
            rdbLabel.Location = new System.Drawing.Point(181, 38);
            rdbLabel.Margin = new System.Windows.Forms.Padding(2);
            rdbLabel.Name = "rdbLabel";
            rdbLabel.Size = new System.Drawing.Size(66, 24);
            rdbLabel.TabIndex = 2;
            rdbLabel.TabStop = true;
            rdbLabel.Text = "Label";
            rdbLabel.UseVisualStyleBackColor = true;
            // 
            // txtMessage
            // 
            txtMessage.Location = new System.Drawing.Point(41, 79);
            txtMessage.Margin = new System.Windows.Forms.Padding(2);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new System.Drawing.Size(608, 327);
            txtMessage.TabIndex = 3;
            // 
            // frmPublishPrintReport
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(685, 425);
            Controls.Add(txtMessage);
            Controls.Add(rdbLabel);
            Controls.Add(rdbPDF);
            Controls.Add(btnSend);
            Margin = new System.Windows.Forms.Padding(2);
            Name = "frmPublishPrintReport";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Publish Print Report Message";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.RadioButton rdbPDF;
        private System.Windows.Forms.RadioButton rdbLabel;
        private System.Windows.Forms.TextBox txtMessage;
    }
}
