using NeuroPriest.Shared;
using System.Collections.Generic;

namespace NeuroPriest.Relics
{
    internal class SpearOfLonginus : ArmaChristi
    {
        public override string Image =>
            @"
   ¥   
   ¦   
   ¦   


";
        public override string Desc => "Blood and water came from the wound";
        public override string Effect => "Attack RIGHT and DOWN";
        public override string Id => "Spear of Longinus";
        public override List<WinWrapper.ArrowKeys> AttackDir { get; } =
            new List<WinWrapper.ArrowKeys>
            {
                WinWrapper.ArrowKeys.VK_RIGHT,
                WinWrapper.ArrowKeys.VK_DOWN
            };
    }
}
