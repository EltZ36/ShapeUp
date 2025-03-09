using UnityEngine;

public class EffectLocalEventInfo : MonoBehaviour
{
    [SerializeField]
    float maxSize = 3f;

    [SerializeField]
    float minSize = 0.5f;

    GameObject current;
    float original;
    Vector3 originalScale;

    // public void DebugTest(EventInfo eventInfo)
    // {
    //     Debug.Log(eventInfo.TargetObject.name);
    // }

    public void DestroyShape(EventInfo eventInfo)
    {
        Destroy(eventInfo.TargetObject);
    }

    public void Move(EventInfo eventInfo)
    {
        eventInfo.TargetObject.transform.position = (Vector2)eventInfo.VectorTwo;
    }

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
    }
}
