using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Render;

namespace NeuroPriest.Relics
{
    internal class Wisdom : Modifier
    {
        public override string Id => "Wisdom";
        public override string Desc => "Immune to Mines";
        public override Sprite Sprite => SpriteSave;
        private static Sprite SpriteSave { get; set; }

        public Wisdom()
            : base(0) { }

        public Wisdom(int? length)
            : base(length) { }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            SpriteSave = new Sprite(
                'W',
                styleProvider.Add(new Colour(255, 255, 255), new Colour(75, 56, 128))
            );
        }
    }
}
