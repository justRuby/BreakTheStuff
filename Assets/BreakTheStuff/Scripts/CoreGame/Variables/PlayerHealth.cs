using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHealth", menuName = "Game Variables/PlayerHealth")]
public class PlayerHealth : ScriptableObject
{
    [SerializeField] int _health;

    public void SetupHealth(int count = 3)
    {
        _health = count;
    }

    public void ApplyDamageToHealth()
    {
        if (_health > 0)
            _health--;
    }

    public int GetHealth()
    {
        return _health;
    }

}
