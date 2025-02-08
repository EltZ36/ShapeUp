using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBreak : MonoBehaviour
{
    int shakes = 0;
    Shape shakeBreakShape;

    void Awake()
    {
        SensorManager.Instance.OnAccelChange += Break;
        shakeBreakShape = GetComponent<Shape>();
    }

    void OnDestroy()
    {
        SensorManager.Instance.OnAccelChange -= Break;
    }

    void Break()
    {
        shakes += 1;
        if (shakes == 3)
        {
            var newShapes = ShapeManager.Instance.TakeApartShape(
                shakeBreakShape.LocalShapeInfo.Shape
            );
            foreach (ShapeType shape in newShapes)
            {
                ShapeManager.Instance.Blacklist(
                    ShapeManager
                        .Instance.CreateShape(
                            shape,
                            (Vector2)shakeBreakShape.gameObject.transform.position
                                + Random.insideUnitCircle
                        )
                        .GetComponent<Shape>(),
                    1
                );
                if (newShapes.Count == 1)
                {
                    ShapeManager.Instance.Blacklist(
                        ShapeManager
                            .Instance.CreateShape(
                                shape,
                                (Vector2)shakeBreakShape.gameObject.transform.position
                                    + Random.insideUnitCircle
                            )
                            .GetComponent<Shape>(),
                        1
                    );
                }
            }
            ShapeManager.Instance.DestroyShape(shakeBreakShape);
        }
    }
}
