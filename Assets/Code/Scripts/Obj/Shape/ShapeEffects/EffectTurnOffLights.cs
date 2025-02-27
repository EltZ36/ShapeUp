using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EffectTurnOffLights : MonoBehaviour
{
    // Start is called before the first frame update
    List<GameObject> houseList = new List<GameObject>();

    public void turnOffLights()
    {
        foreach (GameObject house in houseList)
        {
            //there are four light2ds in each house object
            house.GetComponentInChildren<Light2D>().enabled = false;
        }
    }
}
