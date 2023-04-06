using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ReportPrinterLibrary.Code.Winform.Helper
{
    public class ConfigPreviewHelper
    {
        public static string GeneratePreview(object obj)
        {
            var type = obj.GetType();
            var serializer = new XmlSerializer(type);

            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true, 
                Indent = true, 
                IndentChars = "  ",

            };

            using var stringWriter = new StringWriter();
            using var writer = XmlWriter.Create(stringWriter, settings);

            var xmlns = new XmlSerializerNamespaces();
            xmlns.Add(string.Empty, string.Empty);
            serializer.Serialize(writer, obj, xmlns);
            return stringWriter.ToString();
        }
    }
}