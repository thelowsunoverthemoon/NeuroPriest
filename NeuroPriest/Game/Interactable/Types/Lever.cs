using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Characters;
using NeuroPriest.Maps;
using NeuroPriest.Render;
using NeuroPriest.Shared;

namespace NeuroPriest.Interactables
{
    internal class Lever : Interactable
    {
        public override string Id => "L";
        private bool Used { get; set; }
        public override Sprite Sprite => Used ? UsedSpriteSave : SpriteSave;
        private static Sprite SpriteSave { get; set; }
        private static Sprite UsedSpriteSave { get; set; }

        public Lever()
            : base(Tile.TileType.TILE_NONE, null) { }

        public Lever(Coord coord)
            : base(Tile.TileType.TILE_INTER_SOLID, coord)
        {
            Used = false;
        }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            SpriteSave = new Sprite(
                Id[0],
                styleProvider.Add(new Colour(140, 143, 141), new Colour(255, 255, 255))
            );
            UsedSpriteSave = new Sprite(
                '_',
                styleProvider.Add(new Colour(140, 143, 141), new Colour(255, 255, 255))
            );
            audioPlayer.RegisterEffect("lever", "lever.mp3");
        }

        public override void Touch(
            Player player,
            Map map,
            InterController interController,
            AudioPlayer audioPlayer
        )
        {
            Used = true;
            audioPlayer.PlayEffect("lever");
            foreach (Interactable i in interController)
            {
                if (i.Name == "Door")
                {
                    map.Get(i.Pos.X, i.Pos.Y).Type = Tile.TileType.TILE_INTER_PASS;
                    ((Door)i).Open = true;
                }
            }

            map.Get(base.Pos.X, base.Pos.Y).Type = Tile.TileType.TILE_WALL;
        }
    }
}
