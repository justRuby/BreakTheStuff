using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameMenuAnimation : MonoBehaviour {

    [SerializeField] CanvasGroup topLayout;
    [SerializeField] RectTransform gameNameText;
    [SerializeField] RectTransform bottomLayout;

    [SerializeField] float speedAnimation;

    private void Start()
    {
        this.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        topLayout.alpha = 0;
        gameNameText.localPosition = new Vector3(0, 400.0f, 0);
        bottomLayout.localPosition = new Vector3(0, -400.0f, 0);

        Sequence sequence = DOTween.Sequence();

        sequence.Append(topLayout.DOFade(1.0f, speedAnimation));
        sequence.Join(gameNameText.DOLocalMoveY(0, speedAnimation));
        sequence.Join(bottomLayout.DOLocalMoveY(0, speedAnimation));
    }

}
