using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TapAndBreak : MonoBehaviour
{
    public GameObject Circle3D;
    public GameObject Circle2D;
    public GameObject CrackedCircle;

    //https://docs.unity3d.com/6000.0/Documentation/ScriptReference/SpriteRenderer-sprite.html
    [SerializeField]
    private Sprite[] sprites;
    int numTaps = 0;

    void Start()
    {
        Circle2D.SetActive(false);
        CrackedCircle.SetActive(false);
    }

    void Tap()
    {
        //based on code from https://stackoverflow.com/questions/38565746/tap-detection-on-a-gameobject-in-unity by user Programmer and Umair M
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
                    if (hitObject == Circle3D && numTaps < 3)
                    {
                        Circle3D.GetComponent<SpriteRenderer>().sprite = sprites[numTaps];
                        numTaps += 1;
                    }
                    if (numTaps == 3)
                    {
                        StartCoroutine(BreakCircle());
                        Circle2D.GetComponent<Shape>().SetShapeTags(ShapeTags.Gravity);
                        Debug.Log("Level Complete");
                        LevelManager.Instance.OnCurrentSubLevelComplete();
                    }
                }
            }
        }
    }

    IEnumerator BreakCircle()
    {
        yield return new WaitForSeconds(0.5f);
        Circle3D.SetActive(false);
        CrackedCircle.SetActive(true);
        Circle2D.SetActive(true);
    }

    void Update()
    {
        Tap();
    }
}
