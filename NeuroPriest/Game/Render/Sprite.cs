namespace NeuroPriest.Render
{
    internal class Sprite
    {
        public char Write { get; set; }
        public int Style { get; set; }

        public Sprite(char write, int style)
        {
            Write = write;
            Style = style;
        }
    }
}
