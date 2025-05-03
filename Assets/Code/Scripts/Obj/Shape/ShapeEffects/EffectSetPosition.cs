using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSetPosition : MonoBehaviour
{
    [SerializeField]
    Vector3 setPosition = Vector3.zero;

    public void SetPosition(EventInfo eventInfo)
    {
        gameObject.transform.position = setPosition;
    }
}
