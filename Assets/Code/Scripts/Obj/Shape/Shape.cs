using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public ShapeInfo LocalShapeInfo;

    [SerializeField]
    private Rigidbody2D rb;

    public void SetShapeInfo(ShapeInfo shapeInfo)
    {
        LocalShapeInfo = shapeInfo;
    }

    public void SetShapeTags(ShapeTags tags)
    {
        if (LocalShapeInfo != null)
        {
            LocalShapeInfo.Tags = tags;
        }
    }

    public void ToggleShapeTags(ShapeTags tags)
    {
        if (LocalShapeInfo != null)
        {
            LocalShapeInfo.Tags ^= tags;
        }
    }

    void Start()
    {
        if (LocalShapeInfo.Prefab == null)
        {
            Destroy(gameObject);
            GameObject shape = ShapeManager.Instance.CreateShape(
                LocalShapeInfo.Shape,
                transform.position,
                LocalShapeInfo.Tags
            );
            shape.transform.rotation = transform.rotation;
            shape.transform.parent = transform.parent;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Shape shape = collision.gameObject.GetComponent<Shape>();
        if (shape != null)
        {
            ShapeManager.Instance.CheckShapeCollide((shape, this));
        }
    }
}
