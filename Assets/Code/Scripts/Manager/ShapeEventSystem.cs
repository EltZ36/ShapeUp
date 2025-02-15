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
    Vector3[] start = new Vector3[10];

    void Update()
    {
        if (Input.touchCount < 1)
        {
            return;
        }
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector2 pos = cam.ScreenToWorldPoint(Input.GetTouch(i).position);
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                RaycastHit2D[] hits = Physics2D.CircleCastAll(pos, 0.1f, Vector2.zero);
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider != null)
                    {
                        Shape shape = hit.collider.gameObject.GetComponent<Shape>();
                        if (shape != null)
                        {
                            start[i] = pos;
                            selectedShape = shape;
                            selectedShape.OnTap();
                            selectedShape.OnDragStart(pos);
                            break;
                        }
                    }
                }
            }
            else if (Input.GetTouch(i).phase == TouchPhase.Moved)
            {
                if (selectedShape != null)
                {
                    selectedShape.OnDrag(start[i], pos);
                }
                else
                {
                    RaycastHit2D[] hits = Physics2D.CircleCastAll(pos, 0.1f, Vector2.zero);
                    foreach (RaycastHit2D hit in hits)
                    {
                        if (hit.collider != null)
                        {
                            Shape shape = hit.collider.gameObject.GetComponent<Shape>();
                            if (shape != null)
                            {
                                shape.OnSlice();
                                break;
                            }
                        }
                    }
                }
            }
            else if (Input.GetTouch(i).phase == TouchPhase.Ended)
            {
                if (selectedShape != null)
                {
                    selectedShape.OnDragEnd(pos);
                    selectedShape = null;
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (Input.touchCount > 0)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < Input.touchCount; i++)
            {
                Vector3 pos = cam.ScreenToWorldPoint(Input.GetTouch(i).position);
                Gizmos.DrawSphere(pos, 0.1f);
            }
        }
    }
}
