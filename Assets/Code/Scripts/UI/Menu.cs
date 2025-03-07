using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Awake()
    {
        if (SceneManager.GetSceneByName("Menu").IsValid())
        {
            SceneManager.LoadSceneAsync("SplashLevel", LoadSceneMode.Additive);
        }
    }

    public void OnSuperButton()
    {
        // SceneManager.LoadScene(2);
        Debug.Log("Load Supercluster");
        LevelManager.Instance.LoadLevel(0);
    }

    public void OnStandaloneButton()
    {
        // SceneManager.LoadScene(3);
        Debug.Log("Load Standalone");
        SceneManager.LoadScene("LevelSelect");
    }

    public void OnSettingsButton()
    {
        // SceneManager.LoadScene(4);
        Debug.Log("Load Settings");
    }

    public void OnCreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
