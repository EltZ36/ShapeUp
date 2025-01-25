using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private List<SubLevel> subLevels;

    public void Start()
    {
        LevelManager.Instance.GetSubLevels(subLevels);
    }
}
