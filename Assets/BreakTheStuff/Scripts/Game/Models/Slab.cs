namespace BreakTheStuff.Game.Models
{
    public class Slab
    {
        #region Public

        public int Index { get; private set; }

        public int LocalX { get { return _coordinates.X; } }
        public int LocalY { get { return _coordinates.Y; } }

        public bool IsAlive { get; private set; }
        public bool CanRespawn { get; set; }
        public bool CanInteract { get; private set; }
        #endregion

        #region Private
        private Coordinates _coordinates;

        private int _index0;
        private int _index1;

        #endregion

        public Slab() { }

        public void Initialize(int index, int localX, int localY, int i, int j)
        {
            _coordinates = new Coordinates(localX, localY);
            this.Index = index;

            this._index0 = i;
            this._index1 = j;

            this.CanInteract = true;
            this.IsAlive = true;
        }

        #region OnKill

        public void Kill()
        {
            this.CanInteract = false;
            this.IsAlive = false;
            OnKillEvent(_index0, _index1);
        }

        public void OnKill(StatusChanged method)
        {
            this.OnKillEvent += method;
        }

        public delegate void StatusChanged(int x, int y);
        private event StatusChanged OnKillEvent;

        #endregion

        #region OnIndexChanged

        public void SetupNewIndex(int index)
        {
            this.Index = index;
            OnIndexChangedEvent(Index, _index0, _index1);
        }

        public void OnIndexChanged(IndexChanged method)
        {
            this.OnIndexChangedEvent += method;
        }

        public delegate void IndexChanged(int value, int x, int y);
        private event IndexChanged OnIndexChangedEvent;

        #endregion

        #region OnTeleport

        public void Teleport(int index, bool isAlive)
        {
            this.Index = index;
            this.IsAlive = isAlive;

            OnTeleportedEvent(_index0, _index1);
        }


        public void OnTeleported(PositionChanged method)
        {
            this.OnTeleportedEvent += method;
        }

        public delegate void PositionChanged(int x, int y);
        private event PositionChanged OnTeleportedEvent;

        #endregion

    }
}

