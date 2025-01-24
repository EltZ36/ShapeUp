using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Interface for the LevelManager Singleton
/// </summary>
interface ILevelManager
{
    /// <summary>
    /// Initializes the LevelManager with level information
    /// </summary>
    void Init();

    /// <summary>
    /// Marks level as complete and invokes OnLevelComplete event.
    /// </summary>
    /// <param name="levelID">Based on LevelManager Dict, not build number</param>
    void OnLevelCompleteEvent(int levelID);

    /// <summary>
    /// Moves to the scene named LevelSelect
    /// </summary>
    void LoadLevelSelect();

    /// <summary>
    /// Moves to the scene defined by levelID
    /// </summary>
    /// <param name="levelID">Based on LevelManager Dict, not build number</param>
    void LoadLevel(int levelID);

    /// <summary>
    /// Loads SubLevel scene onto the current scene at the defined position. Ensure SubLevel has a single root node.
    /// Only one SubLevel can be loaded at a time.
    /// </summary>
    /// <param name="SubLevelID">Based on SubLevels list relative to current Level</param>
    /// <param name="position">Position of new scene in world space</param>
    /// <returns></returns>
    bool InjectSubLevel(int SubLevelID, Vector3 position);

    /// <summary>
    /// Unloads the current SubLevel.
    /// </summary>
    void UnloadCurrentSubLevel();
}

[Serializable]
public class SubLevel
{
    public GameObject Thumbnail;
    public string SceneName;
    public bool IsComplete;

    public override string ToString()
    {
        return $"SubLevel: {SceneName}, Status: {(IsComplete ? "Completed" : "Incomplete")}";
    }
}

[Serializable]
public class LevelInfo
{
    public List<SubLevel> SubLevels = new List<SubLevel>();
    public string SceneName;
    public bool IsComplete;

    public override string ToString()
    {
        return $"Level: {SceneName}, Status: {(IsComplete ? "Completed" : "Incomplete")}";
    }
}

public class LevelManager : MonoBehaviour, ILevelManager
{
    #region Singleton Pattern
    public static LevelManager _instance;
    public static LevelManager Instance
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
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion

    //Instead of using the build number, use the index/name defined below.
    [SerializeField]
    private List<string> levelNames = new List<string>();
    private Dictionary<int, LevelInfo> levels = new Dictionary<int, LevelInfo>();
    public Dictionary<int, LevelInfo> Levels
    {
        get { return levels; }
    }

    public enum LMState : uint
    {
        Select = 0, //Level Select Menu
        Level = 1, //Level
        SubLevel = 2, //SubLevel
    }

    private LMState currentState = LMState.Select;
    public LMState CurrentState
    {
        get { return currentState; }
    }

    public event Action<int> OnLevelComplete;

    private LevelInfo currentLevel = null;
    private SubLevel currentSubLevel = null;

    #region Interface Methods
    public void Init()
    {
        for (int i = 0; i < levelNames.Count; i++)
        {
            levels.Add(i, new LevelInfo());
            levels[i].SceneName = levelNames[i];
            levels[i].IsComplete = false;
        }
        OnLevelComplete += PrintLogLevelComplete;
    }

    public void OnLevelCompleteEvent(int levelID)
    {
        OnLevelComplete?.Invoke(levelID);
        levels[levelID].IsComplete = true;
    }

    public void LoadLevelSelect()
    {
        SetLMState(LMState.Select);
        SceneManager.LoadScene("LevelSelect");
    }

    public void LoadLevel(int levelID)
    {
        if (CurrentState == LMState.Select)
        {
            SetLMState(LMState.Level);
            currentLevel = levels[levelID];
            string name = currentLevel.SceneName;
            SceneManager.LoadScene(name);
        }
    }

    //SubLevel ID is relative to current level
    public bool InjectSubLevel(int SubLevelID, Vector3 position)
    {
        if (CurrentState == LMState.Level && currentLevel != null)
        {
            SetLMState(LMState.SubLevel);
            currentSubLevel = currentLevel.SubLevels[SubLevelID];
            string name = currentSubLevel.SceneName;
            SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive).completed += (operation) =>
            {
                Scene subLevel = SceneManager.GetSceneByName(name);
                if (subLevel.isLoaded)
                {
                    //Ensure all sublevels have a root gameobject which every other gameobject is child of.
                    //This allows for moving scene after loading.
                    GameObject root = subLevel.GetRootGameObjects()[0];
                    root.transform.position = position;
                }
            };
            return true;
        }
        return false;
    }

    public void UnloadCurrentSubLevel()
    {
        if (CurrentState == LMState.SubLevel && currentSubLevel != null)
        {
            SceneManager.UnloadSceneAsync(currentSubLevel.SceneName);
            currentSubLevel = null;
            SetLMState(LMState.Level);
        }
    }

    // Called by a level at start, should give the level manager the relevent information needed
    // Might find a different way of doing this.
    public void GetSubLevels(List<SubLevel> subLevels)
    {
        if (currentLevel.SubLevels.Count == 0)
        {
            currentLevel.SubLevels.AddRange(subLevels);
        }
        else
        {
            Debug.Log("Already Loaded SubLevels");
        }
    }
    #endregion

    public void SetLMState(LMState state)
    {
        currentState = state;
    }

    private void PrintLogLevelComplete(int level)
    {
        Debug.Log("Level Complete: " + level);
    }
}
