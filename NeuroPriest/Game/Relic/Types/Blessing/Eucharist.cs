using NeuroPriest.Audio;
using NeuroPriest.Characters;
using NeuroPriest.Render;

namespace NeuroPriest.Relics
{
    internal class Eucharist : Blessing
    {
        public override string Image =>
            @"
  {[\  
  ""[~  
       
";

        public override string Desc => "Remnant of the Body of Christ";
        public override string Effect => "Immune to Heat Vents";
        public override string Id => "Eucharist";

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            new Devotion().Init(styleProvider, audioPlayer);
        }

        public override void BeforeGame(Player player, ModController modController)
        {
            modController.Add(new Devotion(null));
            player.VentImmune = true;
        }
    }
}
