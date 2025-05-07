using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.Analytics;

public class LevelTracker : MonoBehaviour
{
    [SerializeField]
    private string levelName;

    private const string keySuffix = "_numVisits";

    void Start()
    {
        TrackLevelEvent();
    }

    public void TrackLevelEvent()
    {
        if (!string.IsNullOrEmpty(levelName))
        {
            IncrementLevelVisits();

            int levelVisits = GetLevelVisitCount();

            Debug.Log("Visited " + levelName + ", visits: " + levelVisits);

            AnalyticsService.Instance.RecordEvent("levelEntered");

            //  "levelEntered",
            //new Dictionary<string, object> { { "level", levelName }, { "visits", levelVisits } }
        }
        else
        {
            Debug.Log("LevelTracker: level name field is empty");
        }
    }

    private void IncrementLevelVisits()
    {
        int currentVisitCount = GetLevelVisitCount();
        PlayerPrefs.SetInt(levelName + keySuffix, currentVisitCount + 1);
        PlayerPrefs.Save();
    }

    private int GetLevelVisitCount()
    {
        return PlayerPrefs.GetInt(levelName + keySuffix, 0);
    }
}
