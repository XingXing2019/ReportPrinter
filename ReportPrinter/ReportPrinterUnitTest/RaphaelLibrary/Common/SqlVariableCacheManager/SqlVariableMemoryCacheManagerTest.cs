﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using RaphaelLibrary.Code.Common.SqlVariableCacheManager;
using ReportPrinterLibrary.Code.RabbitMQ.Message.PrintReportMessage;

namespace ReportPrinterUnitTest.RaphaelLibrary.Common.SqlVariableCacheManager
{
    public class SqlVariableMemoryCacheManagerTest : TestBase
    {
        private readonly string _fieldName = "_sqlVariableRepo";
        private readonly Guid _messageId = Guid.NewGuid();
        private readonly Dictionary<string, SqlVariable> _expectedVariables;
        private readonly Dictionary<Guid, Dictionary<string, SqlVariable>> _variableRepo;

        public SqlVariableMemoryCacheManagerTest()
        {
            _expectedVariables = GenerateSqlVariables();
            _variableRepo = GetSqlVariableRepo();
        }

        [Test]
        public void TestStoreSqlVariables()
        {
            try
            {
                SqlVariableMemoryCacheManager.Instance.StoreSqlVariables(_messageId, _expectedVariables);

                Assert.AreEqual(1, _variableRepo.Count);
                Assert.IsTrue(_variableRepo.ContainsKey(_messageId));
                var actualVariables = _variableRepo[_messageId];

                AssertSqlVariables(_expectedVariables, actualVariables);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                _variableRepo.Clear();
            }
        }

        [Test]
        public void TestGetSqlVariables()
        {
            try
            {
                SqlVariableMemoryCacheManager.Instance.StoreSqlVariables(_messageId, _expectedVariables);
                Assert.AreEqual(1, _variableRepo.Count);
                var actualVariables = SqlVariableMemoryCacheManager.Instance.GetSqlVariables(_messageId);
                AssertSqlVariables(_expectedVariables, actualVariables);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                _variableRepo.Clear();
            }
        }

        [Test]
        public void TestRemoveSqlVariables()
        {
            try
            {
                SqlVariableMemoryCacheManager.Instance.StoreSqlVariables(_messageId, _expectedVariables);
                Assert.AreEqual(1, _variableRepo.Count);
                SqlVariableMemoryCacheManager.Instance.RemoveSqlVariables(_messageId);
                Assert.AreEqual(0, _variableRepo.Count);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                _variableRepo.Clear();
            }
        }


        #region Helper

        private Dictionary<Guid, Dictionary<string, SqlVariable>> GetSqlVariableRepo()
        {
            return GetPrivateField<Dictionary<Guid, Dictionary<string, SqlVariable>>>(SqlVariableMemoryCacheManager.Instance, _fieldName);
        }

        private Dictionary<string, SqlVariable> GenerateSqlVariables()
        {
            var sqlVariables = new Dictionary<string, SqlVariable>
            {
                { "Variable1", new SqlVariable { Name = "Varable1", Value = "Value1" } },
                { "Variable2", new SqlVariable { Name = "Varable2", Value = "Value2" } },
                { "Variable3", new SqlVariable { Name = "Varable3", Value = "Value3" } },
            };

            return sqlVariables;
        }

        private void AssertSqlVariables(Dictionary<string, SqlVariable> expectedVariables, Dictionary<string, SqlVariable> actualVariables)
        {
            Assert.AreSame(expectedVariables, actualVariables);
            Assert.AreEqual(expectedVariables.Count, actualVariables.Count);

            foreach (var name in expectedVariables.Keys)
            {
                Assert.IsTrue(actualVariables.ContainsKey(name));

                var expectedVariable = expectedVariables[name];
                var actualVariable = actualVariables[name];

                Assert.AreEqual(expectedVariable.Name, actualVariable.Name);
                Assert.AreEqual(expectedVariable.Value, actualVariable.Value);
            }
        }

        #endregion
    }
}