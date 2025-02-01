using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class LevelInfo : LevelCore
{
    public List<SubLevelInfo> SubLevels = new List<SubLevelInfo>();

    public LevelInfo(string sceneName)
    {
        SceneName = sceneName;
    }
}
