using System;
using RaphaelLibrary.Code.Init.Label;
using RaphaelLibrary.Code.Render.Label.Manager;

namespace RaphaelLibrary.Code.Render.Label.PlaceHolder
{
    public class ReferencePlaceHolder : PlaceHolderBase
    {
        private IStructure _labelStructure;

        public ReferencePlaceHolder(string placeHolder, IStructure labelStructure) : base(placeHolder)
        {
            _labelStructure = labelStructure;
        }

        protected override bool TryGetPlaceHolderValue(LabelManager manager, out string value)
        {
            value = String.Empty;
            if (!_labelStructure.TryCreateLabelStructure(manager.MessageId, out var labelStructure))
                return false;

            value = labelStructure.ToString();
            return true;
        }

        public override PlaceHolderBase Clone()
        {
            var cloned = this.MemberwiseClone() as ReferencePlaceHolder;
            cloned._labelStructure = this._labelStructure.Clone();
            return cloned;
        }
    }
}