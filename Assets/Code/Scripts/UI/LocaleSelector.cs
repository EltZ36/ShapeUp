using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private bool isChangingLocale = false;

    public void ChangeLocale(int localeID)
    {
        if (isChangingLocale)
        {
            return;
        }

        isChangingLocale = true;

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[
            localeID
        ];

        isChangingLocale = false;
    }
}
