using NeuroPriest.Render;

namespace NeuroPriest.Shared
{
    internal abstract class StaticSprite : StaticInit
    {
        public abstract Sprite Sprite { get; }
    }
}
