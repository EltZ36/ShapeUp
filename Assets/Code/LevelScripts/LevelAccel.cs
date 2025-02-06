using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAccel : MonoBehaviour
{
    public Shape shakeBreakShape = null;

    int shakes = 0;

    void Awake()
    {
        ShapeManager.Instance.OnCreateShape += SetBreakShape;
        SensorManager.Instance.OnAccelChange += ShakeBreak;
    }

    void SetBreakShape(Shape shape)
    {
        if (shakeBreakShape == null) //probably better way to do this
        {
            shakeBreakShape = shape;
        }
    }

    void ShakeBreak()
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
