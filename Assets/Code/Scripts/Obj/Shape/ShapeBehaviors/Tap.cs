using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

//change this to be a manager or some type of checker for the level and break this script down
public class TapBehavior : MonoBehaviour
{
    public bool Tap(GameObject TargetTap)
    {
        //based on code from https://stackoverflow.com/questions/38565746/tap-detection-on-a-gameobject-in-unity by user Programmer and Umair M
        //Loop through all the touches and then check if the touch is on the circle. If it is, then change the sprite to be the next one in the array.
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                if (hit)
                {
                    //I had to set the prefab of the object to not be none but the actual prefab
                    GameObject hitObject = hit.transform.gameObject;
                    if (hitObject == TargetTap)
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }
}
