using System;
using RaphaelLibrary.Code.Init.Label;
using RaphaelLibrary.Code.Render.Label.Manager;
using RaphaelLibrary.Code.Render.Label.Model;
using RaphaelLibrary.Code.Render.Label.Renderer;
using RaphaelLibrary.Code.Render.PDF.Model;
using RaphaelLibrary.Code.Render.SQL;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.Label.PlaceHolder
{
    public class ValidationPlaceHolder : PlaceHolderBase
    {
        private readonly ValidationType _validationType;
        private readonly string _expectedValue;
        private readonly Comparator _comparator;
        private Sql _sql;
        private SqlResColumn _sqlResColumn;

        private readonly string _trueContent;
        private readonly string _falseContent;
        private IStructure _trueStructure;
        private IStructure _falseStructure;


        public ValidationPlaceHolder(string placeHolder, Sql sql, SqlResColumn sqlResColumn, ValidationModel validationModel)
            : base(placeHolder)
        {
            _validationType = validationModel.ValidationType;
            _expectedValue = validationModel.ExpectedValue;
            _comparator = validationModel.Comparator;
            _sql = sql;
            _sqlResColumn = sqlResColumn;

            _trueContent = validationModel.TrueContent;
            _falseContent = validationModel.FalseContent;
            _trueStructure = validationModel.TrueStructure;
            _falseStructure = validationModel.FalseStructure;
        }

        protected override bool TryGetPlaceHolderValue(LabelManager manager, out string value)
        {
            value = string.Empty;

            if (!_sql.TryExecute(manager.MessageId, _sqlResColumn, out var actualValue))
                return false;

            if (!Validate(_expectedValue, actualValue, _comparator, out var isTrue))
                return false;

            if (isTrue)
            {
                if (_validationType == ValidationType.Text)
                {
                    value = _trueContent;
                }
                else if (_validationType == ValidationType.Structure)
                {
                    if (!_trueStructure.TryCreateLabelStructure(manager.MessageId, out var lines))
                        return false;
                    value = lines.ToString();
                }
            }
            else
            {
                if (_validationType == ValidationType.Text)
                {
                    value = _falseContent;
                }
                else if (_validationType == ValidationType.Structure)
                {
                    if (!_falseStructure.TryCreateLabelStructure(manager.MessageId, out var lines))
                        return false;
                    value = lines.ToString();
                }
            }

            return true;
        }

        public override PlaceHolderBase Clone()
        {
            var cloned = this.MemberwiseClone() as ValidationPlaceHolder;
            cloned._sql = this._sql.Clone() as Sql;
            cloned._sqlResColumn = this._sqlResColumn.Clone();

            if (_validationType == ValidationType.Structure)
            {
                cloned._trueStructure = this._trueStructure.Clone();
                cloned._falseStructure = this._falseStructure.Clone();
            }

            return cloned;
        }


        #region Helper

        private bool Validate(string expected, string actual, Comparator comparator, out bool isTrue)
        {
            var procName = $"{this.GetType().Name}.{nameof(Validate)}";
            isTrue = true;

            try
            {
                if (comparator == Comparator.Equals)
                    isTrue = actual == expected;
                else if (comparator == Comparator.NotEquals)
                    isTrue = actual != expected;

                var expectedValue = double.Parse(expected);
                var actualValue = double.Parse(actual);

                if (comparator == Comparator.Greater)
                    isTrue = actualValue > expectedValue;
                else if (comparator == Comparator.GreaterOrEquals)
                    isTrue = actualValue >= expectedValue;
                else if (comparator == Comparator.Less)
                    isTrue = actualValue < expectedValue;
                else if (comparator == Comparator.LessOrEquals)
                    isTrue = actualValue <= expectedValue;

                return true;
            }
            catch (Exception)
            {
                Logger.Error($"Unable to validate expected: {expected} and actual: {actual} with comparator: {comparator}", procName);
                return false;
            }
        }


        #endregion
    }
}