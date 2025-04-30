using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectShadowToShape : MonoBehaviour
{
    [SerializeField]
    GameObject sun,
        shadow;

    SpriteRenderer squareSprite;
    Rigidbody2D squareBody;

    [SerializeField]
    GameObject anchor;

    void Start()
    {
        squareSprite = gameObject.GetComponent<SpriteRenderer>();
        squareBody = gameObject.GetComponent<Rigidbody2D>();
        squareBody.gravityScale = 0;
    }

    public void DropShape(EventInfo eventInfo)
    {
        if (
            (sun.transform.position.y - anchor.transform.position.y) > 0
            && (sun.transform.position.x - anchor.transform.position.x) > -0.5
            && (sun.transform.position.x - anchor.transform.position.x) < 0.5
        )
        {
            Destroy(shadow);
            squareSprite.sortingOrder = 3;
            squareBody.gravityScale = 1f;
        }
    }
}
