using System.Collections.Generic;
using UnityEngine;

using BreakTheStuff.Game.Models;
using BreakTheStuff.Game.Extension;
using System.Linq;

namespace BreakTheStuff.Game
{
    public class GameLogicController
    {
        #region Const

        public readonly int MAX_SIZE = 5;

        #endregion

        #region Public

        public Slab[,] Slabs { get; private set; }
        public Player Player { get; private set; }
        public bool IsGameStart { get; private set; }
        public bool IsGameLose { get; private set; }

        public bool IsMixing { get; private set; }
        public bool IsTeleporting { get; private set; }

        #endregion

        #region Private

        private int _currentIndex;
        private int _slabIndex;
        private int _countSpawned;

        private float _speedElapse;

        #endregion

        public void Initialize()
        {
            Slabs = new Slab[MAX_SIZE, MAX_SIZE];
            _currentIndex = 1;
            _slabIndex = 0;
            _countSpawned = 25;
            IsGameStart = false;
            IsGameLose = false;

            Player = new Player(3, 0);
            _speedElapse = 1.5f;
        }

        public void GenerateFirstSlabsStack()
        {
            int start = 0;
            int end = MAX_SIZE * MAX_SIZE;

            int basePosX = 80;
            int basePosY = -80;

            int[] numbers = Enumerable.Range(start + 1, end).ToArray();
            numbers.GenerateEnum(start, end);

            for (int i = 0; i < MAX_SIZE; i++)
            {
                for (int j = 0; j < MAX_SIZE; j++)
                {
                    Slabs[i, j] = new Slab();
                    Slabs[i, j].Initialize(numbers[_slabIndex], basePosX, basePosY, i, j);

                    _slabIndex++;
                    basePosX += 110;
                }

                basePosY -= 110;
                basePosX = 80;
            }
        }

        public bool GenerateSlab(out int x, out int y)
        {
            x = 0;
            y = 0;

            if (_countSpawned == 25)
            {
                Lose();
                return false;
            }

            List<Coordinates> cord = GetCoordinates();

            int randIndex = Random.Range(0, cord.Count);

            x = cord[randIndex].X;
            y = cord[randIndex].Y;

            _countSpawned++;
            _slabIndex++;

            Slabs[x, y].Initialize(_slabIndex, Slabs[x, y].LocalX, Slabs[x, y].LocalY, x, y);

            return true;
        }

        public void OnSlabClicked(int index)
        {
            if (!IsGameStart)
                IsGameStart = true;

            var slab = Slabs.ToList().Where(x => x.Index == index).SingleOrDefault();

            if (_currentIndex == index)
            {
                slab.Kill();

                _countSpawned--;
                _currentIndex++;
                Player.AddScore(1);

                if (Player.Score > 0 && Player.Score % 10 == 0)
                    MixingSlabs();

                //if(_countSpawned < 10 && Random.Range(0, 100) < 20)
                //    TeleportSlabs();

                return;
            }
            else if (slab.CanInteract)
            {
                Player.ApplyDamageToHealth(1);

                if (Player.HealthPoint == 0)
                {
                    Lose();
                }
            }
        }

        public void MixingSlabs()
        {
            IsMixing = true;

            int index = 0;
            int start = Player.Score + 1;
            int count = _countSpawned;

            int[] replacedNumbers = Enumerable.Range(start, count).ToArray();

            replacedNumbers.GenerateEnum(0, count);

            var search = Slabs.ToList().Where(x => x.IsAlive == true);

            foreach (var slab in search)
            {
                slab.SetupNewIndex(replacedNumbers[index]);
                index++;
            }

            IsMixing = false;
        }

        public void TeleportSlabs()
        {
            IsTeleporting = true;

            int tCount = Random.Range(0, 5);

            List<Coordinates> coordAlive = GetCoordinates(true);
            List<Coordinates> coordNotAlive = GetCoordinates(false);

            for (int i = 0; i < tCount; i++)
            {
                int indexAS = Random.Range(0, coordAlive.Count);
                int indexNAS = Random.Range(0, coordNotAlive.Count);

                int tempSlabIndex = Slabs[coordAlive[indexAS].X, coordAlive[indexAS].Y].Index;
                Slabs[coordAlive[indexAS].X, coordAlive[indexAS].Y].Kill();
                coordAlive.RemoveAt(indexAS);

                Slabs[coordNotAlive[indexNAS].X, coordNotAlive[indexNAS].Y]
                    .Teleport(tempSlabIndex, true);

                coordNotAlive.RemoveAt(indexNAS);
            }

            IsTeleporting = false;
        }

        public void Lose()
        {
            IsGameLose = true;
            OnLoseEvent();
        }

        public float DeltaTime()
        {
            return Time.deltaTime * (_speedElapse * Player.Score) / _countSpawned;
        }

        private List<Coordinates> GetCoordinates(bool isAlive = false)
        {
            List<Coordinates> coordinates = new List<Coordinates>();

            for (int i = 0; i < MAX_SIZE; i++)
            {
                for (int j = 0; j < MAX_SIZE; j++)
                {
                    switch (isAlive)
                    {
                        case true:

                            if (Slabs[i, j].IsAlive)
                            {
                                coordinates.Add(new Coordinates(i, j));
                            }

                            break;

                        case false:

                            if (!Slabs[i, j].IsAlive && Slabs[i, j].CanRespawn)
                            {
                                coordinates.Add(new Coordinates(i, j));
                            }

                            break;

                        default:
                            break;
                    }
                }
            }

            return coordinates;
        }

        #region Events

        public void OnLose(GameStatusChanged method)
        {
            this.OnLoseEvent += method;
        }

        public delegate void GameStatusChanged();
        private event GameStatusChanged OnLoseEvent;

        #endregion
    }
}

