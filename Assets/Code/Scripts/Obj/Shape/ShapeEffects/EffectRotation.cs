using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRotation : MonoBehaviour
{
    private Rigidbody2D rb;

    // https://chatgpt.com/share/67a94ce1-77b8-8010-9cdb-ab556c8017c7
    private float targetRotation;
    public float torqueAmount = 10f;
    public float rotationDamping = 5f;

    private float offset
    {
        get
        {
            if (Input.deviceOrientation == DeviceOrientation.FaceUp)
            {
                return 180;
            }
            else if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
            {
                return 270;
            }
            return 0;
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetRotation(EventInfo eventInfo)
    {
        float rotation = (eventInfo.QuaternionValue.eulerAngles.z - offset) % 360;
        if (rotation < 0)
        {
            rotation += 360;
        }
        if (rotation < 90 || rotation > 270)
        {
            targetRotation = rotation;
        }
        else
        {
            targetRotation = (rotation - 180) % 360;
            if (targetRotation < 0)
            {
                targetRotation += 360;
            }
        }
    }

    void FixedUpdate()
    {
        float currentRotation = rb.rotation % 360;
        if (currentRotation < 0)
        {
            currentRotation += 360;
        }

        float rotationDifference = Mathf.DeltaAngle(currentRotation, targetRotation);

        float torque = rotationDifference * torqueAmount - rb.angularVelocity * rotationDamping;
        rb.AddTorque(torque);
        rb.totalTorque = Mathf.Clamp(rb.totalTorque, -100, 100);
        if (Mathf.Abs(rotationDifference) < 1)
        {
            rb.totalTorque = 0;
        }
    }
}
