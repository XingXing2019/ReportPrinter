using System;
using System.Threading.Tasks;
using NUnit.Framework;
using RaphaelLibrary.Code.Init.Label;
using RaphaelLibrary.Code.Render.Label.Helper;
using RaphaelLibrary.Code.Render.Label.Model;
using RaphaelLibrary.Code.Render.Label.PlaceHolder;
using RaphaelLibrary.Code.Render.Label.Renderer;
using RaphaelLibrary.Code.Render.PDF.Model;
using ReportPrinterDatabase.Code.Manager.MessageManager.PrintReportMessage;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest.RaphaelLibrary.Render.Label.PlaceHolder
{
    public class ValidationPlaceHolderTest : PlaceHolderTestBase
    {
        [Test]
        [TestCaseSource(nameof(TryReplacePlaceHolderTestCases))]
        public async Task TestTryReplacePlaceHolder(bool expectedRes, string expectedValue, string resColumn, ValidationModel model)
        {
            var filePath = @".\RaphaelLibrary\Render\Label\PlaceHolder\TestFile\SqlTemplate.xml";
            var message = CreateMessage(ReportTypeEnum.PDF);
            var manager = CreateLabelManager(S_PLACE_HOLDER, message.MessageId);

            var sqlTemplate = await SetupSqlTest(filePath, message, expectedRes);
            var isSuccess = sqlTemplate.TryGetSql("TestSqlPlaceHolder", out var sql);
            Assert.IsTrue(isSuccess);

            try
            {
                var sqlResColumn = new SqlResColumn(resColumn);
                var validationPlaceHolder = new ValidationPlaceHolder(S_PLACE_HOLDER, sql, sqlResColumn, model);

                var actualRes = validationPlaceHolder.TryReplacePlaceHolder(manager, 0);
                Assert.AreEqual(expectedRes, actualRes);

                if (expectedRes)
                {
                    Assert.AreEqual(expectedValue, manager.Lines[0]);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                await new PrintReportMessageEFCoreManager().DeleteAll();
            }
        }

        [Test]
        public async Task TestClone()
        {
            var filePath = @".\RaphaelLibrary\Render\Label\PlaceHolder\TestFile\SqlTemplate.xml";
            var message = CreateMessage(ReportTypeEnum.PDF);

            var sqlTemplate = await SetupSqlTest(filePath, message, false);
            var isSuccess = sqlTemplate.TryGetSql("TestSqlPlaceHolder", out var sql);
            Assert.IsTrue(isSuccess);

            var trueContent = "TrueContent";
            var falseContent = "FalseContent";
            var model = new ValidationModel(ValidationType.Text, Comparator.NotEquals, "3", trueContent, falseContent);

            try
            {
                var sqlResColumn = new SqlResColumn("Dummy");
                var validationPlaceHolder = new ValidationPlaceHolder(S_PLACE_HOLDER, sql, sqlResColumn, model);

                var cloned = validationPlaceHolder.Clone();
                AssertObject(validationPlaceHolder, cloned);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        private static object[] TryReplacePlaceHolderTestCases()
        {
            var trueContent = "TrueContent";
            var falseContent = "FalseContent";
            
            var deserializer = new LabelDeserializeHelper(LabelElementHelper.S_DOUBLE_QUOTE, LabelElementHelper.LABEL_RENDERER);

            var trueStructurePath = @".\RaphaelLibrary\Render\Label\PlaceHolder\TestFile\TrueLabelStructure.txt";
            var trueStructure = new LabelStructure("TrueStructureId", deserializer, LabelElementHelper.LABEL_RENDERER);
            trueStructure.ReadFile(trueStructurePath);
            var trueValue = $"{DateTime.UtcNow:dd-MM-yyyy}\r\n";

            var falseStructurePath = @".\RaphaelLibrary\Render\Label\PlaceHolder\TestFile\FalseLabelStructure.txt";
            var falseStructure = new LabelStructure("FalseStructureId", deserializer, LabelElementHelper.LABEL_RENDERER);
            falseStructure.ReadFile(falseStructurePath);
            var falseValue = "DummyId\r\n";
            
            return new object[]
            {
                new object[]
                {
                    true, "TrueContent", "PRM_Status", new ValidationModel(ValidationType.Text, Comparator.Equals, "Publish", trueContent, falseContent)
                },
                new object[]
                {
                    true, "FalseContent", "PRM_Status", new ValidationModel(ValidationType.Text, Comparator.Equals, "Complete", trueContent, falseContent)
                },
                new object[]
                {
                    true, "TrueContent", "PRM_NumberOfCopy", new ValidationModel(ValidationType.Text, Comparator.Greater, "1", trueContent, falseContent)
                },
                new object[]
                {
                    true, "TrueContent", "PRM_NumberOfCopy", new ValidationModel(ValidationType.Text, Comparator.GreaterOrEquals, "3", trueContent, falseContent)
                },
                new object[]
                {
                    true, "FalseContent", "PRM_NumberOfCopy", new ValidationModel(ValidationType.Text, Comparator.Less, "3", trueContent, falseContent)
                },
                new object[]
                {
                    true, "TrueContent", "PRM_NumberOfCopy", new ValidationModel(ValidationType.Text, Comparator.LessOrEquals, "3", trueContent, falseContent)
                },
                new object[]
                {
                    true, "FalseContent", "PRM_NumberOfCopy", new ValidationModel(ValidationType.Text, Comparator.Equals, "4", trueContent, falseContent)
                },
                new object[]
                {
                    true, "FalseContent", "PRM_NumberOfCopy", new ValidationModel(ValidationType.Text, Comparator.NotEquals, "3", trueContent, falseContent)
                },

                new object[]
                {
                    true, trueValue, "PRM_Status", new ValidationModel(ValidationType.Structure, Comparator.Equals, "Publish", trueStructure, falseStructure)
                },
                new object[]
                {
                    true, falseValue, "PRM_Status", new ValidationModel(ValidationType.Structure, Comparator.Equals, "Complete", trueStructure, falseStructure)
                },
                new object[]
                {
                    true, trueValue, "PRM_NumberOfCopy", new ValidationModel(ValidationType.Structure, Comparator.Greater, "1", trueStructure, falseStructure)
                },
                new object[]
                {
                    true, trueValue, "PRM_NumberOfCopy", new ValidationModel(ValidationType.Structure, Comparator.GreaterOrEquals, "3", trueStructure, falseStructure)
                },
                new object[]
                {
                    true, falseValue, "PRM_NumberOfCopy", new ValidationModel(ValidationType.Structure, Comparator.Less, "3", trueStructure, falseStructure)
                },
                new object[]
                {
                    true, trueValue, "PRM_NumberOfCopy", new ValidationModel(ValidationType.Structure, Comparator.LessOrEquals, "3", trueStructure, falseStructure)
                },
                new object[]
                {
                    true, falseValue, "PRM_NumberOfCopy", new ValidationModel(ValidationType.Structure, Comparator.Equals, "4", trueStructure, falseStructure)
                },
                new object[]
                {
                    true, falseValue, "PRM_NumberOfCopy", new ValidationModel(ValidationType.Structure, Comparator.NotEquals, "3", trueStructure, falseStructure)
                },

                new object[]
                {
                    false, string.Empty, "PRM_NumberOfCopy", new ValidationModel(ValidationType.Structure, Comparator.NotEquals, "3", trueStructure, falseStructure)
                },
            };
        } 
    }
}