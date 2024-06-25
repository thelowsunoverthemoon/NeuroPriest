using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Characters;
using NeuroPriest.Maps;
using NeuroPriest.Render;
using NeuroPriest.Shared;

namespace NeuroPriest.Interactables
{
    internal class Mine : Interactable
    {
        public override string Id => "=";
        public override Sprite Sprite => SpriteSave;
        private static Sprite SpriteSave { get; set; }

        public Mine()
            : base(Tile.TileType.TILE_NONE, null) { }

        public Mine(Coord coord)
            : base(Tile.TileType.TILE_INTER_PASS, coord) { }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            SpriteSave = new Sprite(
                Id[0],
                styleProvider.Add(new Colour(128, 35, 23), new Colour(255, 255, 255))
            );
            audioPlayer.RegisterEffect("mine", "mine.mp3");
        }

        public override void Touch(
            Player player,
            Map map,
            InterController inteController,
            AudioPlayer audioPlayer
        )
        {
            if (!player.MineImmune)
            {
                audioPlayer.PlayEffect("mine");
                player.Status = Player.GameStatus.PLAYER_LOSE;
            }
        }
    }
}
