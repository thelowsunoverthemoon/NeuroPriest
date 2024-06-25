using NeuroPriest.Characters;
using NeuroPriest.Interactables;

namespace NeuroPriest.Maps
{
    internal class Tile
    {
        internal enum TileType
        {
            TILE_NONE,
            TILE_WALL,
            TILE_INTER_PASS,
            TILE_INTER_SOLID
        }

        public TileType Type { get; set; }
        public Player Player { get; set; }
        public Enemy Enemy { get; set; }
        public bool HasPlayer => Player != null;
        public bool HasEnemy => Enemy != null;
        public Interactable Interactable { get; set; }

        public Tile(TileType type)
        {
            Type = type;
        }
    }
}
