using NeuroPriest.Characters;

namespace NeuroPriest.Relics
{
    internal abstract class Blessing : Relic
    {
        public virtual void BeforeGame(Player player, ModController modController) { }
    }
}
