using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class ComboWin : MonoBehaviour
{
    [SerializeField]
    ShapeType winShape;

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
        if (created.LocalShapeInfo.Shape == winShape)
        {
            LevelManager.Instance.OnCurrentSubLevelComplete();
            StartCoroutine(CameraController.ZoomOut());
        }
    }
}
