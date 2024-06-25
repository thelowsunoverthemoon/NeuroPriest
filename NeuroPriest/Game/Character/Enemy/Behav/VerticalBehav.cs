﻿using NeuroPriest.Maps;
using NeuroPriest.Render;
using NeuroPriest.Shared;

namespace NeuroPriest.Characters
{
    internal class VerticalBehav : EnemyBehav
    {
        public int Direction { get; set; } = 1;
        private Coord Check { get; } = new Coord(0, 0);

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
            Check.X = enem.Pos.X;
            Check.Y = enem.Pos.Y + Direction;
            Tile.TileType type = map.GetType(Check);
            if (
                type == Tile.TileType.TILE_WALL
                || type == Tile.TileType.TILE_INTER_SOLID
                || levelData.Doors.ContainsKey(Check)
            )
            {
                Direction *= -1;
            }

            enem.SetPrev();
            enem.Pos.Y += Direction;
            map.Set(enem);
        }
    }
}
