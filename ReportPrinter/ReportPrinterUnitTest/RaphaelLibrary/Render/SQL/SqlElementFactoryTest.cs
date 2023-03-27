using System;
using NUnit.Framework;
using RaphaelLibrary.Code.Init.SQL;
using RaphaelLibrary.Code.Render.SQL;

namespace ReportPrinterUnitTest.RaphaelLibrary.Render.SQL
{
    public class SqlElementFactoryTest
    {
        [Test]
        [TestCase("Sql", typeof(Sql))]
        [TestCase("SqlTemplate", typeof(SqlTemplate))]
        [TestCase("InvalidName", null)]
        public void TestCreateSqlElement(string name, Type expectedType)
        {
            try
            {
                var sqlElement = SqlElementFactory.CreateSqlElement(name);
                Assert.AreEqual(expectedType, sqlElement.GetType());
            }
            catch (InvalidOperationException ex)
            {
                var expectedError = $"Invalid name: {name} for sql element";
                Assert.AreEqual(expectedError, ex.Message);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}