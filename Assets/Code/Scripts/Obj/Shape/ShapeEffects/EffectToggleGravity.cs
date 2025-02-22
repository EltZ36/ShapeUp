using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectToggleGravity : MonoBehaviour
{
    public void DisableGravity(EventInfo eventInfo)
    {
        eventInfo.TargetObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
    }

    public void EnableGravity(EventInfo eventInfo)
    {
        eventInfo.TargetObject.GetComponent<Rigidbody2D>().gravityScale = 1f;
    }
}
