namespace BreakTheStuff.CoreGame.Models
{
    public class Slab
    {
        public Coordinates Coordinates;
        public int Index { get; private set; }

        public int LocalX { get { return Coordinates.X; } }
        public int LocalY { get { return Coordinates.Y; } }

        private int X { get; set; }
        private int Y { get; set; }

        public bool IsAlive { get; private set; }

        public Slab(int index, int localX, int localY, int x, int y)
        {
            Coordinates = new Coordinates(localX, localY);
            this.Index = index;

            this.X = x;
            this.Y = y;

            this.IsAlive = true;
        }

        public void SetupNewIndex(int index)
        {
            this.Index = index;
            OnIndexChangedEvent(Index, X, Y);
        }

        public void Kill()
        {
            this.IsAlive = false;
            OnKillEvent(X, Y);
        }

        public void Teleport(int x, int y)
        {

        }

        public void OnKill(StatusChanged method)
        {
            this.OnKillEvent += method;
        }

        public delegate void StatusChanged(int x, int y);
        private event StatusChanged OnKillEvent;

        public void OnIndexChanged(IndexChanged method)
        {
            this.OnIndexChangedEvent += method;
        }

        public delegate void IndexChanged(int value, int x, int y);
        private event IndexChanged OnIndexChangedEvent;

    }
}

