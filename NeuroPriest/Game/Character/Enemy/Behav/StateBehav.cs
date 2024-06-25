using System;

namespace NeuroPriest.Characters
{
    internal abstract class StateBehav
    {
        protected int State { get; set; } = 0;
        protected int Wait { get; set; } = 0;
        protected Random Random { get; } = new Random();

        protected void SetState(int state, int wait)
        {
            State = state;
            Wait = wait;
        }

        protected void IfWaitDoneChangeState(int state, int wait) // change name later
        {
            if (Wait == 0)
            {
                SetState(state, wait);
            }
            Wait--;
        }
    }
}
