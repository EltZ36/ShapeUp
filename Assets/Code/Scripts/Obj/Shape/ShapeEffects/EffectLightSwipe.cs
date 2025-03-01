using UnityEngine;

public class EffectLightSwipe : MonoBehaviour
{
    public bool inLight = false;

    public void DestroyTarget(EventInfo eventInfo)
    {
        if (inLight)
        {
            Destroy(eventInfo.TargetObject);
            GetComponent<SoundEmitter>().PlaySound();
        }
    }
}
