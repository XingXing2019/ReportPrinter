using System;
using RaphaelLibrary.Code.Render.Label.Manager;

namespace RaphaelLibrary.Code.Render.Label.PlaceHolder
{
    public class TimestampPlaceHolder : PlaceHolderBase
    {
        private readonly bool _isUtc;
        private readonly string _mask;

        public TimestampPlaceHolder(string placeHolder, bool isUtc, string mask) : base(placeHolder)
        {
            _isUtc = isUtc;
            _mask = mask;
        }

        protected override bool TryGetPlaceHolderValue(LabelManager manager, out string value)
        {
            var time = _isUtc ? DateTime.UtcNow : DateTime.Now;
            value = time.ToString(_mask);
            return true;
        }

        public override PlaceHolderBase Clone()
        {
            var cloned = this.MemberwiseClone() as TimestampPlaceHolder;
            return cloned;
        }
    }
}