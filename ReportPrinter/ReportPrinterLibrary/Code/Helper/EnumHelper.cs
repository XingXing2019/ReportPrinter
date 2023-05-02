using System;
using System.Reflection;
using System.ComponentModel;

namespace ReportPrinterLibrary.Code.Helper
{
    public static class EnumHelper
    {
        public static string ToDescription(this System.Enum input)
        {
            var type = input.GetType();
            if (!type.IsEnum)
            {
                throw new InvalidOperationException("Input is not an enum");
            }

            var fields = type.GetFields();
            foreach (var field in fields)
            {
                if (field.Name != input.ToString())
                    continue;
                var attribute = field.GetCustomAttribute(typeof(DescriptionAttribute), false);
                return attribute == null ? input.ToString() : ((DescriptionAttribute)attribute).Description;
            }

            return input.ToString();
        }
    }
}