using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    void Start()
    {
        // ShapeManager.Instance.CreateShape(
        //     ShapeType.Triangle,
        //     Vector3.zero,
        //     ShapeTags.Gravity | ShapeTags.Drag
        // );
        // ShapeType shape = (ShapeType)
        //     ShapeManager.Instance.CombineShapes(ShapeType.Triangle, ShapeType.Square);
        // ShapeType shape2 = (ShapeType)
        //     ShapeManager.Instance.CombineShapes(ShapeType.Square, ShapeType.Triangle);

        // Debug.Log(shape);
        // Debug.Log(shape2);
        // Debug.Log(shape == shape2);
        // foreach (ShapeType s in ShapeManager.Instance.TakeApartShape(shape))
        // {
        //     Debug.Log(s);
        // }

        // ShapeManager.Instance.CreateShape(ShapeType.Square, Vector3.one);
        // ShapeManager.Instance.CreateShape(ShapeType.Square, Vector3.left * 2);

        // Debug.Log(1 << 31);

        ShapeManager.Instance.CreateShape(ShapeType.Square, new Vector2(3, 0));
        ShapeManager.Instance.CreateShape(ShapeType.Square, new Vector2(-3, 0));
        ShapeManager.Instance.CreateShape(ShapeType.Triangle, new Vector2(3, 2.4f));
        ShapeManager.Instance.CreateShape(ShapeType.Triangle, new Vector2(-3, 2.4f));
        ShapeManager.Instance.CreateShape(ShapeType.Circle, new Vector2(3, -2.4f));
        ShapeManager.Instance.CreateShape(ShapeType.Circle, new Vector2(-3, -2.4f));
    }
}
