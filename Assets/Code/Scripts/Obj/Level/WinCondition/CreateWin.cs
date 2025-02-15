using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateWin : MonoBehaviour
{
    [SerializeField]
    string[] shapes;

    List<string> active = new List<string>();

    void Awake()
    {
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
        Debug.Log("shape.ShapeName");
        CheckActive();
    }

    void RemoveShape(Shape shape)
    {
        active.Remove(shape.ShapeName);
    }

    void CheckActive()
    {
        if (ShapeManager.ContainsSet(active.ToArray(), shapes))
        {
            Debug.Log("Victory");
            LevelManager.Instance.OnCurrentSubLevelComplete();
            StartCoroutine(CameraController.ZoomOut());
        }
    }
}
