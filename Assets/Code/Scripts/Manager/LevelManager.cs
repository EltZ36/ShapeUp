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

    public Dictionary<int, LevelInfo> Levels { get; private set; } =
        new Dictionary<int, LevelInfo>();

    private int currentLevelID = -1;
    private int currentSubLevelID = -1;

    #region Interface Methods

    public void SetLevelProgress(GameData gd)
    {
        // get each level saved
        foreach (int LNum in gd.LevelCompleteMap.Keys)
        {
            // go throuhg the sublevels of that level
            foreach (var sl in gd.LevelCompleteMap[LNum])
            { // level was beat
                if (sl.Value == true)
                { // set levels accordingly
                    Levels[LNum].SubLevels[sl.Key].IsComplete = true;
                }
            }
        }
    }

    public void OnCurrentLevelComplete()
    {
        if (currentLevelID != -1)
        {
            Levels[currentLevelID].IsComplete = true;
        }
        else
        {
            throw new Exception("OnCurrentLevelComplete: No Level Loaded");
        }
    }

    public void OnCurrentSubLevelComplete()
    {
        if (currentLevelID != -1 || currentSubLevelID != -1)
        {
            Levels[currentLevelID].SubLevels[currentSubLevelID].IsComplete = true;

            // cj elton test
            GameManager.Instance.gameData.SetCompletedSublevel(currentLevelID, currentSubLevelID);
            GameManager.Instance.SaveGame();
        }
        else
        {
            throw new Exception("OnCurrentLevelComplete: No Level or SubLevel Loaded");
        }
    }

    public Dictionary<int, List<int>> GetLevelsProgress()
    {
        Dictionary<int, List<int>> progress = new Dictionary<int, List<int>>();
        for (int i = 0; i < levelNames.Count; i++)
        {
            if (Levels[i] == null)
            {
                progress.Add(i, new List<int>());
                continue;
            }
            else
            {
                List<int> levelProgress = new List<int>();
                LevelInfo level = Levels[i];
                for (int j = 0; j < level.SubLevels.Count; j++)
                {
                    if (level.SubLevels[j].IsComplete)
                    {
                        levelProgress.Add(j);
                    }
                }
            }
        }

        return progress;
    }

    public bool CheckAllSubLevelsComplete()
    {
        if (currentLevelID != -1)
        {
            foreach (var subLevel in Levels[currentLevelID].SubLevels)
            {
                if (subLevel.IsComplete == false)
                {
                    return false;
                }
            }
            return true;
        }
        throw new Exception("CheckAllSubLevelsComplete: No Level Loaded");
    }

    public void LoadLevel(int levelID)
    {
        currentSubLevelID = -1;
        currentLevelID = levelID;
        string name = levelNames[levelID];
        SceneManager.LoadScene(name);
    }

    //SubLevel ID is relative to current level
    public bool InjectSubLevel(int subLevelID, Vector3 position)
    {
        if (currentSubLevelID == -1 && currentLevelID != -1)
        {
            currentSubLevelID = subLevelID;
            string name = Levels[currentLevelID].SubLevels[currentSubLevelID].SceneName;
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
            //replace with a call to UI Manager
            SceneManager.LoadSceneAsync("LevelUI", LoadSceneMode.Additive);
            ///
            return true;
        }
        return false;
    }

    public void UnloadCurrentSubLevel()
    {
        if (currentSubLevelID != -1)
        {
            SceneManager.UnloadSceneAsync(
                Levels[currentLevelID].SubLevels[currentSubLevelID].SceneName
            );
            //replace with a call to UI Manager
            SceneManager.UnloadSceneAsync("LevelUI");
            ///
            currentSubLevelID = -1;
        }
    }

    // Called by a level at start, should give the level manager the relevent information needed
    // Might find a different way of doing this.
    public void GetLevelInfo(LevelInfo levelInfo)
    {
        if (levelInfo == null)
        {
            throw new Exception("GetLevelInfo: Level missing LevelInfo");
        }
        if (levelNames[currentLevelID] != levelInfo.SceneName)
        {
            throw new Exception("GetLevelInfo: Current Level ID and Loaded Level mismatch");
        }
        if (Levels[currentLevelID] == null)
        {
            Levels[currentLevelID] = levelInfo;

            // cj elton test
            GameManager.Instance.gameData.AddLevelToSaveMapping(currentLevelID, levelInfo);
        }
    }
    #endregion

    public void Start()
    {
        for (int i = 0; i < levelNames.Count; i++)
        {
            Levels.Add(i, null);
        }
    }
}
