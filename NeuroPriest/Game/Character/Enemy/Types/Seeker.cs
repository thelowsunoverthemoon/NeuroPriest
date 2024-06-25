using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Render;
using NeuroPriest.Shared;

namespace NeuroPriest.Characters
{
    internal class Seeker : Enemy
    {
        public override string Id => "Seeker";
        public override Sprite Sprite
        {
            get
            {
                if (((ReviveBehav)Behav).Revive != null)
                {
                    return ReviveSpriteSave;
                }
                else
                {
                    return Health == 2 ? SpriteSave : HurtSpriteSave;
                }
            }
        }
        private static Sprite SpriteSave { get; set; }
        private static Sprite HurtSpriteSave { get; set; }
        private static Sprite ReviveSpriteSave { get; set; }

        public Seeker()
            : base(0, null, null, false) { }

        public Seeker(Coord coord)
            : base(2, coord, new ReviveBehav(3, 1), false) { }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            SpriteSave = new Sprite(
                'S',
                styleProvider.Add(new Colour(106, 75, 128), new Colour(1, 1, 1))
            );
            HurtSpriteSave = new Sprite(
                's',
                styleProvider.Add(new Colour(106, 75, 128), new Colour(1, 1, 1))
            );
            ReviveSpriteSave = new Sprite(
                's',
                styleProvider.Add(new Colour(49, 14, 74), new Colour(1, 1, 1))
            );
        }
    }
}
