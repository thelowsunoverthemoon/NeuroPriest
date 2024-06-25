using Kbg.NppPluginNET.PluginInfrastructure;
using System.Linq;
using System.Threading;

namespace NeuroPriest.Menus
{
    internal static class TextEffects
    {
        public static void Shutter(ScintillaGateway window)
        {
            int lines = window.GetLineCount();
            for (int i = 0; i < lines; i++)
            {
                window.DeleteRange(window.PositionFromLine(0), window.LineLength(0));
                Thread.Sleep(25);
            }
            Thread.Sleep(1000);
        }

        public static void FadeOut(ScintillaGateway window, int steps) // instead of default change style to smth else
        {
            Colour save = window.StyleGetFore((int)SciMsg.STYLE_DEFAULT);
            int stepR = (255 - save.Red) / steps; // assume white background
            int stepG = (255 - save.Green) / steps;
            int stepB = (255 - save.Blue) / steps;
            for (int i = 1; i <= steps; i++)
            {
                window.StyleSetFore(
                    (int)SciMsg.STYLE_DEFAULT,
                    new Colour(
                        save.Red + (stepR * i),
                        save.Green + (stepG * i),
                        save.Blue + (stepB * i)
                    )
                );
                window.StartStyling(0, 0);
                window.SetStyling(window.GetLength(), (int)SciMsg.STYLE_DEFAULT);
                Thread.Sleep(10);
            }
            window.ClearAll();
            window.StyleSetFore((int)SciMsg.STYLE_DEFAULT, save);
        }

        public static void FadeIn(ScintillaGateway window, int steps)
        {
            for (int i = 1; i <= steps; i++)
            {
                int step = 255 / steps; // assume text is white
                window.StyleSetFore(
                    (int)SciMsg.STYLE_DEFAULT,
                    new Colour(255 - (step * i), 255 - (step * i), 255 - (step * i))
                );
                window.StartStyling(0, 0);
                window.SetStyling(window.GetLength(), (int)SciMsg.STYLE_DEFAULT);
                Thread.Sleep(10);
            }
        }

        public static void InsertText(ScintillaGateway window, string text)
        {
            foreach (char c in text.Reverse())
            {
                window.InsertText(0, c.ToString());
                Thread.Sleep(5);
            }
        }
    }
}
