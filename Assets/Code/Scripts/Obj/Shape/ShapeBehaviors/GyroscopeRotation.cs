using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GyroscopeRotation : MonoBehaviour
{
    private Rigidbody2D rb;

    // https://chatgpt.com/share/67a94ce1-77b8-8010-9cdb-ab556c8017c7
    public float targetRotation;
    public float torqueAmount = 10f;
    public float rotationDamping = 5f;

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
        targetRotation = rotation;
    }

    void FixedUpdate()
    {
        float currentRotation = rb.rotation;
        float rotationDifference = Mathf.DeltaAngle(currentRotation, targetRotation);

        float torque = rotationDifference * torqueAmount - rb.angularVelocity * rotationDamping;
        rb.AddTorque(torque);
    }
}
