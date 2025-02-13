using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevelCombine : MonoBehaviour
{
    // Start is called before the first frame update
    TapBehavior tapBehavior;
    DragBehavior dragBehavior;

    private GameObject dragObject;
    private GameObject tapObject;

    void Start() { }

    void resetShape()
    {
        if (tapBehavior.Tap(tapObject) == true)
        {
            if (
                ShapeManager.Instance.TakeApartShape(
                    dragObject.GetComponent<Shape>().LocalShapeInfo.Shape
                ) != null
            )
            {
                Debug.Log("Shape Taken Apart");
                ShapeManager.Instance.TakeApartShape(
                    dragObject.GetComponent<Shape>().LocalShapeInfo.Shape
                );
            }
        }
    }

    /*if (dragBehavior.getDragging() == true)
    {
        if (ShapeManager.Instance.CheckShapeCollide(gameObject) == true)
        {
            Debug.Log("Shape Dragged and trying to combine");
            ShapeManager.Instance.CombineShapes(gameObject, gameObject);
        }
    }*
}

/* void ComboShape()
 {
     if (
         dragBehavior.getDragging() == true
         && ShapeManager.Instance.CheckShapeCollide(gameObject) == true
     )
     {
         Debug.Log("Shape Dragged and trying to combine");
         ShapeManager.Instance.CombineShapes(gameObject, gameObject);
     }
 } */

    // Update is called once per frame
    void Update()
    {
        resetShape();
    }
}
