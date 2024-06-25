using System.Collections.Generic;

namespace NeuroPriest.Render
{
    internal class Frame
    {
        public List<AnimSprite> Sprites { get; set; }
        public int Time { get; set; }

        public Frame(List<AnimSprite> sprites, int time)
        {
            Sprites = sprites;
            Time = time;
        }
    }
}
