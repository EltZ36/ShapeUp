using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
        Physics2D.gravity = new UnityEngine.Vector2(0f, -9.8f);
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
        GameObject ob = GameObject.FindGameObjectWithTag("hint");
        if (!ob)
        {
            Debug.Log(
                "could not find hint object to destroy in sublevel. Did you tag hint canvas prefab with 'hint'? "
            );
        }
        else
        {
            Destroy(ob);
        }
        disableUIElement(confirm);
        bool fully = LevelManager.Instance.currentSubLevelID == -1 ? true : false;
        Physics2D.gravity = new UnityEngine.Vector2(0f, -9.8f);
        StartCoroutine(CameraController.ZoomOut(fully));
        if (showMenu)
        {
            ToggleMenu();
        }
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
        GameManager.Instance.ClearLevel(LevelManager.Instance.currentLevelID);
        // bool fully = LevelManager.Instance.currentSubLevelID == -1 ? true : false;
        // StartCoroutine(CameraController.ZoomOut(fully));
        // ToggleMenu();
        LevelManager.Instance.LoadLevel(0);
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
