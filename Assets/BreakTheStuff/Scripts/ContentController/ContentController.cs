using System;
using UnityEngine;

public class ContentController : MonoBehaviour {

    [SerializeField] CanvasGroupFadeAnimation cgfAnim;
    [SerializeField] bool dontHideMainWidget;

    public string Name { get { return gameObject.name; } }
    public bool DontHideMainWidget { get { return dontHideMainWidget; } }

    public void Show()
    {
        SetActive(true);
        cgfAnim.ShowAnimation();
    }

    public void Hide()
    {
        cgfAnim.HideAnimation(() => SetActive(false));
    }

    public void SetActive(bool value)
    {
        this.gameObject.SetActive(value);
    }

}
