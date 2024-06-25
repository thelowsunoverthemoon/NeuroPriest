using NeuroPriest.Characters;
using NeuroPriest.Maps;
using System.Collections.Generic;

namespace NeuroPriest.Render
{
    internal static class AnimHelpers
    {
        public static void SetPositionAroundPlayer(Frame frame, Player player)
        {
            frame.Sprites[0].Pos.X = player.Pos.X;
            frame.Sprites[0].Pos.Y = player.Pos.Y - 1;

            frame.Sprites[1].Pos.X = player.Pos.X;
            frame.Sprites[1].Pos.Y = player.Pos.Y + 1;

            frame.Sprites[2].Pos.X = player.Pos.X - 1;
            frame.Sprites[2].Pos.Y = player.Pos.Y;

            frame.Sprites[3].Pos.X = player.Pos.X + 1;
            frame.Sprites[3].Pos.Y = player.Pos.Y;
        }

        public static void SetOmniPositionAroundPlayer(Frame frame, Player player)
        {
            frame.Sprites[0].Pos.X = player.Pos.X;
            frame.Sprites[0].Pos.Y = player.Pos.Y - 1;

            frame.Sprites[1].Pos.X = player.Pos.X;
            frame.Sprites[1].Pos.Y = player.Pos.Y + 1;

            frame.Sprites[2].Pos.X = player.Pos.X - 1;
            frame.Sprites[2].Pos.Y = player.Pos.Y;

            frame.Sprites[3].Pos.X = player.Pos.X + 1;
            frame.Sprites[3].Pos.Y = player.Pos.Y;

            frame.Sprites[4].Pos.X = player.Pos.X - 1;
            frame.Sprites[4].Pos.Y = player.Pos.Y - 1;

            frame.Sprites[5].Pos.X = player.Pos.X + 1;
            frame.Sprites[5].Pos.Y = player.Pos.Y + 1;

            frame.Sprites[6].Pos.X = player.Pos.X - 1;
            frame.Sprites[6].Pos.Y = player.Pos.Y + 1;

            frame.Sprites[7].Pos.X = player.Pos.X + 1;
            frame.Sprites[7].Pos.Y = player.Pos.Y - 1;
        }

        public static void SetFrameAroundPlayer(List<Frame> frames, Player player)
        {
            frames[0].Sprites[0].Pos.X = player.Pos.X;
            frames[0].Sprites[0].Pos.Y = player.Pos.Y - 1;

            frames[1].Sprites[0].Pos.X = player.Pos.X;
            frames[1].Sprites[0].Pos.Y = player.Pos.Y + 1;

            frames[2].Sprites[0].Pos.X = player.Pos.X - 1;
            frames[2].Sprites[0].Pos.Y = player.Pos.Y;

            frames[3].Sprites[0].Pos.X = player.Pos.X + 1;
            frames[3].Sprites[0].Pos.Y = player.Pos.Y;
        }

        public static List<Frame> CreateDialogueFrame(string dialogue, int style)
        {
            List<Frame> frames = new List<Frame>();

            for (int i = 0; i < dialogue.Length; i++)
            {
                List<AnimSprite> sprites = new List<AnimSprite>();
                for (int j = 0; j < i; j++)
                {
                    sprites.Add(new AnimSprite(frames[i - 1].Sprites[j]));
                }
                sprites.Add(new AnimSprite(i, 0, new Sprite(dialogue[i], style)));
                frames.Add(new Frame(sprites, i == dialogue.Length - 1 ? 4 : 1));
            }
            return frames;
        }

        public static void SetDialoguePosition(List<Frame> frames, Character chara, Map map)
        {
            int confine = 0;
            int testRight = frames.Count + chara.Pos.X - (frames.Count / 2);
            int testLeft = chara.Pos.X - (frames.Count / 2);
            if (testRight >= map.Width)
            {
                confine = testRight - map.Width;
            }
            else if (testLeft < 0)
            {
                confine = testLeft;
            }
            foreach (var f in frames)
            {
                foreach (var a in f.Sprites)
                {
                    a.Pos.X += chara.Pos.X - (frames.Count / 2) - confine;
                    a.Pos.Y = chara.Pos.Y - 1;
                }
            }
        }
    }
}
