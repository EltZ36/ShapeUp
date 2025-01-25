using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Usage
// Configure Unity Remote with the aid of this video: https://youtu.be/iCXwaehzRFQ?si=JsgApI07xvRKaPWC
// Create an empty gameObject and attach this script to it
// Create the following tags in Unity: Drag, Slice, Zoom, Accelerate
// Add 2D gameObjects at your discretion; give each gameObject a collider and a tag depending on what kind of interaction you want it to have
// Drag behavior: touch and drag to move draggable objects
// Slice behavior: on held touch, a line is drawn between where your finger first touches and where it is released. If a sliceable object is on that line, it's renderer will disable for a few seconds
// Zoom behavior: When two fingers are held with an object between them, you can scale the size of the object by increasing and decreasing the distance between your fingers
// Accelerate behavior: with your device oriented such that the screen is facing you and the home button is on the bottom, tilting your phone left and right will slide acceleratable objects back and forth

public class TouchInput : MonoBehaviour
{
    private Camera cam;
    private bool dragging,
        zooming;
    Vector3 touchWorldPos,
        startPos,
        endPos;
    private GameObject dragObject,
        sliceObject,
        zoomObject,
        accelerateObject;
    private float zoom,
        speed;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        dragging = false;
        zoom = 1f;
        speed = 1f;
        accelerateObject = GameObject.FindWithTag("Accelerate");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touches.Length == 0) { }
        else if (Input.touches.Length == 1)
        {
            handleOneTouch();
        }
        else if (Input.touches.Length == 2)
        {
            handleTwoTouches();
        }
        handleAcceleration();
    }

    void checkSlice()
    {
        RaycastHit2D h = Physics2D.Linecast(startPos, endPos);
        if (h.collider != null)
        {
            if (h.transform.gameObject.CompareTag("Slice"))
            {
                sliceObject = h.transform.gameObject;
                StartCoroutine(slice());
            }
        }
    }

    IEnumerator slice()
    {
        sliceObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(2);
        sliceObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    void handleOneTouch()
    {
        Touch touch = Input.touches[0];
        if (touch.phase == TouchPhase.Began)
        {
            touchWorldPos = cam.ScreenToWorldPoint(touch.position);
            Vector2 touchWorldPos2D = new Vector2(touchWorldPos.x, touchWorldPos.y);
            RaycastHit2D hitInformation = Physics2D.Raycast(touchWorldPos2D, cam.transform.forward);
            if (hitInformation.collider != null)
            {
                if (hitInformation.transform.gameObject.CompareTag("Drag"))
                {
                    dragObject = hitInformation.transform.gameObject;
                    dragging = true;
                }
            }
            else
            {
                startPos = cam.ScreenToWorldPoint(
                    new Vector3(touch.position.x, touch.position.y, cam.nearClipPlane)
                );
            }
        }
        else if (touch.phase == TouchPhase.Moved && dragging)
        {
            dragObject.transform.position = cam.ScreenToWorldPoint(
                new Vector3(touch.position.x, touch.position.y, cam.nearClipPlane)
            );
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            if (dragging)
            {
                dragging = false;
            }
            else
            {
                endPos = cam.ScreenToWorldPoint(
                    new Vector3(touch.position.x, touch.position.y, cam.nearClipPlane)
                );
                checkSlice();
            }
        }
    }

    void handleTwoTouches()
    {
        if (!zooming)
        {
            Vector3 pointOne = cam.ScreenToWorldPoint(Input.touches[0].position);
            Vector3 pointTwo = cam.ScreenToWorldPoint(Input.touches[1].position);
            Vector2 pointOne2D = new Vector2(pointOne.x, pointOne.y);
            Vector2 pointTwo2D = new Vector2(pointTwo.x, pointTwo.y);
            Vector2 midPoint = new Vector2(
                (Mathf.Abs(pointOne2D.x - pointTwo2D.x) / 2) + pointOne2D.x,
                (Mathf.Abs(pointOne2D.y - pointTwo2D.y) / 2) + pointOne2D.y
            );
            RaycastHit2D hitInformation = Physics2D.Raycast(midPoint, cam.transform.forward);
            if (hitInformation.collider != null)
            {
                if (hitInformation.transform.gameObject.CompareTag("Zoom"))
                {
                    zoomObject = hitInformation.transform.gameObject;
                    zoom = Vector2.Distance(pointOne2D, pointTwo2D);
                    zooming = true;
                }
            }
        }
        else
        {
            Vector3 pointOne = cam.ScreenToWorldPoint(Input.touches[0].position);
            Vector3 pointTwo = cam.ScreenToWorldPoint(Input.touches[1].position);
            Vector2 pointOne2D = new Vector2(pointOne.x, pointOne.y);
            Vector2 pointTwo2D = new Vector2(pointTwo.x, pointTwo.y);
            Vector3 scaleChange = new Vector3(
                Mathf.Clamp(
                    zoomObject.transform.localScale.x
                        + (Vector2.Distance(pointTwo2D, pointOne2D) - zoom) / 2f,
                    0.5f,
                    2f
                ),
                Mathf.Clamp(
                    zoomObject.transform.localScale.y
                        + (Vector2.Distance(pointTwo2D, pointOne2D) - zoom) / 2f,
                    0.5f,
                    2f
                ),
                1f
            );
            zoomObject.transform.localScale = scaleChange;
            zoom = Vector2.Distance(pointTwo2D, pointOne2D);
        }
        if (
            Input.touches[0].phase == TouchPhase.Ended
            || Input.touches[1].phase == TouchPhase.Ended
        )
        {
            zooming = false;
        }
    }

    void handleAcceleration()
    {
        Vector3 dir = Vector3.zero;

        dir.x = Input.acceleration.x;
        //dir.y = Input.acceleration.y;

        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        dir *= Time.deltaTime;

        // Move object
        accelerateObject.transform.Translate(dir * speed);
    }
}
