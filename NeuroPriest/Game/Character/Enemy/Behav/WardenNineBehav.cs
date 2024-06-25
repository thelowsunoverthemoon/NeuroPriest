using NeuroPriest.Maps;
using NeuroPriest.Render;
using NeuroPriest.Shared;
using System;
using System.Collections.Generic;

namespace NeuroPriest.Characters
{
    internal class WardenNineBehav : StateBehav, EnemyBehav
    {
        private const string WARDEN_NINE_SUMMON = "Find him.";
        private const string WARDEN_NINE_TAUNT = "Found you.";
        private const int AGGRESSIVE_THRESHOLD = 3;
        private const int SUMMON_THRESHOLD = 3;
        private bool HasTaunted { get; set; } = false;
        private TrackBehav TrackBehav { get; } = new TrackBehav(2);
        private HorizontalBehav HorizontalBehav { get; } = new HorizontalBehav();

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
            if (enem.Health <= AGGRESSIVE_THRESHOLD && !HasTaunted)
            {
                List<Frame> dia = AnimHelpers.CreateDialogueFrame(WARDEN_NINE_TAUNT, enem.Style);
                AnimHelpers.SetDialoguePosition(dia, enem, map);
                animSynchronizer.Start(dia);
                HasTaunted = true;
            }

            switch (State)
            {
                case 0:
                    List<Frame> dia = AnimHelpers.CreateDialogueFrame(
                        WARDEN_NINE_SUMMON,
                        enem.Style
                    );
                    AnimHelpers.SetDialoguePosition(dia, enem, map);
                    animSynchronizer.Start(dia);

                    SummonAndroids(enemyController, player.Room);
                    SetState(1, 5);
                    break;
                case 1:
                    IfWaitDoneChangeState( // if enemies below threshold, summon androids or interfacers
                        enemyController.GetActive().Count < SUMMON_THRESHOLD ? Random.Next(2, 4) : 1,
                        6
                    );
                    if (enem.Health <= AGGRESSIVE_THRESHOLD)
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
                    else
                    {
                        HorizontalBehav.Move(
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
                    break;
                case 2:
                    SummonAndroids(enemyController, player.Room);
                    SetState(1, 5);
                    break;
                case 3:
                    SummonInterfacers(enemyController, player.Room);
                    SetState(1, 5);
                    break;
            }
        }

        private void SummonAndroids(EnemyController enemyController, int room)
        {
            enemyController.Add(new Android(new Coord(10, 2)), room);
            enemyController.Add(new Android(new Coord(33, 2)), room);
            enemyController.Add(new Android(new Coord(10, 9)), room);
            enemyController.Add(new Android(new Coord(33, 9)), room);
        }

        private void SummonInterfacers(EnemyController enemyController, int room)
        {
            enemyController.Add(new Interfacer(new Coord(10, 2)), room);
            enemyController.Add(new Interfacer(new Coord(33, 9)), room);
        }
    }
}
