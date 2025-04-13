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

    float tilt;

    public void ChangeTilt(EventInfo eventInfo)
    {
        tilt = eventInfo.QuaternionValue.eulerAngles.z;
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
        Debug.Log(eventInfo.QuaternionValue.eulerAngles.z);
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
