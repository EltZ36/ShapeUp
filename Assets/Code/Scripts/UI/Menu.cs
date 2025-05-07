using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnLevelSelectButton()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void OnDailyPuzzleButton()
    {
        // redirect to daily puzzle
        LevelManager.Instance.LoadLevel(0);
    }

    public void OnHowToPlayButton()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void OnCreditsButton()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnSettingsButton()
    {
        SceneManager.LoadScene("Settings");
    }

    public void OnMenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
