using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class PlayerPrefsLoader : MonoBehaviour
{
    private void Awake()
    {
        LoadLanguage();
    }

    private void LoadLanguage()
    {
        if (PlayerPrefs.HasKey("LocaleID"))
        {
            int localeID = PlayerPrefs.GetInt("LocaleID");

            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[
                localeID
            ];
        }
    }
}
