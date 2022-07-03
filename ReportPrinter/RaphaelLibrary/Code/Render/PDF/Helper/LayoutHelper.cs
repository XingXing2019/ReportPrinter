using System;
using System.Linq;
using RaphaelLibrary.Code.Render.PDF.Model;

namespace RaphaelLibrary.Code.Render.PDF.Helper
{
    public class LayoutHelper
    {
        public static bool TryCalcMarginBoxParameter(out BoxModelParameter marginBox)
        {
            throw new NotImplementedException();
        }

        public static bool TryCalcPaddingBoxParameter(out BoxModelParameter paddingBox)
        {
            throw new NotImplementedException();
        }

        public static bool TryCalcContentBoxParameter(out BoxModelParameter contentBox)
        {
            throw new NotImplementedException();
        }
        
        public static bool TryCreateMarginPadding(string input, out MarginPaddingModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    model = new MarginPaddingModel(0, 0, 0, 0);
                    return false;
                }
                
                var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 4 || parts.Any(x => !double.TryParse(x, out var value)))
                {
                    model = new MarginPaddingModel(0, 0, 0, 0);
                    return false;
                }

                var top = double.Parse(parts[0]);
                var right = double.Parse(parts[1]);
                var bottom = double.Parse(parts[2]);
                var left = double.Parse(parts[3]);
                model = new MarginPaddingModel(top, right, bottom, left);
                return true;
            }
            catch
            {
                model = new MarginPaddingModel(0, 0, 0, 0);
                return false;
            }
        }
    }
}