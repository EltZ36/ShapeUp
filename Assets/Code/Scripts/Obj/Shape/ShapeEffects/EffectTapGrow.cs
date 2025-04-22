using UnityEngine;

public class EffectTapGrow : MonoBehaviour
{
    private bool isGrowing = false;
    private int shrinkDelay = -1;
    private int growDuration;
    private Vector3 maxScale = new(2.25f, 2.25f, 2.25f);
    private Vector3 minScale = new(0.01f, 0.01f, 0.01f);
    private Vector3 scaleStep = new(0.01f, 0.01f, 0.01f);

    void Update()
    {
        if (isGrowing)
        {
            if (transform.localScale == maxScale || growDuration == 0)
            {
                isGrowing = false;
            }
            else
            {
                growDuration -= 1;
                transform.localScale += scaleStep;
            }
        }
        else if (shrinkDelay == 0)
        {
            if (transform.localScale == minScale)
            {
                transform.position = new Vector3(1000f, 1000f, 1000f);
                transform.parent.GetComponent<LightTap>().DecrementLights();
            }
            else
            {
                transform.localScale -= scaleStep;
            }
        }
        else
        {
            shrinkDelay -= 1;
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

    public void ResetShape()
    {
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        shrinkDelay = 600;
    }
}
