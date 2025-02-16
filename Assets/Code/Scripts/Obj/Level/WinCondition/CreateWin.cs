using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateWin : MonoBehaviour
{
    [SerializeField]
    Shape[] shapes;

    string[] names;

    List<string> active = new List<string>();

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
        CheckActive();
    }

    void RemoveShape(Shape shape)
    {
        active.Remove(shape.ShapeName);
    }

    void CheckActive()
    {
        if (ShapeManager.ContainsSet(active.ToArray(), names))
        {
            Debug.Log("Victory");
            LevelManager.Instance.OnCurrentSubLevelComplete();
            StartCoroutine(CameraController.ZoomOut());
        }
    }
}
