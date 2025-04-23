using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashLevelScreenPosition : MonoBehaviour
{
    public float XOffset,
        YOffset = 0.0f;

    private float defaultWidth = 1920f;
    private float defaultHeight = 1080f;

    private float defaultAspectRatio = 16f / 9f;

    void Awake()
    {
        transform.position =
            Camera.main.ScreenToWorldPoint(Vector3.zero) + new Vector3(XOffset, YOffset, 10f);
        float aspectRatio = Screen.width / Screen.height;
        transform.localScale = new Vector3(
            (Screen.width / defaultWidth) * (aspectRatio / defaultAspectRatio),
            (Screen.height / defaultHeight) * (aspectRatio / defaultAspectRatio),
            1f
        );
    }
}
