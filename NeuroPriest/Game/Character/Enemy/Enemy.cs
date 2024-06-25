using NeuroPriest.Shared;

namespace NeuroPriest.Characters
{
    internal abstract class Enemy : Character
    {
        public EnemyBehav Behav { get; }
        public bool IsBoss { get; }
        public int Health { get; set; }

        protected Enemy(int hp, Coord coord, EnemyBehav behav, bool isBoss)
            : base(coord)
        {
            Behav = behav;
            Health = hp;
            IsBoss = isBoss;
        }
    }
}
