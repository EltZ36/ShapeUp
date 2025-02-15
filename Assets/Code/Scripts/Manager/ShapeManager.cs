using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    #region Singleton Pattern
    public static ShapeManager _instance;
    public static ShapeManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion

    [HideInInspector]
    public ShapeDatabase shapeDatabase;

    [HideInInspector]
    public ShapeRecipes shapeRecipes;

    public event Action<Shape> OnCreateShape;
    public event Action<Shape> OnDestroyShape;

    private List<(Shape, Shape)> checkShapes = new List<(Shape, Shape)>();

    public void CreateShapeEvent(Shape shape)
    {
        OnCreateShape?.Invoke(shape);
    }

    public void DestroyShapeEvent(Shape shape)
    {
        OnDestroyShape?.Invoke(shape);
    }

    public void CheckShapeCollide((Shape, Shape) pair)
    {
        checkShapes.Add(pair);
    }

    public string CombineShapes(string shapeA, string shapeB)
    {
        (string, string) pair = (shapeA, shapeB);
        if (shapeRecipes.CombineRecipes.TryGetValue(pair, out string name))
        {
            return name;
        }
        return null;
    }

    public void LateUpdate()
    {
        if (checkShapes.Count < 1)
        {
            return;
        }
        HashSet<Shape> used = new HashSet<Shape>();
        foreach ((Shape, Shape) pair in checkShapes)
        {
            Shape shapeA = pair.Item1;
            Shape shapeB = pair.Item2;
            if (used.Contains(shapeA) || used.Contains(shapeB))
            {
                continue;
            }
            else
            {
                if (shapeA.Prefab == null || shapeB.Prefab == null)
                {
                    continue;
                }
                string combined = CombineShapes(shapeA.Prefab.name, shapeB.Prefab.name);
                if (!string.IsNullOrEmpty(combined))
                {
                    Vector3 pointA = shapeA.gameObject.transform.position;
                    Vector3 pointB = shapeB.gameObject.transform.position;
                    Vector3 midpoint = (pointA + pointB) / 2;

                    Instantiate(shapeDatabase.ShapeDict[combined], midpoint, Quaternion.identity);
                    used.Add(shapeA);
                    used.Add(shapeB);
                    Destroy(shapeA.gameObject);
                    Destroy(shapeB.gameObject);
                }
            }
        }
        checkShapes.Clear();
    }
}
