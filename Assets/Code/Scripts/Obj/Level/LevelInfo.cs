using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class LevelInfo : LevelCore
{
    public List<SubLevel> SubLevels = new List<SubLevel>();

    public LevelInfo(string sceneName)
    {
        SceneName = sceneName;
    }
}
