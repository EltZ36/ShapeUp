using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAcceleratePulse : MonoBehaviour
{
    [SerializeField]
    float multi;

    Rigidbody2D rb;
    bool pulse = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void PulseOffCollider(EventInfo eventInfo)
    {
        if (pulse)
        {
            return;
        }
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 1, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                rb.AddForce(eventInfo.VectorOne * multi, ForceMode2D.Impulse);
                pulse = true;
                break;
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision != null)
        {
            pulse = false;
        }
    }
}
