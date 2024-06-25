using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Characters;
using NeuroPriest.Render;

namespace NeuroPriest.Relics
{
    internal class BlessedWeapon : Modifier
    {
        public override string Id => "EnhanceAttack";
        public override string Desc => "+1 Attack";
        public override Sprite Sprite => SpriteSave;
        private static Sprite SpriteSave { get; set; }
        private bool HasAtk;

        public BlessedWeapon()
            : base(0) { }

        public BlessedWeapon(int? length)
            : base(length) { }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            SpriteSave = new Sprite(
                '^',
                styleProvider.Add(new Colour(237, 24, 70), new Colour(215, 222, 89))
            );
        }

        public override bool Modify(Player player)
        {
            if (!HasAtk)
            {
                player.Atk++;
                HasAtk = true;
            }
            return false;
        }

        public override void Undo(Player player)
        {
            player.Atk--;
        }
    }
}
