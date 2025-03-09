using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject settings;

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
        settings.transform.localScale =
            settings.transform.localScale == Vector3.one ? Vector3.zero : Vector3.one;
    }

    public void OnCreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnHowToPlayButton()
    {
        // SceneManager.LoadScene(5);
        Debug.Log("Load How To Play");
        SceneManager.LoadScene("HowToPlay");
    }
}
