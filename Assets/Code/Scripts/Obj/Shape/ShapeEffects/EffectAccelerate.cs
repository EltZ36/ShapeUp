using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAccelerate : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    float maxSpeed;
    Rigidbody2D rb;

    Vector2 dir = default;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetAccelerationDirecion(EventInfo eventInfo)
    {
        dir = eventInfo.VectorOne.normalized;
    }

    void FixedUpdate()
    {
        rb.AddForce(dir * speed, ForceMode2D.Force);
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
