using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPhysicsMove : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb2D;

    public void MoveShape(EventInfo eventInfo)
    {
        rb2D.MovePosition((Vector2)eventInfo.VectorTwo);
    }

    public void FreezeShape(EventInfo eventInfo)
    {
        rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void UnfreezeShape(EventInfo eventInfo)
    {
        rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
