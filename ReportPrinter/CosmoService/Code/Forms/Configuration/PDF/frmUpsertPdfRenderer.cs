using System;
using System.Windows.Forms;
using CosmoService.Code.UserControls.PDF;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Enum;
using HorizontalAlignment = ReportPrinterLibrary.Code.Enum.HorizontalAlignment;

namespace CosmoService.Code.Forms.Configuration.PDF
{
    public partial class frmUpsertPdfRenderer : Form
    {
        private IPdfRendererUserControl _selectedUserControl;

        public frmUpsertPdfRenderer()
        {
            InitializeComponent();
            Text = "Add Pdf Renderer Config";
            SetupScreen();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (TryGetBasicInfo(out var rendererBase) && _selectedUserControl.ValidateInput())
            {
                _selectedUserControl.Save(rendererBase);
                Close();
            }
        }

        private void ecbRendererType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucPdfAnnotationRenderer.Visible = false;
            ucPdfBarcodeRenderer.Visible = false;
            ucPdfImageRenderer.Visible = false;
            ucPdfPageNumberRenderer.Visible = false;
            ucPdfReprintMarkRenderer.Visible = false;

            var rendererType = (PdfRendererType)ecbRendererType.SelectedValue;

            if (rendererType == PdfRendererType.Annotation)
            {
                ucPdfAnnotationRenderer.Visible = true;
                _selectedUserControl = ucPdfAnnotationRenderer;
            }
            else if (rendererType == PdfRendererType.Barcode)
            {
                ucPdfBarcodeRenderer.Visible = true;
                _selectedUserControl = ucPdfBarcodeRenderer;
            }
            else if (rendererType == PdfRendererType.Image)
            {
                ucPdfImageRenderer.Visible = true;
                _selectedUserControl = ucPdfImageRenderer;
            }
            else if (rendererType == PdfRendererType.PageNumber)
            {
                ucPdfPageNumberRenderer.Visible = true;
                _selectedUserControl = ucPdfPageNumberRenderer;
            }
            else if (rendererType == PdfRendererType.ReprintMark)
            {
                ucPdfReprintMarkRenderer.Visible = true;
                _selectedUserControl = ucPdfReprintMarkRenderer;
            }
        }


        #region Helper

        private void SetupScreen()
        {
            ecbRendererType.EnumType = typeof(PdfRendererType);
            ecbRendererType.SelectedItem = PdfRendererType.Annotation;

            ecbHAlignment.EnumType = typeof(HorizontalAlignment);
            ecbVAlignment.EnumType = typeof(VerticalAlignment);
            ecbPosition.EnumType = typeof(Position);
            ecbFontStyle.EnumType = typeof(XFontStyle);
        }

        private bool TryGetBasicInfo(out PdfRendererBaseModel rendererBase)
        {
            var isValid = true;
            rendererBase = new PdfRendererBaseModel
            {
                RendererType = (PdfRendererType)ecbRendererType.SelectedValue,
                HorizontalAlignment = (HorizontalAlignment)ecbHAlignment.SelectedValue,
                VerticalAlignment = (VerticalAlignment)ecbVAlignment.SelectedValue,
                Position = (Position)ecbPosition.SelectedValue,
                FontStyle = (XFontStyle)ecbFontStyle.SelectedValue,
                Row = int.Parse(nudRow.Text),
                Column = int.Parse(nudColumn.Text)
            };

            if (string.IsNullOrEmpty(tbId.Text.Trim()))
            {
                epPdfConfig.SetError(tbId, "Id is required");
                isValid = false;
            }
            rendererBase.Id = tbId.Text.Trim();

            if (!string.IsNullOrEmpty(tbMargin.Text.Trim()))
            {
                if (tbMargin.Text.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Length != 4)
                {
                    epPdfConfig.SetError(tbMargin, "Margin input is invalid");
                    isValid = false;
                }
                else
                {
                    rendererBase.Margin = tbMargin.Text.Trim();
                }
            }

            if (!string.IsNullOrEmpty(tbPadding.Text.Trim()))
            {
                if (tbPadding.Text.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Length != 4)
                {
                    epPdfConfig.SetError(tbPadding, "Padding input is invalid");
                    isValid = false;
                }
                else
                {
                    rendererBase.Padding = tbPadding.Text.Trim();
                }
            }

            if (rbLeft.Checked)
            {
                rendererBase.Left = double.Parse(nudLeftRight.Text);
            }
            else if (rbRight.Checked)
            {
                rendererBase.Right = double.Parse(nudLeftRight.Text);
            }

            if (rbTop.Checked)
            {
                rendererBase.Top = double.Parse(nudTopBottom.Text);
            }
            else if (rbBottom.Checked)
            {
                rendererBase.Bottom = double.Parse(nudTopBottom.Text);
            }

            if (nudFontSize.Value == 0)
            {
                epPdfConfig.SetError(nudFontSize, "Font size cannot be 0");
                isValid = false;
            }
            else
            {
                rendererBase.FontSize = double.Parse(nudFontSize.Text);
            }

            if (!string.IsNullOrEmpty(tbFontFamily.Text.Trim()))
            {
                rendererBase.FontFamily = tbFontFamily.Text.Trim();
            }

            if (nudOpacity.Value == 0)
            {
                epPdfConfig.SetError(nudOpacity, "Opacity cannot be 0");
                isValid = false;
            }
            else
            {
                rendererBase.Opacity = double.Parse(nudOpacity.Text);
            }

            if (!string.IsNullOrEmpty(tbBrushColor.Text.Trim()))
            {
                if (!Enum.TryParse(tbBrushColor.Text.Trim(), out XKnownColor brushColor))
                {
                    epPdfConfig.SetError(tbBrushColor, "Brush color input is invalid");
                    isValid = false;
                }
                else
                {
                    rendererBase.BrushColor = brushColor;
                }
            }

            if (!string.IsNullOrEmpty(tbBackgroundColor.Text.Trim()))
            {
                if (!Enum.TryParse(tbBackgroundColor.Text.Trim(), out XKnownColor backgroudnColor))
                {
                    epPdfConfig.SetError(tbBackgroundColor, "Brush color input is invalid");
                    isValid = false;
                }
                else
                {
                    rendererBase.BackgroundColor = backgroudnColor;
                }
            }

            if (nudRowSpan.Value == 0)
            {
                epPdfConfig.SetError(nudRowSpan, "Row span cannot be 0");
                isValid = false;
            }
            else
            {
                rendererBase.RowSpan = int.Parse(nudRowSpan.Text);
            }

            if (nudColumnSpan.Value == 0)
            {
                epPdfConfig.SetError(nudColumnSpan, "Column span cannot be 0");
                isValid = false;
            }
            else
            {
                rendererBase.ColumnSpan = int.Parse(nudColumnSpan.Text);
            }

            return isValid;
        }

        #endregion

    }
}
