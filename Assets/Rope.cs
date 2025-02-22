using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject prefabRopeSeg,
        prefabShape;
    public int numLinks = 5;

    void Start()
    {
        GenerateRope();
    }

    void GenerateRope()
    {
        Rigidbody2D prevBod = hook;
        HingeJoint2D hj;
        for (int i = 0; i < numLinks; i++)
        {
            GameObject newSeg = Instantiate(prefabRopeSeg);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;

            prevBod = newSeg.GetComponent<Rigidbody2D>();
        }
        GameObject shape = Instantiate(prefabShape);
        shape.transform.parent = transform;
        shape.transform.position = transform.position;
        hj = shape.GetComponent<HingeJoint2D>();
        hj.connectedBody = prevBod;
    }
}
