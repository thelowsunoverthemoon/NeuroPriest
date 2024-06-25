using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Characters;
using NeuroPriest.Game;
using NeuroPriest.Maps;
using NeuroPriest.Render;
using NeuroPriest.Shared;

namespace NeuroPriest.Interactables
{
    internal class HeatVent : Interactable
    {
        public override string Id => "x";
        private bool OnFire { get; set; } = true; // check style
        public override Sprite Sprite => OnFire ? OnFireSpriteSave : SpriteSave;
        private static Sprite SpriteSave { get; set; }
        private static Sprite OnFireSpriteSave { get; set; }

        public HeatVent()
            : base(Tile.TileType.TILE_NONE, null) { }

        public HeatVent(Coord coord)
            : base(Tile.TileType.TILE_INTER_PASS, coord) { }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            OnFireSpriteSave = new Sprite(
                Id[0],
                styleProvider.Add(new Colour(237, 41, 19), new Colour(196, 193, 188))
            );
            SpriteSave = new Sprite(
                'x',
                styleProvider.Add(new Colour(120, 76, 71), new Colour(196, 193, 188))
            );
            audioPlayer.RegisterEffect("heat", "heat.mp3");
        }

        public override void Touch(
            Player player,
            Map map,
            InterController inteController,
            AudioPlayer audioPlayer
        )
        {
            if (!player.VentImmune && OnFire)
            {
                audioPlayer.PlayEffect("heat");
                player.Status = Player.GameStatus.PLAYER_LOSE;
            }
        }

        public override void Next(TurnSynchronizer turnSynchronizer)
        {
            OnFire = turnSynchronizer.Turn % 2 == 0;
        }
    }
}
