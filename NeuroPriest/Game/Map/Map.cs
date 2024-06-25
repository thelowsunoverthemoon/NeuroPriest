using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Characters;
using NeuroPriest.Interactables;
using NeuroPriest.Shared;
using System.Linq;

namespace NeuroPriest.Maps
{
    internal class Map
    {
        private const int CR_LF_ENDING = 2;
        private static readonly char[] WALL_CHARA =
        {
            ')',
            '(',
            '%',
            '#',
            '-',
            '&',
            '*',
            '@',
            ':',
            '[',
            '!',
            ']',
            '|',
            '{',
            '"',
            '~',
            '\\',
            '/'
        };
        private ScintillaGateway Window { get; }
        private StaticInitController StaticInitController { get; }
        private Tile[,] Grid { get; }

        public int Height { get; }
        public int Width { get; }

        public Map(
            ScintillaGateway window,
            InterController interController,
            StaticInitController staticSpriteController
        )
        {
            Window = window;
            StaticInitController = staticSpriteController;
            Height = Window.GetLineCount();
            Width = Window.LineLength(0) - CR_LF_ENDING;
            Grid = new Tile[Height, Width];
            for (int i = 0; i < Height; i++)
            {
                string line = Window.GetLine(i);
                for (int j = 0; j < Width; j++)
                {
                    Tile tile = new Tile(Tile.TileType.TILE_NONE);
                    if (WALL_CHARA.Contains(line[j]))
                    {
                        tile.Type = Tile.TileType.TILE_WALL;
                    }
                    else
                    {
                        var sprite = StaticInitController.Lookup(line[j].ToString()); // cahnge name bruh
                        if (sprite != null)
                        {
                            StaticInitController.Add(line[j].ToString());
                            Interactable inter = (Interactable)sprite(new Coord(j, i));
                            tile.Type = inter.Type;
                            tile.Interactable = inter;
                            interController.Add(inter);
                        }
                    }
                    Grid[i, j] = tile;
                }
            }
        }

        public Tile Get(int x, int y)
        {
            return Grid[y, x];
        }

        public Tile.TileType GetType(int x, int y)
        {
            return Grid[y, x].Type;
        }

        public Tile.TileType GetType(Coord pos)
        {
            return Grid[pos.Y, pos.X].Type;
        }

        public void Set(Player move)
        {
            if (move.Prev.X != 0 && move.Prev.Y != 0)
            {
                Grid[move.Prev.Y, move.Prev.X].Player = null;
            }
            Grid[move.Pos.Y, move.Pos.X].Player = move;
        }

        public void Set(Enemy move)
        {
            if (
                move.Prev.X != 0
                && move.Prev.Y != 0
                && ReferenceEquals(Grid[move.Prev.Y, move.Prev.X].Enemy, move)
            )
            {
                Grid[move.Prev.Y, move.Prev.X].Enemy = null;
            }
            Grid[move.Pos.Y, move.Pos.X].Enemy = move;
        }

        public void Remove(Enemy move)
        {
            Grid[move.Pos.Y, move.Pos.X].Enemy = null;
        }
    }
}
