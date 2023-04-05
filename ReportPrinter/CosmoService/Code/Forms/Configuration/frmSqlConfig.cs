using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager;
using ReportPrinterDatabase.Code.Manager.ConfigManager.SqlConfigManager;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.Winform.Configuration;

namespace CosmoService.Code.Forms.Configuration
{
    public partial class frmSqlConfig : Form
    {
        private readonly ISqlConfigManager _manager;

        public frmSqlConfig()
        {
            InitializeComponent();
            _manager = (ISqlConfigManager)ManagerFactory.CreateManager<SqlConfig>(typeof(ISqlConfigManager), AppConfig.Instance.DatabaseManagerType);
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await RefreshDataGridView();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var frm = new frmAddSqlConfig();
            frm.ShowDialog();
            await RefreshDataGridView();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }


        #region Helper

        private async Task RefreshDataGridView()
        {
            var sqlConfigs = await _manager.GetAll();
            var data = sqlConfigs.Select(x => new SqlConfigData
            {
                Id = x.Id,
                DatabaseId = x.DatabaseId,
                Query = x.Query,
            }).ToList();

            dgvSqlConfigs.DataSource = data;
        }

        #endregion
    }
}
