namespace BreakTheStuff.CoreGame.Models
{
    public class Player
    {
        public int HealthPoint { get; private set; }
        public int Score { get; private set; }

        public Player(int hp = 3, int score = 0)
        {
            this.HealthPoint = hp;
            this.Score = score;
        }

        public void ApplyDamageToHealth(int value)
        {
            this.HealthPoint -= value;
            OnHealthChangedEvent(HealthPoint);
        }

        public void AddScore(int value)
        {
            this.Score += value;
        }

        public void OnHealthChanged(ValueChanged method)
        {
            this.OnHealthChangedEvent += method;
        }

        public delegate void ValueChanged(int value);
        private event ValueChanged OnHealthChangedEvent;

    }

}
