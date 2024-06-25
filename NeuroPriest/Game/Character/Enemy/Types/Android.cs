using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Render;
using NeuroPriest.Shared;

namespace NeuroPriest.Characters
{
    internal class Android : Enemy
    {
        public override string Id => "Android";
        public override Sprite Sprite => SpriteSave;
        private static Sprite SpriteSave { get; set; }

        public Android()
            : base(0, null, null, false) { }

        public Android(Coord coord)
            : base(1, coord, new TrackBehav(1), false) { }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            SpriteSave = new Sprite(
                'A',
                styleProvider.Add(new Colour(199, 58, 58), new Colour(1, 1, 1))
            );
        }
    }
}
