using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Audio;
using NeuroPriest.Characters;
using NeuroPriest.Game;
using NeuroPriest.Maps;
using NeuroPriest.Render;
using System.Collections.Generic;

namespace NeuroPriest.Relics
{
    internal class GoldenCross : Penance
    {
        public override string Image =>
            @"
  _#_  
   %   
   #   
";
        public override string Desc => "Simple, small cross gilded with gold";
        public override string Effect => "God's will is mysterious";
        public override string Id => "Golden Cross";
        private static List<Frame> FrameSave { get; set; }
        private static Sprite CrossSprite { get; set; }
        private static Sprite CrossGoldSprite { get; set; }

        public override void Init(StyleProvider styleProvider, AudioPlayer audioPlayer)
        {
            CrossSprite = new Sprite(
                'o',
                styleProvider.Add(new Colour(240, 226, 101), new Colour(255, 255, 255))
            );
            CrossGoldSprite = new Sprite(
                'O',
                styleProvider.Add(new Colour(240, 226, 101), new Colour(250, 244, 190))
            );
            FrameSave = new List<Frame>
            {
                new Frame(
                    new List<AnimSprite>
                    {
                        new AnimSprite(0, 0, CrossSprite),
                        new AnimSprite(0, 0, CrossSprite),
                        new AnimSprite(0, 0, CrossSprite),
                        new AnimSprite(0, 0, CrossSprite)
                    },
                    5
                ),
                new Frame(
                    new List<AnimSprite>
                    {
                        new AnimSprite(0, 0, CrossGoldSprite),
                        new AnimSprite(0, 0, CrossGoldSprite),
                        new AnimSprite(0, 0, CrossGoldSprite),
                        new AnimSprite(0, 0, CrossGoldSprite)
                    },
                    7
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
            AnimHelpers.SetPositionAroundPlayer(FrameSave[0], player);
            AnimHelpers.SetPositionAroundPlayer(FrameSave[1], player);

            animSynchronizer.Start(FrameSave);
        }
    }
}
