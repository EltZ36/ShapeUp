using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRedPinch : MonoBehaviour
{
    [SerializeField]
    float maxSize = 3f;

    [SerializeField]
    float minSize = 0.5f;

    // Start is called before the first frame update
    GameObject current;
    float original;
    Vector3 originalScale;

    public void ChangeSize(EventInfo eventInfo)
    {
        current = eventInfo.TargetObject;
        original = eventInfo.FloatValue;
        originalScale = current.transform.localScale;

        float dist = Vector2.Distance(eventInfo.VectorOne, eventInfo.VectorTwo);
        float change = dist - original;
        Debug.Log("Change: " + change);

        current.transform.localScale = new Vector3(
            Mathf.Clamp(originalScale.x + (change / 2), minSize, maxSize),
            Mathf.Clamp(originalScale.y + (change / 2), minSize, maxSize),
            1f
        );
        StartCoroutine(ChangeBackSize(eventInfo));
    }

    IEnumerator ChangeBackSize(EventInfo eventInfo)
    {
        float time = 1.0f;
        float elapsed = 0.0f;
        while ((elapsed / time) < 1)
        {
            elapsed += Time.deltaTime;
            eventInfo.TargetObject.transform.localScale = new Vector3(
                EaseInOutExpo(
                    eventInfo.TargetObject.transform.localScale.x,
                    originalScale.x,
                    elapsed / time
                ),
                EaseInOutExpo(
                    eventInfo.TargetObject.transform.localScale.y,
                    originalScale.y,
                    elapsed / time
                ),
                eventInfo.TargetObject.transform.localScale.z
            );
            Debug.Log(elapsed / time);
            yield return null;
        }
        Debug.Log("Done with Coroutine");
    }

    //from https://gist.github.com/cjddmut/d789b9eb78216998e95c
    public static float EaseInOutExpo(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1)
            return end * 0.5f * Mathf.Pow(2, 10 * (value - 1)) + start;
        value--;
        return end * 0.5f * (-Mathf.Pow(2, -10 * value) + 2) + start;
    }
}
