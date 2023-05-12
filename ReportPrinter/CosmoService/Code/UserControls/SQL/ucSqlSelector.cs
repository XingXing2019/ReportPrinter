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
        private bool _showSqlRes;

        public Guid SelectedSql
        {
            get
            {
                if (cbSql.SelectedItem == null)
                    return Guid.Empty;
                var sql = (SqlConfig)cbSql.SelectedItem;
                var sqlTemplateId = ((SqlTemplateConfigModel)cbSqlTemplate.SelectedItem).SqlTemplateConfigId;
                return sql.SqlTemplateConfigSqlConfigs.Single(x => x.SqlTemplateConfigId == sqlTemplateId).SqlTemplateConfigSqlConfigId;
            }
        }

        public string SqlResult => tbSqlRes.Text.Trim();

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

        public void Init(bool showSqlRes)
        {
            _showSqlRes = showSqlRes;
            SetupScreen(showSqlRes);
        }

        public bool ValidateInput()
        {
            epSqlSelector.Clear();
            var isValid = true;

            if (_showSqlRes && string.IsNullOrEmpty(tbSqlRes.Text.Trim()))
            {
                epSqlSelector.SetError(tbSqlRes, "Sql result is required");
                isValid = false;
            }

            if (SelectedSql == Guid.Empty)
            {
                epSqlSelector.SetError(cbSql, "Sql is required");
                isValid = false;
            }

            return isValid;
        }

        #region Helper

        private void SetupScreen(bool showSqlRes)
        {
            lblSqlRes.Visible = tbSqlRes.Visible = showSqlRes;

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
