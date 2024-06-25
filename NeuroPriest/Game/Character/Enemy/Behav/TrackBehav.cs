using NeuroPriest.Maps;
using NeuroPriest.Render;
using NeuroPriest.Shared;
using Priority_Queue;
using System;
using System.Collections.Generic;

namespace NeuroPriest.Characters
{
    internal class TrackBehav : EnemyBehav
    {
        private static Dictionary<Coord, Coord> Path { get; } = new Dictionary<Coord, Coord>();
        private static Dictionary<Coord, float> CostPath { get; } = new Dictionary<Coord, float>();
        private static SimplePriorityQueue<Coord> Candidates { get; } =
            new SimplePriorityQueue<Coord>();
        private int Every { get; }

        public TrackBehav(int every)
        {
            Every = every;
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
            if ((turn % Every) != 0)
            {
                return;
            }

            Candidates.Clear();
            Path.Clear();
            CostPath.Clear();

            Candidates.Enqueue(enem.Pos, 0);
            CostPath[enem.Pos] = 0;

            while (Candidates.Count > 0)
            {
                var cur = Candidates.Dequeue();

                foreach (var neighbor in graph.PosDict[cur])
                {
                    if (neighbor.Pos.Equals(player.Pos))
                    {
                        Path[player.Pos] = cur;
                        TracebackPath(enem, player);
                        map.Set(enem);
                        return;
                    }
                    Tile tile = map.Get(neighbor.Pos.X, neighbor.Pos.Y);
                    if (
                        tile.HasEnemy
                        || tile.Type == Tile.TileType.TILE_INTER_SOLID
                        || tile.Type == Tile.TileType.TILE_WALL
                        || levelData.Doors.ContainsKey(neighbor.Pos)
                    )
                    {
                        continue;
                    }
                    float cost = CostPath[cur] + 1;
                    if (!Path.ContainsKey(neighbor.Pos) || cost < CostPath[neighbor.Pos])
                    {
                        Candidates.Enqueue(neighbor.Pos, cost + Dist(neighbor.Pos, player.Pos));
                        CostPath[neighbor.Pos] = cost;
                        Path[neighbor.Pos] = cur;
                    }
                }
            }
        }

        private void TracebackPath(Enemy enem, Player player)
        {
            enem.SetPrev();

            Coord dest = player.Pos;
            Coord prev = Path[player.Pos];
            while (prev != enem.Pos)
            {
                dest = prev;
                prev = Path[prev];
            }
            enem.SetPos(dest);
        }

        private float Dist(Coord a, Coord b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.X - b.X);
        }
    }
}
