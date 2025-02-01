using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public ShapeInfo ShapeInfo { get; private set; }

    [SerializeField]
    private Rigidbody2D rb;

    public void SetShapeInfo(ShapeInfo shapeInfo)
    {
        ShapeInfo = shapeInfo;
    }

    public void SetShapeTags(ShapeTags tags)
    {
        if (ShapeInfo != null)
        {
            ShapeInfo.Tags = tags;
        }
    }

    public void ToggleShapeTags(ShapeTags tags)
    {
        if (ShapeInfo != null)
        {
            ShapeInfo.Tags ^= tags;
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
