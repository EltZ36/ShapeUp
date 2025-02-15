/*
Date: 1/25/25 (last change)
File: LevelManager.cs
Author: Luke Murayama
Purpose: Implement Level managment controller
Dependancies: ILevelManager, MonoBehaviour
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    public int currentLevelID { get; private set; } = -1;
    private int currentSubLevelID = -1;
    private GameObject[] thumbnails;

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

    public void ClearOneSubLevelProgress(int lID, int slID)
    {
        if (Levels.ContainsKey(lID) == false)
            return;
        if (Levels[lID].SubLevels[slID] == null)
            return;
        Levels[lID].SubLevels[slID].IsComplete = false;
    }

    public void ClearAllLevelProgress(int lID)
    {
        foreach (var subLevel in Levels[lID].SubLevels)
        {
            subLevel.IsComplete = false;
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
            // if the level is complete, don't reload it
            if (Levels[currentLevelID].SubLevels[subLevelID].IsComplete == true)
            {
                return false;
            }
            // if the level is the last level in the list, only load it if all other levels have been completed
            if (subLevelID == Levels[currentLevelID].SubLevels.Count - 1)
            {
                for (int i = 0; i < subLevelID; i++)
                {
                    if (Levels[currentLevelID].SubLevels[i].IsComplete == false)
                    {
                        return false;
                    }
                }
            }
            currentSubLevelID = subLevelID;
            Camera.main.GetComponent<CameraController>().enabled = false;
            string name = Levels[currentLevelID].SubLevels[currentSubLevelID].SceneName;
            DisableThumbnails();
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
                SceneManager.SetActiveScene(subLevel);
                LoadSavedActiveShapes();
            };
            //replace with a call to UI Manager
            SceneManager.LoadSceneAsync("LevelUI", LoadSceneMode.Additive);
            return true;
        }
        return false;
    }

    public void UnloadCurrentSubLevel()
    {
        if (currentSubLevelID != -1)
        {
            Camera.main.GetComponent<CameraController>().enabled = true;
            EnableThumbails();
            UpdateActiveShapePositions();
            Debug.Log(currentSubLevelID);
            SceneManager.UnloadSceneAsync(
                Levels[currentLevelID].SubLevels[currentSubLevelID].SceneName
            );
            //replace with a call to UI Manager
            currentSubLevelID = -1;

            SceneManager.SetActiveScene(
                SceneManager.GetSceneByName(Levels[currentLevelID].SceneName)
            );
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
        Levels[currentLevelID] = levelInfo;

        // cj elton test
        GameManager.Instance.gameData.AddLevelToSaveMapping(currentLevelID, levelInfo);
    }

    public void LeaveCurrentLevel()
    {
        Levels[currentLevelID] = null;
        currentLevelID = -1;
        currentSubLevelID = -1;
    }
    #endregion

    public SubLevelInfo CurrentSubLevelInfo()
    {
        return Levels[currentLevelID].SubLevels[currentSubLevelID];
    }

    public void Start()
    {
        for (int i = 0; i < levelNames.Count; i++)
        {
            Levels.Add(i, null);
        }
        ShapeManager.Instance.OnCreateShape += AddShapeToSubLevelActive;
        ShapeManager.Instance.OnDestroyShape += RemoveShapeFromSubLevelActive;
    }

    private void AddShapeToSubLevelActive(Shape shape)
    {
        CurrentSubLevelInfo()
            .ActiveShapes.Add(
                shape,
                new ShapeSaveInfo(
                    shape.LocalShapeInfo.Shape,
                    shape.LocalShapeInfo.Tags,
                    shape.gameObject.transform
                )
            );
    }

    private void RemoveShapeFromSubLevelActive(Shape shape)
    {
        CurrentSubLevelInfo().ActiveShapes.Remove(shape);
    }

    //call this before leaving a level
    private void UpdateActiveShapePositions()
    {
        var activeShapes = CurrentSubLevelInfo().ActiveShapes;
        foreach (Shape shape in activeShapes.Keys.ToList())
        {
            activeShapes[shape].transformInfo = new TransformInfo(shape.gameObject.transform);
        }
    }

    private void LoadSavedActiveShapes()
    {
        var subLevelShapes = CurrentSubLevelInfo().ActiveShapes;
        if (subLevelShapes.Count == 0)
        {
            return;
        }
        CurrentSubLevelInfo().firstLoad = true;
        var savedShapes = subLevelShapes.Values.ToList();
        CurrentSubLevelInfo().ActiveShapes.Clear();

        foreach (ShapeSaveInfo shapeSave in savedShapes)
        {
            ShapeManager.Instance.CreateShape(
                shapeSave.shapeType,
                shapeSave.transformInfo.position,
                shapeSave.transformInfo.rotation,
                shapeSave.transformInfo.scale,
                shapeSave.shapeTags
            );
        }
    }

    public void Update()
    {
        // probably needs better guards
        if (currentLevelID == -1 || Levels[currentLevelID] == null)
        {
            return;
        }

        if (Camera.main.orthographicSize > 6)
        {
            return;
        }
        (int, float, Vector3) closestSubLevel = new(-1, float.MaxValue, Vector3.zero);

        Vector2 camPos = Camera.main.transform.position;

        for (int i = 0; i < Levels[currentLevelID].SubLevels.Count; i++)
        {
            SubLevelInfo subLevel = Levels[currentLevelID].SubLevels[i];
            float dist = Vector2.Distance(camPos, subLevel.Thumbnail.transform.position);
            if (dist < closestSubLevel.Item2)
            {
                if (subLevel.IsComplete)
                {
                    continue;
                }
                closestSubLevel.Item1 = i;
                closestSubLevel.Item2 = dist;
                closestSubLevel.Item3 = subLevel.Thumbnail.transform.position;
            }
        }

        Vector2 pull = Snap(camPos, closestSubLevel.Item3, 2, 20);
        if (pull.magnitude > 0.1)
        {
            Camera.main.transform.position += (Vector3)pull * Time.deltaTime;
        }
        if (closestSubLevel.Item2 < 2)
        {
            InjectSubLevel(closestSubLevel.Item1, closestSubLevel.Item3);
        }
    }

    private Vector3 Snap(Vector3 position, Vector3 anchor, float snapDist, float strength)
    {
        float distance = Vector2.Distance(position, anchor);
        if (distance < snapDist)
        {
            Vector2 direction = anchor - position;
            return direction.normalized * (distance > strength ? strength : distance);
        }
        return Vector2.zero;
    }

    private void DisableThumbnails()
    {
        thumbnails = GameObject.FindGameObjectsWithTag("thumbnail");
        foreach (var thumbnail in thumbnails)
        {
            thumbnail.SetActive(false);
        }
    }

    private void EnableThumbails()
    {
        foreach (var thumbnail in thumbnails)
        {
            thumbnail.SetActive(true);
        }
    }
}
