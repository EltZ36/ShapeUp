using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SetLightSizeToSpriteSize : MonoBehaviour
{
    [SerializeField]
    Light2D lightSpot;

    [SerializeField]
    GameObject sprite;

    void Update()
    {
        lightSpot.pointLightOuterRadius = sprite.transform.localScale.x;
    }
}
