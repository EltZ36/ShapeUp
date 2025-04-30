using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderScaleEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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

    private RectTransform knobTransform;

    void Start()
    {
        knobTransform = transform.Find("Handle Slide Area/Handle")?.GetComponent<RectTransform>();

        if (knobTransform == null)
            return;

        originalScale = knobTransform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (knobTransform != null)
        {
            currentTween?.Kill();
            currentTween = knobTransform
                .DOScale(originalScale * pressedScale, animationDuration)
                .SetEase(easeIn);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (knobTransform != null)
        {
            currentTween?.Kill();
            currentTween = knobTransform.DOScale(originalScale, animationDuration).SetEase(easeOut);
        }
    }
}
