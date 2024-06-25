using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Render;
using NeuroPriest.Shared;

namespace NeuroPriest.Characters
{
    internal class WardenNine : Enemy
    {
        public override string Id => "WardenNine";
        public override Sprite Sprite => Health > 3 ? SpriteSave : HurtSpriteSave;
        private static Sprite SpriteSave { get; set; }
        private static Sprite HurtSpriteSave { get; set; }

        public WardenNine()
            : base(0, null, null, true) { }

        public WardenNine(Coord coord)
            : base(6, coord, new WardenNineBehav(), true) { }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            SpriteSave = new Sprite(
                'W',
                styleProvider.Add(new Colour(123, 137, 176), new Colour(29, 76, 84))
            );
            HurtSpriteSave = new Sprite(
                'w',
                styleProvider.Add(new Colour(191, 75, 75), new Colour(29, 76, 84))
            );

            new Android().Init(styleProvider, audioPlayer);
            new Interfacer().Init(styleProvider, audioPlayer);
        }
    }
}
