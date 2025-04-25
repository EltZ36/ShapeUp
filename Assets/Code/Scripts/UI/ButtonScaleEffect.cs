using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaleEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private float pressedScale = 0.97f;

    [SerializeField]
    private float animationDuration = 0.1f;

    [SerializeField]
    private Ease easeIn = Ease.OutBack,
        easeOut = Ease.InOutSine;

    private Vector3 originalScale;
    private Tween currentTween;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        currentTween?.Kill();
        currentTween = transform
            .DOScale(originalScale * pressedScale, animationDuration)
            .SetEase(easeIn);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        currentTween?.Kill();
        currentTween = transform.DOScale(originalScale, animationDuration).SetEase(easeOut);
    }
}
