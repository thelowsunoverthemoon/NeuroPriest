using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Characters;
using NeuroPriest.Maps;
using NeuroPriest.Render;
using NeuroPriest.Shared;

namespace NeuroPriest.Interactables
{
    internal class Crate : Interactable
    {
        public override string Id => "O";

        private bool Broken { get; set; }
        public override Sprite Sprite => Broken ? BrokenSpriteSave : SpriteSave;
        private static Sprite SpriteSave { get; set; }
        private static Sprite BrokenSpriteSave { get; set; }

        public Crate()
            : base(Tile.TileType.TILE_NONE, null) { }

        public Crate(Coord coord)
            : base(Tile.TileType.TILE_INTER_SOLID, coord)
        {
            Broken = false;
        }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            SpriteSave = new Sprite(
                Id[0],
                styleProvider.Add(new Colour(110, 96, 41), new Colour(255, 255, 255))
            );
            BrokenSpriteSave = new Sprite(
                ' ',
                styleProvider.Add(new Colour(255, 255, 255), new Colour(255, 255, 255))
            );
            audioPlayer.RegisterEffect("jar", "jar.mp3");
        }

        public override void Touch(
            Player player,
            Map map,
            InterController inteController,
            AudioPlayer audioPlayer
        )
        {
            if (!Broken)
            {
                Broken = true;
                audioPlayer.PlayEffect("jar");
                map.Get(base.Pos.X, base.Pos.Y).Type = Tile.TileType.TILE_INTER_PASS;
            }
        }
    }
}
