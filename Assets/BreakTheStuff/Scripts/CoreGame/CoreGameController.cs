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

    private PlayerScore playerScore;

    private int basePosX;
    private int basePosY;

    private int fromNumber;
    private int toNumber;

    private int _currentIndex;
    private int _slabIndex;
    private int _countSpawned;

    private float speedElapse;
    #endregion

    public void Initialize()
    {
        Slabs = new Slab[MAX_SIZE, MAX_SIZE];

        basePosX = 30;
        basePosY = -30;

        fromNumber = 1;
        toNumber = MAX_SIZE * MAX_SIZE + 1;

        _currentIndex = 1;
        _slabIndex = 1;
        _countSpawned = 25;
        IsGameStart = false;

        Player = new Player(3, 0);
        speedElapse = 1.5f;
    }

    public void GenerateFirstSlabsStack()
    {
        int[] numbers = Enumerable.Range(0, MAX_SIZE * MAX_SIZE + 1).ToArray();

        numbers = EnumExtension.GenerateEnum(numbers, numbers.Length, fromNumber, toNumber);

        for (int i = 0; i < MAX_SIZE; i++)
        {
            for (int j = 0; j < MAX_SIZE; j++)
            {
                Slabs[i, j] = new Slab(numbers[_slabIndex++], basePosX, basePosY, i, j);

                basePosX += 110;
            }

            basePosY -= 110;
            basePosX = 30;
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

    public void OnLose()
    {
        IsGameLose = true;
    }

    public IEnumerator DestroyAllSlabs()
    {
        yield return null;
    }

    public void ReplaceSlabs()
    {

    }

    public void TeleportSlabsToFreeSpace()
    {

    }


    #region other functions

    public float DeltaTime()
    {
        return Time.deltaTime * (speedElapse * Player.Score) / _countSpawned;
    }

    #endregion
}
