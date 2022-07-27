using System;

namespace RaphaelLibrary.Code.Render.Label.Manager
{
    public class LabelManager
    {
        public string[] Lines { get; }
        public Guid MessageId { get; }

        public LabelManager(string[] lines, Guid messageId)
        {
            Lines = lines;
            MessageId = messageId;
        }
    }
}