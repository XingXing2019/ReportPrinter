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
        public void TestCreateSqlElement(string name, Type expectedType)
        {
            try
            {
                var actualType = SqlElementFactory.CreateSqlElement(name);
                Assert.AreEqual(expectedType, actualType.GetType());
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}