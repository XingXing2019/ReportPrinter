using System;
using System.IO;
using RaphaelLibrary.Code.Common;
using NUnit.Framework;

namespace ReportPrinterUnitTest.RaphaelLibrary.Common
{
    public class FileHelperTest
    {
        [Test]
        public void TestManipulateDirectory()
        {
            var path = CreatePath();

            try
            {
                var isExist = FileHelper.DirectoryExists(path);
                Assert.IsFalse(isExist);

                FileHelper.CreateDirectory(path);
                isExist = FileHelper.DirectoryExists(path);
                Assert.IsTrue(isExist);

                FileHelper.DeleteDirectory(path);
                isExist = FileHelper.DirectoryExists(path);
                Assert.IsFalse(isExist);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void TestCreateFile()
        {
            var path = CreatePath();
            var expectedData = "This is create file test";

            FileHelper.CreateFile(path, expectedData);

            try
            {
                var isExist = File.Exists(path);
                Assert.IsTrue(isExist);

                var actualData = File.ReadAllText(path);
                Assert.AreEqual(expectedData, actualData);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                FileHelper.DeleteDirectory(path);
            }
        }


        #region Helper

        private string CreatePath()
        {
            var tempFolder = Path.GetTempPath();
            var subFolder = "temp";
            var file = "temp.txt";
            var path = Path.Combine(tempFolder, subFolder, file);

            return path;
        }

        #endregion
    }
}