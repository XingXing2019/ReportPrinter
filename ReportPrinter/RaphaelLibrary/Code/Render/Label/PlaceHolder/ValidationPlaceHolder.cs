using RaphaelLibrary.Code.Render.Label.Manager;

namespace RaphaelLibrary.Code.Render.Label.PlaceHolder
{
    public class ValidationPlaceHolder : PlaceHolderBase
    {
        public ValidationPlaceHolder(string placeHolder) : base(placeHolder)
        {
        }

        protected override bool TryGetPlaceHolderValue(LabelManager manager, out string value)
        {
            throw new System.NotImplementedException();
        }
    }
}