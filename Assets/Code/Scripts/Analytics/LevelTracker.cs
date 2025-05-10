using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

/*
KPI tracker script for Levels.

Currently tracks:
    - Number of visits made to that level
    - Time spent completing the level
*/

public class LevelTracker : MonoBehaviour
{
    private string levelName;

    private float levelStartTime;

    private const string keySuffix = "_totalVisits";

    void Start()
    {
        levelName = SceneManager.GetActiveScene().name;
        levelStartTime = Time.time;

        TrackLevelVisitsEvent();
    }

    private void TrackLevelVisitsEvent()
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

    public void TrackLevelCompleted()
    {
        float timeSpent = Time.time - levelStartTime;

        Debug.Log($"Level '{levelName}' completed in {timeSpent:F2} seconds.");

        LevelCompletedEvent levelCompletedEvent = new LevelCompletedEvent
        {
            LevelName = levelName,
            TimeSpent = timeSpent,
        };

        AnalyticsService.Instance.RecordEvent(levelCompletedEvent);
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

public class LevelCompletedEvent : Unity.Services.Analytics.Event
{
    public LevelCompletedEvent()
        : base("levelCompleted") { }

    public string LevelName
    {
        set { SetParameter("levelName", value); }
    }

    public float TimeSpent
    {
        set { SetParameter("timeSpent", value); }
    }
}
