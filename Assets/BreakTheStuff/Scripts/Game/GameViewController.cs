using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using BreakTheStuff.Game;

public class GameViewController : MonoBehaviour {

    [Header("Parents")]
    [SerializeField] RectTransform parentHealth;
    [SerializeField] RectTransform parentSlab;

    [Header("Prefabs")]
    [SerializeField] GameObject slabPrefab;
    [SerializeField] GameObject healthPrefab;

    [Header("Layouts")]
    [SerializeField] GameObject gameLayout;
    [SerializeField] GameObject loseLayout;
    [SerializeField] GameObject menuLayout;

    [Header("Text")]
    [SerializeField] Text scoreText;
    [SerializeField] Text endScoreText;

    [Header("Player Data")]
    [SerializeField] PlayerScore playerScore;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;

    [Header("Widget Controller")]
    [SerializeField] WidgetNavigationManager widgetManager;

    private GameLogicController cgController;

    private GameObject[,] viewSlabArray;
    private List<GameObject> viewHealthList;
    private int[,] viewValueSlab;

    private float _timer = 10f;

    private bool _isInitializeSlab;
    private bool _isGameInPause;

    void Start()
    {
        cgController = new GameLogicController();
        cgController.Initialize();
        cgController.OnLose(Lose);
        cgController.Player.OnHealthChanged(OnHealthChanged);
        cgController.Player.OnScoreChanged(OnScoreChanged);
        cgController.GenerateFirstSlabsStack();

        viewSlabArray = new GameObject[cgController.MAX_SIZE, cgController.MAX_SIZE];
        viewValueSlab = new int[cgController.MAX_SIZE, cgController.MAX_SIZE];
        viewHealthList = new List<GameObject>();

        for (int i = 0; i < cgController.MAX_SIZE; i++)
        {
            for (int j = 0; j < cgController.MAX_SIZE; j++)
            {
                viewSlabArray[i, j] = InitializeSlab(i, j);

                cgController.Slabs[i, j].OnKill(OnKill);
                cgController.Slabs[i, j].OnIndexChanged(OnIndexChanged);
                cgController.Slabs[i, j].OnTeleported(OnSlabTeleported);
            }
        }

        for (int i = 0; i < cgController.Player.HealthPoint; i++)
        {
            viewHealthList.Add(Instantiate(healthPrefab));
            viewHealthList[i].transform.SetParent(parentHealth);
            viewHealthList[i].transform.localScale = Vector3.one;
        }
    }

    #region Game Events

    public void Lose()
    {
        for (int i = 0; i < cgController.MAX_SIZE; i++)
        {
            for (int j = 0; j < cgController.MAX_SIZE; j++)
            {
                if (viewSlabArray[i, j] != null)
                    viewSlabArray[i, j].transform.DOScale(0, 1.2f).SetEase(Ease.OutQuart);
            }
        }

        if (playerScore.Score < cgController.Player.Score)
        {
            playerScore.Score = cgController.Player.Score;
        }

        endScoreText.text = cgController.Player.Score.ToString();

        widgetManager.PushWidget("WidgetLose");
    }

    #endregion

    #region Player Events

    public void OnHealthChanged(int value)
    {
        //Debug.Log(value);
        if (viewHealthList.Count > value)
        {
            Destroy(viewHealthList[value]);
            viewHealthList.RemoveAt(value);
        }
        //TEST
        else
        {
            viewHealthList.Add(Instantiate(healthPrefab));
            viewHealthList[viewHealthList.Count - 1].transform.SetParent(parentHealth);
            viewHealthList[viewHealthList.Count - 1].transform.localScale = Vector3.one;
        }
    }

    public void OnScoreChanged(int value)
    {
        scoreText.text = value.ToString();
    }

    #endregion

    #region Slab Events

    public void OnKill(int x, int y)
    {
        cgController.Slabs[x, y].CanRespawn = false;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(viewSlabArray[x, y].transform.DOScale(1.1f, 0.2f).SetEase(Ease.OutQuad));
        sequence.Append(viewSlabArray[x, y].transform.DOScale(0.1f, 0.4f).SetEase(Ease.InQuad))
            .OnComplete(() =>
            {
                Destroy(viewSlabArray[x, y]);
                cgController.Slabs[x, y].CanRespawn = true;
            });
    }

    public void OnIndexChanged(int value, int x, int y)
    {
        if (viewSlabArray[x, y] != null)
        {
            viewSlabArray[x, y].transform.Find("Text").GetComponent<Text>().text = value.ToString();
            viewValueSlab[x, y] = value;
        }
    }

    public void OnSlabTeleported(int x, int y)
    {
        viewSlabArray[x, y] = InitializeSlab(x, y);
    }

    #endregion

    private void Update()
    {
        if (!_isGameInPause && (cgController.IsGameStart && !cgController.IsGameLose))
        {
            if (_timer < 0 && !_isInitializeSlab)
            {
                int x, y;
                _isInitializeSlab = true;
                var value = cgController.GenerateSlab(out x, out y);

                if (value)
                    viewSlabArray[x, y] = InitializeSlab(x, y);

                _isInitializeSlab = false;

                _timer = 3f;
            }

            if (_timer > 0)
                _timer -= cgController.DeltaTime();
        }
    }

    private GameObject InitializeSlab(int x, int y)
    {
        viewValueSlab[x, y] = cgController.Slabs[x, y].Index;

        GameObject result = Instantiate(slabPrefab, Vector3.one, Quaternion.identity);
        result.transform.SetParent(parentSlab);
        result.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        //result.transform.localScale = new Vector3(1f, 1f, 1f);
        result.GetComponent<RectTransform>().anchoredPosition = new Vector3(cgController.Slabs[x, y].LocalX, cgController.Slabs[x, y].LocalY, 0);

        result.GetComponent<Button>().onClick.AddListener(() => { cgController.OnSlabClicked(viewValueSlab[x, y]); });
        result.GetComponent<Button>().onClick.AddListener(audioSource.Play);
        result.transform.Find("Text").GetComponent<Text>().text = cgController.Slabs[x, y].Index.ToString();

        result.transform.DOScale(1f, 0.8f).SetEase(Ease.OutQuint);

        return result;
    }

    public void OpenPauseMenu(bool value)
    {
        _isGameInPause = value;
    }
}
