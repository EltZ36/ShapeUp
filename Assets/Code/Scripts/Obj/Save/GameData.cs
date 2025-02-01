// using UnityEngine;

using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //code for saving is based on https://videlais.com/2021/02/25/using-jsonutility-in-unity-to-save-and-load-game-data/
    private int LevelsCompleted;

    //https://discussions.unity.com/t/how-to-serialize-a-custom-class/925263
    private Dictionary<int, Dictionary<int, bool>> LevelCompleteMap;

    public List<int> levelNums;
    public List<int> sublevelNums;
    public List<bool> isSubLevelComplete;

    public GameData()
    {
        LevelCompleteMap = new Dictionary<int, Dictionary<int, bool>>();
        levelNums = new List<int>();
        sublevelNums = new List<int>();
        isSubLevelComplete = new List<bool>();
    }

    public void AddLevelToSaveMapping(int levelID, LevelInfo li)
    {
        Dictionary<int, bool> d = new Dictionary<int, bool>();

        int count = 0;
        foreach (SubLevel sl in li.SubLevels)
        {
            d.Add(count++, sl.IsComplete);
        }

        LevelCompleteMap.Add(levelID, d);
    }

    public void setCompletedSublevel(int lID, int slID)
    {
        if (LevelCompleteMap.ContainsKey(lID) == false)
        {
            Debug.Log("coudlnt find key in the dicxtionaoryu");
            return;
        }

        Dictionary<int, bool> CurrentLevel = LevelCompleteMap[lID];
        if (CurrentLevel.ContainsKey(slID) == false)
        {
            Debug.Log("couldnt find sub level in the level");
            return;
        }

        CurrentLevel[slID] = true;

        return;
    }

    public void serialize()
    {
        // go throuhg the base dict by key
        foreach (int levelNum in LevelCompleteMap.Keys)
        {
            levelNums.Add(levelNum);
            // now go through each sublevel dictionary
            foreach (int subLevelNum in LevelCompleteMap[levelNum].Keys)
            {
                sublevelNums.Add(subLevelNum);
                isSubLevelComplete.Add(LevelCompleteMap[levelNum][subLevelNum]);
            }
        }
    }
}
