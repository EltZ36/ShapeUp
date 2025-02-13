using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class ComboWin : MonoBehaviour
{
    [SerializeField]
    List<ShapeType> winShapes;
    LevelUI LevelUI;

    int winCount = 0;

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
        // something something remove shape from win list

        if (winShapes.Contains(created.LocalShapeInfo.Shape))
        {
            winCount++;
            if (winCount == 2)
            {
                LevelUI.Instance.VictoryScreen();
            }
        }
    }

    // TODO: reset win method
    // method to reset the list of shapes to win, used when shake (or reset) occurs
    // fn resetWinCond()
}
