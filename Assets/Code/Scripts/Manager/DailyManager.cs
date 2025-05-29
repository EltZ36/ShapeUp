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

    private List<AsyncOperation> preLoadedLevels = new List<AsyncOperation>();

    [SerializeField]
    public DailyUI UI;

    private int currentLevelIndex;
    public int timer;

    private bool complete;

    public string copyString;

    void Start()
    {
        Physics2D.gravity = new Vector2(0f, -9.8f);
        timer = 0;
        complete = false;
        SetSeed();
        PopulateLevelDict();
        PickLevels();
        // Debug.Log(randomLevels[0] + ", " + randomLevels[1] + ", " + randomLevels[2]);
        StartCoroutine(PreLoadLevels());
    }

    IEnumerator PreLoadLevels()
    {
        PreLoadLevel(0);
        while (preLoadedLevels[0].progress < 0.9f)
        {
            Debug.Log("Level 1 Progress: " + preLoadedLevels[0].progress);
            yield return null;
        }
        PreLoadLevel(1);
        while (preLoadedLevels[1].progress < 0.9f)
        {
            Debug.Log("Level 2 Progress: " + preLoadedLevels[1].progress);
            yield return null;
        }
        PreLoadLevel(2);
        while (preLoadedLevels[2].progress < 0.9f)
        {
            Debug.Log("Level 3 Progress: " + preLoadedLevels[2].progress);
            yield return null;
        }
        preLoadedLevels[0].allowSceneActivation = true;
        preLoadedLevels[0].completed += (operation) =>
        {
            Scene subLevel = SceneManager.GetSceneByName(randomLevels[0]);
            SceneManager.SetActiveScene(subLevel);
        };
        currentLevelIndex = 0;
        StartCoroutine(IncrementTimer());
    }

    private void PreLoadLevel(int index)
    {
        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(randomLevels[index]);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        preLoadedLevels.Add(asyncOperation);
    }

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
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        if (aspectRatio < 16f / 9f)
        {
            Camera.main.orthographicSize = 5f * ((16f / 9f) / aspectRatio);
        }

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
            preLoadedLevels[currentLevelIndex].allowSceneActivation = true;
            preLoadedLevels[currentLevelIndex].completed += (operation) =>
            {
                Scene subLevel = SceneManager.GetSceneByName(randomLevels[currentLevelIndex]);
                SceneManager.SetActiveScene(subLevel);
            };
            if (aspectRatio < 16f / 9f)
            {
                Camera.main.orthographicSize = 5f * ((16f / 9f) / aspectRatio);
            }
            else
            {
                Camera.main.orthographicSize = 5f;
            }
            ShapeEventSystem.Instance.ClearSelectedShape();
        }
    }

    public void PopulateLevelDict()
    {
        levelDict.Add("EggLevel", "🥚");
        levelDict.Add("SledLevel", "🛷");
        levelDict.Add("TableLevel", "🧺");
        levelDict.Add("PigDragLevel", "🪙");
        levelDict.Add("BoxTap", "📦");
        levelDict.Add("TapDragFinal", "🪖");

        levelDict.Add("CubeheadForThree", "🏀");
        levelDict.Add("PigSwipeLevel", "🐖");
        levelDict.Add("PinchDrag", "🚦");
        levelDict.Add("PlateLevel", "🍽️");
        levelDict.Add("CirclePinch", "🏔️");
        levelDict.Add("RopeLevel", "🎣");
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
        DateTime dt = DateTime.Now;
        copyString =
            "Shape Up \n"
            + dt.Month
            + "/"
            + dt.Day
            + "/"
            + dt.Year
            + " \n\n"
            + levelDict[randomLevels[0]]
            + levelDict[randomLevels[1]]
            + levelDict[randomLevels[2]];
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
