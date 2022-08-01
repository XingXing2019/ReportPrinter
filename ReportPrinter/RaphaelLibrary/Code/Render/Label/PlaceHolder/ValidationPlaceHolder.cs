using System;
using RaphaelLibrary.Code.Init.Label;
using RaphaelLibrary.Code.Render.Label.Manager;
using RaphaelLibrary.Code.Render.Label.Model;
using RaphaelLibrary.Code.Render.Label.Renderer;
using RaphaelLibrary.Code.Render.PDF.Model;
using RaphaelLibrary.Code.Render.SQL;

namespace RaphaelLibrary.Code.Render.Label.PlaceHolder
{
    public class ValidationPlaceHolder : PlaceHolderBase
    {
        private readonly ValidationType _validationType;
        private readonly string _expectedValue;
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
            _sql = sql;
            _sqlResColumn = sqlResColumn;

            _trueContent = validationModel.TrueContent;
            _falseContent = validationModel.FalseContent;
            _trueStructure = validationModel.TrueStructure;
            _falseStructure = validationModel.FalseStructure;
        }

        protected override bool TryGetPlaceHolderValue(LabelManager manager, out string value)
        {
            throw new System.NotImplementedException();
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
    }
}