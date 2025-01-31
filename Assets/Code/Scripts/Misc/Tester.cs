using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    void Start()
    {
        // LevelManager.Instance.Init();
        LevelManager.Instance.OnLevelCompleteEvent(0);
        LevelManager.Instance.LoadLevel(0);
    }
}
