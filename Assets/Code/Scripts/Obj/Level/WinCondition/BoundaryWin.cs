using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class BoundaryWin : MonoBehaviour
{
    [SerializeField]
    private ShapeType ST;
    LevelUI LevelUI;

    void OnTriggerExit2D(Collider2D collision)
    {
        Shape shape = collision.gameObject.GetComponent<Shape>();
        if (shape.LocalShapeInfo.Shape == ST)
        {
            // maybe turn this into its function  -> cameracontroller.zoomoutcoroutine() ?
            StartCoroutine(CameraController.ZoomOut());
            // end
            ShapeManager.Instance.DestroyShape(shape);
        }
    }
}
