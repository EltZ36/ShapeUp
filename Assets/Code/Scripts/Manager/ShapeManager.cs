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
    private List<Shape> blacklistShapes = new List<Shape>();

    public void CreateShapeEvent(Shape shape)
    {
        OnCreateShape?.Invoke(shape);
    }

    public void DestroyShapeEvent(Shape shape)
    {
        OnDestroyShape?.Invoke(shape);
    }

    public void TakeApartShape(Shape shape)
    {
        if (shape.gameObject != null)
        {
            (string, string) pair = SeparateShapes(shape.ShapeName);
            if (pair.Item1 != null)
            {
                Blacklist(
                    Instantiate(
                            shapeDatabase.ShapeDict[pair.Item1],
                            (Vector2)shape.gameObject.transform.position
                                + UnityEngine.Random.insideUnitCircle,
                            Quaternion.identity
                        )
                        .GetComponent<Shape>(),
                    1f
                );
                Blacklist(
                    Instantiate(
                            shapeDatabase.ShapeDict[pair.Item2],
                            (Vector2)shape.gameObject.transform.position
                                + UnityEngine.Random.insideUnitCircle,
                            Quaternion.identity
                        )
                        .GetComponent<Shape>(),
                    1f
                );
            }
        }
    }

    public void Blacklist(Shape shape, float time)
    {
        shape.gameObject.SetActive(true);
        blacklistShapes.Add(shape);
        StartCoroutine(RemoveFromBlacklist(shape, time));
    }

    IEnumerator RemoveFromBlacklist(Shape shape, float time)
    {
        yield return new WaitForSeconds(time);
        blacklistShapes.Remove(shape);
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

    public (string, string) SeparateShapes(string shape)
    {
        if (
            shapeRecipes != null
            && shapeRecipes.SeparateRecipes.TryGetValue(shape, out (string, string) pair)
        )
        {
            return pair;
        }
        return (null, null);
    }

    public void LateUpdate()
    {
        if (checkShapes.Count < 1 || shapeRecipes == null)
        {
            return;
        }
        HashSet<Shape> used = new HashSet<Shape>();
        foreach (Shape shape in blacklistShapes)
        {
            used.Add(shape);
        }
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
                if (shapeA == null || shapeB == null)
                {
                    continue;
                }
                string combined = CombineShapes(shapeA.ShapeName, shapeB.ShapeName);
                if (!string.IsNullOrEmpty(combined))
                {
                    Vector3 pointA = shapeA.gameObject.transform.position;
                    Vector3 pointB = shapeB.gameObject.transform.position;
                    Vector3 midpoint = (pointA + pointB) / 2;

                    Instantiate(shapeDatabase.ShapeDict[combined], midpoint, Quaternion.identity)
                        .SetActive(true);
                    used.Add(shapeA);
                    used.Add(shapeB);
                    shapeA.Combined();
                    shapeB.Combined();
                    Destroy(shapeA.gameObject);
                    Destroy(shapeB.gameObject);
                }
            }
        }
        checkShapes.Clear();
    }

    public static bool ContainsSet(string[] subset, string[] superset)
    {
        Dictionary<string, int> supersetCount = new Dictionary<string, int>();

        foreach (string name in superset)
        {
            if (supersetCount.ContainsKey(name))
                supersetCount[name]++;
            else
                supersetCount[name] = 1;
        }

        foreach (string name in subset)
        {
            if (!supersetCount.ContainsKey(name))
            {
                continue;
            }

            supersetCount[name]--;
        }
        foreach (int val in supersetCount.Values)
        {
            if (val > 0)
            {
                return false;
            }
        }

        return true;
    }
}
