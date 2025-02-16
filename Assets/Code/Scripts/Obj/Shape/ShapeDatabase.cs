using System;
using System.Collections.Generic;
using UnityEngine;

public class ShapeDatabase : MonoBehaviour
{
    [SerializeField]
    private List<string> shapeNames = new List<string>();

    [SerializeField]
    private List<GameObject> prefabs = new List<GameObject>();

    public List<string> ShapeNames
    {
        get { return shapeNames; }
    }

    public Dictionary<string, GameObject> ShapeDict { get; private set; } =
        new Dictionary<string, GameObject>();

    void Awake()
    {
        ShapeDict.Clear();
        for (int i = 0; i < shapeNames.Count; i++)
        {
            if (!string.IsNullOrEmpty(shapeNames[i]) && !ShapeDict.ContainsKey(shapeNames[i]))
            {
                ShapeDict[shapeNames[i]] = prefabs[i];
            }
        }
    }

    void Start()
    {
        ShapeManager.Instance.shapeDatabase = this;
    }
}
