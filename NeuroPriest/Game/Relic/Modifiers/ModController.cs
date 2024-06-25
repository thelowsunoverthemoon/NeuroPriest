using NeuroPriest.Characters;
using NeuroPriest.Game;
using NeuroPriest.Maps;
using System.Collections;
using System.Collections.Generic;

namespace NeuroPriest.Relics
{
    internal class ModController : IEnumerable
    {
        private List<Modifier> ModList { get; }
        private Player Player { get; }
        private Map Map { get; }
        private TurnSynchronizer TurnSynchronizer { get; }

        public ModController(TurnSynchronizer turn, Player player, Map map)
        {
            TurnSynchronizer = turn;
            ModList = new List<Modifier>();
            Player = player;
            Map = map;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (Modifier mod in ModList) // mke copy?
            {
                yield return mod;
            }
        }

        public void Use()
        {
            foreach (Modifier mod in new List<Modifier>(ModList))
            {
                bool remove = mod.Modify(Player);
                mod.Length--;
                if (mod.Length == 0 || remove)
                {
                    mod.Undo(Player);
                    ModList.Remove(mod);
                }
            }
        }

        public void Add(Modifier mod)
        {
            ModList.Add(mod);
        }
    }
}
