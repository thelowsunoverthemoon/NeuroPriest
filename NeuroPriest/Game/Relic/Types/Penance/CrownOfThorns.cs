using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Characters;
using NeuroPriest.Game;
using NeuroPriest.Maps;
using NeuroPriest.Render;
using NeuroPriest.Shared;
using System.Collections.Generic;

namespace NeuroPriest.Relics
{
    internal class CrownOfThorns : Penance
    {
        public override string Image =>
            @"
  {^\  
 : = /  
  ""v~  
";

        public override string Desc => "Bloodstained thorns on a twisted stem";
        public override string Effect => "Deal 1 damage to enemies in your attack directions";
        public override string Id => "Crown Of Thorns";
        private static List<Frame> FrameSave { get; set; }
        private static Sprite ThornSprite { get; set; }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            new BlessedWeapon().Init(styleProvider, audioPlayer);
            ThornSprite = new Sprite(
                '[',
                styleProvider.Add(new Colour(184, 15, 15), new Colour(255, 255, 255))
            );
            FrameSave = new List<Frame>
            {
                new Frame(
                    new List<AnimSprite>
                    {
                        new AnimSprite(0, 0, ThornSprite),
                        new AnimSprite(0, 0, ThornSprite),
                        new AnimSprite(0, 0, ThornSprite),
                        new AnimSprite(0, 0, ThornSprite),
                        new AnimSprite(0, 0, ThornSprite),
                        new AnimSprite(0, 0, ThornSprite),
                        new AnimSprite(0, 0, ThornSprite),
                        new AnimSprite(0, 0, ThornSprite)
                    },
                    2
                )
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
            foreach (var d in player.ArmaChristi.AttackDir)
            {
                AnimHelpers.SetOmniPositionAroundPlayer(FrameSave[0], player);
                animSynchronizer.Start(FrameSave);

                int testX = player.Pos.X;
                int testY = player.Pos.Y;
                switch (d)
                {
                    case WinWrapper.ArrowKeys.VK_RIGHT:
                        testX++;
                        break;
                    case WinWrapper.ArrowKeys.VK_LEFT:
                        testX--;
                        break;
                    case WinWrapper.ArrowKeys.VK_UP:
                        testY--;
                        break;
                    case WinWrapper.ArrowKeys.VK_DOWN:
                        testY++;
                        break;
                }
                Tile tile = map.Get(testX, testY);
                if (tile.HasEnemy)
                {
                    enemyController.Hurt(tile.Enemy);
                }
            }
        }
    }
}
