using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Characters;
using NeuroPriest.Game;
using NeuroPriest.Maps;
using NeuroPriest.Render;
using System.Collections.Generic;

namespace NeuroPriest.Relics
{
    internal class Tabernacle : Penance
    {
        public override string Image =>
            @"
 {{_\\ 
 }""^~` 
 ""@@@~  
";

        public override string Desc => "Gold gilded chest from a small chapel";
        public override string Effect => "Next attack for 2 turns does 1 more damage";
        public override string Id => "Tabernacle";
        private static List<Frame> FrameSave { get; set; }
        private static Sprite CrossSprite { get; set; }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            new BlessedWeapon().Init(styleProvider, audioPlayer);
            CrossSprite = new Sprite(
                '%',
                styleProvider.Add(new Colour(255, 250, 186), new Colour(168, 149, 0))
            );
            FrameSave = new List<Frame>
            {
                new Frame(new List<AnimSprite> { new AnimSprite(0, 0, CrossSprite) }, 1),
                new Frame(new List<AnimSprite> { new AnimSprite(0, 0, CrossSprite) }, 1),
                new Frame(new List<AnimSprite> { new AnimSprite(0, 0, CrossSprite) }, 1),
                new Frame(new List<AnimSprite> { new AnimSprite(0, 0, CrossSprite) }, 1)
            };
        }

        public override void Use(
            Player player,
            ModController modController,
            TurnSynchronizer turnSynchronizer,
            AnimSynchronizer animSynchronizer,
            EnemyController enemyController,
            Map map,
            AudioPlayer audioPlayer
        )
        {
            AnimHelpers.SetFrameAroundPlayer(FrameSave, player);
            animSynchronizer.Start(FrameSave);
            modController.Add(new BlessedWeapon(3));
        }
    }
}
