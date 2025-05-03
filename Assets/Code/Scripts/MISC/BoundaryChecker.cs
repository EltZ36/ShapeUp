using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoundaryChecker : MonoBehaviour
{
    [SerializeField]
    Shape[] shapes;

    [SerializeField]
    EnterWin enterWin;

    string[] names;

    List<string> active = new List<string>();

    public bool shapeInside = false;

    void Awake()
    {
        names = shapes.Select(s => s.ShapeName).ToArray();
        ShapeManager.Instance.OnDestroyShape += RemoveShape;
        ShapeManager.Instance.OnCreateShape += AddShape;
    }

    void OnDestroy()
    {
        ShapeManager.Instance.OnDestroyShape -= RemoveShape;
        ShapeManager.Instance.OnCreateShape -= AddShape;
    }

    void AddShape(Shape shape)
    {
        active.Add(shape.ShapeName);
    }

    void RemoveShape(Shape shape)
    {
        active.Remove(shape.ShapeName);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var shape = collision.gameObject.GetComponent<Shape>();
        if (shape != null && shapeInside == false)
        {
            if (names.Contains(shape.ShapeName))
            {
                shapeInside = true;
                enterWin.checkWin();
            }
            else
            {
                Debug.Log(shape.UniqueID + " is not a target shape");
                Debug.Log(shapes[0].UniqueID + " is the target shape");
            }
        }
    }
}
