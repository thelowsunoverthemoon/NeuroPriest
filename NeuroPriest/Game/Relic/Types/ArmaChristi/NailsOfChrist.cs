using NeuroPriest.Shared;
using System.Collections.Generic;

namespace NeuroPriest.Relics
{
    internal class NailsOfChrist : ArmaChristi
    {
        public override string Image =>
            @"
 +[[[> 
 <[@[+ 
 +@@[> 
";
        public override string Desc => "On the third day, He rose again";
        public override string Effect => "Attack LEFT and UP";
        public override string Id => "Nails Of Christ";
        public override List<WinWrapper.ArrowKeys> AttackDir { get; } =
            new List<WinWrapper.ArrowKeys>
            {
                WinWrapper.ArrowKeys.VK_LEFT,
                WinWrapper.ArrowKeys.VK_UP
            };
    }
}
