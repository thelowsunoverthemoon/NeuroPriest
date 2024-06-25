using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Characters;
using NeuroPriest.Render;

namespace NeuroPriest.Relics
{
    internal class DivineIntervention : Modifier
    {
        public override string Id => "Divine Intervention";
        public override string Desc => "Shielded from the First Enemy Hit";
        public override Sprite Sprite => SpriteSave;
        private static Sprite SpriteSave { get; set; }

        public DivineIntervention()
            : base(0) { }

        public DivineIntervention(int? length)
            : base(length) { }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            SpriteSave = new Sprite(
                'G',
                styleProvider.Add(new Colour(255, 255, 255), new Colour(227, 204, 50))
            );
        }

        public override bool Modify(Player player)
        {
            return !player.HasShield;
        }
    }
}
