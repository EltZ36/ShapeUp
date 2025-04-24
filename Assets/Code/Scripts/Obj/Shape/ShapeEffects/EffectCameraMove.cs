using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField]
    private GameObject ObjectToZoom;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    public void ZoomIn(EventInfo eventInfo)
    {
        mainCamera.orthographicSize = 2.0f;
        mainCamera.transform.position = new Vector3(0, -1.29f, -10);
    }

    public void ZoomOut()
    {
        mainCamera.orthographicSize = 5.0f;
        mainCamera.transform.position = new Vector3(0, 0, -10);
    }
}
