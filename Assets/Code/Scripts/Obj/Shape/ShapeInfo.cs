using System;
using UnityEngine;

[Flags]
public enum ShapeTags
{
    None = 0,
    OnFixedUpdate = 1 << 0,
    OnDrag = 1 << 1,
    OnPinch = 1 << 2,
    OnSlice = 1 << 3,
    OnTap = 1 << 4,
    OnAccelerate = 1 << 5,
    OnAttitudeChange = 1 << 6,
    OnGravityChange = 1 << 7,
    OnCreate = 1 << 8,
    OnDestroy = 1 << 9,
}
