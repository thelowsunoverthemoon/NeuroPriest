using NeuroPriest.Maps;
using NeuroPriest.Render;

namespace NeuroPriest.Characters
{
    internal class ReviveBehav : EnemyBehav
    {
        private int ReviveWait { get; }
        private int ReviveThreshold { get; }
        public int? Revive { get; set; }
        private bool HasRevived { get; set; }
        private TrackBehav TrackBehav { get; }

        public ReviveBehav(int reviveWait, int reviveThreshold)
        {
            ReviveWait = reviveWait;
            ReviveThreshold = reviveThreshold;
            Revive = null;
            HasRevived = false;
            TrackBehav = new TrackBehav(1);
        }

        public void Move(
            AnimSynchronizer animSynchronizer,
            EnemyController enemyController,
            Enemy enem,
            Player player,
            Map map,
            LevelData levelData,
            Graph graph,
            int turn
        )
        {
            if (Revive == null)
            {
                if (enem.Health == ReviveThreshold && !HasRevived)
                {
                    Revive = ReviveWait;
                    Tile tile = map.Get(enem.Pos.X, enem.Pos.Y);
                    tile.Enemy = null;
                    tile.Type = Tile.TileType.TILE_WALL;
                    HasRevived = true;
                }
                else
                {
                    TrackBehav.Move(
                        animSynchronizer,
                        enemyController,
                        enem,
                        player,
                        map,
                        levelData,
                        graph,
                        turn
                    );
                }
            }

            if (Revive != null)
            {
                Revive--;
                if (Revive == 0)
                {
                    Revive = null;
                    Tile tile = map.Get(enem.Pos.X, enem.Pos.Y);
                    tile.Enemy = enem;
                    tile.Type = Tile.TileType.TILE_NONE;
                }
            }
        }
    }
}
