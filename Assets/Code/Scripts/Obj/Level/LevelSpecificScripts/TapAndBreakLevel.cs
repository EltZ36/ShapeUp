using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TapAndBreakLevel : MonoBehaviour
{
    public GameObject LargeObject;
    public GameObject SmallObject;
    public GameObject CrackedObject;

    public TapBehavior tapBehavior;

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

    void CheckTap()
    {
        if (numTaps < tapAmount && tapBehavior.Tap(LargeObject) == true)
        {
            LargeObject.GetComponent<SpriteRenderer>().sprite = sprites[numTaps];
            numTaps += 1;
        }
        if (numTaps == tapAmount)
        {
            Break();
        }
    }

    void Break()
    {
        StartCoroutine(BreakCircle());
    }

    IEnumerator BreakCircle()
    {
        yield return new WaitForSeconds(0.5f);
        LargeObject.SetActive(false);
        CrackedObject.SetActive(true);
        if (SmallObject != null)
        {
            SmallObject.SetActive(true);
        }
    }

    void Update()
    {
        CheckTap();
    }
}
