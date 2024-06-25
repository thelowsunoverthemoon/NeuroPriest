using System.Collections.Generic;

namespace NeuroPriest.Render
{
    internal class AnimSynchronizer
    {
        public List<Frame> Frames { get; set; }
        private int CurIndex { get; set; }
        public bool HasAnim => Frames != null;
        TextWriter TextWriter { get; }
        public Frame CurFrame => Frames[CurIndex];

        public AnimSynchronizer(TextWriter textWriter)
        {
            TextWriter = textWriter;
        }

        public void Start(List<Frame> frames)
        {
            Frames = frames;
        }

        public void Next()
        {
            CurIndex++;
            if (CurIndex == Frames.Count)
            {
                CurIndex = 0;
                TextWriter.UseSaved();
                TextWriter.ClearSaved();
                Frames = null;
            }
        }
    }
}
