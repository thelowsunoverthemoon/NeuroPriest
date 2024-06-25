using NeuroPriest.Game;
using NeuroPriest.Maps;
using NeuroPriest.Render;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NeuroPriest.Characters
{
    internal class EnemyController
    {
        private LevelData LevelData { get; }
        private List<Enemy> EnemyList => LevelData.Rooms[Player.Room].Enems;
        public int Count => AllEnemyList.Count;
        private List<Enemy> AllEnemyList { get; }
        private Player Player { get; }
        private Map Map { get; }
        private Graph Graph { get; }
        private TurnSynchronizer TurnSynchronizer { get; }
        private TextWriter TextWriter { get; }
        private AnimSynchronizer AnimSynchronizer { get; }

        public EnemyController(
            AnimSynchronizer animSynchronizer,
            TurnSynchronizer turn,
            TextWriter textWriter,
            LevelData levelData,
            Player player,
            Map map,
            Graph graph
        )
        {
            TurnSynchronizer = turn;
            TextWriter = textWriter;
            AnimSynchronizer = animSynchronizer;
            LevelData = levelData;
            Player = player;
            Map = map;
            Graph = graph;

            AllEnemyList = new List<Enemy>();
            foreach (var room in levelData.Rooms)
            {
                AllEnemyList.AddRange(room.Enems);
                foreach (var e in room.Enems) // add seperate thing for map being altered
                {
                    map.Set(e);
                }
            }
        }

        public void Move()
        {
            foreach (Enemy enemy in new List<Enemy>(EnemyList))
            {
                enemy.Behav.Move(
                    AnimSynchronizer,
                    this,
                    enemy,
                    Player,
                    Map,
                    LevelData,
                    Graph,
                    TurnSynchronizer.Turn
                );
            }
        }

        public List<Enemy> GetActive()
        {
            return EnemyList;
        }

        public IEnumerable GetSleeping()
        {
            return AllEnemyList.Where(e => !EnemyList.Contains(e));
        }

        public void Add(Enemy enemy, int room)
        {
            AllEnemyList.Add(enemy);
            LevelData.Rooms[room].Enems.Add(enemy);
        }

        public bool Hurt(Enemy enemy)
        {
            enemy.Health -= Player.Atk;
            if (enemy.Health <= 0)
            {
                Remove(enemy, false);
                return true;
            }
            return false;
        }

        public void Remove(Enemy enemy, bool removePrev)
        {
            AllEnemyList.Remove(enemy);
            EnemyList.Remove(enemy);
            Map.Remove(enemy);
            TextWriter.Replace(enemy.Pos, ' ');
            if (removePrev)
            {
                TextWriter.Replace(enemy.Prev, ' ');
            }
        }
    }
}
