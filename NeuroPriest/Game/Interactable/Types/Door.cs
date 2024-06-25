using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Characters;
using NeuroPriest.Maps;
using NeuroPriest.Render;
using NeuroPriest.Shared;

namespace NeuroPriest.Interactables
{
    internal class Door : Interactable
    {
        public override string Id => "7";

        public bool Open { get; set; }
        public override Sprite Sprite => Open ? OpenSpriteSave : SpriteSave;
        private static Sprite SpriteSave { get; set; }
        private static Sprite OpenSpriteSave { get; set; }

        public Door()
            : base(Tile.TileType.TILE_NONE, null) { }

        public Door(Coord coord)
            : base(Tile.TileType.TILE_INTER_SOLID, coord)
        {
            Name = "Door";
        }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            SpriteSave = new Sprite(
                Id[0],
                styleProvider.Add(new Colour(0, 0, 0), new Colour(255, 255, 255))
            );
            // CHANGE TO NO COL
            OpenSpriteSave = new Sprite(
                ' ',
                styleProvider.Add(new Colour(0, 0, 0), new Colour(255, 255, 255))
            );
        }

        // maybe add sound?
        public override void Touch(
            Player player,
            Map map,
            InterController inteController,
            AudioPlayer audioPlayer
        ) { }
    }
}
