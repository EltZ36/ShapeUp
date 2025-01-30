using System;
using UnityEngine;

public enum ShapeType
{
    Triangle,
    Square,
    Circle,
    Cylinder,
}

/// <summary>
/// BitFlags to represent what tags are currently active. Utilize bitwise operations to compare and evaluate.
/// </summary>
[Flags]
public enum ShapeTags
{
    None = 0,
    Physics = 1 << 0,
    Drag = 1 << 1,
    Zoom = 1 << 2,
    Slice = 1 << 3,
    Accelerate = 1 << 4,
    Gyroscope = 1 << 5,
}

[Serializable]
public class ShapeInfo
{
    public ShapeType Shape;
    public GameObject Prefab;

    [HideInInspector]
    public ShapeTags Tags;

    public ShapeInfo(ShapeType type, GameObject prefab)
    {
        Shape = type;
        Prefab = prefab;
    }
}
