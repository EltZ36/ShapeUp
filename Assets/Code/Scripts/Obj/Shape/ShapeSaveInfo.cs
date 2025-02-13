using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TransformInfo
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public TransformInfo(Transform transform)
    {
        position = transform.position;
        rotation = transform.rotation;
        scale = transform.localScale;
    }
}

public class ShapeSaveInfo
{
    public ShapeType shapeType;
    public ShapeTags shapeTags;
    public TransformInfo transformInfo;

    public ShapeSaveInfo(ShapeType shape, ShapeTags tags, Transform transform)
    {
        shapeType = shape;
        shapeTags = tags;
        transformInfo = new TransformInfo(transform);
    }
}
