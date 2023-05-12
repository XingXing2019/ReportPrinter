using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CosmoService.Code.Forms.Configuration.SQL;
using ReportPrinterDatabase.Code.Helper;
using ReportPrinterDatabase.Code.Manager;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager;
using ReportPrinterDatabase.Code.Manager.ConfigManager.PdfRendererManager.PdfTableRenderer;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.Winform.Configuration.SQL;
using HorizontalAlignment = ReportPrinterLibrary.Code.Enum.HorizontalAlignment;

namespace CosmoService.Code.UserControls.PDF
{
    public partial class ucPdfTableRenderer : UserControl, IPdfRendererUserControl
    {
        private PdfRendererManagerBase<PdfTableRendererModel> _manager;
        private BindingList<SqlResColumnData> _sqlResColumns;

        public ucPdfTableRenderer()
        {
            InitializeComponent();
        }


        public bool ValidateInput()
        {
            epRendererInfo.Clear();
            var isValid = true;

            if (nudBoardThickness.Value == 0)
            {
                epRendererInfo.SetError(nudBoardThickness, "Board thickness cannot be 0");
                isValid = false;
            }

            if (nudLineSpace.Value == 0)
            {
                epRendererInfo.SetError(nudLineSpace, "Line space cannot be 0");
                isValid = false;
            }

            if (nudSpace.Value == 0)
            {
                epRendererInfo.SetError(nudSpace, "Space cannot be 0");
                isValid = false;
            }

            if (!string.IsNullOrEmpty(tbTitleColor.Text.Trim()) && !Enum.TryParse(tbTitleColor.Text.Trim(), out XKnownColor brushColor))
            {
                epRendererInfo.SetError(tbTitleColor, "Title color input is invalid");
                isValid = false;
            }

            if (nudTitleOpacity.Value == 0)
            {
                epRendererInfo.SetError(nudTitleOpacity, "Title opacity cannot be 0");
                isValid = false;
            }

            if (!ucSqlSelector.ValidateInput())
            {
                isValid = false;
            }

            if (_sqlResColumns.Count == 0)
            {
                epRendererInfo.SetError(lblSqlresColumns, "At least one sql result column is required");
                isValid = false;
            }

            return isValid;
        }

        public void Save(PdfRendererBaseModel rendererBase)
        {
            var renderer = PdfRendererHelper<PdfTableRendererModel>.CreatePdfRenderer(rendererBase);

            renderer.PdfRendererBaseId = Guid.NewGuid();
            renderer.BoardThickness = double.Parse(nudBoardThickness.Text);
            renderer.LineSpace = double.Parse(nudLineSpace.Text);
            renderer.Space = double.Parse(nudSpace.Text);
            renderer.HideTitle = cbHideTitle.Checked;
            renderer.TitleHorizontalAlignment = (HorizontalAlignment)ecbTitleHAlignment.SelectedValue;

            if (!string.IsNullOrEmpty(tbTitleColor.Text.Trim()))
            {
                renderer.TitleColor = Enum.Parse<XKnownColor>(tbTitleColor.Text.Trim());
            }

            renderer.TitleColorOpacity = double.Parse(nudTitleOpacity.Text);
            renderer.SqlTemplateConfigSqlConfigId = ucSqlSelector.SelectedSql;
            renderer.SqlVariable = tbSqlVariable.Text.Trim();

            if (cbSubTable.SelectedItem != null)
            {
                var subTable = (PdfRendererBaseModel)cbSubTable.SelectedItem;
                if (subTable.PdfRendererBaseId != Guid.Empty)
                {
                    renderer.SubPdfTableRendererId = subTable.PdfRendererBaseId;
                }
            }

            renderer.SqlResColumns = _sqlResColumns.Select(x => new SqlResColumnModel
            {
                PdfRendererBaseId = renderer.PdfRendererBaseId,
                Id = x.Id,
                Title = x.Title,
                WidthRatio = x.WidthRatio,
                Position = x.Position,
                Left = x.Left,
                Right = x.Right
            }).ToList();

            _manager.Post(renderer);
        }

        public void ClearError()
        {
            epRendererInfo.Clear();
            ucSqlSelector.ClearError();
        }

        private void ucPdfTableRenderer_Load(object sender, EventArgs e)
        {
            if (AppConfig.Instance.DatabaseManagerType == DatabaseManagerType.EFCore)
                _manager = new PdfTableRendererEFCoreManager();
            else if (AppConfig.Instance.DatabaseManagerType == DatabaseManagerType.SP)
                _manager = new PdfTableRendererSPManager();

            SetupScreen();
            ucSqlSelector.Init(false);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var frm = new frmAddSqlResColumnConfig(_sqlResColumns);
            frm.ShowDialog();
            ToggleDeleteButton();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedConfigs = ((BindingList<SqlResColumnData>)dgvSqlResultColumns.DataSource).Where(x => x.IsSelected).ToList();

            if (selectedConfigs.Count == 0)
            {
                epRendererInfo.SetError(btnDelete, "Please select one config");
                return;
            }

            if (MessageBox.Show("Do you want to delete selected sql variable configs?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                selectedConfigs.ForEach(x => _sqlResColumns.Remove(x));
                ToggleDeleteButton();
            }
        }

        #region Helper

        private void SetupScreen()
        {
            ecbTitleHAlignment.EnumType = typeof(HorizontalAlignment);

            var rendererBaseManager = (IPdfRendererBaseManager)ManagerFactory.CreateManager<PdfRendererBaseModel>(typeof(IPdfRendererBaseManager), AppConfig.Instance.DatabaseManagerType);
            var renderers = Task.Run(() => rendererBaseManager.GetAllByRendererType(PdfRendererType.Table)).Result;

            cbSubTable.DisplayMember = nameof(PdfRendererBaseModel.Id);
            cbSubTable.Items.Clear();
            cbSubTable.Items.Add(new PdfRendererBaseModel());
            renderers.ForEach(x => cbSubTable.Items.Add(x));

            _sqlResColumns = new BindingList<SqlResColumnData>();
            dgvSqlResultColumns.DataSource = _sqlResColumns;
            ToggleDeleteButton();
        }

        private void ToggleDeleteButton()
        {
            btnDelete.Enabled = _sqlResColumns.Count > 0;
        }

        #endregion
    }
}
