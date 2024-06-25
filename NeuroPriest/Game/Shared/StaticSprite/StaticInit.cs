using NeuroPriest.Audio;
using NeuroPriest.Render;

namespace NeuroPriest.Shared
{
    internal abstract class StaticInit
    {
        public virtual void Init(StyleProvider styleProvider, AudioPlayer audioPlayer) { } // virtual or abstract?

        public abstract string Id { get; }
    }
}
