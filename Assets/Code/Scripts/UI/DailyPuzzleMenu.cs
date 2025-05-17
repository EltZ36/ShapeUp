using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DailyPuzzleMenu : MonoBehaviour
{
    [SerializeField]
    CanvasGroup popup;

    private UIPopupEffect popupEffect;

    void Start()
    {
        popupEffect = popup.GetComponent<UIPopupEffect>();

        TriggerPopup();
    }

    public void OnBackButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("DailyPuzzle");
    }

    public void TriggerPopup()
    {
        if (popupEffect != null)
        {
            popupEffect.AnimatePopup();
        }
    }
}
