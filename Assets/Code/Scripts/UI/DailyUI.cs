using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DailyUI : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI timerUI;

    [SerializeField]
    public GameObject winUI;

    void Update()
    {
        timerUI.text = ConvertNumToTime(DailyManager.Instance.timer);
    }

    private string ConvertNumToTime(int num)
    {
        string time = "";
        if (num < 600)
        {
            time += "0";
        }
        time += num / 60 + ":";
        if (num % 60 < 10)
        {
            time += "0";
        }
        time += num % 60;
        return time;
    }

    public void Win()
    {
        winUI.SetActive(true);
    }

    public void OnExitToMenu()
    {
        // Quit game, load menu
        SceneManager.LoadScene("Menu");
    }
}
