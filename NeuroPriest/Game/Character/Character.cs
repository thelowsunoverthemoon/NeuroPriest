using NeuroPriest.Shared;

namespace NeuroPriest.Characters
{
    internal abstract class Character : StaticSprite
    {
        public Coord Pos { get; set; }
        public Coord Prev { get; set; }

        public int Style { get; }

        protected Character(Coord pos)
        {
            Prev = new Coord(0, 0);
            Pos = pos;
        }

        public void SetPrev()
        {
            Prev.X = Pos.X;
            Prev.Y = Pos.Y;
        }

        public void SetPos(Coord pos)
        {
            Pos.X = pos.X;
            Pos.Y = pos.Y;
        }
    }
}
