using System;
using System.Windows.Forms;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager;
using ReportPrinterDatabase.Code.Manager.ConfigManager.SqlConfigManager;
using ReportPrinterLibrary.Code.Config.Configuration;

namespace CosmoService.Code.Forms.Configuration
{
    public partial class frmSqlConfig : Form
    {
        private readonly IManager<SqlConfig> _manager;

        public frmSqlConfig()
        {
            InitializeComponent();
            _manager = ManagerFactory.CreateManager<SqlConfig>(typeof(ISqlConfigManager), AppConfig.Instance.ManagerType);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }


        #region Helper

        private void Refresh()
        {

        }

        #endregion
    }
}
