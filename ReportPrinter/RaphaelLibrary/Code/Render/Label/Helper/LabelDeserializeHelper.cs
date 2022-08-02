using System;
using System.Collections.Generic;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Render.Label.Helper
{
    public class LabelDeserializeHelper
    {
        private readonly string _quoteSign;
        private readonly Dictionary<string, string> _labelRenderer;

        public LabelDeserializeHelper(string quoteSign, Dictionary<string, string> labelRenderer)
        {
            _quoteSign = quoteSign;
            _labelRenderer = labelRenderer;
        }

        public bool TryGetLines(string[] input, out string[] output)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryGetLines)}";
            output = null;

            var startSign = _labelRenderer[LabelElementHelper.S_START];
            var endSign = _labelRenderer[LabelElementHelper.S_END];

            try
            {
                var lines = new List<string>();
                int li = 0, hi = 0;

                while (hi < input.Length)
                {
                    var line = input[hi];

                    var startSignCount = CountSign(line, startSign);
                    var endSignCount = CountSign(line, endSign);

                    if (startSignCount == 0 && endSignCount == 0)
                    {
                        lines.Add(line);
                        li = ++hi;
                        continue;
                    }

                    if (startSignCount < endSignCount)
                    {
                        Logger.Error($"Unpaired start and end sign detected in line: {line}", procName);
                        return false;
                    }

                    while (startSignCount != endSignCount)
                    {
                        hi++;
                        if (hi >= input.Length)
                        {
                            Logger.Error($"Unpaired start and end sign detected in line: {line}", procName);
                            return false;
                        }

                        line = input[hi];
                        startSignCount += CountSign(line, startSign);
                        endSignCount += CountSign(line, endSign);
                    }

                    lines.Add(string.Join(LabelElementHelper.S_NEW_LINE, input, li, hi - li + 1));
                    li = ++hi;
                }

                output = lines.ToArray();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Unable to get line from input. Ex: {ex.Message}", procName);
                return false;
            }
        }

        public bool TryGetPlaceHolders(string input, string placeHolderName, string placeHolderEnd, out HashSet<string> placeholders)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryGetPlaceHolders)}";
            placeholders = new HashSet<string>();

            var startSign = _labelRenderer[placeHolderName];
            var endSign = _labelRenderer[placeHolderEnd];

            var startIndexes = new List<int>();
            var endIndexes = new List<int>();
            int startPos = 0, endPos = 0;
            
            try
            {
                while (input.IndexOf(startSign, startPos, StringComparison.Ordinal) != -1 && 
                       input.IndexOf(endSign, endPos, StringComparison.Ordinal) != -1)
                {
                    var startIndex = input.IndexOf(startSign, startPos, StringComparison.Ordinal);
                    var endIndex = input.IndexOf(endSign, startIndex, StringComparison.Ordinal);

                    startIndexes.Add(startIndex);
                    endIndexes.Add(endIndex);

                    startPos = startIndex + startSign.Length;
                    endPos = endIndex + endSign.Length;
                }

                if (startIndexes.Count != endIndexes.Count)
                {
                    Logger.Error($"Unpaired start sign and end sign for place holder: {placeHolderName} detected from input: {input}", procName);
                    return false;
                }

                for (int i = 0; i < startIndexes.Count; i++)
                {
                    var placeHolder = input.Substring(startIndexes[i], endIndexes[i] - startIndexes[i] + endSign.Length);
                    placeholders.Add(placeHolder);
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Unable to get place holders for {placeholders} from input: {input}. Ex: {ex.Message}", procName);
                return false;
            }
        }

        public bool TryGetValue(string input, string name, out string value)
        {
            var procName = $"{this.GetType().Name}.{nameof(TryGetValue)}";
            value = string.Empty;

            try
            {
                var nameIndex = input.IndexOf(name, StringComparison.Ordinal);
                var startQuote = input.IndexOf(_quoteSign, nameIndex, StringComparison.Ordinal);
                var endQuote = input.IndexOf(_quoteSign, startQuote + 1, StringComparison.Ordinal);
                value = input.Substring(startQuote + 1, endQuote - startQuote - 1);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Unable to get value of {name} from input: {input}. Ex: {ex.Message}", procName);
                return false;
            }
        }

        public bool ContainsPlaceholder(string input, string placeholder)
        {
            var index = input.IndexOf(placeholder, StringComparison.Ordinal);
            if (index == -1)
                return false;

            return input[index + placeholder.Length] == ' ' || input[index + placeholder.Length] == '\n';
        }

        #region Helper

        private int CountSign(string line, string sign)
        {
            int count = 0, start = 0;
            while (line.IndexOf(sign, start, StringComparison.Ordinal) >= 0)
            {
                count++;
                var index = line.IndexOf(sign, start, StringComparison.Ordinal);
                start = index + sign.Length;
            }
            return count;
        }

        #endregion
    }
}