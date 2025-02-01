using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeManager : MonoBehaviour, IShapeManager
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
    [SerializeField]
    private ShapeDatabase shapeDatabase;

    [SerializeField]
    private ShapeRecipes shapeRecipes;

    private List<(Shape, Shape)> checkShapes = new List<(Shape, Shape)>();

    //event that tells subscriber a new shape was created
    public event Action<Shape> OnCreateShape;
    public event Action<Shape> OnDestroyShape;

    #region Interface Methods

    public GameObject CreateShape(ShapeType shapeType, Vector3 position)
    {
        GameObject shapePrefab = shapeDatabase.GetPrefab(shapeType);
        if (shapePrefab != null)
        {
            GameObject shapeObj = Instantiate(shapePrefab, position, Quaternion.identity);
            Shape shape = shapeObj.GetComponent<Shape>();
            shape.SetShapeInfo(new ShapeInfo(shapeType, shapePrefab));
            OnCreateShape?.Invoke(shape);
            return shapeObj;
        }
        return shapePrefab;
    }

    public void DestroyShape(Shape shape)
    {
        OnDestroyShape?.Invoke(shape);
        Destroy(shape.gameObject);
    }

    public ShapeType? CombineShapes(ShapeType shapeA, ShapeType shapeB)
    {
        HashSet<ShapeType> pair = new HashSet<ShapeType> { shapeA, shapeB };
        if (shapeRecipes.ShapeRecipesDict.TryGetValue(pair, out ShapeType shape))
        {
            return shape;
        }
        return null;
    }

    public HashSet<ShapeType> TakeApartShape(ShapeType shape)
    {
        if (shapeRecipes.ShapeRecipesDictInverse.TryGetValue(shape, out HashSet<ShapeType> shapes))
        {
            return shapes;
        }
        return null;
    }

    public void CheckShapeCollide((Shape, Shape) pair)
    {
        checkShapes.Add(pair);
    }
    #endregion

    #region Monobehavior
    public void LateUpdate()
    {
        if (checkShapes.Count >= 1)
        {
            HashSet<Shape> used = new HashSet<Shape>();
            foreach ((Shape, Shape) pair in checkShapes)
            {
                if (used.Contains(pair.Item1) || used.Contains(pair.Item2))
                {
                    continue;
                }
                else
                {
                    ShapeType? combined = CombineShapes(
                        pair.Item1.ShapeInfo.Shape,
                        pair.Item2.ShapeInfo.Shape
                    );
                    if (combined.HasValue)
                    {
                        Vector3 pointA = pair.Item1.gameObject.transform.position;
                        Vector3 pointB = pair.Item2.gameObject.transform.position;
                        Vector3 midpoint = (pointA + pointB) / 2;
                        CreateShape((ShapeType)combined, midpoint);
                        used.Add(pair.Item1);
                        used.Add(pair.Item2);
                        DestroyShape(pair.Item1);
                        DestroyShape(pair.Item2);
                    }
                }
            }
        }
        checkShapes.Clear();
    }
    #endregion
}
