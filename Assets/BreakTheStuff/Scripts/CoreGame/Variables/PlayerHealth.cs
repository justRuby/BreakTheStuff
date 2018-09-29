using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHealth", menuName = "Game Variables/PlayerHealth")]
public class PlayerHealth : ScriptableObject
{
    public int Health { get; set; }
}
