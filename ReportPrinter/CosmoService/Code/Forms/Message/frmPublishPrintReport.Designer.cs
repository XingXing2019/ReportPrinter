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
            btnSend.Location = new System.Drawing.Point(257, 30);
            btnSend.Name = "btnSend";
            btnSend.Size = new System.Drawing.Size(130, 40);
            btnSend.TabIndex = 0;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // rdbPDF
            // 
            rdbPDF.AutoSize = true;
            rdbPDF.Checked = true;
            rdbPDF.Location = new System.Drawing.Point(25, 30);
            rdbPDF.Name = "rdbPDF";
            rdbPDF.Size = new System.Drawing.Size(75, 34);
            rdbPDF.TabIndex = 1;
            rdbPDF.TabStop = true;
            rdbPDF.Text = "PDF";
            rdbPDF.UseVisualStyleBackColor = true;
            // 
            // rdbLabel
            // 
            rdbLabel.AutoSize = true;
            rdbLabel.Location = new System.Drawing.Point(137, 30);
            rdbLabel.Name = "rdbLabel";
            rdbLabel.Size = new System.Drawing.Size(87, 34);
            rdbLabel.TabIndex = 2;
            rdbLabel.Text = "Label";
            rdbLabel.UseVisualStyleBackColor = true;
            // 
            // txtMessage
            // 
            txtMessage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            txtMessage.Location = new System.Drawing.Point(25, 89);
            txtMessage.Multiline = true;
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new System.Drawing.Size(638, 541);
            txtMessage.TabIndex = 3;
            // 
            // frmPublishPrintReport
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(690, 658);
            Controls.Add(txtMessage);
            Controls.Add(rdbLabel);
            Controls.Add(rdbPDF);
            Controls.Add(btnSend);
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
