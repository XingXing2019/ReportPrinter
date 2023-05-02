using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ReportPrinterLibrary.Code.Enum;
using HorizontalAlignment = ReportPrinterLibrary.Code.Enum.HorizontalAlignment;

namespace CosmoService.Code.Forms.Configuration.PDF
{
    public partial class frmUpsertPdfRenderer : Form
    {
        public frmUpsertPdfRenderer()
        {
            InitializeComponent();

            SetupScreen();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ecbRendererType.SelectedItem = PdfRendererType.Table;
        }



        #region Helper

        private void SetupScreen()
        {
            ecbRendererType.EnumType = typeof(PdfRendererType);
            ecbHAlignment.EnumType = typeof(HorizontalAlignment);
            ecbVAlignment.EnumType = typeof(VerticalAlignment);
            ecbPosition.EnumType = typeof(Position);
            ecbFontStyle.EnumType = typeof(FontStyle);
        }

        #endregion
    }
}
