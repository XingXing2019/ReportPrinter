using System.Xml;
using RaphaelLibrary.Code.Common;

namespace RaphaelLibrary.Code.Init.Label
{
    public class LabelStructureManager : IXmlReader
    {
        private static readonly object _lock = new object();

        private static LabelStructureManager _instance;
        public static LabelStructureManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new LabelStructureManager();
                        }
                    }
                }

                return _instance;
            }
        }

        private LabelStructureManager() { }

        public bool ReadXml(XmlNode node)
        {
            throw new System.NotImplementedException();
        }
    }
}