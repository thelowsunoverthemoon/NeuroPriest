using NeuroPriest.Audio;
using NeuroPriest.Characters;
using NeuroPriest.Game;
using NeuroPriest.Maps;
using NeuroPriest.Shared;

namespace NeuroPriest.Interactables
{
    internal abstract class Interactable : StaticSprite
    {
        public string Name { get; set; }
        public Coord Pos { get; set; }
        public Tile.TileType Type { get; set; }

        protected Interactable(Tile.TileType type, Coord pos)
        {
            Type = type;
            Pos = pos;
        }

        public abstract void Touch(
            Player player,
            Map map,
            InterController inteController,
            AudioPlayer audioPlayer
        );

        public virtual void Next(TurnSynchronizer turnSynchronizer) { }
    }
}
