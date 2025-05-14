using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

/*
 * Records:
 *  - Number of visits made --> (levelName: string, visitCount: int)
 *  - Time spent completing the level --> (levelName: string, timeSpent: float)
*/

public class LevelRecorder : MonoBehaviour
{
    private string levelName;

    private float levelStartTime;

    private const string keySuffix = "_totalVisits";

    void Start()
    {
        levelName = SceneManager.GetActiveScene().name;
        levelStartTime = Time.time;

        RecordLevelVisitsEvent();
    }

    private void RecordLevelVisitsEvent()
    {
        int visitCount = IncrementLevelVisits();

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

    public void RecordLevelCompleted()
    {
        float timeSpent = Mathf.Floor((Time.time - levelStartTime) * 10f) / 10f;

        LevelCompletionTimeEvent levelCompletionTimeEvent = new LevelCompletionTimeEvent
        {
            LevelName = levelName,
            TimeSpent = timeSpent,
        };

        AnalyticsService.Instance.RecordEvent(levelCompletionTimeEvent);
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

public class LevelCompletionTimeEvent : Unity.Services.Analytics.Event
{
    public LevelCompletionTimeEvent()
        : base("levelCompletionTime") { }

    public string LevelName
    {
        set { SetParameter("levelName", value); }
    }

    public float TimeSpent
    {
        set { SetParameter("timeSpent", value); }
    }
}
