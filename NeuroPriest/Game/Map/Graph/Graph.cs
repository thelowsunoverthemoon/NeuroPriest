using NeuroPriest.Shared;
using System.Collections.Generic;

namespace NeuroPriest.Maps
{
    internal class Graph
    {
        private readonly Coord[] DIRECTIONS =
        {
            new Coord(1, 0),
            new Coord(-1, 0),
            new Coord(0, 1),
            new Coord(0, -1)
        };
        private Map Map { get; }
        public Dictionary<Coord, Node> PosDict { get; }

        public Graph(Map map)
        {
            PosDict = new Dictionary<Coord, Node>();
            Map = map;

            Coord orig = new Coord(0, 0);
            Coord dir = new Coord(0, 0);
            for (int y = 1; y < Map.Height - 1; y++)
            {
                for (int x = 1; x < Map.Width - 1; x++)
                {
                    if (IsMovableTile(x, y))
                    {
                        orig.X = x;
                        orig.Y = y;
                        if (!PosDict.ContainsKey(orig))
                        {
                            Coord pos = new Coord(x, y);
                            PosDict.Add(pos, new Node(pos));
                        }
                        foreach (Coord pos in DIRECTIONS)
                        {
                            dir.X = x + pos.X;
                            dir.Y = y + pos.Y;

                            if (IsMovableTile(dir.X, dir.Y))
                            {
                                if (!PosDict.ContainsKey(dir))
                                {
                                    Coord posD = new Coord(dir.X, dir.Y);
                                    PosDict.Add(posD, new Node(posD));
                                }
                                PosDict[orig].Add(PosDict[dir]);
                            }
                        }
                    }
                }
            }
        }

        private bool IsMovableTile(int x, int y)
        {
            return Map.GetType(x, y) != Tile.TileType.TILE_WALL;
        }
    }
}
