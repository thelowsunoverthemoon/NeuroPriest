namespace NeuroPriest.Game
{
    internal class TurnSynchronizer
    {
        public int Turn { get; private set; }

        public TurnSynchronizer()
        {
            Turn = 0;
        }

        public void Next()
        {
            Turn++;
        }

        public void Reset()
        {
            Turn = -1;
        }
    }
}
