using System.IO;

namespace RaphaelLibrary.Code.Common
{
    public class FileHelper
    {
        public static bool DirectoryExists(string path)
        {
            var dir = path.Substring(0, path.LastIndexOf('\\'));
            return Directory.Exists(dir);
        }

        public static void CreateDirectory(string path)
        {
            var dir = path.Substring(0, path.LastIndexOf('\\'));
            Directory.CreateDirectory(dir);
        }

        public static void DeleteDirectory(string path)
        {
            var dir = path.Substring(0, path.LastIndexOf('\\'));
            Directory.Delete(dir);
        }

        public static void CreateFile(string filePath, string data)
        {
            File.WriteAllText(filePath, data);
        }
    }
}