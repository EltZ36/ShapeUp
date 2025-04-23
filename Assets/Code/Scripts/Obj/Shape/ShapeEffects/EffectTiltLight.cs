using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTiltLight : MonoBehaviour
{
    [SerializeField]
    float radius = 3f;

    // [SerializeField]
    // float manualRotation = 0f;

    [SerializeField]
    float initialRotation = Mathf.PI / 2;
    private float initialTilt = 0f;
    float tilt;

    void Start()
    {
        initialTilt = Input.gyro.attitude.eulerAngles.z;
    }

    public void ChangeTilt(EventInfo eventInfo)
    {
        tilt = eventInfo.QuaternionValue.eulerAngles.z - initialTilt;
        tilt = tilt * Mathf.Deg2Rad;
        // Debug.Log(tilt.x + "," + tilt.y);
        transform.position = new Vector3(
            radius * Mathf.Cos(initialRotation + tilt) * 2f,
            (radius * Mathf.Sin(initialRotation + tilt) * 1.5f) - 2f,
            0f
        );
    }

    public void showAttitude(EventInfo eventInfo)
    {
        // Debug.Log(eventInfo.QuaternionValue.eulerAngles);
    }

    // void Update()
    // {
    //     transform.position = new Vector3(
    //         radius * Mathf.Cos(initialRotation + manualRotation) * 2f,
    //         (radius * Mathf.Sin(initialRotation + manualRotation) * 1.5f) - 2f,
    //         0f
    //     );
    // }
}
