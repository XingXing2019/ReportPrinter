using System;
using System.Text;

namespace RaphaelLibrary.Code.Init.Label
{
    public interface IStructure
    {
        public IStructure Clone();
        public bool ReadFile(string filePath);
        public bool TryCreateLabelStructure(Guid messageId, out StringBuilder lines);
    }
}