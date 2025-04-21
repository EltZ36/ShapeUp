using UnityEngine;

public class EffectTapGrow : MonoBehaviour
{
    private GameObject ob;
    private bool isGrowing = false;
    private int shrinkDelay;
    private int growDuration;
    private Vector3 maxScale = new(2.25f, 2.25f, 2.25f);
    private Vector3 minScale = new(0.01f, 0.01f, 0.01f);
    private Vector3 scaleStep = new(0.01f, 0.01f, 0.01f);

    void Start()
    {
        shrinkDelay = 600;
        ob = gameObject;
    }

    void Update()
    {
        if (isGrowing)
        {
            if (ob.transform.localScale == maxScale || growDuration == 0)
            {
                isGrowing = false;
            }
            else
            {
                growDuration -= 1;
                ob.transform.localScale += scaleStep;
            }
        }
        else
        {
            if (shrinkDelay == 0)
            {
                if (ob.transform.localScale == minScale)
                {
                    ob.SetActive(false);
                }
                else
                {
                    ob.transform.localScale -= scaleStep;
                }
            }
            else
            {
                shrinkDelay -= 1;
            }
        }
    }

    public void GrowShape(EventInfo eventInfo)
    {
        if (!isGrowing)
        {
            isGrowing = true;
            shrinkDelay = 600;
            growDuration = 300;
        }
    }
}
