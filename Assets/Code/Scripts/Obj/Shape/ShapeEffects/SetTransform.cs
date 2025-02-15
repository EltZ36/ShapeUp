using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransform : MonoBehaviour
{
    [SerializeField]
    Vector3 position;

    [SerializeField]
    Vector3 scale;

    [SerializeField]
    Vector3 rotation;

    public void SetPosition(EventInfo eventInfo)
    {
        eventInfo.TargetObject.gameObject.transform.position = position;
    }

    public void SetScale(EventInfo eventInfo)
    {
        eventInfo.TargetObject.gameObject.transform.localScale = scale;
    }

    public void SetRotation(EventInfo eventInfo)
    {
        eventInfo.TargetObject.gameObject.transform.rotation = Quaternion.Euler(rotation);
    }
}
