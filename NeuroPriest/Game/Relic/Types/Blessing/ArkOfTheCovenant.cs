using NeuroPriest.Audio;
using NeuroPriest.Characters;
using NeuroPriest.Render;

namespace NeuroPriest.Relics
{
    internal class ArkOfTheCovenant : Blessing
    {
        public override string Image =>
            @"
  {=\  
 `+++} 
 `+++} 
";

        public override string Desc => "The Word of God contained within the Holiest of Relics";
        public override string Effect => "Shielded from a weak attack";
        public override string Id => "Ark Of The Covenant";

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            new DivineIntervention().Init(styleProvider, audioPlayer);
        }

        public override void BeforeGame(Player player, ModController modController)
        {
            modController.Add(new DivineIntervention(null));
            player.HasShield = true;
        }
    }
}
