using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIPopupEffect : MonoBehaviour
{
    [Header("Fade Settings")]
    [Range(0f, 1f)]
    public float startOpacity = 0f;

    [Range(0f, 1f)]
    public float endOpacity = 1f;

    [Header("Scale Settings")]
    public Vector3 startScale = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector3 endScale = Vector3.one;

    [Header("Animation Settings")]
    public float duration = 0.5f;
    public Ease easeType = Ease.OutBack;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void AnimatePopup()
    {
        canvasGroup.alpha = startOpacity;
        rectTransform.localScale = startScale;

        // Stop any current tweens
        canvasGroup.DOKill();
        rectTransform.DOKill();

        canvasGroup.DOFade(endOpacity, duration).SetEase(easeType);
        rectTransform.DOScale(endScale, duration).SetEase(easeType);
    }
}
