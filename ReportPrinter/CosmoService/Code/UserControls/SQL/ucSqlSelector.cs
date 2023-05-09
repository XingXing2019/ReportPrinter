using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReportPrinterDatabase.Code.Entity;
using ReportPrinterDatabase.Code.Manager;
using ReportPrinterDatabase.Code.Manager.ConfigManager.SqlTemplateConfigManager;
using ReportPrinterDatabase.Code.Model;
using ReportPrinterLibrary.Code.Config.Configuration;

namespace CosmoService.Code.UserControls.SQL
{
    public partial class ucSqlSelector : UserControl
    {
        public ucSqlSelector()
        {
            InitializeComponent();
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sqlTemplate = (SqlTemplateConfigModel)cbSqlTemplate.SelectedItem;
            var sqls = sqlTemplate.SqlConfigs;

            cbSql.Items.Clear();
            sqls.ForEach(x => cbSql.Items.Add(x));
        }

        public void Init()
        {
            SetupScreen();
        }

        public Guid GetSelectedSql()
        {
            if (cbSql.SelectedItem == null) 
                return Guid.Empty;
            var sql = (SqlConfig)cbSql.SelectedItem;
            var sqlTemplateId = ((SqlTemplateConfigModel)cbSqlTemplate.SelectedItem).SqlTemplateConfigId;
            return sql.SqlTemplateConfigSqlConfigs.Single(x => x.SqlTemplateConfigId == sqlTemplateId).SqlTemplateConfigSqlConfigId;
        }

        #region Helper

        private void SetupScreen()
        {
            var sqlTemplateConfigManager = (ISqlTemplateConfigManager)ManagerFactory.CreateManager<SqlTemplateConfigModel>(typeof(ISqlTemplateConfigManager), AppConfig.Instance.DatabaseManagerType);
            var sqlTemplates = Task.Run(sqlTemplateConfigManager.GetAll).Result;

            cbSqlTemplate.DisplayMember = nameof(SqlTemplateConfigModel.Id);
            cbSqlTemplate.Items.Clear();
            sqlTemplates.ToList().ForEach(x => cbSqlTemplate.Items.Add(x));

            cbSql.DisplayMember = nameof(SqlConfig.Id);
        }

        #endregion
    }
}
