using NeuroPriest.Audio;
using NeuroPriest.Characters;
using NeuroPriest.Game;
using NeuroPriest.Maps;
using NeuroPriest.Render;

namespace NeuroPriest.Relics
{
    internal abstract class Penance : Relic
    {
        public abstract void Use(
            Player player,
            ModController modController,
            TurnSynchronizer turnSynchronizer,
            AnimSynchronizer animSynchronizer,
            EnemyController enemyController,
            Map map,
            AudioPlayer audioPlayer
        );
    }
}
