using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Characters;
using NeuroPriest.Interactables;
using NeuroPriest.Maps;
using NeuroPriest.Relics;
using NeuroPriest.Shared;
using System.Text;

namespace NeuroPriest.Render
{
    internal class TextWriter
    {
        const string SIN_TEXT = "\n Sin * ";
        private int Width { get; }
        private ScintillaGateway Window { get; }
        private ColourWriter ColourWriter { get; }
        public StringBuilder Grid { get; set; } // make grid + statlist private and make new var?
        private StringBuilder GridFreeze { get; set; }
        private Map Map { get; }
        private Player Player { get; }
        private string SinDisp { get; }
        private int SinStyle { get; }
        private int SinLen { get; }
        public StringBuilder StatList { get; }
        private int ModLen { get; }

        public TextWriter(
            ScintillaGateway window,
            StyleProvider styleProvider,
            ColourWriter colourWriter,
            Player player,
            Map map
        )
        {
            GridFreeze = null;
            Window = window;
            SinStyle = styleProvider.Add(new Colour(0, 0, 0), new Colour(255, 255, 0));
            ColourWriter = colourWriter;
            Player = player;
            Map = map;
            StatList = new StringBuilder();
            Width = Window.LineLength(0);

            Grid = new StringBuilder();
            for (int i = 0; i < Window.GetLineCount(); i++)
            {
                Grid.Append(Window.GetLine(i));
            }
            SinDisp = $"{SIN_TEXT}%{new StringBuilder().Insert(0, "=%", Player.SinMax)}";
            foreach (var d in Player.ArmaChristi.AttackDir)
            {
                SinDisp += ' ';
                switch (d)
                {
                    case WinWrapper.ArrowKeys.VK_RIGHT:
                        SinDisp += '>';
                        break;
                    case WinWrapper.ArrowKeys.VK_LEFT:
                        SinDisp += '<';
                        break;
                    case WinWrapper.ArrowKeys.VK_UP:
                        SinDisp += '^';
                        break;
                    case WinWrapper.ArrowKeys.VK_DOWN:
                        SinDisp += 'v';
                        break;
                }
            }
            SinDisp += '\n';
            SinLen = Window.GetLength() + SIN_TEXT.Length;
            ModLen = Window.GetLength() + SinDisp.Length;
        }

        public void Render()
        {
            Window.SetText(Grid.ToString() + StatList);
        }

        private void PutMap(Coord pos, Sprite sprite, bool color = true)
        {
            int flatPos = ToFlat(pos);
            Grid[flatPos] = sprite.Write;
            if (color)
            {
                ColourWriter.Add(flatPos, sprite.Style, 1);
            }
        }

        private void BlitChara(Coord pos, Coord prev, Sprite sprite, bool color = true)
        {
            Tile prevTile = Map.Get(prev.X, prev.Y);
            if (
                prev.X != 0
                && prev.Y != 0
                && prevTile.Type == Tile.TileType.TILE_NONE
                && !prevTile.HasEnemy
            )
            {
                Grid[ToFlat(prev)] = ' ';
            }

            PutMap(pos, sprite, color);
        }

        public void BlitStats(ModController modController)
        {
            StatList.Clear();
            StatList.Append(SinDisp);
            ColourWriter.Add(SinLen, SinStyle, (Player.Sin > 0 ? 1 : 0) + (Player.Sin * 2));

            int modPos = ModLen + 1; // space in front of mod

            foreach (Modifier mod in modController)
            {
                string line =
                    $" {mod.Sprite.Write} {mod.Desc} ({(mod.Length == null ? "I" : mod.Length.ToString())})\n"; // make better name!
                StatList.Append(line);
                ColourWriter.Add(modPos, mod.Sprite.Style, 1);
                modPos += line.Length;
            }
        }

        public void Blit(Character chara, bool color = true)
        {
            BlitChara(chara.Pos, chara.Prev, chara.Sprite, color);
        }

        public void Blit(AnimSynchronizer animSynchronizer)
        {
            foreach (var disp in animSynchronizer.CurFrame.Sprites)
            {
                PutMap(disp.Pos, disp.Sprite);
            }
        }

        public void Blit(InterController interController)
        {
            foreach (Interactable inter in interController)
            {
                PutMap(inter.Pos, inter.Sprite);
            }
        }

        public void Blit(EnemyController enemyController)
        {
            foreach (Enemy enemy in enemyController.GetActive())
            {
                Blit(enemy);
            }
            foreach (Enemy enemy in enemyController.GetSleeping())
            {
                Blit(enemy, false);
            }
        }

        public void UseSaved()
        {
            if (GridFreeze == null)
            {
                GridFreeze = Grid;
            }
            Grid = new StringBuilder(GridFreeze.Length);
            Grid.Append(GridFreeze);
        }

        public void ClearSaved()
        {
            GridFreeze = null;
        }

        public bool IsRendered()
        {
            return Window.GetLength() >= Grid.Length;
        }

        public void Replace(Coord pos, char write)
        {
            Grid[ToFlat(pos)] = write;
        }

        private int ToFlat(Coord pos)
        {
            return pos.Y * Width + pos.X;
        }
    }
}
