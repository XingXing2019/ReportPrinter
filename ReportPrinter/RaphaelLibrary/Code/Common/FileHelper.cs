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
    }
}