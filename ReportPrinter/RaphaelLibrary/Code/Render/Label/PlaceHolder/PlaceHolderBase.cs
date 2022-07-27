using RaphaelLibrary.Code.Render.Label.Manager;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.Label.PlaceHolder
{
    public abstract class PlaceHolderBase
    {
        private readonly string _placeHolder;

        protected PlaceHolderBase(string placeHolder)
        {
            _placeHolder = placeHolder;
        }

        public bool TryReplacePlaceHolder(LabelManager manager, int lineIndex)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryReplacePlaceHolder)}";
            if (!TryGetPlaceHolderValue(manager, out var value))
                return false;

            manager.Lines[lineIndex] = manager.Lines[lineIndex].Replace(_placeHolder, value);

            Logger.Info($"Success to replace place holder: {_placeHolder} with value: {value}", procName);
            return true;
        }

        protected abstract bool TryGetPlaceHolderValue(LabelManager manager, out string value);

        public abstract PlaceHolderBase Clone();
    }
}