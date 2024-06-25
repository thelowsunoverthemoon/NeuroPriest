using NeuroPriest.Game;
using System.Collections;
using System.Collections.Generic;

namespace NeuroPriest.Interactables
{
    internal class InterController : IEnumerable
    {
        private List<Interactable> InterList { get; }
        private TurnSynchronizer TurnSynchronizer { get; }

        public InterController(TurnSynchronizer turnSynchronizer)
        {
            TurnSynchronizer = turnSynchronizer;
            InterList = new List<Interactable>();
        }

        public void Remove(Interactable inter)
        {
            InterList.Remove(inter);
        }

        public void Move()
        {
            // maybe the copy bug?
            foreach (Interactable inter in InterList)
            {
                inter.Next(TurnSynchronizer);
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (Interactable inter in InterList)
            {
                yield return inter;
            }
        }

        public void Add(Interactable inter)
        {
            InterList.Add(inter);
        }
    }
}
