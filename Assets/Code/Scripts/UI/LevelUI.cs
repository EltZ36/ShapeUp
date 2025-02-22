using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    //switch to a singleton type of manager
    [SerializeField]
    public Canvas Default;

    [SerializeField]
    public Image menuImage,
        confirmImage;

    public GameManager GameManager;
    private Camera cam;
    private CanvasGroup menuScreen,
        confirm;

    private bool showMenu;

    void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        menuScreen = menuImage.GetComponent<CanvasGroup>();
        disableUIElement(menuScreen);

        confirm = confirmImage.GetComponent<CanvasGroup>();
        disableUIElement(confirm);

        showMenu = false;
    }

    public void ToggleMenu()
    {
        showMenu = !showMenu;
        if (showMenu)
        {
            enableUIElement(menuScreen);
        }
        else
        {
            disableUIElement(menuScreen);
        }
    }

    public void GoToConfirmButton()
    {
        enableUIElement(confirm);
    }

    public void OnZoomOutButton()
    {
        disableUIElement(confirm);
        StartCoroutine(CameraController.ZoomOut());
    }

    public void OnAreYouSure()
    {
        // Quit game, load menu
        LevelManager.Instance.LeaveCurrentLevel();
        SceneManager.LoadScene("Menu");
    }

    public void OnCancelButton()
    {
        disableUIElement(confirm);
        ToggleMenu();
    }

    public void OnLevelReset()
    {
        StartCoroutine(CameraController.ZoomOut());
        GameManager.Instance.ClearLevel(LevelManager.Instance.currentLevelID);
        LevelManager.Instance.LoadLevel(LevelManager.Instance.currentLevelID);
    }

    void disableUIElement(CanvasGroup element)
    {
        element.alpha = 0;
        element.interactable = false;
        element.blocksRaycasts = false;
    }

    void enableUIElement(CanvasGroup element)
    {
        element.alpha = 1;
        element.interactable = true;
        element.blocksRaycasts = true;
    }
}
