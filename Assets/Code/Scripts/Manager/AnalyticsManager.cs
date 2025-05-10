using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsManager : MonoBehaviour
{
    private FPSRecorder fpsRecorder;

    #region Singleton Pattern
    private static AnalyticsManager _instance;
    public static AnalyticsManager Instance
    {
        get { return _instance; }
    }

    async void Awake()
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

            await UnityServices.InitializeAsync();
            AnalyticsService.Instance.StartDataCollection();
        }

        fpsRecorder = GetComponent<FPSRecorder>();
        if (fpsRecorder != null)
        {
            fpsRecorder.StartTackingFPS();
        }
        else
        {
            Debug.Log("FPS Recorder component not found");
        }

        // DELETE THIS BEFORE SHIPPING. SOLELY FOR DEVELOPMENT TESTING
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    #endregion
}
