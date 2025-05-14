using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public Dictionary<string, string> levelDict = new Dictionary<string, string>();
    private List<string> randomLevels = new List<string>();

    [SerializeField]
    public DailyUI UI;

    private int currentLevelIndex;
    public int timer;

    private bool complete;

    public string copyString;

    void Start()
    {
        timer = 0;
        complete = false;
        SetSeed();
        PopulateLevelDict();
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
        availableLevels.AddRange(levelDict.Keys);
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
            SetCopyString();
            Debug.Log(copyString);
            UI.Win();
        }
        else
        {
            SceneManager.LoadSceneAsync(randomLevels[currentLevelIndex], LoadSceneMode.Additive);
        }
    }

    public void PopulateLevelDict()
    {
        levelDict.Add("EggLevel", "🥚");
        levelDict.Add("LevelTap", "🛷");
        levelDict.Add("TableLevel", "🧺");
        // levelDict.Add("PigDragLevel", "🪙");
        levelDict.Add("BoxTap", "📦");
        levelDict.Add("LevelOneFinal", "🪖");
        levelDict.Add("LevelPinch", "🏔️");

        levelDict.Add("BreakRope", "🎣");

        levelDict.Add("BoulderLevel", "🪤");

        levelDict.Add("HouseLevel", "🏚️");
        levelDict.Add("MazeLevel", "🪓");
        levelDict.Add("PigLevel", "🐽");
        levelDict.Add("ShadowLevel", "☀️");
        levelDict.Add("TwoShadowLevel", "🌙");
        levelDict.Add("LightTap", "🪰");
        levelDict.Add("LightbulbLevel", "💡");
    }

    public void SetCopyString()
    {
        copyString =
            levelDict[randomLevels[0]] + levelDict[randomLevels[1]] + levelDict[randomLevels[2]];
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
