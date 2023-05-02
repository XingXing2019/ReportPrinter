using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CosmoService.Code.Forms.Configuration.PDF
{
    public partial class frmPdfConfig : Form
    {
        public frmPdfConfig()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new frmUpsertPdfRenderer();
            frm.ShowDialog();
        }
    }
}
