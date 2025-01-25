using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] public Canvas Victory, Save, Default;
    [SerializeField] public Image img;
    public bool hints;

    private void Start()
    {
        Victory.enabled = false;
        Save.enabled = false;
        img.enabled = false;
        hints = false;
    }
    public void OnBackButton()
    {
        // Load Save UI
        Save.enabled = true;
    }

    public void OnRefreshButton()
    {
        // Revert level to starting state
        Debug.Log("Refresh Level");
        img.enabled = false;
        hints = false;

    }

    public void OnHintButton()
    {
        // Show Hint Icons
        Debug.Log("Show Hints");
        img.enabled = true;
        hints = true;
    }

    public void OnVictoryButton()
    {
        // Load Victory UI
        Victory.enabled = true;
    }
    public void OnNextButton()
    {
        // Load Next Level
        Debug.Log("Next Level");
        Victory.enabled = false;
    }

    public void OnMenuButton()
    {
        // Load Menu
        Debug.Log("Menu");
        Victory.enabled = false;
        SceneManager.LoadScene("Menu");
    }

    public void OnSaveAndQuitButton()
    {
        // Save game, load menu
        Debug.Log("Save and Quit");
        Debug.Log("Menu");
        Save.enabled = false;
        SceneManager.LoadScene("Menu");
    }

    public void OnQuitButton()
    {
        // Save game, load menu
        Debug.Log("Quit");
        Debug.Log("Menu");
        Save.enabled = false;
        SceneManager.LoadScene("Menu");
    }

}
