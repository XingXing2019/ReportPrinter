using RaphaelLibrary.Code.Render.Label.Manager;
using RaphaelLibrary.Code.Render.PDF.Model;
using RaphaelLibrary.Code.Render.SQL;

namespace RaphaelLibrary.Code.Render.Label.PlaceHolder
{
    public class SqlPlaceHolder : PlaceHolderBase
    {
        private Sql _sql;
        private SqlResColumn _sqlResColumn;

        public SqlPlaceHolder(string placeHolder, Sql sql, SqlResColumn sqlResColumn) : base(placeHolder)
        {
            _sql = sql;
            _sqlResColumn = sqlResColumn;
        }

        protected override bool TryGetPlaceHolderValue(LabelManager manager, out string value)
        {
            return _sql.TryExecute(manager.MessageId, _sqlResColumn, out value);
        }

        public override PlaceHolderBase Clone()
        {
            var cloned = this.MemberwiseClone() as SqlPlaceHolder;
            cloned._sql = this._sql.Clone() as Sql;
            cloned._sqlResColumn = this._sqlResColumn.Clone();
            return cloned;
        }
    }
}