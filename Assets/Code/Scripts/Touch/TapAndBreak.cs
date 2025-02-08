using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class TapAndBreak : MonoBehaviour
{
    public GameObject LargeObject;
    public GameObject SmallObject;
    public GameObject CrackedObject;

    //https://docs.unity3d.com/6000.0/Documentation/ScriptReference/SpriteRenderer-sprite.html
    [SerializeField]
    private Sprite[] sprites;
    private int tapAmount = 3;
    int numTaps = 0;

    void Start()
    {
        SmallObject.SetActive(false);
        CrackedObject.SetActive(false);
    }

    void Tap()
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
                    if (hitObject == LargeObject && numTaps < tapAmount)
                    {
                        LargeObject.GetComponent<SpriteRenderer>().sprite = sprites[numTaps];
                        numTaps += 1;
                    }
                    if (numTaps == tapAmount)
                    {
                        Break();
                    }
                }
            }
        }
    }

    void Break()
    {
        StartCoroutine(BreakCircle());
        SmallObject.GetComponent<Shape>().SetShapeTags(ShapeTags.Gravity);
        Debug.Log("Level Complete");
    }

    IEnumerator BreakCircle()
    {
        yield return new WaitForSeconds(0.5f);
        LargeObject.SetActive(false);
        CrackedObject.SetActive(true);
        SmallObject.SetActive(true);
    }

    void Update()
    {
        Tap();
    }
}
