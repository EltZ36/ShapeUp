using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMove : MonoBehaviour
{
    [SerializeField]
    private float scale;

    private Vector3 originalScale;

    private Vector3 targetPos;

    void Awake()
    {
        originalScale = gameObject.transform.localScale;
    }

    public void SetSize(EventInfo eventInfo)
    {
        eventInfo.TargetObject.transform.localScale = new Vector3(scale, scale);
    }

    public void DefaultSize(EventInfo eventInfo)
    {
        eventInfo.TargetObject.transform.localScale = originalScale;
    }

    public void ToggleCollision(EventInfo eventInfo)
    {
        if (LayerMask.LayerToName(eventInfo.TargetObject.layer) == "Shape")
        {
            eventInfo.TargetObject.layer = LayerMask.NameToLayer("IgnoreShape");
        }
        else
        {
            eventInfo.TargetObject.layer = LayerMask.NameToLayer("Shape");
        }
    }

    public void MoveShape(EventInfo eventInfo)
    {
        eventInfo.TargetObject.transform.position = (Vector2)eventInfo.VectorTwo;
    }
}
