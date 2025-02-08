using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeRotation : MonoBehaviour
{
    private Rigidbody2D rb;
    private Quaternion dir = Quaternion.identity;
    bool check = false;
    private Quaternion offset;

    void Awake()
    {
        SensorManager.Instance.OnGyroChange += RotateShape;
        rb = GetComponent<Rigidbody2D>();
    }

    void OnDestroy()
    {
        SensorManager.Instance.OnGyroChange -= RotateShape;
    }

    void RotateShape(float rotation)
    {
        if (check == false)
        {
            check = true;
            offset = Quaternion.Euler(0, 0, rotation);
        }
        dir = Quaternion.Euler(0, 0, rotation);
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Inverse(offset) * dir;
    }
}
