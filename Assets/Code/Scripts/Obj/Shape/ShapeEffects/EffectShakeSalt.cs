using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectShakeSalt : MonoBehaviour
{
    // Start is called before the first frame update
    private float shakeTimes = 8;

    [SerializeField]
    private GameObject circleToHide,
        circleToShow;

    public void DecrementSalt(EventInfo eventInfo)
    {
        if (SaltPool.SaltInstance != null)
        {
            SaltPool.SaltInstance.RemoveMultipleObjects();
            shakeTimes--;
        }
        if (shakeTimes <= 0)
        {
            if (circleToHide != null)
            {
                circleToHide.SetActive(false);
            }
            if (circleToShow != null)
            {
                circleToShow.SetActive(true);
            }
        }
    }
}
