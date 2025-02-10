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
    List<ShapeType> winShapes = GetFilteredEnumValues<ShapeType>(shape =>
        shape.ToString().Contains("Person") && shape.ToString().Contains("Complete")
    );

    //from gpt at this link: https://chatgpt.com/share/67a99313-e274-800c-b5f0-2cd8c688fc2a for the enum helper function
    private static List<T> GetFilteredEnumValues<T>(Func<T, bool> predicate)
    {
        return Enum.GetValues(typeof(T)).Cast<T>().Where(predicate).ToList();
    }

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
