using RaphaelLibrary.Code.Init.Label;
using RaphaelLibrary.Code.Render.Label.Renderer;
using ReportPrinterLibrary.Code.Enum;

namespace RaphaelLibrary.Code.Render.Label.Model
{
    public class ValidationModel
    {
        public ValidationType ValidationType { get; }
        public Comparator Comparator { get; }
        public string ExpectedValue { get; }
        public string TrueContent { get; }
        public string FalseContent { get; }
        public IStructure TrueStructure { get; }
        public IStructure FalseStructure { get; }

        public ValidationModel(ValidationType validationType, Comparator comparator, string expectedValue, string trueContent, string falseContent)
        {
            ValidationType = validationType;
            Comparator = comparator;
            ExpectedValue = expectedValue;
            TrueContent = trueContent;
            FalseContent = falseContent;
        }

        public ValidationModel(ValidationType validationType, Comparator comparator, string expectedValue, IStructure trueStructure, IStructure falseStructure)
        {
            ValidationType = validationType;
            Comparator = comparator;
            ExpectedValue = expectedValue;
            TrueStructure = trueStructure;
            FalseStructure = falseStructure;
        }
    }
}