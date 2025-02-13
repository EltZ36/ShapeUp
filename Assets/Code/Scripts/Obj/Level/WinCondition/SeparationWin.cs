using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SeparationWin : MonoBehaviour
{
    [SerializeField]
    ShapeType winShape;

    void Awake()
    {
        ShapeManager.Instance.OnDestroyShape += CheckWinShape;
    }

    void OnDestroy()
    {
        ShapeManager.Instance.OnDestroyShape -= CheckWinShape;
    }

    void CheckWinShape(Shape destroyed)
    {
        if (destroyed.LocalShapeInfo.Shape == winShape)
        {
            LevelManager.Instance.OnCurrentSubLevelComplete();
            StartCoroutine(CameraController.ZoomOut());
        }
    }
}
