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

    private Dictionary<ShapeType, GameObject> shapeInfoDict;

    private void OnEnable()
    {
        if (shapeInfoDict == null && shapeInfoList != null)
        {
            shapeInfoDict = new Dictionary<ShapeType, GameObject>();
            foreach (var shapeInfo in shapeInfoList)
            {
                if (!shapeInfoDict.ContainsKey(shapeInfo.Shape))
                {
                    shapeInfoDict.Add(shapeInfo.Shape, shapeInfo.Prefab);
                }
            }
        }
    }

    public GameObject GetPrefab(ShapeType shape)
    {
        if (shapeInfoDict.TryGetValue(shape, out GameObject prefab))
        {
            return prefab;
        }
        return null;
    }
}
