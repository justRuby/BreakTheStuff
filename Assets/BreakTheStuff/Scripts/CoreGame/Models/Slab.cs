namespace BreakTheStuff.CoreGame.Models
{
    public class Slab
    {
        public int Index { get; }
        public int LocalX { get; }
        public int LocalY { get; }

        private int X { get; set; }
        private int Y { get; set; }

        public bool IsAlive { get; private set; }

        public Slab(int index, int localX, int localY, int x, int y)
        {
            this.Index = index;
            this.LocalX = localX;
            this.LocalY = localY;

            this.X = x;
            this.Y = y;

            this.IsAlive = true;
        }

        public void Kill()
        {
            this.IsAlive = false;
            OnKillEvent(X, Y);
        }

        #region Events

        public void OnKill(ValueChanged method)
        {
            this.OnKillEvent += method;
        }

        public delegate void ValueChanged(int x, int y);
        private event ValueChanged OnKillEvent;

        #endregion
    }
}

