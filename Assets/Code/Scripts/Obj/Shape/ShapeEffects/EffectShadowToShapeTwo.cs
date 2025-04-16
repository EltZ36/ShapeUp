using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EffectShadowToShapeTwo : MonoBehaviour
{
    [SerializeField]
    GameObject sun;

    [SerializeField]
    Light2D globalLight,
        light1,
        light2;

    SpriteRenderer triangleSprite;
    Rigidbody2D triangleBody;

    void Start()
    {
        triangleSprite = gameObject.GetComponent<SpriteRenderer>();
        triangleBody = gameObject.GetComponent<Rigidbody2D>();
        triangleBody.gravityScale = 0;
    }

    public void DropShape(EventInfo eventInfo)
    {
        if (
            sun.transform.position.y > 0
            && sun.transform.position.x > -4.5
            && sun.transform.position.x < -3.5
        )
        {
            triangleSprite.sortingOrder = 3;
            triangleBody.gravityScale = 1f;
            globalLight.intensity = 1f;
            light1.enabled = false;
            light2.enabled = false;
        }
    }
}
