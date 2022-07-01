using System.Xml;

namespace RaphaelLibrary.Code.Common
{
    public interface IXmlReader
    {
        bool ReadXml(XmlNode node);
    }
}