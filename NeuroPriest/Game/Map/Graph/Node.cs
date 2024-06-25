using NeuroPriest.Shared;
using System.Collections;
using System.Collections.Generic;

namespace NeuroPriest.Maps
{
    internal class Node : IEnumerable<Node>
    {
        private List<Node> Surround { get; }
        public Coord Pos { get; }

        public Node(Coord pos)
        {
            Surround = new List<Node>();
            Pos = pos;
        }

        public void Add(Node node)
        {
            Surround.Add(node);
        }

        public IEnumerator<Node> GetEnumerator()
        {
            foreach (Node neigh in Surround)
            {
                yield return neigh;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
