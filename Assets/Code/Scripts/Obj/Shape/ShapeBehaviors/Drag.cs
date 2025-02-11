using UnityEngine;

public class DragBehavior : MonoBehaviour
{
    private Camera cam;
    private bool dragging,
        zooming;
    Vector3 touchWorldPos,
        startPos,
        endPos;

    private GameObject dragObject;

    void Awake()
    {
        cam = Camera.main;
    }

    void Drag()
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
            }
        }
    }

    void Update()
    {
        if (Input.touches.Length == 1)
        {
            Drag();
        }
    }

    public bool getDragging()
    {
        return dragging;
    }
}
