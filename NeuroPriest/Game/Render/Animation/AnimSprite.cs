using NeuroPriest.Shared;

namespace NeuroPriest.Render
{
    internal class AnimSprite
    {
        public Sprite Sprite { get; set; }
        public Coord Pos { get; set; }

        public AnimSprite(int x, int y, Sprite sprite)
        {
            Sprite = sprite;
            Pos = new Coord(x, y);
        }

        public AnimSprite(AnimSprite animSprite)
        {
            Sprite = animSprite.Sprite;
            Pos = new Coord(animSprite.Pos.X, animSprite.Pos.Y);
        }
    }
}
