using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    void Start()
    {
        ShapeManager.Instance.CreateShape(ShapeType.Triangle, Vector3.zero);
        ShapeType shape = (ShapeType)
            ShapeManager.Instance.CombineShapes(ShapeType.Triangle, ShapeType.Square);
        ShapeType shape2 = (ShapeType)
            ShapeManager.Instance.CombineShapes(ShapeType.Square, ShapeType.Triangle);

        Debug.Log(shape);
        Debug.Log(shape2);
        Debug.Log(shape == shape2);
        foreach (ShapeType s in ShapeManager.Instance.TakeApartShape(shape))
        {
            Debug.Log(s);
        }

        ShapeManager
            .Instance.CreateShape(ShapeType.Square, Vector3.one)
            .GetComponent<Rigidbody2D>()
            .gravityScale = 0;
        ShapeManager
            .Instance.CreateShape(ShapeType.Square, Vector3.left)
            .GetComponent<Rigidbody2D>()
            .gravityScale = 0;
    }
}
