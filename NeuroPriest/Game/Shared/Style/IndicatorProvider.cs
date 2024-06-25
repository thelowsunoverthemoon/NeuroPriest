using Kbg.NppPluginNET.PluginInfrastructure;

namespace NeuroPriest.Shared
{
    internal class IndicatorProvider
    {
        public static int IndicDefault { get; set; }
        public static int IndicSelect { get; set; }
        public static int IndicStart { get; set; }
        private ScintillaGateway Window { get; }
        private int IndicNum { get; set; }

        public IndicatorProvider(ScintillaGateway window)
        {
            Window = window;
            IndicNum = 0; // 22 + 1; // 22 is INDIC_POINT_TOP but not defind in template 0 vs 23?????
            IndicDefault = Add(
                IndicatorStyle.FULLBOX,
                ColourResource.ColMenuDefault,
                ColourResource.ColMenuHover
            );
            IndicSelect = Add(
                IndicatorStyle.FULLBOX,
                ColourResource.ColMenuSelect,
                ColourResource.ColMenuHover
            );
            IndicStart = Add(
                IndicatorStyle.FULLBOX,
                ColourResource.ColMenuStart,
                ColourResource.ColMenuHover
            );
        }

        private int Add(IndicatorStyle style, Colour fore, Colour hover)
        {
            Window.IndicSetAlpha(IndicNum, (Alpha)20);
            Window.IndicSetOutlineAlpha(IndicNum, (Alpha)255);

            Window.IndicSetStyle(IndicNum, style);
            Window.IndicSetFore(IndicNum, fore);
            Window.IndicSetHoverFore(IndicNum, hover);
            return IndicNum++;
        }
    }
}
