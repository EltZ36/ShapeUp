using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashLevelScreenPosition : MonoBehaviour
{
    public float XOffset,
        YOffset = 0.0f;

    private float defaultAspectRatio = 16f / 9f;

    void Awake()
    {
        // transform.position =
        //     Camera.main.ScreenToWorldPoint(Vector3.zero) + new Vector3(XOffset, YOffset, 10f);
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        float width = aspectRatio / defaultAspectRatio;
        Debug.Log(defaultAspectRatio + ", " + aspectRatio);
        transform.localScale = new Vector3(width, 1f, 1f);
    }
}
