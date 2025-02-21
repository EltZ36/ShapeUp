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

    public void ChangeSizeDist(EventInfo eventInfo)
    {
        if (current != eventInfo.TargetObject || original != eventInfo.FloatValue)
        {
            current = eventInfo.TargetObject;
            original = eventInfo.FloatValue;
            originalScale = current.transform.localScale;
        }

        float dist = Vector2.Distance(eventInfo.VectorOne, eventInfo.VectorTwo);
        float change = dist - original;

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
            eventInfo.TargetObject.transform.localScale = Vector3.Lerp(
                current.transform.localScale,
                originalScale,
                elapsed / time
            );
            yield return null;
        }
        yield return new WaitForSeconds(1f);
    }
}
