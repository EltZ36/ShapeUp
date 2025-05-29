using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRocket : MonoBehaviour
{
    public void ChangeLayer(EventInfo eventInfo)
    {
        gameObject.layer = LayerMask.NameToLayer("IgnorePhysics");
    }
}
