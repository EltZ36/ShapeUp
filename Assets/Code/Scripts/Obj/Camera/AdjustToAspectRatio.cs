using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustToAspectRatio : MonoBehaviour
{
    void Start()
    {
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        if (aspectRatio < 16f / 9f)
        {
            Camera.main.orthographicSize = 5f * ((16f / 9f) / aspectRatio);
        }
    }
}
