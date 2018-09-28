using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoreGameController", menuName = "System/CoreGameController")]
public class CoreGameController : ScriptableObject
{
    private const int MAX_SIZE = 5;

    [SerializeField] PlayerScore playerScore;
    [SerializeField] PlayerHealth playerHealth;

    public void Initialize()
    {

    }

    public IEnumerator SpawnFirstSlabs()
    {
        yield return null;
    }

    public void SpawnSlab()
    {

    }

    public void OnLose()
    {

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
}
