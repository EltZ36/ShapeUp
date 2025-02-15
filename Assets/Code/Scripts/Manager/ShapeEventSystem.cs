using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeEventSystem : MonoBehaviour
{
    #region Singleton Pattern
    public static ShapeEventSystem _instance;
    public static ShapeEventSystem Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion
    private Camera cam
    {
        get { return Camera.main; }
    }

    Shape selectedShape = null;
    Vector3 start = default;

    void Update()
    {
        if (Input.touchCount < 1)
        {
            return;
        }
        Vector2 pos = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null)
            {
                Shape shape = hit.collider.gameObject.GetComponent<Shape>();
                if (shape == null)
                {
                    return;
                }
                start = pos;

                selectedShape = shape;

                selectedShape.OnTap();
                selectedShape.OnDragStart(pos);
            }
        }
        else if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            if (selectedShape != null)
            {
                selectedShape.OnDrag(start, pos);
            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
                if (hit.collider != null)
                {
                    Shape shape = hit.collider.gameObject.GetComponent<Shape>();
                    if (shape == null)
                    {
                        return;
                    }
                    shape.OnSlice();
                }
            }
        }
        else if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            if (selectedShape != null)
            {
                selectedShape.OnDragEnd(Input.mousePosition);
                selectedShape = null;
            }
        }
    }
}
