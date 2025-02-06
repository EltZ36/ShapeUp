using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TapAndBreak : MonoBehaviour
{
    [SerializeField]
    public GameObject Circle3D;
    public GameObject Circle2D;

    //https://docs.unity3d.com/6000.0/Documentation/ScriptReference/SpriteRenderer-sprite.html
    private Sprite[] sprites;
    int numTaps = 0;

    void Start()
    {
        Circle2D.SetActive(false);
    }

    void Tap()
    {
        //based on code from https://stackoverflow.com/questions/38565746/tap-detection-on-a-gameobject-in-unity by user Programmer
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                GameObject hitObject = raycastHit.transform.gameObject;
                if (hitObject == Circle3D)
                {
                    Debug.Log("Circle was tapped");
                    Circle3D.GetComponent<SpriteRenderer>().sprite = sprites[numTaps];
                    numTaps += 1;
                }
                //once it hits 3 taps, destroy the 3D circle and reveal the 2D circle
                if (numTaps == 3)
                {
                    Destroy(Circle3D);
                    //reveal 2D circle
                    Circle2D.SetActive(true);
                    Circle2D.GetComponent<Shape>().SetShapeTags(ShapeTags.Gravity);
                }
            }
        }
    }

    void Update()
    {
        Tap();
    }
}
