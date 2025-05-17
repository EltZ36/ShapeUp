using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.HasKey("LocaleID"))
        {
            int localeID = PlayerPrefs.GetInt("LocaleID");

            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[
                localeID
            ];
        }
    }

    public void OnLevelSelectButton()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void OnDailyPuzzleButton()
    {
        SceneManager.LoadScene("DailyPuzzleMenu");
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
