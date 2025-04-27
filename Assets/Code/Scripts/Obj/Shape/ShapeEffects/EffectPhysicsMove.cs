using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPhysicsMove : MonoBehaviour
{
    private Rigidbody2D rb2D;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void MoveShape(EventInfo eventInfo)
    {
        Vector2 currentPosition = (Vector2)eventInfo.VectorTwo;
        rb2D.MovePosition(currentPosition);
    }

    public void PickUpShape(EventInfo eventInfo)
    {
        rb2D.gravityScale = 0f;
        rb2D.velocity = Vector2.zero;
        rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void ReleaseShape(EventInfo eventInfo)
    {
        rb2D.gravityScale = 1f;
        rb2D.constraints = RigidbodyConstraints2D.None;
    }
}
