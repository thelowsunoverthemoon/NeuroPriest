using NeuroPriest.Audio;
using NeuroPriest.Characters;
using NeuroPriest.Render;

namespace NeuroPriest.Relics
{
    internal class GospelFragment : Blessing
    {
        public override string Image =>
            @"
 :-&&  
 :--&  
  &--/ 
";

        public override string Desc => "In the beginning was the Word, and the Word was with God";
        public override string Effect => "Immune to Mines";
        public override string Id => "Gospel Fragment";

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            new Wisdom().Init(styleProvider, audioPlayer);
        }

        public override void BeforeGame(Player player, ModController modController)
        {
            modController.Add(new Wisdom(null));
            player.MineImmune = true;
        }
    }
}
