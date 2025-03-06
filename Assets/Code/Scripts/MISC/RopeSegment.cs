using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Credit to Julia's Games for the entirety of this file, this comes from
// her tutorial on creating dynamic rope in Unity2D, which can be found here:
// https://www.youtube.com/watch?v=yQiR2-0sbNw

public class RopeSegment : MonoBehaviour
{
    public GameObject connectedAbove,
        connectedBelow;

    void Start()
    {
        connectedAbove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
        RopeSegment aboveSegment = connectedAbove.GetComponent<RopeSegment>();
        if (aboveSegment != null)
        {
            aboveSegment.connectedBelow = gameObject;
            float spriteBottom = connectedAbove.GetComponent<SpriteRenderer>().bounds.size.y;
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, spriteBottom * -1);
        }
        else
        {
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
        }
    }
}
