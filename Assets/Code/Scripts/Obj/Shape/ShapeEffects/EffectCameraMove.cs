using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    public void ZoomIn(EventInfo eventInfo)
    {
        mainCamera.orthographicSize = 2.0f;
        mainCamera.transform.position = new Vector3(0, -0.52f, -10);
    }
}
