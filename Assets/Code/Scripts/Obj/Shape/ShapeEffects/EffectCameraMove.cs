using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField]
    private GameObject ObjectToZoom;
    private float zoomInSize = 1.2f;
    private float zoomOutSize = 5f;

    void Awake()
    {
        mainCamera = Camera.main;
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        if (aspectRatio < 16f / 9f)
        {
            zoomInSize = 1.2f * ((16f / 9f) / aspectRatio);
            zoomOutSize = 5f * ((16f / 9f) / aspectRatio);
        }
    }

    public void ZoomIn(EventInfo eventInfo)
    {
        mainCamera.orthographicSize = zoomInSize;
        // mainCamera.transform.position = new Vector3(0, -1.29f, -10);
    }

    public void ZoomOut()
    {
        mainCamera.orthographicSize = zoomOutSize;
        // mainCamera.transform.position = new Vector3(0, 0, -10);
    }
}
