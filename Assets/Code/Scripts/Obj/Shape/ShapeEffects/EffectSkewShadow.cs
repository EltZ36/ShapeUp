using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class EffectSkewShadow : MonoBehaviour
{
    public Light2D shadow;

    [SerializeField]
    public GameObject lightSource;

    public Vector3[] initialShadowCorners = new Vector3[4];

    void Start()
    {
        shadow = GetComponent<Light2D>();
        initialShadowCorners[0] = shadow.shapePath[0];
        initialShadowCorners[1] = shadow.shapePath[1];
        initialShadowCorners[2] = shadow.shapePath[2];
        initialShadowCorners[3] = shadow.shapePath[3];
    }

    void Update()
    {
        shadow.SetShapePath(SetShadowShape(2f));
    }

    private Vector3[] SetShadowShape(float angle)
    {
        Vector3[] shadowCorners = new Vector3[4];
        shadowCorners[0] = new Vector3(
            initialShadowCorners[0].x + SkewX(),
            initialShadowCorners[0].y + SkewY(),
            0f
        );
        //Debug.Log(shadowCorners[0].x + ", " + shadowCorners[0].y);
        shadowCorners[1] = new Vector3(
            initialShadowCorners[1].x + SkewX(),
            initialShadowCorners[1].y + SkewY(),
            0f
        );
        //Debug.Log(shadowCorners[1].x + ", " + shadowCorners[1].y);
        shadowCorners[2] = initialShadowCorners[2];
        //Debug.Log(shadowCorners[2].x + ", " + shadowCorners[2].y);
        shadowCorners[3] = initialShadowCorners[3];
        //Debug.Log(shadowCorners[3].x + ", " + shadowCorners[3].y);
        return shadowCorners;
    }

    private float SkewX()
    {
        float skewedX = lightSource.transform.position.x * -1f / 3f;
        return skewedX;
    }

    private float SkewY()
    {
        float skewedY = Mathf.Min(Mathf.Abs(lightSource.transform.position.x) / 10f, 1f);
        return skewedY;
    }
}
