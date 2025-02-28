using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class EffectRedDrag : MonoBehaviour
{
    //maybe the same where it cracks and then comes back
    //spawn particles on where your finger drags and then collision
    Vector3 originalPosition;

    float maxDist = 0.5f;

    public void DragStart(EventInfo eventInfo)
    {
        originalPosition = transform.position;
    }

    public void MoveShapeRedDrag(EventInfo eventInfo)
    {
        if (Vector2.Distance(transform.position, originalPosition) > maxDist)
        {
            transform.position =
                ((eventInfo.VectorTwo - originalPosition).normalized * maxDist * 1.1f)
                + originalPosition;
            return;
        }
        eventInfo.TargetObject.transform.position = (Vector2)eventInfo.VectorTwo;
    }

    public void DragBack(EventInfo eventInfo)
    {
        StartCoroutine(DragBackCoroutine(eventInfo));
    }

    IEnumerator DragBackCoroutine(EventInfo eventInfo)
    {
        float time = 1.0f;
        float elapsed = 0.0f;
        while ((elapsed / time) < 1)
        {
            elapsed += Time.deltaTime;
            eventInfo.TargetObject.transform.position = new Vector3(
                EaseInOutBounce(
                    eventInfo.TargetObject.transform.position.x,
                    originalPosition.x,
                    elapsed / time
                ),
                EaseInOutBounce(
                    eventInfo.TargetObject.transform.position.y,
                    originalPosition.y,
                    elapsed / time
                ),
                originalPosition.z
            );
            yield return null;
        }
    }

    //from https://gist.github.com/cjddmut/d789b9eb78216998e95c
    public static float EaseInOutBounce(float start, float end, float value)
    {
        end -= start;
        float d = 1f;
        if (value < d * 0.5f)
            return EaseInBounce(0, end, value * 2) * 0.5f + start;
        else
            return EaseOutBounce(0, end, value * 2 - d) * 0.5f + end * 0.5f + start;
    }

    public static float EaseInBounce(float start, float end, float value)
    {
        end -= start;
        float d = 1f;
        return end - EaseOutBounce(0, end, d - value) + start;
    }

    public static float EaseOutBounce(float start, float end, float value)
    {
        value /= 1f;
        end -= start;
        if (value < (1 / 2.75f))
        {
            return end * (7.5625f * value * value) + start;
        }
        else if (value < (2 / 2.75f))
        {
            value -= 1.5f / 2.75f;
            return end * (7.5625f * value * value + .75f) + start;
        }
        else if (value < (2.5 / 2.75))
        {
            value -= 2.25f / 2.75f;
            return end * (7.5625f * value * value + .9375f) + start;
        }
        else
        {
            value -= 2.625f / 2.75f;
            return end * (7.5625f * value * value + .984375f) + start;
        }
    }
}
