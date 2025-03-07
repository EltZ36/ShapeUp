using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashLevelScreenPosition : MonoBehaviour
{
    public float XOffset,
        YOffset = 0.0f;

    void Awake()
    {
        transform.position =
            Camera.main.ScreenToWorldPoint(Vector3.zero) + new Vector3(XOffset, YOffset, 10f);
    }
}
