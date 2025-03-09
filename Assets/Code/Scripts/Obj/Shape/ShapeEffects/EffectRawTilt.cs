using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRawTilt : MonoBehaviour
{
    Vector3 tilt;
    private bool cutsceneEnd = false;
    public float startTime = 2.0f;

    void Start()
    {
        StartCoroutine(Delay());
    }

    public void ChangeTilt(EventInfo eventInfo)
    {
        if (cutsceneEnd)
        {
            tilt = eventInfo.VectorOne;
            float rotationAmount = tilt.x * -90.0f;
            if (tilt.y > 0.0f)
            {
                rotationAmount = 180.0f - rotationAmount;
            }
            Debug.Log(tilt.x + " " + tilt.y);
            transform.rotation = Quaternion.Euler(0, 0, -rotationAmount);
        }
    }

    IEnumerator Delay()
    {
        float elapsed = 0.0f;
        while (elapsed / startTime < 1)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        cutsceneEnd = true;
        yield return null;
    }
}
