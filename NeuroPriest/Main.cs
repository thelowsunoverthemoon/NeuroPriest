using FMOD;
using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Characters;
using NeuroPriest.Game;
using NeuroPriest.Interactables;
using NeuroPriest.Maps;
using NeuroPriest.Menus;
using NeuroPriest.Relics;
using NeuroPriest.Render;
using NeuroPriest.Shared;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Thread = System.Threading.Thread;

namespace Kbg.NppPluginNET
{
    internal static class Main
    {
        private const int LEVEL_GROUP = 3;
        private const int BORDER_LEN = 4;
        private const int DEFAULT_STYLE = 0;
        private const int LAST_LEVEL = 5;
        private const int BEFORE_MENU_TIME = 400;
        private const int GAME_END_WAIT = 500;
        private const int GAME_END_TIME = 1000;
        private const int END_SCREEN_TIME = 2300;
        private const int FINISH_TEXT_WAIT = 250;
        private const int MUSIC_FADEOUT_TIME = 3;
        private const int MUSIC_FADEIN_TIME = 5;
        private const int FADE_STEPS = 7;
        internal const string PluginName = "NeuroPriest";
        private static string PluginPath { get; set; }
        private static AudioPlayer AudioPlayer { get; set; }
        private static int Level { get; set; }
        private static bool IsRunning { get; set; }
        private static Menu Menu { get; set; }
        private static GameController Game { get; set; }
        private static Renderer Render { get; set; }
        private static IndicatorProvider IndicatorProvider { get; set; }
        private static TextAttribute TextAttribute { get; set; }
        private static ScintillaGateway Window { get; set; }
        private static Channel MenuTrack { get; set; }
        private static Channel AreaTrack { get; set; }
        private static RelicMenu RelicMenu { get; set; }
        private static RelicController RelicController { get; set; }
        private static IntPtr RenderNow { get; set; }
        private static IntPtr RenderFinish { get; set; }

        internal static void SetToolBarIcon() { }

        internal static void PluginCleanUp() { }

        public static void OnNotification(ScNotification notification)
        {
            if (IsRunning && notification.Header.Code == (uint)SciMsg.SCN_INDICATORCLICK)
            {
                foreach (Button but in Menu)
                {
                    if (
                        (notification.Position.Value >= but.Begin)
                        && (notification.Position.Value <= but.End)
                    )
                    {
                        AudioPlayer.PlayEffect(but.Sound);
                        but.Press();
                    }
                }
            }
        }

        internal static void CommandMenuInit()
        {
            // obsolete but works
            AppDomain.CurrentDomain.AppendPrivatePath("plugins\\Neuropriest");
            StringBuilder getPath = new StringBuilder(Win32.MAX_PATH);
            Win32.SendMessage(
                PluginBase.nppData._nppHandle,
                (uint)NppMsg.NPPM_GETPLUGINHOMEPATH,
                Win32.MAX_PATH,
                getPath
            );

            TextAttribute = new TextAttribute();
            Level = 0;
            PluginPath = $"{getPath}\\{PluginName}\\Data\\";
            PluginBase.SetCommand(0, "PLAY", StartMenuCheck);
            PluginBase.SetCommand(1, "STOP", EndGame);
        }

        private static void StartMenuCheck()
        {
            if (IsRunning)
            {
                return;
            }
            IsRunning = true;
            StartMenu();
        }

        private static void EndGame()
        {
            if (!IsRunning)
            {
                return;
            }
            IsRunning = false;

            if (AudioPlayer != null)
            {
                AudioPlayer.Stop();
                AudioPlayer = null;
            }

            Game?.End();
            Render?.End();

            Window.RestoreAttr(TextAttribute);
            Window = null;

            ClosePage();

            GC.Collect();
        }

        private static void StartMenu()
        {
            SwapPage("menu.txt", Window != null);
            SetWindow();

            Window.MakeGui(TextAttribute, false);
            Window.HideUser(TextAttribute);
            Window.DisableHighlight(TextAttribute);

            if (AudioPlayer == null)
            {
                AudioPlayer = new AudioPlayer();
                AudioPlayer.RegisterTrack("menu", "menu.mp3");
                AudioPlayer.RegisterEffect("bell", "bell.mp3");
                AudioPlayer.RegisterEffect("wood", "wood.mp3");
                AudioPlayer.RegisterEffect("footstep", "footstep.mp3");
                AudioPlayer.RegisterEffect("hit", "hit.mp3");
                AudioPlayer.RegisterEffect("kill", "kill.mp3");
                AudioPlayer.RegisterEffect("penance", "penance.mp3");
                AudioPlayer.RegisterEffect("death", "death.mp3");
                AudioPlayer.RegisterEffect("shatter", "shatter.mp3");
            }
            MenuTrack = AudioPlayer.FadeInTrack("menu", MUSIC_FADEIN_TIME);
            IndicatorProvider = new IndicatorProvider(Window);

            Menu = new Menu(Window)
            {
                new Button(
                    Window,
                    ": START /",
                    IndicatorProvider.IndicStart,
                    touch: () => StartInfo(),
                    sound: "bell"
                ),
                new Button(
                    Window,
                    ": ENEMIES /",
                    IndicatorProvider.IndicDefault,
                    MenuStrings.ENEMIES
                ),
                new Button(
                    Window,
                    ": INTERACTABLES /",
                    IndicatorProvider.IndicDefault,
                    MenuStrings.INTERACTABLES
                ),
                new Button(
                    Window,
                    ": GAMEPLAY /",
                    IndicatorProvider.IndicDefault,
                    MenuStrings.GAMEPLAY
                ),
                new Button(
                    Window,
                    ": CREDITS /",
                    IndicatorProvider.IndicDefault,
                    MenuStrings.CREDITS
                )
            };
        }

        private static void StartGifts()
        {
            SwapPage("gifts.txt", true);

            SetWindow();

            RelicController = new RelicController();
            RelicMenu = new RelicMenu(Window, RelicController);

            foreach (
                var str in new string[]
                {
                    RelicMenu.Blessing.Id,
                    RelicMenu.ArmaChristi.Id,
                    RelicMenu.Penance.Id,
                    ": FINISHED /"
                }
            )
            {
                int center = (Window.LineLength(0) / 2) - ((str.Length + BORDER_LEN) / 2);
                string line = $"\n\n{new string(' ', center)}{str}";
                Window.AppendText(line.Length, line);
            }
            Window.AppendText(1, "\n");
            Window.SetReadOnly(true);

            Menu = new Menu(Window)
            {
                new Button(
                    Window,
                    ": FINISHED /",
                    IndicatorProvider.IndicDefault,
                    touch: () =>
                    {
                        Task.Factory.StartNew(() => TextEffects.FadeOut(Window, FADE_STEPS)).Wait();
                        StartRelic();
                    }
                )
            };
        }

        private static void StartInfo()
        {
            Window.SetReadOnly(false);

            Task.Factory
                .StartNew(() =>
                {
                    TextEffects.FadeOut(Window, FADE_STEPS);
                    TextEffects.InsertText(Window, " [[[@ PRAYER @[[[[[[[[[[[[[[[/");
                    foreach (var line in File.ReadLines(PluginPath + $"info{Level}.txt"))
                    {
                        string bullet = $"\n = {line}";
                        Window.AppendText(bullet.Length, bullet);
                        Thread.Sleep(5);
                    }
                    string finish = "\n : FINISHED /\n";
                    Window.AppendText(finish.Length, finish);
                })
                .Wait();

            Window.SetReadOnly(true);

            Menu = new Menu(Window)
            {
                new Button(
                    Window,
                    ": FINISHED /",
                    IndicatorProvider.IndicDefault,
                    touch: () =>
                    {
                        Task.Factory.StartNew(() => TextEffects.FadeOut(Window, FADE_STEPS)).Wait();
                        if (Level == 0 && RelicController == null)
                        {
                            StartGifts();
                        }
                        else
                        {
                            StartRelic();
                        }
                    }
                )
            };
        }

        private static void StartEnding()
        {
            SwapPage("ending.txt", true);

            SetWindow();
            Window.SetReadOnly(true);

            Menu = new Menu(Window)
            {
                new Button(
                    Window,
                    ": RESTART /",
                    IndicatorProvider.IndicDefault,
                    touch: () =>
                    {
                        Task.Factory.StartNew(() => TextEffects.FadeOut(Window, FADE_STEPS)).Wait();
                        Level = 0;
                        StartMenu();
                    }
                )
            };
        }

        private static void StartRelic()
        {
            SwapPage("church.txt", true);
            SetWindow();
            Menu = new Menu(Window);

            Window.DisableHighlight(TextAttribute); // for some reason it highlights

            var len = new FileInfo(PluginPath + "church.txt").Length;

            RelicMenu.SetText(Menu);
            Window.SetReadOnly(true);

            Menu.Add(
                new Button(
                    Window,
                    ": FINISHED /",
                    IndicatorProvider.IndicDefault,
                    touch: () =>
                    {
                        AudioPlayer.FadeOutTrack(MenuTrack, MUSIC_FADEOUT_TIME);
                        Task.Factory.StartNew(() => TextEffects.FadeOut(Window, FADE_STEPS)).Wait();
                        StartGame();
                    }
                )
            );
        }

        private static void StartGame()
        {
            SwapPage($"level{Level}.txt", true);

            AudioPlayer.RegisterTrack("area", $"area{Level / LEVEL_GROUP}.mp3");
            AreaTrack = AudioPlayer.FadeInTrack("area", MUSIC_FADEIN_TIME);

            var window = new ScintillaGateway(PluginBase.GetCurrentScintilla());
            window.MakeGui(TextAttribute, true);

            var turn = new TurnSynchronizer();

            var style = new StyleProvider(window);
            var staticInit = new StaticInitController(style, AudioPlayer);

            var inter = new InterController(turn);

            var map = new Map(window, inter, staticInit);
            var colour = new ColourWriter(window, map);
            var graph = new Graph(map);
            var levelData = new LevelData($"{PluginPath}data{Level}.txt", staticInit);

            var player = new Player(levelData.PlayerPos, levelData.PlayerRoom);
            staticInit.Add("Player");

            RelicMenu.Transfer(player, staticInit);

            var mod = new ModController(turn, player, map);
            player.Blessing.BeforeGame(player, mod);

            var text = new NeuroPriest.Render.TextWriter(window, style, colour, player, map);
            var anim = new AnimSynchronizer(text);

            var enemy = new EnemyController(anim, turn, text, levelData, player, map, graph);

            staticInit.InitAll();
            Window.SetLayoutCache(LineCache.DOCUMENT); // seems to be less flicker this way

            RenderNow = WinWrapper.CreateEventA(IntPtr.Zero, false, false, string.Empty);
            RenderFinish = WinWrapper.CreateEventA(IntPtr.Zero, false, false, string.Empty);

            Game = new GameController(
                RenderNow,
                RenderFinish,
                turn,
                anim,
                mod,
                enemy,
                inter,
                levelData,
                player,
                map,
                AudioPlayer
            );
            Render = new Renderer(
                RenderNow,
                RenderFinish,
                colour,
                text,
                anim,
                mod,
                inter,
                enemy,
                player
            );
            Game.Start();
            Render.Start();

            Task.Factory.StartNew(() =>
            {
                while (player.Status == Player.GameStatus.PLAYER_NONE)
                {
                    Thread.Sleep(GAME_END_WAIT);
                }
                AudioPlayer.FadeOutTrack(AreaTrack, MUSIC_FADEOUT_TIME);
                Game.End();
                Render.End();

                Thread.Sleep(GAME_END_TIME);

                PlayResults(window, player);

                Thread.Sleep(END_SCREEN_TIME);

                Task.Factory.StartNew(() => TextEffects.FadeOut(Window, FADE_STEPS)).Wait();

                Thread.Sleep(BEFORE_MENU_TIME);

                if (player.Status == Player.GameStatus.PLAYER_WIN && Level == LAST_LEVEL)
                {
                    StartEnding();
                }
                else
                {
                    StartMenu();
                }
            });
        }

        private static void PlayResults(ScintillaGateway window, Player player)
        {
            TextEffects.Shutter(window);
            window.StyleSetFore(DEFAULT_STYLE, new Colour(255, 255, 255));
            if (player.Status == Player.GameStatus.PLAYER_WIN)
            {
                if (Level == LAST_LEVEL)
                {
                    window.SetText(MenuStrings.WIN);
                }
                else
                {
                    Level++;
                    Relic newRelic = RelicController.Add();
                    window.SetText(MenuStrings.WIN + "\n @ Received " + newRelic.Id);
                }
            }
            else
            {
                window.SetText(MenuStrings.LOSE);
            }
            Task.Factory.StartNew(() => TextEffects.FadeIn(window, FADE_STEPS)).Wait();
        }

        private static void SwapPage(string page, bool prev)
        {
            if (prev)
            {
                ClosePage();
            }
            File.Copy(PluginPath + page, PluginPath + "Neuropriest.txt", true);
            Win32.SendMessage(
                PluginBase.nppData._nppHandle,
                (uint)NppMsg.NPPM_DOOPEN,
                0,
                PluginPath + "Neuropriest.txt"
            );
        }

        private static void ClosePage()
        {
            Win32.SendMessage(
                PluginBase.nppData._nppHandle,
                (uint)NppMsg.NPPM_MENUCOMMAND,
                0,
                NppMenuCmd.IDM_FILE_SAVE
            );
            Win32.SendMessage(
                PluginBase.nppData._nppHandle,
                (uint)NppMsg.NPPM_MENUCOMMAND,
                0,
                NppMenuCmd.IDM_FILE_CLOSE
            );
        }

        private static void SetWindow()
        {
            Window = new ScintillaGateway(PluginBase.GetCurrentScintilla());
        }
    }
}
