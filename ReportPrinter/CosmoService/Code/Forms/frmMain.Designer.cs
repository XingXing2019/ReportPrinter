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
            tsmiSqlConfig = new System.Windows.Forms.ToolStripMenuItem();
            sQLConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            publishMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            tsmiPublishMessage = new System.Windows.Forms.ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { tsmiSqlConfig, publishMessageToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            menuStrip1.Size = new System.Drawing.Size(1552, 40);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // tsmiSqlConfig
            // 
            tsmiSqlConfig.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { sQLConfigToolStripMenuItem });
            tsmiSqlConfig.MergeAction = System.Windows.Forms.MergeAction.Insert;
            tsmiSqlConfig.Name = "tsmiSqlConfig";
            tsmiSqlConfig.Size = new System.Drawing.Size(158, 34);
            tsmiSqlConfig.Text = "Configuration";
            // 
            // sQLConfigToolStripMenuItem
            // 
            sQLConfigToolStripMenuItem.Name = "sQLConfigToolStripMenuItem";
            sQLConfigToolStripMenuItem.Size = new System.Drawing.Size(315, 40);
            sQLConfigToolStripMenuItem.Text = "SQL Template";
            sQLConfigToolStripMenuItem.Click += sQLConfigToolStripMenuItem_Click;
            // 
            // publishMessageToolStripMenuItem
            // 
            publishMessageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { tsmiPublishMessage });
            publishMessageToolStripMenuItem.Name = "publishMessageToolStripMenuItem";
            publishMessageToolStripMenuItem.Size = new System.Drawing.Size(113, 34);
            publishMessageToolStripMenuItem.Text = "Message";
            // 
            // tsmiPublishMessage
            // 
            tsmiPublishMessage.Name = "tsmiPublishMessage";
            tsmiPublishMessage.Size = new System.Drawing.Size(286, 40);
            tsmiPublishMessage.Text = "Publish Message";
            tsmiPublishMessage.Click += tsmiPublishMessage_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1552, 986);
            Controls.Add(menuStrip1);
            Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
        private System.Windows.Forms.ToolStripMenuItem publishMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiPublishMessage;
        private System.Windows.Forms.ToolStripMenuItem tsmiSqlConfig;
        private System.Windows.Forms.ToolStripMenuItem sQLConfigToolStripMenuItem;
    }
}