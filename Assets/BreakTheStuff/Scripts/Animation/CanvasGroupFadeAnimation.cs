using UnityEngine;
using DG.Tweening;

public class CanvasGroupFadeAnimation : MonoBehaviour {

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float speedAnimation;

    private void OnEnable()
    {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1.0f, speedAnimation).SetEase(Ease.Flash);
    }

    private void OnDisable()
    {
        canvasGroup.DOFade(0, speedAnimation).SetEase(Ease.Flash);
    }
}
