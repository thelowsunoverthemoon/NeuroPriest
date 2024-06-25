using NeuroPriest.Shared;
using System.Collections.Generic;

namespace NeuroPriest.Relics
{
    internal class GoliathClub : ArmaChristi
    {
        public override string Image =>
            @"
  )#(  
   +   
   +   
";
        public override string Desc => "Hardened iron, stained with blood";
        public override string Effect => "Attack UP and DOWN";
        public override string Id => "Goliath's club";
        public override List<WinWrapper.ArrowKeys> AttackDir { get; } =
            new List<WinWrapper.ArrowKeys>
            {
                WinWrapper.ArrowKeys.VK_UP,
                WinWrapper.ArrowKeys.VK_DOWN
            };
    }
}
