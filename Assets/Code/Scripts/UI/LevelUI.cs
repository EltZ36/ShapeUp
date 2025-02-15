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
    public Image hintImage,
        victoryImage,
        menuImage,
        quitImage,
        confirmImage;
    private Camera cam;
    public bool hints;
    public bool showMenu;
    public GameManager GameManager;

    private CanvasGroup victory,
        menuScreen,
        save,
        quitWindow,
        confirm;

    void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        victory = victoryImage.GetComponent<CanvasGroup>();
        disableUIElement(victory);

        menuScreen = menuImage.GetComponent<CanvasGroup>();
        disableUIElement(menuScreen);

        quitWindow = quitImage.GetComponent<CanvasGroup>();
        disableUIElement(quitWindow);

        confirm = confirmImage.GetComponent<CanvasGroup>();
        disableUIElement(confirm);

        hintImage.enabled = false;
        hints = false;
        showMenu = false;
    }

    public void OnBackMenuButton()
    {
        // Load Save UI
        enableUIElement(save);
    }

    public void OnBackLevelButton()
    {
        StartCoroutine(CameraController.ZoomOut());
    }

    public void OnRefreshButton()
    {
        // Revert level to starting state
        Debug.Log("Refresh Level");
        hintImage.enabled = false;
        hints = false;
    }

    public void OnHintButton()
    {
        // Show Hint Icons
        Debug.Log("Show Hints");
        hintImage.enabled = true;
        hints = true;
    }

    public void OnVictoryButton()
    {
        // Load Victory UI
        //enableUIElement(victory);
        StartCoroutine(CameraController.ZoomOut());
        LevelManager.Instance.OnCurrentSubLevelComplete();
    }

    public void VictoryScreen()
    {
        enableUIElement(victory);
    }

    public void OnNextButton()
    {
        // Load Next Level
        Debug.Log("Next Level");
        disableUIElement(victory);
        LevelManager.Instance.OnCurrentSubLevelComplete();
        LevelManager.Instance.UnloadCurrentSubLevel();
        CameraController.ZoomOut();
    }

    public void OnMenuButton()
    {
        // Load Menu
        Debug.Log("Menu");
        disableUIElement(victory);
        SceneManager.LoadScene("Menu");
    }

    public void GoToMenuBar()
    {
        showMenu = !showMenu;
        enableUIElement(menuScreen);
        disableUIElement(quitWindow);
        disableUIElement(confirm);
    }

    public void GoToConfirmButton()
    {
        enableUIElement(confirm);
        disableUIElement(quitWindow);
    }

    public void OnSaveAndQuitButton()
    {
        LevelManager.Instance.LeaveCurrentLevel();
        GameManager.Instance.SaveGame();
        SceneManager.LoadScene("Menu");
    }

    public void OnSaveButton()
    {
        GameManager.Instance.SaveGame();
    }

    public void OnQuitButton()
    {
        // Quit game, load menu
        Debug.Log("Quit");
        Debug.Log("Menu");
        LevelManager.Instance.LeaveCurrentLevel();
        SceneManager.LoadScene("Menu");
    }

    public void GoToQuitMenuButton()
    {
        enableUIElement(quitWindow);
    }

    public void onCancelConfirmButton()
    {
        disableUIElement(save);
        disableUIElement(quitWindow);
        disableUIElement(confirm);
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
