using Kbg.NppPluginNET;
using NeuroPriest.Audio;
using NeuroPriest.Characters;
using NeuroPriest.Interactables;
using NeuroPriest.Maps;
using NeuroPriest.Relics;
using NeuroPriest.Render;
using NeuroPriest.Shared;
using System;
using System.Runtime.InteropServices;

namespace NeuroPriest.Game
{
    internal class GameController
    {
        private const int RENDER_WAIT_TIME = 50;
        private IntPtr InputHook { get; set; }
        private static WinWrapper.HookProc Proc { get; set; }
        private Player Player { get; }
        private LevelData LevelData { get; }
        private TurnSynchronizer TurnSynchronizer { get; }
        private EnemyController EnemyController { get; }
        private ModController ModController { get; }
        private AnimSynchronizer AnimSynchronizer { get; }
        private InterController InterController { get; }
        private IntPtr RenderNow { get; }
        private IntPtr RenderFinish { get; }
        private Map Map { get; }
        private AudioPlayer AudioPlayer { get; }

        public GameController(
            IntPtr renderNow,
            IntPtr renderFinish,
            TurnSynchronizer turnSynchronizer,
            AnimSynchronizer animSynchronizer,
            ModController modController,
            EnemyController enemyController,
            InterController interController,
            LevelData levelData,
            Player player,
            Map map,
            AudioPlayer audioPlayer
        )
        {
            RenderNow = renderNow;
            RenderFinish = renderFinish;
            InterController = interController;
            TurnSynchronizer = turnSynchronizer;
            ModController = modController;
            EnemyController = enemyController;
            LevelData = levelData;
            Player = player;
            AnimSynchronizer = animSynchronizer;
            Map = map;
            AudioPlayer = audioPlayer;
        }

        public void Start()
        {
            Proc = new WinWrapper.HookProc(InputProc);

            InputHook = WinWrapper.SetWindowsHookEx(
                WinWrapper.WH_KEYBOARD,
                Proc,
                Marshal.GetHINSTANCE(typeof(Main).Module),
                WinWrapper.GetCurrentThreadId()
            );
        }

        public void End()
        {
            WinWrapper.UnhookWindowsHookEx(InputHook);
        }

        public IntPtr InputProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (
                !AnimSynchronizer.HasAnim
                && (Player.Status == Player.GameStatus.PLAYER_NONE)
                && nCode >= 0
                && (((long)lParam & WinWrapper.KF_UP) == WinWrapper.KF_UP)
            )
            {
                int keyCode = (int)wParam;
                if (keyCode == WinWrapper.VK_CTRL)
                {
                    if (Player.Sin == Player.SinMax && EnemyController.GetActive().Count != 0)
                    {
                        AudioPlayer.PlayEffect("penance");
                        Player.Penance.Use(
                            Player,
                            ModController,
                            TurnSynchronizer,
                            AnimSynchronizer,
                            EnemyController,
                            Map,
                            AudioPlayer
                        );
                        if (Player.DoublePenance)
                        {
                            Player.Penance.Use(
                                Player,
                                ModController,
                                TurnSynchronizer,
                                AnimSynchronizer,
                                EnemyController,
                                Map,
                                AudioPlayer
                            );
                        }
                        Player.Sin = 0;
                        NextTurn();
                    }
                }
                else
                {
                    Move(keyCode);
                }
            }
            return new IntPtr(1); // don't call next hook since don't want letters popping up
        }

        private void Move(int keyCode)
        {
            int testX = Player.Pos.X;
            int testY = Player.Pos.Y;
            switch ((WinWrapper.ArrowKeys)keyCode)
            {
                case WinWrapper.ArrowKeys.VK_RIGHT:
                    testX++;
                    break;
                case WinWrapper.ArrowKeys.VK_LEFT:
                    testX--;
                    break;
                case WinWrapper.ArrowKeys.VK_UP:
                    testY--;
                    break;
                case WinWrapper.ArrowKeys.VK_DOWN:
                    testY++;
                    break;
                default:
                    return;
            }

            Tile tile = Map.Get(testX, testY);
            if (tile.Type == Tile.TileType.TILE_WALL)
            {
                return;
            }

            InterController.Move();
            if (tile.HasEnemy)
            {
                if (!Player.ArmaChristi.AttackDir.Contains((WinWrapper.ArrowKeys)keyCode))
                {
                    return;
                }
                if (EnemyController.Hurt(tile.Enemy))
                {
                    AudioPlayer.PlayEffect("kill");
                    Player.Sin++;
                }
                else
                {
                    AudioPlayer.PlayEffect("hit");
                }
            }
            else
            {
                if (Player.Sin == Player.SinMax && EnemyController.GetActive().Count != 0)
                {
                    return;
                }
                if (tile.Type == Tile.TileType.TILE_INTER_SOLID)
                {
                    tile.Interactable.Touch(Player, Map, InterController, AudioPlayer);
                }
                else
                {
                    if (tile.Type == Tile.TileType.TILE_INTER_PASS)
                    {
                        tile.Interactable.Touch(Player, Map, InterController, AudioPlayer);
                    }
                    AudioPlayer.PlayEffect("footstep");
                    Map.Set(Player);
                    Player.Prev.X = Player.Pos.X;
                    Player.Prev.Y = Player.Pos.Y;
                    Player.Pos.X = testX;
                    Player.Pos.Y = testY;
                    CheckDoor();
                }
            }
            NextTurn();
        }

        private void CheckDoor()
        {
            if (
                LevelData.Doors.ContainsKey(Player.Pos)
                && (Player.Room != LevelData.Doors[Player.Pos])
            )
            {
                TurnSynchronizer.Reset();
                Player.Room = LevelData.Doors[Player.Pos];
            }
        }

        private void NextTurn()
        {
            AudioPlayer.Update();
            EnemyController.Move();
            TurnSynchronizer.Next();
            if (Map.Get(Player.Pos.X, Player.Pos.Y).HasEnemy)
            {
                if (Player.HasShield)
                {
                    Enemy remove = Map.Get(Player.Pos.X, Player.Pos.Y).Enemy;
                    if (remove.IsBoss)
                    {
                        KillPlayer();
                    }
                    else
                    {
                        EnemyController.Remove(
                            remove,
                            !Map.Get(remove.Prev.X, remove.Prev.Y).HasEnemy
                        );
                        Player.HasShield = false;
                        AudioPlayer.PlayEffect("shatter");
                        foreach (var enem in EnemyController.GetActive())
                        {
                            if (enem.Pos.Equals(Player.Pos))
                            {
                                KillPlayer();
                                break;
                            }
                        }
                    }
                }
                else
                {
                    KillPlayer();
                }
            }
            ModController.Use();
            if (EnemyController.Count == 0)
            {
                Player.Status = Player.GameStatus.PLAYER_WIN;
            }
            WinWrapper.SignalObjectAndWait(RenderNow, RenderFinish, RENDER_WAIT_TIME, false); // make into constant
        }

        private void KillPlayer()
        {
            AudioPlayer.PlayEffect("death");
            Player.Status = Player.GameStatus.PLAYER_LOSE;
        }
    }
}
