using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using static UnityEditor.Progress;

public class LanguageOptions : MonoBehaviour
{
    [SerializeField]
    CanvasGroup optionsPanel;

    TMP_Text languageLabel;

    private void Start()
    {
        languageLabel = transform.Find("Label").GetComponent<TMP_Text>();

        UpdateSelectedLanguageText();
    }

    public void OpenLanguageOptionsPanel()
    {
        optionsPanel.alpha = 1;
        optionsPanel.interactable = true;
        optionsPanel.blocksRaycasts = true;
    }

    public void CloseLanguageOptionsPanel()
    {
        optionsPanel.alpha = 0;
        optionsPanel.interactable = false;
        optionsPanel.blocksRaycasts = false;
    }

    public void OnLanguageSelected()
    {
        CloseLanguageOptionsPanel();
        UpdateSelectedLanguageText();
    }

    private void UpdateSelectedLanguageText()
    {
        languageLabel.text = LocalizationSettings.SelectedLocale.LocaleName;
    }
}
