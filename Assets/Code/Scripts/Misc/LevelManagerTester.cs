using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerTester : MonoBehaviour
{
    void Start()
    {
        // LevelManager.Instance.Init();
        LevelManager.Instance.LoadLevel(0);
    }
}
