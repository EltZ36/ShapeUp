using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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

    private List<ShapeDatabase> shapeDatabases = new List<ShapeDatabase>();
    private List<ShapeRecipes> shapeRecipes = new List<ShapeRecipes>();

    [SerializeField]
    public DailyUI UI;

    [SerializeField]
    public TextMeshProUGUI loading;

    private int currentLevelIndex;
    public int timer;

    private bool complete;

    public string copyString;

    void Start()
    {
        Physics2D.gravity = new Vector2(0f, -9.8f);
        timer = 0;
        complete = false;
        currentLevelIndex = 0;
        SetSeed();
        PopulateLevelDict();
        PickLevels();
        // Debug.Log(randomLevels[0] + ", " + randomLevels[1] + ", " + randomLevels[2]);
        StartCoroutine(PreLoadLevels());
    }

    IEnumerator PreLoadLevels()
    {
        for (int i = 2; i >= 0; i--)
        {
            //Begin to load the Scene you specify
            AsyncOperation level = SceneManager.LoadSceneAsync(
                randomLevels[i],
                LoadSceneMode.Additive
            );
            while (!level.isDone)
            {
                yield return null;
            }
            Scene subLevel = SceneManager.GetSceneByName(randomLevels[i]);
            GameObject root = subLevel.GetRootGameObjects()[0];
            shapeDatabases.Insert(0, root.GetComponentInChildren<ShapeDatabase>());
            shapeRecipes.Insert(0, root.GetComponentInChildren<ShapeRecipes>());
            if (i == 0)
            {
                SceneManager.SetActiveScene(subLevel);
                if (shapeDatabases[0] != null)
                {
                    ShapeManager.Instance.shapeDatabase = shapeDatabases[0];
                }
                if (shapeRecipes[0] != null)
                {
                    ShapeManager.Instance.shapeRecipes = shapeRecipes[0];
                }
            }
            else if (i != 0)
            {
                root.SetActive(false);
            }
        }
        Debug.Log("levels loaded");
        loading.enabled = false;
        StartCoroutine(IncrementTimer());
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
        SceneManager.UnloadSceneAsync(randomLevels[currentLevelIndex]).completed += (operation) =>
        {
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
                Scene subLevel = SceneManager.GetSceneByName(randomLevels[currentLevelIndex]);
                GameObject root = subLevel.GetRootGameObjects()[0];
                root.SetActive(true);
                SceneManager.SetActiveScene(subLevel);
                if (shapeDatabases[currentLevelIndex] != null)
                {
                    ShapeManager.Instance.shapeDatabase = shapeDatabases[currentLevelIndex];
                }
                if (shapeRecipes[currentLevelIndex] != null)
                {
                    ShapeManager.Instance.shapeRecipes = shapeRecipes[currentLevelIndex];
                }
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
        };
    }

    public void PopulateLevelDict()
    {
        // levelDict.Add("EggLevel", "🥚");
        // levelDict.Add("SledLevel", "🛷");
        // levelDict.Add("TableLevel", "🧺");
        // levelDict.Add("PigDragLevel", "🪙");
        // levelDict.Add("BoxTap", "📦");
        // levelDict.Add("TapDragFinal", "🪖");

        // levelDict.Add("CubeheadForThree", "🏀");
        // // levelDict.Add("PigSwipeLevel", "🐖");
        // levelDict.Add("PinchDrag", "☃️");
        // levelDict.Add("PlateLevel", "🍽️");
        // levelDict.Add("CirclePinch", "🏔️");
        // levelDict.Add("RopeLevel", "🎣");
        // levelDict.Add("BoulderLevel", "🪤");

        // levelDict.Add("PigShake", "🐷");
        levelDict.Add("RocketLevel", "🚀");
        levelDict.Add("SaltShaker", "🧂");
        levelDict.Add("SeesawTilt", "⚖️");
        levelDict.Add("ShakeBoulder", "🗿");
        levelDict.Add("ShakeMaze", "🧰");
        levelDict.Add("SmashIt", "🧀");

        // levelDict.Add("HouseLevel", "🏚️");
        // levelDict.Add("MazeLevel", "🗺️");
        // levelDict.Add("PigLevel", "🐽");
        // levelDict.Add("ShadowLevel", "☀️");
        // levelDict.Add("TwoShadowLevel", "🌙");
        // levelDict.Add("LightTap", "🐝");
        // levelDict.Add("LightbulbLevel", "💡");
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
