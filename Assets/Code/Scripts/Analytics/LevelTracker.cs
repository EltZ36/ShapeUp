using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class LevelTracker : MonoBehaviour
{
    private string levelName;

    private const string keySuffix = "_totalVisits";

    void Start()
    {
        levelName = SceneManager.GetActiveScene().name;

        TrackLevelEvent();
    }

    public void TrackLevelEvent()
    {
        int visitCount = IncrementLevelVisits();

        Debug.Log("visit count: " + visitCount);

        LevelEnteredEvent levelEnteredEvent = new LevelEnteredEvent
        {
            LevelName = levelName,
            VisitCount = visitCount,
        };

        AnalyticsService.Instance.RecordEvent(levelEnteredEvent);
    }

    private int IncrementLevelVisits()
    {
        string prefsKey = levelName + keySuffix;
        int visitCount = PlayerPrefs.GetInt(prefsKey, 0) + 1;

        PlayerPrefs.SetInt(levelName + keySuffix, visitCount);
        PlayerPrefs.Save();

        return visitCount;
    }
}

public class LevelEnteredEvent : Unity.Services.Analytics.Event
{
    public LevelEnteredEvent()
        : base("levelEntered") { }

    public string LevelName
    {
        set { SetParameter("levelName", value); }
    }
    public int VisitCount
    {
        set { SetParameter("visitCount", value); }
    }
}
