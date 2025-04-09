using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTiltLight : MonoBehaviour
{
    [SerializeField]
    float radius = 3f;

    [SerializeField]
    float manualRotation = 0f;

    Vector3 tilt;

    public void ChangeTilt(EventInfo eventInfo)
    {
        tilt = eventInfo.VectorOne;
        float rotation = tilt.x * 2f * Mathf.PI;
        transform.position = new Vector3(
            radius * Mathf.Cos(rotation),
            radius * Mathf.Sin(rotation),
            0f
        );
    }

    void Update()
    {
        transform.position = new Vector3(
            radius * Mathf.Cos(manualRotation) * 2f,
            radius * Mathf.Sin(manualRotation),
            0f
        );
    }
}
