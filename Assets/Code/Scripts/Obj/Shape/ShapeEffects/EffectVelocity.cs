using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectVelocity : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    [SerializeField]
    Vector2 velocity;

    [SerializeField]
    float speed = 1;

    public void SetTargetVelocity(EventInfo eventInfo)
    {
        target.GetComponent<Rigidbody2D>().velocity = velocity * speed;
    }

    public void SetTargetVelocityFromEvent(EventInfo eventInfo)
    {
        eventInfo.TargetObject.GetComponent<Rigidbody2D>().velocity = eventInfo.VectorOne * speed;
    }
}
