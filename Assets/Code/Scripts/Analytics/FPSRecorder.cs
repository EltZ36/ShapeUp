using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Services.Analytics;
using UnityEngine;

/*
 * Records:
 *  - Client FPS every X interval --> (fps: int)
*/

public class FPSRecorder : MonoBehaviour
{
    [SerializeField]
    private float snapshotInterval = 10f;

    private List<int> fpsSamples = new List<int>();

    #region Singleton Pattern
    private static FPSRecorder _instance;
    public static FPSRecorder Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    public void StartTackingFPS()
    {
        StartCoroutine(CalculateFPS());
        StartCoroutine(TakeFPSSnapshot());
    }

    private IEnumerator CalculateFPS()
    {
        while (true)
        {
            int fps = Mathf.FloorToInt(1f / Time.unscaledDeltaTime);
            fpsSamples.Add(fps);

            yield return null;
        }
    }

    private IEnumerator TakeFPSSnapshot()
    {
        while (true)
        {
            yield return new WaitForSeconds(snapshotInterval);

            RecordFPS();
            fpsSamples.Clear();
        }
    }

    private void RecordFPS()
    {
        if (fpsSamples.Count == 0)
            return;

        int averageFPS = Mathf.FloorToInt((float)fpsSamples.Average());

        List<int> sortedFPS = fpsSamples.OrderBy(f => f).ToList();

        int onePercentCount = Mathf.Max(1, Mathf.FloorToInt(sortedFPS.Count * 0.01f));
        int lowFPSAverage = Mathf.FloorToInt((float)sortedFPS.Take(onePercentCount).Average());

        FPSReportEvent fpsReportEvent = new FPSReportEvent
        {
            AverageFPS = averageFPS,
            AverageOnePercentLowFPS = lowFPSAverage,
        };

        AnalyticsService.Instance.RecordEvent(fpsReportEvent);
    }

    public class FPSReportEvent : Unity.Services.Analytics.Event
    {
        public FPSReportEvent()
            : base("fpsReport") { }

        public int AverageFPS
        {
            set { SetParameter("averageFPS", value); }
        }

        public int AverageOnePercentLowFPS
        {
            set { SetParameter("averageOnePercentLowFPS", value); }
        }
    }
}
