using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EffectTurnOffLights : MonoBehaviour
{
    // Start is called before the first frame update
    List<Light2D> lightsList = new List<Light2D>();

    public void turnOffLights()
    {
        foreach (Light2D light in lightsList)
        {
            light.enabled = false;
        }
    }
}
