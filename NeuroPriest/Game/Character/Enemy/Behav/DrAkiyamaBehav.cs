using NeuroPriest.Maps;
using NeuroPriest.Render;
using NeuroPriest.Shared;
using System;
using System.Collections.Generic;

namespace NeuroPriest.Characters
{
    internal class DrAkiyamaBehav : StateBehav, EnemyBehav
    {
        private const string AKIYAMA_MONOLOGUE = "You have no idea what she knows.";
        private const string AKIYAMA_TAUNT = "Ignorant fool...";
        private const int AGGRESSIVE_THRESHOLD = 3;
        private const int SUMMON_THRESHOLD = 3;
        private readonly Coord RUN_A = new Coord(2, 6);
        private readonly Coord RUN_B = new Coord(34, 6);
        private bool HasTaunted { get; set; } = false;
        private bool HasMonologued { get; set; } = false;
        private int? Hurt = null;
        private TrackBehav TrackBehav { get; } = new TrackBehav(1);

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
            if (Hurt == null)
            {
                Hurt = enem.Health;
            }
            else if (enem.Health != Hurt)
            {
                Hurt = enem.Health;
                enem.SetPrev();
                int distA = Math.Abs(RUN_A.X - player.Pos.X) + Math.Abs(RUN_A.Y - player.Pos.Y);
                int distB = Math.Abs(RUN_B.X - player.Pos.X) + Math.Abs(RUN_B.Y - player.Pos.Y);
                if (distA > distB)
                {
                    enem.SetPos(RUN_A);
                }
                else
                {
                    enem.SetPos(RUN_B);
                }
                map.Set(enem);
                SetState(2, 2);
            }
            if (!HasMonologued)
            {
                List<Frame> dia = AnimHelpers.CreateDialogueFrame(AKIYAMA_MONOLOGUE, enem.Style);
                AnimHelpers.SetDialoguePosition(dia, enem, map);
                animSynchronizer.Start(dia);
                HasMonologued = true;
            }
            else if (enem.Health <= AGGRESSIVE_THRESHOLD && !HasTaunted)
            {
                List<Frame> dia = AnimHelpers.CreateDialogueFrame(AKIYAMA_TAUNT, enem.Style);
                AnimHelpers.SetDialoguePosition(dia, enem, map);
                animSynchronizer.Start(dia);
                HasTaunted = true;
            }

            switch (State)
            {
                case 0:
                    SummonSeekers(enemyController, player.Room);
                    SetState(1, 3);
                    break;
                case 1:
                    IfWaitDoneChangeState(2, 2);

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
                    break;
                case 2:
                    IfWaitDoneChangeState(
                        enemyController.GetActive().Count < SUMMON_THRESHOLD
                            ? Random.Next(3, 5)
                            : 1,
                        3
                    );
                    break;
                case 3:
                    SummonAndroids(enemyController, player.Room);
                    SetState(1, 3);
                    break;
                case 4:
                    SummonSeekers(enemyController, player.Room);
                    SetState(1, 3);
                    break;
            }
        }

        private void SummonSeekers(EnemyController enemyController, int room)
        {
            enemyController.Add(new Seeker(new Coord(3, 9)), room);
            enemyController.Add(new Seeker(new Coord(33, 9)), room);
        }

        private void SummonAndroids(EnemyController enemyController, int room)
        {
            enemyController.Add(new Android(new Coord(3, 9)), room);
            enemyController.Add(new Android(new Coord(33, 9)), room);
        }
    }
}
