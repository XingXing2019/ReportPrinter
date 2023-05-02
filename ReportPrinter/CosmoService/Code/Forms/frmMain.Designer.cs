namespace CosmoService.Code.Forms
{
    partial class frmMain
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
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            sqlConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            pdfConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            messageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            publishMessage = new System.Windows.Forms.ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { configurationToolStripMenuItem, messageToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            menuStrip1.Size = new System.Drawing.Size(1552, 40);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // configurationToolStripMenuItem
            // 
            configurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { sqlConfiguration, pdfConfiguration });
            configurationToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            configurationToolStripMenuItem.Size = new System.Drawing.Size(158, 34);
            configurationToolStripMenuItem.Text = "Configuration";
            // 
            // sqlConfiguration
            // 
            sqlConfiguration.Name = "sqlConfiguration";
            sqlConfiguration.Size = new System.Drawing.Size(315, 40);
            sqlConfiguration.Text = "SQL Configuration";
            sqlConfiguration.Click += sqlConfiguration_Click;
            // 
            // pdfConfiguration
            // 
            pdfConfiguration.Name = "pdfConfiguration";
            pdfConfiguration.Size = new System.Drawing.Size(315, 40);
            pdfConfiguration.Text = "PDF Configuration";
            pdfConfiguration.Click += pdfConfiguration_Click;
            // 
            // messageToolStripMenuItem
            // 
            messageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { publishMessage });
            messageToolStripMenuItem.Name = "messageToolStripMenuItem";
            messageToolStripMenuItem.Size = new System.Drawing.Size(113, 34);
            messageToolStripMenuItem.Text = "Message";
            // 
            // publishMessage
            // 
            publishMessage.Name = "publishMessage";
            publishMessage.Size = new System.Drawing.Size(286, 40);
            publishMessage.Text = "Publish Message";
            publishMessage.Click += publishMessage_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1552, 986);
            Controls.Add(menuStrip1);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "frmMain";
            Text = "Report Printer";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem messageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem publishMessage;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sqlConfiguration;
        private System.Windows.Forms.ToolStripMenuItem pdfConfiguration;
    }
}