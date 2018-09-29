using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreGameViewController : MonoBehaviour {

    [SerializeField] RectTransform parentHealth;
    [SerializeField] RectTransform parentSlab;

    [SerializeField] GameObject slabPrefab;
    [SerializeField] GameObject healthPrefab;

    [SerializeField] CoreGameController cgController;

    private GameObject[,] viewSlabArray;
    private List<GameObject> viewHealthList;
    private int[,] viewValueSlab;

    private float timer = 10f;

    void Start()
    {
        cgController.Initialize();
        cgController.Player.OnHealthChanged(OnHealthChanged);

        viewSlabArray = new GameObject[cgController.MAX_SIZE, cgController.MAX_SIZE];
        viewValueSlab = new int[cgController.MAX_SIZE, cgController.MAX_SIZE];

        viewHealthList = new List<GameObject>();
        cgController.GenerateFirstSlabsStack();

        for (int i = 0; i < cgController.MAX_SIZE; i++)
        {
            for (int j = 0; j < cgController.MAX_SIZE; j++)
            {
                viewSlabArray[i, j] = InitializeSlab(i, j);
            }
        }

        for (int i = 0; i < cgController.Player.HealthPoint; i++)
        {
            viewHealthList.Add(Instantiate(healthPrefab));
            viewHealthList[i].transform.SetParent(parentHealth);
            viewHealthList[i].transform.localScale = Vector3.one;
        }
    }

    public void OnKill(int x, int y)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(viewSlabArray[x, y].transform.DOScale(1.1f, 0.2f).SetEase(Ease.OutQuad));
        sequence.Append(viewSlabArray[x, y].transform.DOScale(0.1f, 0.4f).SetEase(Ease.InQuad))
            .OnComplete(()=>
            {
                Destroy(viewSlabArray[x, y]);
            });
    }

    public void OnHealthChanged(int value)
    {
        //Debug.Log(value);
        if (viewHealthList.Count > value)
        {
            Destroy(viewHealthList[value]);
            viewHealthList.RemoveAt(value);
        }
    }
	
    public void OnIndexChanged(int value, int x, int y)
    {
        //Debug.Log(value);
        if(viewSlabArray[x,y] != null)
        {
            viewSlabArray[x, y].transform.Find("Text").GetComponent<Text>().text = value.ToString();
            viewValueSlab[x, y] = value;
        }

        //viewSlabArray[x, y].GetComponent<Button>().onClick.RemoveAllListeners();
        //viewSlabArray[x, y].GetComponent<Button>().onClick.AddListener(() => { cgController.OnSlabClicked(value); });
    }

	private void Update()
    {
        if ((cgController.IsGameStart && !cgController.IsGameLose) || !cgController.IsMixing)
        {
            if (timer < 0)
            {
                int x, y;
                var value = cgController.GenerateNewSlab(out x, out y);

                //Debug.Log(timer);

                if (value)
                {
                    viewSlabArray[x, y] = InitializeSlab(x, y);
                }

                timer = 3f;
            }
            if (timer > 0)
            {
                //Debug.Log(timer);

                timer -= cgController.DeltaTime();
            }

            if (cgController.IsSlabCanMix)
            {
                cgController.MixingSlabs();
            }
        }
	}

    private GameObject InitializeSlab(int x, int y)
    {
        viewValueSlab[x, y] = cgController.Slabs[x, y].Index;

        Sequence sequence = DOTween.Sequence();

        GameObject result = Instantiate(slabPrefab, Vector3.one, Quaternion.identity);
        result.transform.SetParent(parentSlab);
        result.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        result.GetComponent<RectTransform>().anchoredPosition = new Vector3(cgController.Slabs[x, y].LocalX, cgController.Slabs[x, y].LocalY, 0);

        result.GetComponent<Button>().onClick.AddListener(() => { cgController.OnSlabClicked(viewValueSlab[x, y]); });
        result.transform.Find("Text").GetComponent<Text>().text = cgController.Slabs[x, y].Index.ToString();

        cgController.Slabs[x, y].OnKill(OnKill);
        cgController.Slabs[x, y].OnIndexChanged(OnIndexChanged);

        sequence.Append(result.transform.DOScale(1f, 0.8f).SetEase(Ease.OutQuint));

        return result;
    }
}
