﻿namespace BreakTheStuff.Game.Models
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

        #region OnHealthDamaged

        public void ApplyDamageToHealth(int value)
        {
            this.HealthPoint -= value;
            OnHealthChangedEvent(HealthPoint);
        }

        public void OnHealthChanged(HealthChanged method)
        {
            this.OnHealthChangedEvent += method;
        }

        public delegate void HealthChanged(int value);
        private event HealthChanged OnHealthChangedEvent;

        #endregion

        #region OnAddedScore

        public void AddScore(int value)
        {
            this.Score += value;
            OnScoreChangedEvent(this.Score);
        }

        public void OnScoreChanged(ScoreChanged method)
        {
            this.OnScoreChangedEvent += method;
        }

        public delegate void ScoreChanged(int value);
        private event ScoreChanged OnScoreChangedEvent;

        #endregion

    }

}
