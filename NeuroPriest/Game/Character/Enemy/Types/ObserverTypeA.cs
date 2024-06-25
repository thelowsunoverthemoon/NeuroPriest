using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Render;
using NeuroPriest.Shared;

namespace NeuroPriest.Characters
{
    internal class ObserverTypeA : Enemy
    {
        public override string Id => "ObserverTypeA";
        public override Sprite Sprite =>
            ((HorizontalBehav)Behav).Direction == -1 ? LeftSpriteSave : RightSpriteSave;
        private static Sprite LeftSpriteSave { get; set; }
        private static Sprite RightSpriteSave { get; set; }

        public ObserverTypeA()
            : base(0, null, null, false) { }

        public ObserverTypeA(Coord coord)
            : base(1, coord, new HorizontalBehav(), false) { }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            LeftSpriteSave = new Sprite(
                '<',
                styleProvider.Add(new Colour(51, 135, 132), new Colour(1, 1, 1))
            );
            RightSpriteSave = new Sprite(
                '>',
                styleProvider.Add(new Colour(51, 135, 132), new Colour(1, 1, 1))
            );
        }
    }
}
