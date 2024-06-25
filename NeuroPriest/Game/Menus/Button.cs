using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Shared;

namespace NeuroPriest.Menus
{
    internal class Button
    {
        public delegate void Touch();
        public int Begin { get; }
        public int End { get; }
        public string Sound { get; }
        public string Search { get; }
        private string Text { get; set; }
        private Touch Action { get; set; }
        private ScintillaGateway Window { get; }
        private bool Touched { get; set; }

        public Button(
            ScintillaGateway window,
            string search,
            int indic,
            string text = null,
            Touch touch = null,
            string sound = "wood"
        )
        {
            Window = window;
            Coord pos = Window.Find(search);
            Begin = pos.X;
            End = pos.Y;
            Search = search;
            Sound = sound;
            Set(search.Length + 1, indic, text, touch);
        }

        public Button(
            ScintillaGateway window,
            int begin,
            int len,
            int indic,
            string text = null,
            Touch touch = null,
            string sound = "wood"
        )
        {
            Window = window;
            Begin = begin;
            End = begin + len - 1;
            Sound = sound;
            Set(len, indic, text, touch);
        }

        private void Set(int len, int indic, string text = null, Touch touch = null)
        {
            Window.SetIndicator(indic, Begin, len);

            Action = touch;
            if (text != null)
            {
                Text = text;
            }
            else if (touch != null)
            {
                Action = touch;
            }

            Touched = false;
        }

        public void Press()
        {
            if (Action != null)
            {
                Action();
            }
            else if (Text != null)
            {
                Window.AnnotationSetText(Window.LineFromPosition(Begin), Touched ? "" : Text);
            }
            Touched = !Touched;
        }
    }
}
