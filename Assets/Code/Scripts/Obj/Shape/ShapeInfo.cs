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
    UseDatabaseDefault = 1 << 31,
    None = 0,
    Gravity = 1 << 0,
    Drag = 1 << 1,
    Zoom = 1 << 2,
    Slice = 1 << 3,
    Accelerate = 1 << 4,
    Gyroscope = 1 << 5,
    Storable = 1 << 6, //can be placed in the inventory
}

[Serializable]
public class ShapeInfo
{
    public ShapeType Shape;
    public GameObject Prefab;
    public ShapeTags Tags;

    public ShapeInfo(ShapeType type, GameObject prefab)
    {
        Shape = type;
        Prefab = prefab;
    }
}
