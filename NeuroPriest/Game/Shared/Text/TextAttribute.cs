using Kbg.NppPluginNET.PluginInfrastructure;

namespace NeuroPriest.Shared
{
    internal class TextAttribute
    {
        public Colour CaretLineBack { get; set; }
        public Alpha Alpha { get; set; }
        public CaretStyle CaretStyle { get; set; }
        public bool CaretLineVisible { get; set; }
        public int MouseDwellTime { get; set; }
        public AnnotationVisible AnnotationVisible { get; set; }
        public string WordChars { get; set; }
        public IndentView IndentView { get; set; }
        public int Highlight { get; set; }
    }
}
