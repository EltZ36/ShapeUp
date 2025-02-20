using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRedDrag : MonoBehaviour
{
    Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    public void ChangePosition(EventInfo eventInfo)
    {
        eventInfo.TargetObject.transform.position = (Vector2)eventInfo.VectorTwo;
    }

    public void ChangeBackPosition()
    {
        transform.position = originalPosition;
    }

    //use ondragstart to get initial position
    //some type of lerp
    //changebackposition
}
