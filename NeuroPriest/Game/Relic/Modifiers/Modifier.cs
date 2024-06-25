using NeuroPriest.Characters;
using NeuroPriest.Shared;

namespace NeuroPriest.Relics
{
    internal abstract class Modifier : StaticSprite
    {
        public int? Length { get; set; }
        public abstract string Desc { get; }

        protected Modifier(int? length)
        {
            Length = length;
        }

        public virtual bool Modify(Player player) => false;

        public virtual void Undo(Player player) { }
    }
}
