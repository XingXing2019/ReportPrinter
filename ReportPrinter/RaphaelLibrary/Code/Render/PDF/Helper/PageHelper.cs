using System;
using PdfSharp;
using PdfSharp.Drawing;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Helper
{
    public class PageHelper
    {
        public static bool TryGetPageSize(string input, out XSize size)
        {
            size = Enum.TryParse(input, out PageSize pageSize)
                ? PageSizeConverter.ToSize(pageSize)
                : ConvertCustomerPageSize(input);

            return size != XSize.Empty;
        }


        /// <summary>
        /// Convert customer page size (width : height) into XSize
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private static XSize ConvertCustomerPageSize(string input)
        {
            var procName = $"PageHelper.{nameof(ConvertCustomerPageSize)}";

            try
            {
                var dimension = input.Split(':', StringSplitOptions.RemoveEmptyEntries);
                double width = double.Parse(dimension[0]), height = double.Parse(dimension[1]);
                var size = PageSizeConverter.ToSize(PageSize.A0);
                size.Width = width;
                size.Height = height;
                return size;
            }
            catch (Exception ex)
            {
                Logger.Warn($"Unable to convert input: {input} into valid page size. Ex: {ex.Message}", procName);
                return XSize.Empty;
            }
        }
    }
}