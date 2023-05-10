using System;
using System.ComponentModel;
using System.Windows.Forms;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.Winform.Configuration.SQL;

namespace CosmoService.Code.Forms.Configuration.SQL
{
    public partial class frmAddSqlResColumnConfig : Form
    {
        private readonly BindingList<SqlResColumnData> _sqlResColumns;

        public frmAddSqlResColumnConfig(BindingList<SqlResColumnData> sqlResColumns)
        {
            InitializeComponent();
            SetupScreen();

            _sqlResColumns = sqlResColumns;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            var sqlResColumn = new SqlResColumnData
            {
                Id = tbId.Text.Trim(),
                WidthRatio = double.Parse(nudWidthRatio.Text),
                Position = (Position)ecbPosition.SelectedValue,
            };

            if (!string.IsNullOrEmpty(tbTitle.Text.Trim()))
                sqlResColumn.Title = tbTitle.Text.Trim();

            if (rbLeft.Checked)
            {
                sqlResColumn.Left = double.Parse(nudLeftRight.Text);
            }
            else
            {
                sqlResColumn.Right = double.Parse(nudLeftRight.Text);
            }

            _sqlResColumns.Add(sqlResColumn);
            Close();
        }


        #region Helper

        private void SetupScreen()
        {
            ecbPosition.EnumType = typeof(Position);
        }

        private bool ValidateInput()
        {
            epSqlResColumn.Clear();
            var isValid = true;

            if (string.IsNullOrEmpty(tbId.Text.Trim()))
            {
                epSqlResColumn.SetError(tbId, "Id is required");
                isValid = false;
            }

            if (nudWidthRatio.Value == 0)
            {
                epSqlResColumn.SetError(nudWidthRatio, "Width ratio cannot be 0");
                isValid = false;
            }

            return isValid;
        }

        #endregion
    }
}
