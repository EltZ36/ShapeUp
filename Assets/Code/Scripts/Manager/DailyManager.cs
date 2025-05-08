using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using Unity.Mathematics;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class DailyManager : MonoBehaviour
{
    #region Singleton Pattern
    public static DailyManager _instance;
    public static DailyManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            // DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion

    [SerializeField]
    public List<string> levelPool = new List<string>();
    private List<string> randomLevels = new List<string>();

    [SerializeField]
    public DailyUI UI;

    public int currentLevelIndex,
        timer;

    private bool complete;

    void Start()
    {
        timer = 0;
        complete = false;
        SetSeed();
        PickLevels();
        // Debug.Log(randomLevels[0] + ", " + randomLevels[1] + ", " + randomLevels[2]);
        SceneManager.LoadSceneAsync(randomLevels[0], LoadSceneMode.Additive);
        currentLevelIndex = 0;
        StartCoroutine(IncrementTimer());
    }

    void Update() { }

    private void SetSeed()
    {
        DateTime dt = DateTime.Now;
        int randomSeed = Int32.Parse(dt.DayOfYear + "" + dt.Year);
        UnityEngine.Random.InitState(randomSeed);
    }

    private void PickLevels()
    {
        List<string> availableLevels = new List<string>();
        availableLevels.AddRange(levelPool);
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableLevels.Count - 1);
            randomLevels.Add(availableLevels[randomIndex]);
            availableLevels.RemoveAt(randomIndex);
        }
    }

    public void LoadNextLevel()
    {
        SceneManager.UnloadSceneAsync(randomLevels[currentLevelIndex]);
        currentLevelIndex++;
        if (currentLevelIndex == 3)
        {
            complete = true;
            UI.Win();
        }
        else
        {
            SceneManager.LoadSceneAsync(randomLevels[currentLevelIndex], LoadSceneMode.Additive);
        }
    }

    IEnumerator IncrementTimer()
    {
        while (!complete)
        {
            yield return new WaitForSeconds(1);
            timer++;
        }
    }
}
