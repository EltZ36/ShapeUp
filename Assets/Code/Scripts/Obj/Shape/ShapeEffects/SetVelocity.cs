using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVelocity : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    [SerializeField]
    Vector2 velocity;

    public void SetTargetVelocity(EventInfo eventInfo)
    {
        target.GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
