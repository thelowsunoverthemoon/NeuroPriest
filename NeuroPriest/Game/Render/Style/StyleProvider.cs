using Kbg.NppPluginNET.PluginInfrastructure;
using System.Collections.Generic;

namespace NeuroPriest.Render
{
    internal class StyleProvider
    {
        private int StyleNum { get; set; }
        private ScintillaGateway Window { get; }
        private Dictionary<Style, int> StyleCache { get; }

        public StyleProvider(ScintillaGateway window)
        {
            StyleNum = (int)SciMsg.STYLE_LASTPREDEFINED + 1;
            StyleCache = new Dictionary<Style, int>();
            Window = window;
        }

        public int Add(Colour fore, Colour back)
        {
            Style check = new Style(fore, back);
            if (StyleCache.ContainsKey(check))
            {
                return StyleCache[check];
            }
            StyleCache[check] = StyleNum;
            Window.StyleSetFore(StyleNum, fore);
            Window.StyleSetBack(StyleNum, back);

            return StyleNum++;
        }
    }
}
