using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;

public class FPSRecorder : MonoBehaviour
{
    [SerializeField]
    private float updateInterval = 5f;

    public static int CurrentFPS { get; private set; }

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
    }

    private IEnumerator CalculateFPS()
    {
        while (true)
        {
            CurrentFPS = Mathf.FloorToInt(1f / Time.deltaTime);
            RecordFPS();

            yield return new WaitForSeconds(updateInterval);
        }
    }

    public void RecordFPS()
    {
        FPSReportEvent fpsReportEvent = new FPSReportEvent { FPS = CurrentFPS };
        AnalyticsService.Instance.RecordEvent(fpsReportEvent);
    }

    public class FPSReportEvent : Unity.Services.Analytics.Event
    {
        public FPSReportEvent()
            : base("fpsReport") { }

        public int FPS
        {
            set { SetParameter("fps", value); }
        }
    }
}
