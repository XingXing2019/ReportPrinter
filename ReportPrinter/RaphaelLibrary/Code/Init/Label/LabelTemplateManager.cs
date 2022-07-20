using System.Xml;
using RaphaelLibrary.Code.Common;

namespace RaphaelLibrary.Code.Init.Label
{
    public class LabelTemplateManager : TemplateManagerBase, IXmlReader
    {
        private static readonly object _lock = new object();
        
        private static LabelTemplateManager _instance;
        public static LabelTemplateManager Instances
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new LabelTemplateManager();
                        }
                    }
                }

                return _instance;
            }
        }
        
        private LabelTemplateManager() { }

        public bool ReadXml(XmlNode node)
        {
            throw new System.NotImplementedException();
        }
    }
}