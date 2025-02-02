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
    public List<(int, int)> sublevelNums; // first is the associated level, second is the sublevel
    public List<(int, int, bool)> isSubLevelComplete; // first is level num, second is sublevel num, thirs is whether or not its complete

    public GameData()
    {
        LevelCompleteMap = new Dictionary<int, Dictionary<int, bool>>();
        levelNums = new List<int>();
        sublevelNums = new List<(int, int)>();
        isSubLevelComplete = new List<(int, int, bool)>();
    }

    public void AddLevelToSaveMapping(int levelID, LevelInfo li)
    {
        Debug.Log("here in lkevel to savge mappiung");
        // todo add protection for already added
        Dictionary<int, bool> d = new Dictionary<int, bool>();

        int count = 0;
        foreach (SubLevelInfo sl in li.SubLevels)
        {
            Debug.Log("hereinloop");
            Debug.Log(sl.SceneName);
            d.Add(count++, sl.IsComplete);
        }

        LevelCompleteMap.Add(levelID, d);
    }

    public void setCompletedSublevel(int lID, int slID)
    {
        Debug.Log("here in completeedSublevel");
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
        Debug.Log("Hello from serialize");
        // go throuhg the base dict by key
        //no tuple do a class thank you
        foreach (var levelNum in LevelCompleteMap.Keys)
        {
            levelNums.Add(levelNum);
            // now go through each sublevel dictionary
            foreach (int subLevelNum in LevelCompleteMap[levelNum].Keys)
            {
                Debug.Log(subLevelNum);
                Debug.Log(LevelCompleteMap[levelNum][subLevelNum]);
                sublevelNums.Add((levelNum, subLevelNum));
                isSubLevelComplete.Add(
                    (levelNum, subLevelNum, LevelCompleteMap[levelNum][subLevelNum])
                );
            }
        }
    }
}
