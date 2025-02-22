using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSlide : MonoBehaviour
{
    public void SlideRight(EventInfo eventInfo)
    {
        eventInfo.TargetObject.GetComponent<Rigidbody2D>().velocity = Vector2.right;
    }
}
