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

    [SerializeField]
    GameObject target;

    public void SetPosition(EventInfo eventInfo)
    {
        target.gameObject.transform.position = position;
    }

    public void SetScale(EventInfo eventInfo)
    {
        target.gameObject.transform.localScale = scale;
    }

    public void SetRotation(EventInfo eventInfo)
    {
        target.gameObject.transform.rotation = Quaternion.Euler(rotation);
    }
}
