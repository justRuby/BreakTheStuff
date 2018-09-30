using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BreakTheStuff.CoreGame.Models;
using BreakTheStuff.CoreGame.Extension;
using System.Linq;

[CreateAssetMenu(fileName = "CoreGameController", menuName = "System/CoreGameController")]
public class CoreGameController : ScriptableObject
{
    #region Const

    public readonly int MAX_SIZE = 5;

    #endregion

    #region Public
    //private PlayerHealth playerHealth;

    //public GameObject SlabPrefab { get; private set; }
    public Slab[,] Slabs { get; private set; }
    public Player Player { get; private set; }
    public bool IsGameStart { get; private set; }
    public bool IsGameLose { get; private set; }
    #endregion

    #region Private

    private PlayerScore _playerScore;

    private int _currentIndex;
    private int _slabIndex;
    private int _countSpawned;

    private float _speedElapse;
    private bool _isMixing;
    private bool _isTeleporting;
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
                Slabs[i, j] = new Slab(numbers[_slabIndex], basePosX, basePosY, i, j);

                _slabIndex++;
                basePosX += 110;
            }

            basePosY -= 110;
            basePosX = 80;
        }
    }

    public bool GenerateNewSlab(out int x, out int y)
    {
        x = 0;
        y = 0;

        if (_countSpawned == 25)
        {
            OnLose();
            return false;
        }

        int i = Random.Range(0, MAX_SIZE);
        int j = Random.Range(0, MAX_SIZE);

        if (Slabs[i,j].IsAlive == false)
        {
            Slabs[i, j] = new Slab(_slabIndex, Slabs[i, j].LocalX, Slabs[i, j].LocalY, i, j);

            _countSpawned++;
            _slabIndex++;

            x = i;
            y = j;
            
            return true;
        }

        return false;
    }

    public bool GenerateNewSlabV2(out int x, out int y)
    {
        x = 0;
        y = 0;

        if (_countSpawned == 25)
        {
            OnLose();
            return false;
        }

        List<Coordinates> cord = GetFreeSpace();

        int randIndex = Random.Range(0, cord.Count);    

        x = cord[randIndex].X;
        y = cord[randIndex].Y;

        _countSpawned++;
        _slabIndex++;

        Slabs[x, y] = new Slab(_slabIndex, Slabs[x, y].LocalX, Slabs[x, y].LocalY, x, y);

        return true;
    }

    public void OnSlabClicked(int index)
    {
        if (!IsGameStart)
            IsGameStart = true;

        if (_currentIndex == index)
        {
            var search = Slabs.ToList().Where(x => x.Index == index);

            foreach (var item in search)
            {
                item.Kill();
                break;
            }

            _countSpawned--;
            _currentIndex++;
            Player.AddScore(1);

            if (Player.Score > 0 && Player.Score % 10 == 0)
                MixingSlabs();

            return;
        }
        else
        {
            Player.ApplyDamageToHealth(1);

            if (Player.HealthPoint == 0)
            {
                OnLose();
                Debug.Log("LOSE!");
            }
        }
    }

    public void MixingSlabs()
    {
        _isMixing = true;

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

        _isMixing = false;
    }

    public void TeleportSlabs()
    {
        _isTeleporting = true;

        //Teleport  1 - 5 slabs to free space
        int tCount = Random.Range(0, 5);

        List<Coordinates> freeSpaceCoord = GetFreeSpace();
        List<Coordinates> reserveCoord = new List<Coordinates>();

        for (int i = 0; i < tCount; i++)
        {
            int randIndex = Random.Range(0, freeSpaceCoord.Count);
            reserveCoord.Add(freeSpaceCoord[randIndex]);
            freeSpaceCoord.RemoveAt(randIndex);

            //Slabs[freeSpaceCoord[i].X, freeSpaceCoord[i].Y].Teleport();

        }

        _isTeleporting = false;
    }

    public void OnLose()
    {
        IsGameLose = true;
    }

    public IEnumerator DestroyAllSlabs()
    {
        yield return null;
    }

    #region Other functions

    public float DeltaTime()
    {
        return Time.deltaTime * (_speedElapse * Player.Score) / _countSpawned;
    }

    public void AddLife()
    {
        Player.ApplyDamageToHealth(-1);
    }

    public List<Coordinates> GetFreeSpace()
    {
        List<Coordinates> coordinates = new List<Coordinates>();

        for (int i = 0; i < MAX_SIZE; i++)
        {
            for (int j = 0; j < MAX_SIZE; j++)
            {
                if (Slabs[i, j].IsAlive == false)
                {
                    coordinates.Add(new Coordinates(i, j));
                }
            }
        }

        return coordinates;
    }

    public List<Coordinates> GetRandomAliveSlabs()
    {
        List<Coordinates> coordinates = new List<Coordinates>();

        return coordinates;
    }

    #endregion
}
