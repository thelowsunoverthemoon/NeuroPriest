using NeuroPriest.Audio;
using NeuroPriest.Characters;
using NeuroPriest.Render;

namespace NeuroPriest.Relics
{
    internal class KohensBreastPlate : Blessing
    {
        public override string Image =>
            @"
  { \  
 {#£#\ 
  `#}   

";
        public override string Desc => "Sacred Armour of many Stones";
        public override string Effect => "Penance strikes twice";
        public override string Id => "Kohen's Breastplate";

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            new Judgement().Init(styleProvider, audioPlayer);
        }

        public override void BeforeGame(Player player, ModController modController)
        {
            modController.Add(new Judgement(null));
            player.DoublePenance = true;
        }
    }
}
