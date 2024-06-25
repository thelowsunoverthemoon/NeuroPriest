using NeuroPriest.Shared;
using System.Collections.Generic;

namespace NeuroPriest.Relics
{
    internal abstract class ArmaChristi : Relic
    {
        public abstract List<WinWrapper.ArrowKeys> AttackDir { get; }
    }
}
