using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SubLevelInfo : LevelCore
{
    public GameObject Thumbnail;

    public Dictionary<Shape, ShapeSaveInfo> ActiveShapes = new Dictionary<Shape, ShapeSaveInfo>();
    public bool firstLoad = false;
}
