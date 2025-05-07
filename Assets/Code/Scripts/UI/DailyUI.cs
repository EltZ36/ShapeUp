using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyUI : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI timerUI;

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
}
