using Kbg.NppPluginNET.PluginInfrastructure;
using System;
using System.Diagnostics;

namespace NeuroPriest.Render
{
    internal class Style
    {
        private Colour Fore { get; }
        private Colour Back { get; }

        public Style(Colour fore, Colour back)
        {
            Fore = fore;
            Back = back;
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Style p = (Style)obj;
                Debug.WriteLine((Fore.Value == p.Fore.Value) && (Back.Value == p.Back.Value));
                return (Fore.Value == p.Fore.Value) && (Back.Value == p.Back.Value);
            }
        }

        public override int GetHashCode()
        {
            return Fore.Value * 31 + Back.Value;
        }
    }
}
