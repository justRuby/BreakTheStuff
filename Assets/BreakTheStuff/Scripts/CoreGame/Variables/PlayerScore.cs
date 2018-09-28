using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScore", menuName = "Game Variables/PlayerScore")]
public class PlayerScore : ScriptableObject
{
    [SerializeField] int _score;

    public void ResetScore()
    {
        _score = 0;
    }

    public bool SetScore(int score)
    {
        bool isNewScore = false;

        if (_score < score)
            isNewScore = true;

        _score = score;

        return isNewScore;
    }

    public int GetScore()
    {
        return _score;
    }
}
