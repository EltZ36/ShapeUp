using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class ComboWin : MonoBehaviour
{
    [SerializeField]
    List<ShapeType> winShapes;

    void Awake()
    {
        ShapeManager.Instance.OnCreateShape += CheckWinShape;
    }

    void OnDestroy()
    {
        ShapeManager.Instance.OnCreateShape -= CheckWinShape;
    }

    void CheckWinShape(Shape created)
    {
        if (winShapes.Contains(created.LocalShapeInfo.Shape))
        {
            LevelManager.Instance.OnCurrentSubLevelComplete();
            StartCoroutine(CameraController.ZoomOut());
        }
    }
}
