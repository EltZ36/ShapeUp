using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectGravity : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChangeGravity(EventInfo eventInfo)
    {
        Vector2 new_direction = eventInfo.VectorOne;
        Physics2D.gravity = new_direction * Physics.gravity.magnitude;
    }
}
