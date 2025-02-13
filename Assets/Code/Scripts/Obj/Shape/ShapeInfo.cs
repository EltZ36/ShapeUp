using System;
using UnityEngine;

public enum ShapeType
{
    Triangle,
    Square,
    Rectangle,
    Circle,
    Oval,
    Pyramid3D,
    Cube3D,
    Rectangle3D,
    Oval3D,
    Sphere3D,
    Cone3D,
    BreakableCircle,
    ConePerson,
    CubePerson,
    CompleteCubePerson,
    CompleteConePerson,
}

/// <summary>
/// BitFlags to represent what tags are currently active. Utilize bitwise operations to compare and evaluate.
/// </summary>
[Flags]
public enum ShapeTags
{
    UseDatabaseDefault = 1 << 31, //max shift for a 32 bit int
    None = 0,
    Gravity = 1 << 0,
    Drag = 1 << 1,
    Zoom = 1 << 2,
    Slice = 1 << 3,
    Accelerate = 1 << 4,
    GyroscopeF = 1 << 5,
    GyroscopeR = 1 << 6,
    Storable = 1 << 7, //can be placed in the inventory
    ShakeBreak = 1 << 8,
    FreezeX = 1 << 9,
    FreezeY = 1 << 10,
    FreezeRotation = 1 << 11,
    Frictionless = 1 << 12,
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
