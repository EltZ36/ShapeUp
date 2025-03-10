using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnHit : MonoBehaviour
{
    [SerializeField]
    CircleCollider2D circleCollider2D;

    [SerializeField]
    SoundEmitter sm;

    [SerializeField]
    float velocityThreshold = 0.1f;
    Rigidbody2D rb;
    private Vector2 previousVelocity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        previousVelocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        previousVelocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 currentVelocity = rb.velocity;
        float velocityDifference = (previousVelocity - currentVelocity).magnitude;

        if (velocityDifference > velocityThreshold)
        {
            sm.PlaySound();
        }
    }
}
