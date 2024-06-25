namespace NeuroPriest.Render
{
    internal partial class ColourWriter
    {
        private struct ColourCell
        {
            public int Pos { get; }
            public int Len { get; }
            public int Style { get; }

            public ColourCell(int pos, int style, int len)
            {
                Pos = pos;
                Len = len;
                Style = style;
            }
        }
    }
}
