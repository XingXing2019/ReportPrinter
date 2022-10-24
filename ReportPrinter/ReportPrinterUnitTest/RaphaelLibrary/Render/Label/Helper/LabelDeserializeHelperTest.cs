using System;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;
using RaphaelLibrary.Code.Render.Label.Helper;

namespace ReportPrinterUnitTest.RaphaelLibrary.Render.Label.Helper
{
    public class LabelDeserializeHelperTest
    {
        [Test]
        [TestCase(true)]
        [TestCase(false, "AddStartSign")]
        [TestCase(false, "AddEndSign")]
        [TestCase(false, "InsertStartSign")]
        [TestCase(false, "InsertEndSign")]
        [TestCase(false, "RemoveStartSign")]
        [TestCase(false, "RemoveEndSign")]
        public void TestTryGetLines(bool expectedRes, string operation = "")
        {
            var filePath = @".\RaphaelLibrary\Render\Label\Helper\TestFile\ValidStructure.txt";
            var input = File.ReadAllLines(filePath);
            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);

            if (!expectedRes)
            {
                input = input[2..];

                if (operation == "AddStartSign")
                    input[0] += LabelElementHelper.S_RENDERER_START;
                else if (operation == "AddEndSign")
                    input[^1] += LabelElementHelper.S_RENDERER_END;
                else if (operation == "InsertStartSign")
                    input[1] += LabelElementHelper.S_RENDERER_START;
                else if (operation == "InsertEndSign")
                    input[1] += LabelElementHelper.S_RENDERER_END;
                else if (operation == "RemoveStartSign")
                    input = input[1..];
                else if (operation == "RemoveEndSign")
                    input = input[..^1];
            }

            try
            {
                var actualRes = deserializer.TryGetLines(input, out var output);
                Assert.AreEqual(expectedRes, actualRes);

                if (expectedRes)
                {
                    Assert.AreEqual(3, output.Length);

                    var line1 = "^A0N,41,40^FO41,41^FD%%%<Sql SqlResColumn=\"DeliveryToName\" SqlTemplateId=\"PrintLabelQuery\" SqlId=\"FullCaseContainer\" />%%%^FS";
                    Assert.AreEqual(line1, output[0]);

                    var line2 = "%%%<Timestamp Mask=\"dd-MM-yyyy\" IsUTC=\"true\"/>%%%";
                    Assert.AreEqual(line2, output[1]);

                    var line3 = $"%%%<Validation\n\tType=\"Structure\"\n\tComparator=\"Equals\"\n\tExpectedValue=\"500024\"\n\tSqlTemplateId=\"PrintLabelQuery\"" +
                                $"\n\tSqlId=\"SplitCaseContainer\"\n\tSqlResColumn=\"ContainerID\"\n\tTrueStructure=\"DeliveryInfoBody\"\n\tFalseStructure=\"DeliveryInfoFooter\"\n/>%%%";
                    Assert.AreEqual(line3, output[2]);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        [TestCase(true, LabelElementHelper.S_TIMESTAMP)]
        [TestCase(true, LabelElementHelper.S_REFERENCE)]
        [TestCase(false, LabelElementHelper.S_TIMESTAMP, "AddStartSign")]
        [TestCase(false, LabelElementHelper.S_TIMESTAMP, "AddEndSign")]
        [TestCase(false, LabelElementHelper.S_TIMESTAMP, "RemoveEndSign")]
        public void TestTryGetPlaceHolders(bool expectedRes, string placeHolderName, string operation = "")
        {
            var line = "%%%<Timestamp Mask=\"dd-MM-yyyy\" IsUTC=\"true\"/>%%% " +
                       "%%%<Timestamp />%%% " +
                       "%%%<Reference StructureId=\"DeliveryInfoHeader\"/>%%%";

            var placeHolderEnd = LabelElementHelper.S_END;
            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);

            if (!expectedRes)
            {
                line = "%%%<Timestamp Mask=\"dd-MM-yyyy\" IsUTC=\"true\"/>%%%";

                if (operation == "AddStartSign")
                {
                    line = LabelElementHelper.S_TIMESTAMP_RENDERER + line;
                }
                else if (operation == "AddEndSign")
                {
                    line += LabelElementHelper.S_RENDERER_END;
                }
                else if (operation == "RemoveStartSign")
                {
                    line = line.Substring(LabelElementHelper.S_TIMESTAMP_RENDERER.Length);
                }
                else if (operation == "RemoveEndSign")
                {
                    line = line.Substring(0, line.Length - LabelElementHelper.S_RENDERER_END.Length);
                }
            }

            try
            {
                var actualRes = deserializer.TryGetPlaceHolders(line, placeHolderName, placeHolderEnd, out var placeholders);
                Assert.AreEqual(expectedRes, actualRes);

                if (expectedRes)
                {
                    if (placeHolderName == LabelElementHelper.S_TIMESTAMP)
                    {
                        Assert.AreEqual(2, placeholders.Count);
                        Assert.IsTrue(placeholders.Contains("%%%<Timestamp Mask=\"dd-MM-yyyy\" IsUTC=\"true\"/>%%%"));
                        Assert.IsTrue(placeholders.Contains("%%%<Timestamp />%%%"));
                    }
                    else if (placeHolderName == LabelElementHelper.S_REFERENCE)
                    {
                        Assert.AreEqual(1, placeholders.Count);
                        Assert.IsTrue(placeholders.Contains("%%%<Reference StructureId=\"DeliveryInfoHeader\"/>%%%"));
                    }
                    else if (placeHolderName == LabelElementHelper.S_VALIDATION)
                    {
                        Assert.AreEqual(0, placeholders.Count);
                    }
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}