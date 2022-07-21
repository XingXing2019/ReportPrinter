namespace RaphaelLibrary.Code.Init.Label
{
    public class LabelStructure : IStructure
    {
        public string Id { get; private set; }

        public LabelStructure(string id)
        {
            Id = id;
        }
        
        public IStructure Clone()
        {
            throw new System.NotImplementedException();
        }

        public bool ReadFile(string filePath)
        {
            throw new System.NotImplementedException();
        }

        public bool TryCreateLabelStructure()
        {
            throw new System.NotImplementedException();
        }
    }
}