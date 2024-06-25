using NeuroPriest.Characters;
using NeuroPriest.Interactables;
using NeuroPriest.Relics;
using NeuroPriest.Shared;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NeuroPriest.Render
{
    internal class Renderer
    {
        private const int RENDER_WAIT = 500;
        private const int ANIM_INTERVAL = 25;
        private EnemyController EnemyController { get; }
        private ColourWriter ColourWriter { get; }
        private TextWriter TextWriter { get; }
        private InterController InterController { get; }
        private AnimSynchronizer AnimSynchronizer { get; }
        private ModController ModController { get; }
        private Player Player { get; }
        private IntPtr RenderNow { get; }
        private IntPtr RenderFinish { get; }
        private CancellationTokenSource Cancel { get; set; }
        private Task Render { get; set; }

        public Renderer(
            IntPtr renderNow,
            IntPtr renderFinish,
            ColourWriter colourWriter,
            TextWriter textWriter,
            AnimSynchronizer animSynchronizer,
            ModController modController,
            InterController interController,
            EnemyController enemyController,
            Player player
        )
        {
            RenderNow = renderNow;
            RenderFinish = renderFinish;
            ColourWriter = colourWriter;
            TextWriter = textWriter;
            EnemyController = enemyController;
            InterController = interController;
            ModController = modController;
            AnimSynchronizer = animSynchronizer;
            Player = player;
        }

        public void Start()
        {
            Cancel = new CancellationTokenSource();
            Render = Task.Factory.StartNew(
                () =>
                {
                    while (!Cancel.IsCancellationRequested)
                    {
                        if (AnimSynchronizer.HasAnim)
                        {
                            TextWriter.UseSaved();

                            ColourWriter.Clear();
                            TextWriter.Blit(InterController);
                            TextWriter.Blit(Player);
                            TextWriter.Blit(EnemyController);
                            TextWriter.BlitStats(ModController);
                            TextWriter.Blit(AnimSynchronizer);

                            TextWriter.Render();
                            ColourWriter.Colourize(TextWriter.Grid, TextWriter.StatList);

                            Thread.Sleep(AnimSynchronizer.CurFrame.Time * ANIM_INTERVAL);
                            AnimSynchronizer.Next();
                        }
                        else
                        {
                            WinWrapper.WaitForSingleObject(RenderNow, RENDER_WAIT);
                            ColourWriter.Clear();
                            TextWriter.Blit(InterController);
                            TextWriter.Blit(Player);
                            TextWriter.Blit(EnemyController);
                            TextWriter.BlitStats(ModController);
                            TextWriter.Render();
                            ColourWriter.Colourize(TextWriter.Grid, TextWriter.StatList);

                            WinWrapper.SetEvent(RenderFinish);
                        }
                    }
                },
                Cancel.Token
            );
        }

        public void End()
        {
            Cancel.Cancel();
            Render.Wait();
        }
    }
}
