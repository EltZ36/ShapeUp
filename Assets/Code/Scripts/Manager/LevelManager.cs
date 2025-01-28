/*
Date: 1/25/25 (last change)
File: LevelManager.cs
Author: Luke Murayama
Purpose: Implement Level managment controller
Dependancies: ILevelManager, MonoBehaviour
*/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            levels.Add(i, new LevelInfo(levelNames[i]));
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
            // change set equal instead of overwriting
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
