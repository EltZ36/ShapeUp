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
        saveImage,
        confirmImage;
    private Camera cam;
    public bool hints;
    public int whichSave;
    public GameManager GameManager;

    private CanvasGroup victory,
        save,
        confirm;

    #region Singleton Pattern
    private static LevelUI _instance;
    public static LevelUI Instance
    {
        get { return _instance; }
    }
    #endregion

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        cam = Camera.main;
    }

    private void Start()
    {
        save = saveImage.GetComponent<CanvasGroup>();
        disableUIElement(save);

        victory = victoryImage.GetComponent<CanvasGroup>();
        disableUIElement(victory);

        confirm = confirmImage.GetComponent<CanvasGroup>();
        disableUIElement(confirm);

        hintImage.enabled = false;
        hints = false;
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

    public void GoToConfirmButton()
    {
        enableUIElement(confirm);
    }

    public void YesButton()
    {
        if (whichSave == 1)
        {
            GameManager.Instance.SaveGame();
            Debug.Log("Save and Quit");
            Debug.Log(whichSave);
        }
        Debug.Log("Menu");
        SceneManager.LoadScene("Menu");
    }

    public void NoButton()
    {
        disableUIElement(confirm);
    }

    public void OnSaveAndQuitButton()
    {
        disableUIElement(save);
        LevelManager.Instance.LeaveCurrentLevel();
        GameManager.Instance.SaveGame();
        SceneManager.LoadScene("Menu");
        whichSave = 1;
    }

    public void OnSaveButton()
    {
        disableUIElement(save);
        GameManager.Instance.SaveGame();
    }

    public void OnQuitButton()
    {
        // Quit game, load menu
        Debug.Log("Quit");
        Debug.Log("Menu");
        disableUIElement(save);
        LevelManager.Instance.LeaveCurrentLevel();
        SceneManager.LoadScene("Menu");
        whichSave = 2;
    }

    public void OnCancelSaveButton()
    {
        disableUIElement(save);
    }

    //the difference between OnCancelSaveButton and OnCancelConfirmButton is that this is for the confirm
    public void onCancelConfirmButton()
    {
        disableUIElement(save);
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

    public void DisableEverything()
    {
        disableUIElement(victory);
        disableUIElement(save);
        disableUIElement(confirm);
    }
}
