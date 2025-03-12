using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRawTilt : MonoBehaviour
{
    Vector3 tilt;
    private bool cutsceneEnd = false;
    public float startTime = 2.0f;
    public float flipTime = 0.5f;

    void Start()
    {
        StartCoroutine(Delay());
    }

    public void CheckUpsideDown(EventInfo eventInfo)
    {
        if (cutsceneEnd)
        {
            tilt = eventInfo.VectorOne;
            Debug.Log(tilt.x + " " + tilt.y);
            if (tilt.y > 0.0f && Math.Abs(tilt.x) < 0.1f)
            {
                StartCoroutine(Flip());
                cutsceneEnd = false;
            }
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

    IEnumerator Flip()
    {
        float elapsed = 0.0f;
        while (elapsed / flipTime < 1)
        {
            elapsed += Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0f, 0f, elapsed / flipTime * 180f);
            yield return null;
        }
        yield return null;
    }
}
