using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //code for saving is based on https://videlais.com/2021/02/25/using-jsonutility-in-unity-to-save-and-load-game-data/
    public int LevelsCompleted;
    //https://discussions.unity.com/t/how-to-serialize-a-custom-class/925263 
    //public SubLevel subLevel;
    //public LevelInfo levelInfo;
}