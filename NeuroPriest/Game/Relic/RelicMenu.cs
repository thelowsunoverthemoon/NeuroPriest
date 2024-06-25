using Kbg.NppPluginNET.PluginInfrastructure;
using NeuroPriest.Characters;
using NeuroPriest.Menus;
using NeuroPriest.Shared;
using System.Collections.Generic;
using System.Threading;

namespace NeuroPriest.Relics
{
    internal class RelicMenu
    {
        private ScintillaGateway Window { get; }
        private Menu Menu { get; set; }
        public Relic ArmaChristi { get; set; }
        public Relic Blessing { get; set; }
        public Relic Penance { get; set; }
        private RelicController RelicController { get; }

        public RelicMenu(ScintillaGateway window, RelicController relicController)
        {
            Window = window;
            RelicController = relicController;
            ArmaChristi = RelicController.ArmaChristiList[0];
            Blessing = RelicController.BlessingList[0];
            Penance = RelicController.PenanceList[0];
        }

        public void Transfer(Player player, StaticInitController staticInitController)
        {
            staticInitController.Add(ArmaChristi.Id);
            staticInitController.Add(Blessing.Id);
            staticInitController.Add(Penance.Id);
            player.ArmaChristi = (ArmaChristi)ArmaChristi;
            player.Blessing = (Blessing)Blessing;
            player.Penance = (Penance)Penance;
        }

        public void SetText(Menu menu)
        {
            Menu = menu;
            Window.GotoLine(Window.LineFromPosition(Window.Find(": FINISHED /").X));
            SetChoices("ARMA CHRISTI", RelicController.ArmaChristiList, ArmaChristi);
            SetChoices("BLESSING", RelicController.BlessingList, Blessing);
            SetChoices("PENANCE", RelicController.PenanceList, Penance);
        }

        private void SetChoices(string type, List<Relic> list, Relic cur)
        {
            string title = $"  :[[[[@({type})@[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[*\n\n";
            Window.AddText(title.Length, title);
            foreach (var relic in list)
            {
                string image =
                    $"   {relic.Id} \n"
                    + relic.Image.Substring(0, 9)
                    + relic.Desc
                    + relic.Image.Substring(9, 9)
                    + relic.Effect
                    + relic.Image.Substring(18)
                    + "\n";
                Window.AddText(image.Length, image);

                Thread.Sleep(25); // nice effect and also needed so insert text BEFORE look

                AddChoice(relic, cur, type);
            }
        }

        private void AddChoice(Relic relic, Relic cur, string type)
        {
            Coord find = Window.Find($" {relic.Id} ");
            int x = find.X;
            int y = find.Y - find.X;
            Menu.Add(
                new Button(
                    Window,
                    x,
                    y,
                    relic.Id == cur.Id
                        ? IndicatorProvider.IndicSelect
                        : IndicatorProvider.IndicDefault,
                    touch: () =>
                    {
                        if (type == "ARMA CHRISTI")
                        {
                            Menu.Deselect($" {ArmaChristi.Id} ");
                            ArmaChristi = relic;
                        }
                        else if (type == "BLESSING")
                        {
                            Menu.Deselect($" {Blessing.Id} ");
                            Blessing = relic;
                        }
                        else
                        {
                            Menu.Deselect($" {Penance.Id} ");
                            Penance = relic;
                        }
                        Menu.Select(x, y);
                    }
                )
            );
        }
    }
}
