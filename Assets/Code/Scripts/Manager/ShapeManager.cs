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

    public GameObject CreateShape(ShapeType shapeType, Vector3 position)
    {
        GameObject shapePrefab = shapeDatabase.GetPrefab(shapeType);
        if (shapePrefab != null)
        {
            GameObject shape = Instantiate(shapePrefab, position, Quaternion.identity);
            return shape;
        }
        return shapePrefab;
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
}
