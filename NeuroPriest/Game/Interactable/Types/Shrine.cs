using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Characters;
using NeuroPriest.Maps;
using NeuroPriest.Render;
using NeuroPriest.Shared;
using System.Diagnostics;

namespace NeuroPriest.Interactables
{
    internal class Shrine : Interactable
    {
        public override string Id => "W";
        public override Sprite Sprite => SpriteSave;
        private static Sprite SpriteSave { get; set; }

        public Shrine()
            : base(Tile.TileType.TILE_NONE, null) { }

        public Shrine(Coord coord)
            : base(Tile.TileType.TILE_INTER_SOLID, coord) { }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            SpriteSave = new Sprite(
                Id[0],
                styleProvider.Add(new Colour(140, 143, 141), new Colour(255, 255, 255))
            );
        }

        public override void Touch(
            Player player,
            Map map,
            InterController inteController,
            AudioPlayer audioPlayer
        )
        {
            Debug.WriteLine("Shrined");
        }
    }
}
