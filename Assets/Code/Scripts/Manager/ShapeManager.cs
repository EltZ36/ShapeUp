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
    private List<Shape> blacklistShapes = new List<Shape>();

    //event that tells subscriber a new shape was created
    public event Action<Shape> OnCreateShape;
    public event Action<Shape> OnDestroyShape;

    #region Interface Methods

    public GameObject CreateShape(
        ShapeType shapeType,
        Vector3 position,
        Quaternion rotation = default,
        Vector3 scale = default,
        ShapeTags tags = ShapeTags.UseDatabaseDefault
    )
    {
        ShapeInfo shapeInfo = shapeDatabase.GetShapeInfo(shapeType);
        if (shapeInfo != null)
        {
            if (rotation == default)
            {
                rotation = Quaternion.identity;
            }
            if (scale == default)
            {
                scale = Vector3.one;
            }
            GameObject shapeObj = Instantiate(shapeInfo.Prefab, position, rotation);

            Rigidbody2D shapeRB = shapeObj.GetComponent<Rigidbody2D>();
            shapeObj.transform.localScale = scale;
            Shape shape = shapeObj.GetComponent<Shape>();
            shape.SetShapeInfo(new ShapeInfo(shapeType, shapeInfo.Prefab));
            if (tags == ShapeTags.UseDatabaseDefault)
            {
                shape.SetShapeTags(shapeInfo.Tags);
            }
            else
            {
                shape.SetShapeTags(tags);
            }
            if ((shape.LocalShapeInfo.Tags & ShapeTags.Gravity) != ShapeTags.Gravity)
            {
                shapeRB.gravityScale = 0;
            }
            if ((shape.LocalShapeInfo.Tags & ShapeTags.FreezeX) == ShapeTags.FreezeX)
            {
                shapeRB.constraints ^= RigidbodyConstraints2D.FreezePositionX;
            }
            if ((shape.LocalShapeInfo.Tags & ShapeTags.FreezeY) == ShapeTags.FreezeY)
            {
                shapeRB.constraints ^= RigidbodyConstraints2D.FreezePositionY;
            }
            if ((shape.LocalShapeInfo.Tags & ShapeTags.FreezeRotation) == ShapeTags.FreezeRotation)
            {
                shapeRB.constraints ^= RigidbodyConstraints2D.FreezeRotation;
            }
            //add components here
            if ((shape.LocalShapeInfo.Tags & ShapeTags.ShakeBreak) == ShapeTags.ShakeBreak)
            {
                shapeObj.AddComponent<ShakeBreak>();
            }
            if ((shape.LocalShapeInfo.Tags & ShapeTags.GyroscopeF) == ShapeTags.GyroscopeF)
            {
                shapeObj.AddComponent<GyroscopeForce>();
            }
            if ((shape.LocalShapeInfo.Tags & ShapeTags.GyroscopeR) == ShapeTags.GyroscopeR)
            {
                shapeObj.AddComponent<GyroscopeRotation>();
            }
            if ((shape.LocalShapeInfo.Tags & ShapeTags.Zoom) == ShapeTags.Zoom)
            {
                shapeObj.AddComponent<Zoom>();
            }
            if ((shape.LocalShapeInfo.Tags & ShapeTags.Frictionless) == ShapeTags.Frictionless)
            {
                PhysicsMaterial2D frictionless = new PhysicsMaterial2D();
                frictionless.friction = 0f;
                shapeObj.GetComponent<Collider2D>().sharedMaterial = frictionless;
            }
            OnCreateShape?.Invoke(shape);
            shapeObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
            return shapeObj;
        }
        return null;
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

    public void Blacklist(Shape shape, float time)
    {
        blacklistShapes.Add(shape);
        StartCoroutine(RemoveFromBlacklist(shape, time));
    }
    #endregion

    IEnumerator RemoveFromBlacklist(Shape shape, float time)
    {
        yield return new WaitForSeconds(time);
        blacklistShapes.Remove(shape);
    }

    #region Monobehavior
    public void LateUpdate()
    {
        if (checkShapes.Count >= 1)
        {
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
                    ShapeType? combined = CombineShapes(
                        shapeA.LocalShapeInfo.Shape,
                        shapeB.LocalShapeInfo.Shape
                    );
                    if (combined.HasValue)
                    {
                        Vector3 pointA = shapeA.gameObject.transform.position;
                        Vector3 pointB = shapeB.gameObject.transform.position;
                        Vector3 midpoint = (pointA + pointB) / 2;
                        CreateShape((ShapeType)combined, midpoint);
                        used.Add(shapeA);
                        used.Add(shapeB);
                        DestroyShape(shapeA);
                        DestroyShape(shapeB);
                    }
                }
            }
        }
        checkShapes.Clear();
    }
    #endregion
}
