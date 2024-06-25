using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Relics;
using NeuroPriest.Render;
using NeuroPriest.Shared;

namespace NeuroPriest.Characters
{
    internal class Player : Character
    {
        internal enum GameStatus
        {
            PLAYER_NONE,
            PLAYER_LOSE,
            PLAYER_WIN
        }

        public override string Id => "Player";

        public override Sprite Sprite => SpriteSave;
        private static Sprite SpriteSave { get; set; }
        public ArmaChristi ArmaChristi { get; set; } // cast to armachristi later bruh
        public Blessing Blessing { get; set; }
        public Penance Penance { get; set; }
        public bool HasShield { get; set; }
        public bool MineImmune { get; set; }
        public bool VentImmune { get; set; }
        public bool DoublePenance { get; set; }
        public int Atk { get; set; }
        public GameStatus Status { get; set; }
        public int Room { get; set; }
        public int SinMax { get; set; }
        private int sin;
        public int Sin
        {
            get { return sin; }
            set { sin = value > SinMax ? SinMax : value; }
        }

        public Player()
            : base(null) { }

        public Player(Coord coord, int room)
            : base(coord)
        {
            sin = 0;
            SinMax = 3;
            Atk = 1;
            Status = GameStatus.PLAYER_NONE;
            Room = room;
            MineImmune = false;
            VentImmune = false;
            HasShield = false;
            DoublePenance = false;
        }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            SpriteSave = new Sprite(
                '$',
                styleProvider.Add(new Colour(252, 186, 3), new Colour(86, 122, 105))
            );
        }
    }
}
