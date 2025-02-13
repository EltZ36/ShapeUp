using UnityEngine;

public class DragBehavior : MonoBehaviour
{
    // [SerializeField]
    private Camera cam; // delete this
    private bool dragging;
    Vector3 touchWorldPos;

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
            if (hitInformation.collider != null) // it hit!
            {
                // tapped on the game object this script is attached to
                if (hitInformation.transform.gameObject == gameObject)
                {
                    dragging = true;
                }
            }
        }
        else if (touch.phase == TouchPhase.Moved && dragging)
        {
            gameObject.transform.position = cam.ScreenToWorldPoint(
                new Vector3(touch.position.x, touch.position.y, cam.nearClipPlane)
            );
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            if (dragging)
            {
                dragging = false;
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
}
