using System.IO;

namespace RaphaelLibrary.Code.Common
{
    public class FileHelper
    {
        public static bool DirectoryExists(string path)
        {
            var dir = Path.GetDirectoryName(path);
            return Directory.Exists(dir);
        }

        public static void CreateDirectory(string path)
        {
            var dir = Path.GetDirectoryName(path);
            Directory.CreateDirectory(dir);
        }

        public static void DeleteDirectory(string path)
        {
            var dir = Path.GetDirectoryName(path);
            Directory.Delete(dir, true);
        }

        public static void CreateFile(string filePath, string data)
        {
            if (!DirectoryExists(filePath))
                CreateDirectory(filePath);
            File.WriteAllText(filePath, data);
        }
    }
}