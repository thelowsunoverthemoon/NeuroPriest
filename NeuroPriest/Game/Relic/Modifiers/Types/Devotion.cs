using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Render;

namespace NeuroPriest.Relics
{
    internal class Devotion : Modifier
    {
        public override string Id => "Devotion";
        public override string Desc => "Immune to Heat Vents";
        public override Sprite Sprite => SpriteSave;
        private static Sprite SpriteSave { get; set; }

        public Devotion()
            : base(0) { }

        public Devotion(int? length)
            : base(length) { }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            SpriteSave = new Sprite(
                'D',
                styleProvider.Add(new Colour(255, 255, 255), new Colour(57, 209, 212))
            );
        }
    }
}
