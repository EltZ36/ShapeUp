using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTilt : MonoBehaviour
{
    [SerializeField]
    float torqueAmount = 10f;

    [SerializeField]
    float rotationDamping = 5f;

    Vector3 tilt;
    Quaternion targetRotation;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ChangeTilt(EventInfo eventInfo)
    {
        tilt = eventInfo.VectorOne;
        float rotation = tilt.x * 180;
        targetRotation = Quaternion.Euler(0, 0, -rotation);
        // Debug.Log(tilt);
    }

    void FixedUpdate()
    {
        float currentRotation = rb.rotation;
        float rotationDifference = Mathf.DeltaAngle(currentRotation, targetRotation.eulerAngles.z);

        float torque = rotationDifference * torqueAmount - rb.angularVelocity * rotationDamping;
        rb.AddTorque(torque);

        if (Mathf.Abs(rotationDifference) < 1)
        {
            rb.totalTorque = 0;
        }
    }
}
