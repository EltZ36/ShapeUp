using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "ShapeDatabaseScriptableObject",
    menuName = "Shapes/ShapeDatabaseScriptableObject"
)]
public class ShapeDatabase : ScriptableObject
{
    [SerializeField]
    private List<ShapeInfo> shapeInfoList;

    private Dictionary<ShapeType, ShapeInfo> shapeInfoDict;

    private void OnEnable()
    {
        if (shapeInfoDict == null && shapeInfoList != null)
        {
            shapeInfoDict = new Dictionary<ShapeType, ShapeInfo>();
            foreach (var shapeInfo in shapeInfoList)
            {
                if (!shapeInfoDict.ContainsKey(shapeInfo.Shape))
                {
                    shapeInfoDict.Add(shapeInfo.Shape, shapeInfo);
                }
            }
        }
    }

    public ShapeInfo GetShapeInfo(ShapeType shape)
    {
        if (shapeInfoDict.TryGetValue(shape, out ShapeInfo info))
        {
            return info;
        }
        return null;
    }
}
