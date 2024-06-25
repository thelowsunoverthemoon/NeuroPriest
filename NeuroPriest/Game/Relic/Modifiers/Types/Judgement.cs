using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Render;

namespace NeuroPriest.Relics
{
    internal class Judgement : Modifier
    {
        public override string Id => "Judgement";
        public override string Desc => "Penance strikes twice";
        public override Sprite Sprite => SpriteSave;
        private static Sprite SpriteSave { get; set; }

        public Judgement()
            : base(0) { }

        public Judgement(int? length)
            : base(length) { }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            SpriteSave = new Sprite(
                'v',
                styleProvider.Add(new Colour(73, 75, 128), new Colour(92, 15, 33))
            );
        }
    }
}
