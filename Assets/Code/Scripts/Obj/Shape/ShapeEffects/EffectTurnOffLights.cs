using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EffectTurnOffLights : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<GameObject> houseList = new List<GameObject>();

    [SerializeField]
    Light2D global;

    public void turnOffLights(EventInfo eventInfo)
    {
        foreach (GameObject house in houseList)
        {
            //there are four light2ds in each house object
            if (house == null)
            {
                continue;
            }
            foreach (Light2D light in house.GetComponentsInChildren<Light2D>())
            {
                light.enabled = false;
            }
        }
    }

    public void turnOnGlobal(EventInfo eventInfo)
    {
        global.intensity = 1;
    }
}
