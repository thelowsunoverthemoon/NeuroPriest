using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Render;
using NeuroPriest.Shared;

namespace NeuroPriest.Characters
{
    internal class Interfacer : Enemy
    {
        public override string Id => "Interfacer";
        public override Sprite Sprite => Health == 2 ? SpriteSave : HurtSpriteSave;
        private static Sprite SpriteSave { get; set; }
        private static Sprite HurtSpriteSave { get; set; }

        public Interfacer()
            : base(0, null, null, false) { }

        public Interfacer(Coord coord)
            : base(2, coord, new TrackBehav(2), false) { }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            SpriteSave = new Sprite(
                'I',
                styleProvider.Add(new Colour(145, 145, 35), new Colour(181, 181, 177))
            );
            HurtSpriteSave = new Sprite(
                'i',
                styleProvider.Add(new Colour(201, 201, 66), new Colour(181, 181, 177))
            );
        }
    }
}
