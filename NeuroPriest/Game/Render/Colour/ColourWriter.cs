using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Maps;
using System;
using System.Text;

namespace NeuroPriest.Render
{
    internal partial class ColourWriter
    {
        private ScintillaGateway Window { get; }
        private Map Map { get; }
        private byte[] StyleGrid { get; }

        public ColourWriter(ScintillaGateway window, Map map)
        {
            Window = window;
            Map = map;
            StyleGrid = new byte[9000]; // make const, never gonna be larger than 9000 chars, make constnt
        }

        public void Add(int pos, int style, int len)
        {
            for (int i = 0; i < len; i++)
            {
                StyleGrid[pos + i] = Convert.ToByte(style);
            }
        }

        public void Clear()
        {
            for (int i = 0; i < StyleGrid.Length; i++)
            {
                StyleGrid[i] = Convert.ToByte(SciMsg.STYLE_DEFAULT);
            }
        }

        public void Colourize(StringBuilder grid, StringBuilder stat)
        {
            Window.StartStyling(0, 0);
            Window.SetStylingEx(grid.Length + stat.Length, StyleGrid);
        }
    }
}
