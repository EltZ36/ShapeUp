using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class FinalWin : MonoBehaviour
{
    [SerializeField]
    List<ShapeType> winShapes;

    int AmountNeededForWin = 2;
    int AmountCombinedCounter = 0;

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
        //win condition would be the cone person and the cube person both having hats
        if (winShapes.Contains(created.LocalShapeInfo.Shape))
        {
            AmountCombinedCounter++;
        }
        if (AmountCombinedCounter == AmountNeededForWin)
        {
            LevelManager.Instance.OnCurrentSubLevelComplete();
            StartCoroutine(CameraController.ZoomOut());
        }
    }
}
