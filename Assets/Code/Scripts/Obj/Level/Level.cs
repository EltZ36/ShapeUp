using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private LevelInfo levelInfo;

    public void Start()
    {
        LevelManager.Instance.GetLevelInfo(levelInfo);
    }
}
