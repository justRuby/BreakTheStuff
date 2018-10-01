using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{
    [SerializeField] Text scoreText;

    [SerializeField] PlayerScore playerScore;

    private void Start()
    {
        scoreText.text = playerScore.Score.ToString();
    }

}
