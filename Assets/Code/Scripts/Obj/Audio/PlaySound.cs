using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    public int index;

    public void PlaySound()
    {
        AudioManager.Instance.Play(index);
    }
}
