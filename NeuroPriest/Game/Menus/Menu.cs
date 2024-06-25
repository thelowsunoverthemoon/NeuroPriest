using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Shared;
using System.Collections;
using System.Collections.Generic;

namespace NeuroPriest.Menus
{
    internal class Menu : IEnumerable
    {
        ScintillaGateway Window { get; }
        private List<Button> ButtonList { get; }

        public Menu(ScintillaGateway window)
        {
            ButtonList = new List<Button>();
            Window = window;
        }

        public void Add(Button button)
        {
            ButtonList.Add(button);
        }

        public void Clear()
        {
            ButtonList.Clear();
        }

        public IEnumerator GetEnumerator()
        {
            foreach (Button but in ButtonList)
            {
                yield return but;
            }
        }

        public void Select(int begin, int len)
        {
            Window.ClearIndicator(IndicatorProvider.IndicDefault, begin, len);
            Window.SetIndicator(IndicatorProvider.IndicSelect, begin, len);
        }

        public void EraseIndicators()
        {
            foreach (var b in ButtonList)
            {
                Coord coord = Window.Find(b.Search);
                Window.ClearIndicator(IndicatorProvider.IndicStart, coord.X, coord.Y - coord.X);
                Window.ClearIndicator(IndicatorProvider.IndicDefault, coord.X, coord.Y - coord.X);
            }
        }

        public void Deselect(string text)
        {
            Coord coord = Window.Find(text);

            Window.ClearIndicator(IndicatorProvider.IndicSelect, coord.X, coord.Y - coord.X);
            Window.SetIndicator(IndicatorProvider.IndicDefault, coord.X, coord.Y - coord.X);
        }
    }
}
