using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Services.Analytics;
using UnityEngine;

/*
 * Records:
 *  - Average client FPS per defined interval --> (averageFPS: int)
 *  - Average of the lowest 100 frames per defined interval --> (averageLowFPS: int)
*/

public class FPSRecorder : MonoBehaviour
{
    [SerializeField]
    private float snapshotInterval = 10f;

    private int totalFPS = 0,
        frameCount = 0;

    private List<int> lowFPSBuffer = new List<int>();
    private const int maxLowBufferSize = 100;

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

    void Start()
    {
        // pre-allocating memory to store X elements
        lowFPSBuffer.Capacity = maxLowBufferSize;
    }

    public void StartTrackingFPS()
    {
        StartCoroutine(CalculateFPS());
        StartCoroutine(TakeFPSSnapshot());
    }

    private IEnumerator CalculateFPS()
    {
        while (true)
        {
            int fps = Mathf.FloorToInt(1f / Time.unscaledDeltaTime);

            totalFPS += fps;
            frameCount++;

            AddToLowFPSBuffer(fps);

            yield return null;
        }
    }

    private IEnumerator TakeFPSSnapshot()
    {
        while (true)
        {
            yield return new WaitForSeconds(snapshotInterval);
            RecordFPS();
        }
    }

    private void RecordFPS()
    {
        if (totalFPS == 0)
            return;

        int averageFPS = totalFPS / frameCount;

        int averageLowFPS = 0;
        if (lowFPSBuffer.Count > 0)
        {
            int sum = 0;
            for (int i = 0; i < lowFPSBuffer.Count; i++)
                sum += lowFPSBuffer[i];

            averageLowFPS = sum / lowFPSBuffer.Count;
        }
        else
        {
            averageLowFPS = averageFPS;
        }

        FPSReportEvent fpsReportEvent = new FPSReportEvent
        {
            AverageFPS = averageFPS,
            AverageLowFPS = averageLowFPS,
        };

        AnalyticsService.Instance.RecordEvent(fpsReportEvent);

        // Clear sampling period
        totalFPS = 0;
        frameCount = 0;
        lowFPSBuffer.Clear();
    }

    private void AddToLowFPSBuffer(int fps)
    {
        if (lowFPSBuffer.Count < maxLowBufferSize)
        {
            lowFPSBuffer.Add(fps);
        }
        else
        {
            int maxInBuffer = lowFPSBuffer[0];
            int maxIndex = 0;
            for (int i = 1; i < lowFPSBuffer.Count; i++)
            {
                if (lowFPSBuffer[i] > maxInBuffer)
                {
                    maxInBuffer = lowFPSBuffer[i];
                    maxIndex = i;
                }
            }

            if (fps < maxInBuffer)
            {
                lowFPSBuffer[maxIndex] = fps;
            }
        }
    }

    public class FPSReportEvent : Unity.Services.Analytics.Event
    {
        public FPSReportEvent()
            : base("fpsReport") { }

        public int AverageFPS
        {
            set { SetParameter("averageFPS", value); }
        }

        public int AverageLowFPS
        {
            set { SetParameter("averageLowFPS", value); }
        }
    }
}
