using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventInfo
{
    public GameObject TargetObject;
    public float FloatValue;
    public int IntValue;
    public Vector3 VectorOne;
    public Vector3 VectorTwo;
    public Quaternion QuaternionValue;

    public EventInfo(
        GameObject targetObject = null,
        float floatValue = 0f,
        int intValue = 0,
        Vector3 vectorOne = default,
        Vector3 vectorTwo = default,
        Quaternion quaternionValue = default
    )
    {
        TargetObject = targetObject;
        FloatValue = floatValue;
        IntValue = intValue;
        VectorOne = vectorOne;
        VectorTwo = vectorTwo;
        QuaternionValue = quaternionValue;
    }
}
