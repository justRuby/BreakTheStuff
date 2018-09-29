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

    private float timer = 10f;

    void Start()
    {
        cgController.Initialize();
        cgController.Player.OnHealthChanged(OnHealthChanged);

        viewSlabArray = new GameObject[cgController.MAX_SIZE, cgController.MAX_SIZE];
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
        Debug.Log("YEAH!");

        //viewSlabArray[x, y].GetComponent<Button>().onClick.RemoveAllListeners();
        Destroy(viewSlabArray[x, y]);
        Debug.Log("Destroy!");
    }

    public void OnHealthChanged(int value)
    {
        Debug.Log(value);
        if (viewHealthList.Count > value)
        {
            Destroy(viewHealthList[value]);
            viewHealthList.RemoveAt(value);
        }
    }
	
	private void Update()
    {
        if (cgController.IsGameStart && !cgController.IsGameLose)
        {
            if (timer < 0)
            {
                int x, y;
                var value = cgController.GenerateNewSlab(out x, out y);

                if (value)
                {
                    ViewNewSlab(x, y);
                }

                timer = 3f;
            }
            if (timer > 0)
            {
                timer -= cgController.DeltaTime();
            }
        }
	}

    private void ViewNewSlab(int x, int y)
    {
        viewSlabArray[x, y] = InitializeSlab(x, y);
    }

    private GameObject InitializeSlab(int i, int j)
    {
        int index = cgController.Slabs[i, j].Index;

        GameObject result = Instantiate(slabPrefab, Vector3.one, Quaternion.identity);
        result.transform.SetParent(parentSlab);
        result.transform.localScale = Vector3.one;
        result.GetComponent<RectTransform>().anchoredPosition = new Vector3(cgController.Slabs[i, j].LocalX, cgController.Slabs[i, j].LocalY, 0);

        result.GetComponent<Button>().onClick.AddListener(() => { cgController.OnSlabClicked(index); });
        result.transform.Find("Text").GetComponent<Text>().text = cgController.Slabs[i, j].Index.ToString();

        cgController.Slabs[i, j].OnKill(OnKill);

        return result;
    }
}
