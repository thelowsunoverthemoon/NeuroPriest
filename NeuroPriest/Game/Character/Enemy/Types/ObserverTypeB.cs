using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Render;
using NeuroPriest.Shared;

namespace NeuroPriest.Characters
{
    internal class ObserverTypeB : Enemy
    {
        public override string Id => "ObserverTypeB";
        public override Sprite Sprite =>
            ((VerticalBehav)Behav).Direction == -1 ? UpSpriteSave : DownSpriteSave;
        private static Sprite DownSpriteSave { get; set; }
        private static Sprite UpSpriteSave { get; set; }

        public ObserverTypeB()
            : base(0, null, null, false) { }

        public ObserverTypeB(Coord coord)
            : base(1, coord, new VerticalBehav(), false) { }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            DownSpriteSave = new Sprite(
                'v',
                styleProvider.Add(new Colour(51, 135, 132), new Colour(1, 1, 1))
            );
            UpSpriteSave = new Sprite(
                '^',
                styleProvider.Add(new Colour(51, 135, 132), new Colour(1, 1, 1))
            );
        }
    }
}
