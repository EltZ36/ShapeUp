
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //code for saving is based on https://videlais.com/2021/02/25/using-jsonutility-in-unity-to-save-and-load-game-data/

    //https://discussions.unity.com/t/how-to-serialize-a-custom-class/925263
    public Dictionary<int, Dictionary<int, bool>> LevelCompleteMap { get; private set; }

    public List<serializedLevel> SLevels;
    public List<serializedSubLevel> SSLevels;

    public GameData()
    {
        LevelCompleteMap = new Dictionary<int, Dictionary<int, bool>>();
        SLevels = new List<serializedLevel>();
        SSLevels = new List<serializedSubLevel>();
    }

    /// <summary>
    /// pass in a level id and a level info instance to set the storage tracker
    /// </summary>
    /// <param name="levelID"></param>
    /// <param name="li"></param>
    public void AddLevelToSaveMapping(int levelID, LevelInfo li)
    {
        if (LevelCompleteMap.ContainsKey(levelID)) return;
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
    /// <summary>
    /// pass in level id and sub level id to set sublevel completed. 
    /// </summary>
    /// <param name="lID">level id</param>
    /// <param name="slID">sub level id</param>
    public void SetCompletedSublevel(int lID, int slID)
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
    /// <summary>
    /// serialize the high level mapping into stupid unity acceptable format because
    /// no one ever though to make a dictionary serializeable in unity i guess
    /// </summary>
    public void Serialize()
    {
        // go throuhg the base dict by key
        //no tuple do a class thank you
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
                SSLevels.Add(ssl);
            }
            SLevels.Add(sl);
        }
    }
    /// <summary>
    /// the opposeide of serialize. sets up the dictionary. what more should be said.
    /// </summary>
    public void Deserialize()
    {
        foreach (var level in SLevels)
        {
            Dictionary<int, bool> d = new Dictionary<int, bool>();
            foreach (var sublevel in SSLevels)
            {
                if (sublevel.ParentLevelNumber == level.LevelNumber)
                {
                    d.Add(sublevel.sublevelNumber, sublevel.completed);
                }
            }

            LevelCompleteMap.Add(level.LevelNumber, d);
        }
    }
}
[System.Serializable]
public struct serializedLevel
{
    public int LevelNumber;

    public int SubLevelCount; // not needed idek
}
[System.Serializable]
public struct serializedSubLevel
{
    public int ParentLevelNumber;

    public int sublevelNumber;

    public bool completed;
}