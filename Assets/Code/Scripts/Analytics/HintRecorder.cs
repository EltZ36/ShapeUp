using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HintRecorder : MonoBehaviour
{
    private string levelName;

    private const string keySuffix = "_hintUses";

    void Start()
    {
        levelName = SceneManager.GetActiveScene().name;
    }

    public void RecordHints()
    {
        int hintCount = IncrementHintUses();

        Debug.Log("hint uses: " + hintCount);

        HintUsedEvent hintUsedEvent = new HintUsedEvent
        {
            LevelName = levelName,
            HintCount = hintCount,
        };

        AnalyticsService.Instance.RecordEvent(hintUsedEvent);
    }

    private int IncrementHintUses()
    {
        string prefsKey = levelName + keySuffix;
        int hintCount = PlayerPrefs.GetInt(prefsKey, 0) + 1;

        PlayerPrefs.SetInt(levelName + keySuffix, hintCount);
        PlayerPrefs.Save();

        return hintCount;
    }

    public class HintUsedEvent : Unity.Services.Analytics.Event
    {
        public HintUsedEvent()
            : base("hintsUsed") { }

        public string LevelName
        {
            set { SetParameter("levelName", value); }
        }
        public int HintCount
        {
            set { SetParameter("hintCount", value); }
        }
    }
}
