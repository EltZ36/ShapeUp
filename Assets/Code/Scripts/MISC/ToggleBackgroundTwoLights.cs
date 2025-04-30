using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ToggleBackgroundTwoLights : MonoBehaviour
{
    [SerializeField]
    Collider2D sun1,
        sun2;

    [SerializeField]
    Light2D globalLight,
        light1,
        light2;

    [SerializeField]
    SpriteRenderer paperBackground,
        darkBackground;

    private int lightCount = 0;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider == sun1)
        {
            setBright();
            enableLight(light1);
        }
        else if (collider == sun2)
        {
            setBright();
            enableLight(light2);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider == sun1)
        {
            disableLight(light1);
            if (lightCount == 0)
            {
                setDark();
            }
        }
        else if (collider == sun2)
        {
            disableLight(light2);
            if (lightCount == 0)
            {
                setDark();
            }
        }
    }

    void setBright()
    {
        paperBackground.sortingOrder = 0;
        darkBackground.sortingOrder = -2;
        globalLight.intensity = 0f;
    }

    void setDark()
    {
        paperBackground.sortingOrder = -2;
        darkBackground.sortingOrder = 0;
        globalLight.intensity = 0.5f;
    }

    void enableLight(Light2D light)
    {
        light.intensity = 0.7f;
        lightCount++;
    }

    void disableLight(Light2D light)
    {
        lightCount--;
        light.intensity = 0f;
    }
}
