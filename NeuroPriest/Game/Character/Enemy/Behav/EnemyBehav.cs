using NeuroPriest.Maps;
using NeuroPriest.Render;

namespace NeuroPriest.Characters
{
    internal interface EnemyBehav
    {
        void Move(
            AnimSynchronizer animSynchronizer,
            EnemyController enemyController,
            Enemy enem,
            Player player,
            Map map,
            LevelData levelData,
            Graph graph,
            int turn
        );
    }
}
