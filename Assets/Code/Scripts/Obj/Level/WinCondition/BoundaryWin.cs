using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryWin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Shape shapeTarget;

    string targetName;

    void Awake()
    {
        targetName = shapeTarget.ShapeName;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Shape shape = collision.gameObject.GetComponent<Shape>();
        if (shape.ShapeName == targetName)
        {
            StartCoroutine(CameraController.ZoomOut());
            Destroy(shape.gameObject);
            LevelManager.Instance.OnCurrentSubLevelComplete();
        }
    }
}
