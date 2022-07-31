using System.Collections.Generic;
using RaphaelLibrary.Code.Render.Label.Helper;

namespace RaphaelLibrary.Code.Render.Label.Renderer
{
    public class LabelValidationRenderer : LabelRendererBase
    {
        public LabelValidationRenderer(int lineIndex) : base(lineIndex) { }

        protected override bool TryCreatePlaceHolders(LabelDeserializeHelper deserializer, HashSet<string> placeholders)
        {
            throw new System.NotImplementedException();
        }
    }
}