using UnityEngine;
using DG.Tweening;
using System;

public class CanvasGroupFadeAnimation : MonoBehaviour {

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float speedAnimation;

    public void ShowAnimation()
    {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1.0f, speedAnimation).SetEase(Ease.InQuad);
    }

    public void HideAnimation(Action action)
    {
        canvasGroup.alpha = 1;
        canvasGroup.DOFade(0, speedAnimation).SetEase(Ease.OutQuad).OnComplete(()=>
        {
            action();
        });
    }
}
