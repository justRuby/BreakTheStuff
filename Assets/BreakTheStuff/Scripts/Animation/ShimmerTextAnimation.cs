using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShimmerTextAnimation : MonoBehaviour {

    [SerializeField] float animationSpeed = 2.0f;

    Color oldColor;
    Color newColor;
    Text text;

	void Start ()
    {
        text = this.gameObject.GetComponent<Text>();
        oldColor = text.color;
        newColor = new Color(0, 0, 0, 0.2f);

        Sequence anim = DOTween.Sequence();

        anim.Append(text.DOColor(newColor, animationSpeed));
        anim.Append(text.DOColor(oldColor, animationSpeed));
        anim.SetLoops(-1);
    }
}
