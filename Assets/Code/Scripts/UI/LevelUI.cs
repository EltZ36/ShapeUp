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
        saveImage;
    private Camera cam;
    public bool hints;
    public GameManager GameManager;

    private CanvasGroup victory,
        save;

    public void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        save = saveImage.GetComponent<CanvasGroup>();
        disableUIElement(save);

        victory = victoryImage.GetComponent<CanvasGroup>();
        disableUIElement(victory);

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
        StartCoroutine(ZoomOut(cam.orthographicSize, 10, 1f));
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
        enableUIElement(victory);
    }

    public void OnNextButton()
    {
        // Load Next Level
        Debug.Log("Next Level");
        disableUIElement(victory);
        LevelManager.Instance.OnCurrentSubLevelComplete();
        LevelManager.Instance.UnloadCurrentSubLevel();
    }

    public void OnMenuButton()
    {
        // Load Menu
        Debug.Log("Menu");
        disableUIElement(victory);
        SceneManager.LoadScene("Menu");
    }

    public void OnSaveAndQuitButton()
    {
        // Save game, load menu
        Debug.Log("Save and Quit");
        Debug.Log("Menu");
        disableUIElement(save);
        GameManager.Instance.SaveGame();
        SceneManager.LoadScene("Menu");
    }

    public void OnQuitButton()
    {
        // Quit game, load menu
        Debug.Log("Quit");
        Debug.Log("Menu");
        disableUIElement(save);
        SceneManager.LoadScene("Menu");
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

    IEnumerator ZoomOut(float StartPos, float EndPos, float time)
    {
        float elapsed = 0.0f;
        while (elapsed / time < 1)
        {
            elapsed += Time.deltaTime;
            cam.orthographicSize = EaseOutQuad(StartPos, EndPos, elapsed / time);
            yield return null;
        }
        cam.orthographicSize = EndPos;
        GameManager.Instance.SaveGame();
        SceneManager.UnloadSceneAsync("LevelUI");
        LevelManager.Instance.UnloadCurrentSubLevel();
    }

    //Created by C.J. Kimberlin https://gist.github.com/cjddmut/d789b9eb78216998e95c
    private float EaseOutQuad(float start, float end, float value)
    {
        end -= start;
        return -end * value * (value - 2) + start;
    }
}
