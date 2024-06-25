using Kbg.NppPluginNET.PluginInfrastructure;

namespace NeuroPriest.Shared
{
    internal static class WindowExtensions // remember to replace all isntances with extensions
    {
        public static void HideUser(this ScintillaGateway window, TextAttribute save)
        {
            window.SetEmptySelection(window.GetLength());

            save.CaretLineBack = window.GetCaretLineBack();

            save.Alpha = window.GetSelAlpha();
            window.SetSelAlpha(Alpha.TRANSPARENT);

            save.CaretStyle = window.GetCaretStyle();
            window.SetCaretStyle(CaretStyle.INVISIBLE);

            save.CaretLineVisible = window.GetCaretLineVisible();
            window.SetCaretLineVisible(false);
        }

        public static void MakeGui(
            this ScintillaGateway window,
            TextAttribute save,
            bool willChange
        )
        {
            if (!willChange)
            {
                window.SetReadOnly(true);
            }
            save.AnnotationVisible = window.AnnotationGetVisible();
            window.AnnotationSetVisible(AnnotationVisible.BOXED);

            save.MouseDwellTime = window.GetMouseDwellTime();
            window.SetMouseDwellTime(5);

            save.IndentView = window.GetIndentationGuides();
            window.SetIndentationGuides(IndentView.NONE);

            save.Highlight = window.GetHighlightGuide();
            window.SetHighlightGuide(0);
        }

        public static Coord Find(this ScintillaGateway window, string search)
        {
            window.SetTargetStart(0);
            window.SetTargetEnd(window.GetLength());
            int begin = window.SearchInTarget(search.Length, search);
            return new Coord(begin, begin + search.Length);
        }

        public static void SetIndicator(this ScintillaGateway window, int indic, int begin, int end)
        {
            window.SetIndicatorCurrent(indic);
            window.IndicatorFillRange(begin, end);
        }

        public static void ClearIndicator(
            this ScintillaGateway window,
            int indic,
            int begin,
            int end
        )
        {
            window.SetIndicatorCurrent(indic);
            window.IndicatorClearRange(begin, end);
        }

        public static void DisableHighlight(this ScintillaGateway window, TextAttribute save)
        {
            save.WordChars = window.GetWordChars();
            window.SetWordChars("");
        }

        public static void RestoreAttr(this ScintillaGateway window, TextAttribute save)
        {
            window.SetReadOnly(false);
            window.SetSelAlpha(save.Alpha);
            window.SetCaretLineBack(save.CaretLineBack);
            window.SetCaretStyle(save.CaretStyle);
            window.SetCaretLineVisible(save.CaretLineVisible);
            window.AnnotationSetVisible(save.AnnotationVisible);
            window.SetMouseDwellTime(save.MouseDwellTime);
            window.SetWordChars(save.WordChars);
            window.SetIndentationGuides(save.IndentView);
            window.SetHighlightGuide(save.Highlight);
        }
    }
}
