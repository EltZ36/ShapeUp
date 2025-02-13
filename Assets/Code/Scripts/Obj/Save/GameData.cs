using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct serializedLevel
{
    public int LevelNumber;

    public int SubLevelCount;
}

[System.Serializable]
public struct serializedSubLevel
{
    public int ParentLevelNumber;

    public int sublevelNumber;

    public bool completed;
}

[System.Serializable]
public class GameData
{
    //code for saving is based on https://videlais.com/2021/02/25/using-jsonutility-in-unity-to-save-and-load-game-data/
    //https://discussions.unity.com/t/how-to-serialize-a-custom-class/925263
    public Dictionary<int, Dictionary<int, bool>> LevelCompleteMap { get; private set; }
    public List<serializedLevel> SerialLevels;
    public List<serializedSubLevel> SerialSubLevels;

    public GameData()
    {
        LevelCompleteMap = new Dictionary<int, Dictionary<int, bool>>();
        SerialLevels = new List<serializedLevel>();
        SerialSubLevels = new List<serializedSubLevel>();
    }

    /// <summary>
    /// pass in a level id and a level info instance to set the storage tracker
    /// </summary>
    /// <param name="levelID"></param>
    /// <param name="li"></param>
    public void AddLevelToSaveMapping(int levelID, LevelInfo li)
    {
        if (LevelCompleteMap.ContainsKey(levelID))
            return;
        // todo add protection for already added
        Dictionary<int, bool> d = new Dictionary<int, bool>();

        int count = 0;
        foreach (SubLevelInfo sl in li.SubLevels)
        {
            d.Add(count++, sl.IsComplete);
        }

        LevelCompleteMap.Add(levelID, d);
    }

    /// <summary>
    /// pass in level id and sub level id to set sublevel completed.
    /// </summary>
    /// <param name="lID">level id</param>
    /// <param name="slID">sub level id</param>
    public void SetCompletedSublevel(int lID, int slID)
    {
        if (LevelCompleteMap.ContainsKey(lID) == false)
        {
            return;
        }

        Dictionary<int, bool> CurrentLevel = LevelCompleteMap[lID];
        if (CurrentLevel.ContainsKey(slID) == false)
        {
            return;
        }

        CurrentLevel[slID] = true;
    }

    /// <summary>
    /// remove a single level from the levels progress dictionary
    /// </summary>
    /// <param name="lID">level id</param>
    public bool DeleteOneLevelProgress(int lID)
    {
        return LevelCompleteMap.Remove(lID);
    }

    /// <summary>
    /// remove one sublevel from the levels progress dict
    /// </summary>
    /// <param name="lID">level id</param>
    /// <param name="slID">sublevel id</param>
    public bool DeleteOneSubLevelProgress(int lID, int slID)
    {
        return LevelCompleteMap[lID].Remove(slID);
    }

    /// <summary>
    /// Clear the levels progress dictionary
    /// </summary>
    public void DeleteAllLevelProgress()
    {
        LevelCompleteMap.Clear();
    }

    /// <summary>
    ///convert the dictionary into lists of struct so that the Unity JSON uility can save them
    /// </summary>
    public void Serialize()
    {
        //check the serialize to make sure the keys are supposed to be there
        // go through the base dict by key
        foreach (var levelNum in LevelCompleteMap.Keys)
        {
            serializedLevel sl;
            sl.LevelNumber = levelNum;
            sl.SubLevelCount = 0;
            // now go through each sublevel dictionary
            foreach (int subLevelNum in LevelCompleteMap[levelNum].Keys)
            {
                sl.SubLevelCount += 1;

                serializedSubLevel ssl;
                ssl.ParentLevelNumber = levelNum;
                ssl.sublevelNumber = subLevelNum;
                ssl.completed = LevelCompleteMap[levelNum][subLevelNum];

                SerialSubLevels.Add(ssl);
            }
            SerialLevels.Add(sl);
        }
    }

    /// <summary>
    /// Take the lists of structs and write them back to the dictionary
    /// </summary>
    public void Deserialize()
    {
        foreach (var level in SerialLevels)
        {
            Dictionary<int, bool> d = new Dictionary<int, bool>();
            foreach (var sublevel in SerialSubLevels)
            {
                if (
                    sublevel.ParentLevelNumber == level.LevelNumber
                    && d.ContainsKey(sublevel.sublevelNumber) != true
                )
                {
                    d.Add(sublevel.sublevelNumber, sublevel.completed);
                }
            }

            if (LevelCompleteMap.ContainsKey(level.LevelNumber) == false)
            {
                LevelCompleteMap.Add(level.LevelNumber, d);
            }
        }
    }
}
