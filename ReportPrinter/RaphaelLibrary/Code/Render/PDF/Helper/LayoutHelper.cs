using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PdfSharp.Drawing;
using RaphaelLibrary.Code.Render.PDF.Model;
using RaphaelLibrary.Code.Render.PDF.Renderer;
using RaphaelLibrary.Code.Render.PDF.Structure;
using ReportPrinterLibrary.Code.Enum;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.PDF.Helper
{
    public class LayoutHelper
    {
        public static bool TryCreateMarginBox(BoxModel container, int totalRows, int totalColumns, PdfRendererBase renderer, out BoxModel marginBox)
        {
            var procName = $"LayoutHelper.{nameof(TryCreateMarginPadding)}";
            marginBox = null;

            var dimension = CalcBoxDimension(container, totalRows, totalColumns);
            var layoutParam = renderer.GetLayoutParameter();
            
            var x = container.X + dimension[0] * layoutParam.Column;
            var y = container.Y + dimension[1] * layoutParam.Row;
            var width = dimension[0] * layoutParam.ColumnSpan;
            var height = dimension[1] * layoutParam.RowSpan;

            marginBox = new BoxModel(x, y, width, height);

            return true;
        }

        public static bool TryCreateMarginBox(BoxModel container, XSize textSize, PdfRendererBase renderer, out BoxModel marginBox, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center)
        {
            var procName = $"LayoutHelper.{nameof(TryCreateMarginPadding)}";
            marginBox = null;

            var layoutParam = renderer.GetLayoutParameter();
            MarginPaddingModel margin = layoutParam.Margin, padding = layoutParam.Padding;
            double x = container.X, y = container.Y;
            var width = textSize.Width + margin.Left + margin.Right + padding.Left + padding.Right;
            var height = textSize.Height + margin.Top + margin.Bottom + padding.Top + padding.Bottom;

            if (width > container.Width)
            {
                Logger.Error($"{renderer.GetType().Name}: Sum of horizontal margin and padding is too large, there is no space for content", procName);
                return false;
            }

            if (height > container.Height)
            {
                Logger.Error($"{renderer.GetType().Name}: Sum of vertical margin and padding is too large, there is no space for content", procName);
                return false;
            }

            if (horizontalAlignment == HorizontalAlignment.Center)
                x += container.Width / 2 - width / 2;
            else if (horizontalAlignment == HorizontalAlignment.Right)
                x += container.Width - width;
            
            y += container.Height / 2 - height / 2;
            
            marginBox = new BoxModel(x, y, width, height);
            return true;
        }

        public static bool TryCreatePaddingBox(BoxModel container, int totalRows, int totalColumns, PdfRendererBase renderer, out BoxModel paddingBox)
        {
            var procName = $"LayoutHelper.{nameof(TryCreatePaddingBox)}";
            paddingBox = null;

            var dimension = CalcBoxDimension(container, totalRows, totalColumns);
            var layoutParam = renderer.GetLayoutParameter();
            var margin = layoutParam.Margin;

            var x = container.X + dimension[0] * layoutParam.Column + margin.Left;
            var y = container.Y + dimension[1] * layoutParam.Row + margin.Top;
            var height = dimension[1] * layoutParam.RowSpan - margin.Top - margin.Bottom;
            var width = dimension[0] * layoutParam.ColumnSpan - margin.Left - margin.Right;

            if (height <= 0)
            {
                Logger.Error($"{renderer.GetType().Name}: Sum of vertical margin and padding is too large, there is no space for content", procName);
                return false;
            }

            if (width <= 0)
            {
                Logger.Error($"{renderer.GetType().Name}: Sum of horizontal margin and padding is too large, there is no space for content", procName);
                return false;
            }

            paddingBox = new BoxModel(x, y, width, height);
            return true;
        }

        public static bool TryCreatePaddingBox(BoxModel container, XSize textSize, PdfRendererBase renderer, out BoxModel paddingBox, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center)
        {
            var procName = $"LayoutHelper.{nameof(TryCreatePaddingBox)}";
            paddingBox = null;
            
            var layoutParam = renderer.GetLayoutParameter();
            MarginPaddingModel margin = layoutParam.Margin, padding = layoutParam.Padding;
            double x = container.X + margin.Left, y = container.Y + margin.Top;
            var width = textSize.Width + margin.Left + margin.Right + padding.Left + padding.Right;
            var height = textSize.Height + margin.Top + margin.Bottom + padding.Top + padding.Bottom;

            if (width > container.Width)
            {
                Logger.Error($"{renderer.GetType().Name}: Sum of horizontal margin and padding is too large, there is no space for content", procName);
                return false;
            }

            if (height > container.Height)
            {
                Logger.Error($"{renderer.GetType().Name}: Sum of vertical margin and padding is too large, there is no space for content", procName);
                return false;
            }

            if (horizontalAlignment == HorizontalAlignment.Center)
                x += container.Width / 2 - width / 2;
            else if (horizontalAlignment == HorizontalAlignment.Right)
                x += container.Width - width;
            
            y += container.Height / 2 - height / 2;
            width = width - margin.Left - margin.Right;
            height = height - margin.Top - margin.Bottom;

            paddingBox = new BoxModel(x, y, width, height);
            return true;
        }
        
        public static bool TryCreateContentBox(BoxModel container, int totalRows, int totalColumns, PdfRendererBase renderer, out BoxModel contentBox)
        {
            var procName = $"LayoutHelper.{nameof(TryCreateContentBox)}";
            contentBox = null;

            var dimension = CalcBoxDimension(container, totalRows, totalColumns);
            var layoutParam = renderer.GetLayoutParameter();
            var margin = layoutParam.Margin;
            var padding = layoutParam.Padding;

            var x = container.X + dimension[0] * layoutParam.Column + margin.Left + padding.Left;
            var y = container.Y + dimension[1] * layoutParam.Row + margin.Top + padding.Top;
            var height = dimension[1] * layoutParam.RowSpan - margin.Top - margin.Bottom - padding.Top - padding.Bottom;
            var width = dimension[0] * layoutParam.ColumnSpan - margin.Left - margin.Right - padding.Left - padding.Right;

            if (height <= 0)
            {
                Logger.Error($"{renderer.GetType().Name}: Sum of vertical margin and padding is too large, there is no space for content", procName);
                return false;
            }

            if (width <= 0)
            {
                Logger.Error($"{renderer.GetType().Name}: Sum of horizontal margin and padding is too large, there is no space for content", procName);
                return false;
            }

            contentBox = new BoxModel(x, y, width, height);
            return true;
        }

        public static bool TryCreateContentBox(BoxModel container, XSize textSize, PdfRendererBase renderer, out BoxModel contentBox, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center)
        {
            var procName = $"LayoutHelper.{nameof(TryCreateContentBox)}";
            contentBox = null;
            
            var layoutParam = renderer.GetLayoutParameter();
            MarginPaddingModel margin = layoutParam.Margin, padding = layoutParam.Padding;
            var x = container.X + margin.Left + padding.Left;
            var y = container.Y + margin.Top + padding.Top;
            var width = textSize.Width + margin.Left + margin.Right + padding.Left + padding.Right;
            var height = textSize.Height + margin.Top + margin.Bottom + padding.Top + padding.Bottom;

            if (width > container.Width)
            {
                Logger.Error($"{renderer.GetType().Name}: Sum of horizontal margin and padding is too large, there is no space for content", procName);
                return false;
            }

            if (height > container.Height)
            {
                Logger.Error($"{renderer.GetType().Name}: Sum of vertical margin and padding is too large, there is no space for content", procName);
                return false;
            }

            if (horizontalAlignment == HorizontalAlignment.Center)
                x += container.Width / 2 - width / 2;
            else if (horizontalAlignment == HorizontalAlignment.Right)
                x += container.Width - width;
            
            y += container.Height / 2 - height / 2;
            width = width - margin.Left - margin.Right - padding.Left - padding.Right;
            height = height - margin.Top - margin.Bottom - padding.Top - padding.Bottom;

            contentBox = new BoxModel(x, y, width, height);
            return true;
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

        public static BoxModel AdjustBoxLocation(BoxModel box, LayoutParameter layoutParam)
        {
            var procName = $"LayoutHelper.{nameof(AdjustBoxLocation)}";
            var position = layoutParam.Position;

            if (position == Position.Static)
            {
                Logger.Debug($"Position is {position}, no need to adjust box location", procName);
                return box;
            }
            else
            {
                var x = box.X + layoutParam.Left - layoutParam.Right;
                var y = box.Y + layoutParam.Top - layoutParam.Bottom;
                return new BoxModel(x, y, box.Width, box.Height);
            }
        }

        public static BoxModel AdjustBoxLocation(BoxModel box, Position position, double left, double right)
        {
            var procName = $"LayoutHelper.{nameof(AdjustBoxLocation)}";

            if (position == Position.Static)
            {
                Logger.Debug($"Position is {position}, no need to adjust box location", procName);
                return box;
            }
            else
            {
                var x = box.X + left - right;
                return new BoxModel(x, box.Y, box.Width, box.Height);
            }
        }

        public static BoxModel CreateContainer(XSize pageSize, PdfStructure location, Dictionary<PdfStructure, PdfStructureBase> pdfStructureList)
        {
            var structure = pdfStructureList[location];
            MarginPaddingModel margin = structure.Margin, padding = structure.Padding;

            double x = margin.Left + padding.Left, y = margin.Top + padding.Top;
            double width = pageSize.Width, height = 0;
            if (location == PdfStructure.PdfReportHeader || location == PdfStructure.PdfReportFooter)
            {
                var totalHeightRatio = pdfStructureList[PdfStructure.PdfReportHeader].HeightRatio +
                                       pdfStructureList[PdfStructure.PdfPageBody].HeightRatio +
                                       pdfStructureList[PdfStructure.PdfReportFooter].HeightRatio;

                if (location == PdfStructure.PdfReportHeader)
                {
                    height = pageSize.Height * pdfStructureList[PdfStructure.PdfReportHeader].HeightRatio / totalHeightRatio;
                }
                else
                {
                    height = pageSize.Height * pdfStructureList[PdfStructure.PdfReportFooter].HeightRatio / totalHeightRatio;
                    y = pageSize.Height - height;
                }
            }
            else if (location == PdfStructure.PdfPageHeader || location == PdfStructure.PdfPageFooter)
            {
                var totalHeightRatio = pdfStructureList[PdfStructure.PdfPageHeader].HeightRatio +
                                       pdfStructureList[PdfStructure.PdfPageBody].HeightRatio +
                                       pdfStructureList[PdfStructure.PdfPageFooter].HeightRatio;

                if (location == PdfStructure.PdfPageHeader)
                {
                    height = pageSize.Height * pdfStructureList[PdfStructure.PdfPageHeader].HeightRatio / totalHeightRatio;
                }
                else
                {
                    height = pageSize.Height * pdfStructureList[PdfStructure.PdfPageFooter].HeightRatio / totalHeightRatio;
                    y = pageSize.Height - height;
                }
            }

            width = width - margin.Left - margin.Right - padding.Left - padding.Right;
            height = height - margin.Top - margin.Bottom - padding.Top - padding.Bottom;
            return new BoxModel(x, y, width, height);
        }

        public static double CalcPdfStructureHeight(XSize pageSize, PdfStructure location, Dictionary<PdfStructure, PdfStructureBase> pdfStructureList)
        {
            double height = 0;

            if (location == PdfStructure.PdfReportHeader || location == PdfStructure.PdfReportFooter)
            {
                var totalHeightRatio = pdfStructureList[PdfStructure.PdfReportHeader].HeightRatio +
                                       pdfStructureList[PdfStructure.PdfPageBody].HeightRatio +
                                       pdfStructureList[PdfStructure.PdfReportFooter].HeightRatio;

                if (location == PdfStructure.PdfReportHeader)
                {
                    height = pageSize.Height * pdfStructureList[PdfStructure.PdfReportHeader].HeightRatio / totalHeightRatio;
                }
                else
                {
                    height = pageSize.Height * pdfStructureList[PdfStructure.PdfReportFooter].HeightRatio / totalHeightRatio;
                }
            }
            else if (location == PdfStructure.PdfPageHeader || location == PdfStructure.PdfPageFooter)
            {
                var totalHeightRatio = pdfStructureList[PdfStructure.PdfPageHeader].HeightRatio +
                                       pdfStructureList[PdfStructure.PdfPageBody].HeightRatio +
                                       pdfStructureList[PdfStructure.PdfPageFooter].HeightRatio;

                if (location == PdfStructure.PdfPageHeader)
                {
                    height = pageSize.Height * pdfStructureList[PdfStructure.PdfPageHeader].HeightRatio / totalHeightRatio;
                }
                else
                {
                    height = pageSize.Height * pdfStructureList[PdfStructure.PdfPageFooter].HeightRatio / totalHeightRatio;
                }
            }

            return height;
        }

        public static List<string> AllocateWords(string text, double widthPerLetter, double containerWidth)
        {
            var words = text.Split(' ');
            var res = new List<string>();

            if (words.Any(x => x.Length * widthPerLetter > containerWidth))
            {
                var lettersPerLine = (int)(containerWidth / widthPerLetter);
                var lines = Math.Ceiling((double)text.Length / lettersPerLine);
                var index = 0;
                for (int i = 0; i < lines; i++)
                {
                    res.Add(text.Substring(index, Math.Min(lettersPerLine, text.Length - index)));
                    index += lettersPerLine;
                }
            }
            else
            {
                var textPerLine = new StringBuilder();
                foreach (var word in words)
                {
                    if ((textPerLine.Length + word.Length) * widthPerLetter <= containerWidth)
                        textPerLine.Append($"{word} ");
                    else
                    {
                        res.Add(textPerLine.ToString().Trim());
                        textPerLine = new StringBuilder($"{word} ");
                    }
                }
                res.Add(textPerLine.ToString().Trim());
            }

            return res;
        }

        #region Helper

        private static double[] CalcBoxDimension(BoxModel container, int totalRows, int totalColumns)
        {
            var width = container.Width / totalColumns;
            var height = container.Height / totalRows;
            return new[] { width, height };
        }

        #endregion
    }
}