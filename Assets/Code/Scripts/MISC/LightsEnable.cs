using UnityEngine;

namespace Lights
{
    public class LightsEnable : MonoBehaviour
    {
        void Awake() { }

        void OnTriggerEnter2D(Collider2D col)
        {
            try
            {
                col.GetComponent<EffectLightSwipe>().inLight = true;
            }
            catch (System.Exception)
            {
                return;
            }
        }

        void OnTriggerExit2D(Collider2D col)
        {
            try
            {
                col.GetComponent<EffectLightSwipe>().inLight = false;
            }
            catch (System.Exception)
            {
                return;
            }
        }
    }
}
