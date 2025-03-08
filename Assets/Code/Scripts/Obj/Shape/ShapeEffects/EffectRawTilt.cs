using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRawTilt : MonoBehaviour
{
    Vector3 tilt;

    public void ChangeTilt(EventInfo eventInfo)
    {
        tilt = eventInfo.VectorOne;
        float rotationAmount = tilt.x * 180;
        transform.rotation = Quaternion.Euler(0, 0, -rotationAmount);
        // Debug.Log(tilt);
    }
}
