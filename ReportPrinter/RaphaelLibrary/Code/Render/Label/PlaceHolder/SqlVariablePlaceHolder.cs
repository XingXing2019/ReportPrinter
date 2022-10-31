using System;
using RaphaelLibrary.Code.Common.SqlVariableCacheManager;
using RaphaelLibrary.Code.Render.Label.Manager;
using ReportPrinterLibrary.Code.Config.Configuration;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.Label.PlaceHolder
{
    public class SqlVariablePlaceHolder : PlaceHolderBase
    {
        private readonly string _name;

        public SqlVariablePlaceHolder(string placeHolder, string name) : base(placeHolder)
        {
            _name = name;
        }

        protected override bool TryGetPlaceHolderValue(LabelManager manager, out string value)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryGetPlaceHolderValue)}";
            value = string.Empty;
            
            var messageId = manager.MessageId;
            var sqlVariableManagerType = AppConfig.Instance.SqlVariableCacheManagerType;
            var sqlVariableManager = SqlVariableCacheManagerFactory.CreateSqlVariableCacheManager(sqlVariableManagerType);

            try
            {
                var sqlVariables = sqlVariableManager.GetSqlVariables(messageId);

                if (!sqlVariables.ContainsKey(_name))
                {
                    Logger.Error($"Unable to retrieve sql variable with name: {_name} for message: {messageId}", procName);
                    return false;
                }

                value = sqlVariables[_name].Value;
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during getting placeholder value. Ex: {ex.Message}", procName);
                return false;
            }
        }
    }
}