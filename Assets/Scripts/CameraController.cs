using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public float speed;
    private float zoom;

    private void Start()
    {
        zoom = 1f;
    }

    void Update()
    {
        if (Input.touches.Length == 0) { }
        else if (Input.touches.Length == 1)
        {
            handleOneTouch();
        }
        else if (Input.touches.Length == 2)
        {
            handleOneTouch();
            handleTwoTouches();
        }
    }

    void handleOneTouch()
    {
        Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
        transform.Translate(-touchDeltaPosition.x * speed * Time.deltaTime, -touchDeltaPosition.y * speed * Time.deltaTime, 0);
    }

    void handleTwoTouches()
    {
        if (Input.touches[0].phase == TouchPhase.Began || Input.touches[1].phase == TouchPhase.Began)
        {
            zoom = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
        } 
        else if (Input.touches[0].phase == TouchPhase.Moved || Input.touches[1].phase == TouchPhase.Moved)
        {
            float scaleChange = Mathf.Clamp(Camera.main.orthographicSize - (Vector2.Distance(Input.touches[0].position, Input.touches[1].position) - zoom) / 10f, 5f, 25f);
            Camera.main.orthographicSize = scaleChange;
            zoom = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);

        }
    }
}
