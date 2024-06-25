using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Render;
using NeuroPriest.Shared;

namespace NeuroPriest.Characters
{
    internal class DrAkiyama : Enemy
    {
        public override string Id => "DrAkiyama";
        public override Sprite Sprite => Health > 3 ? SpriteSave : HurtSpriteSave;
        private static Sprite SpriteSave { get; set; }
        private static Sprite HurtSpriteSave { get; set; }

        public DrAkiyama()
            : base(0, null, null, true) { }

        public DrAkiyama(Coord coord)
            : base(1, coord, new DrAkiyamaBehav(), true) { }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            SpriteSave = new Sprite(
                'D',
                styleProvider.Add(new Colour(128, 161, 171), new Colour(121, 44, 122))
            );
            HurtSpriteSave = new Sprite(
                'D',
                styleProvider.Add(new Colour(128, 161, 171), new Colour(121, 44, 122))
            );

            new Android().Init(styleProvider, audioPlayer);
            new Seeker().Init(styleProvider, audioPlayer);
        }
    }
}
