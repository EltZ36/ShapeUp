using UnityEngine;

/// <summary>
/// Put this bad boy on game objects you want to play an audio clip
/// set the audio clip in the editor
/// run play function to play it
/// </summary>
public class SoundEmitter : MonoBehaviour
{
    public AudioClip sound;

    public void PlaySound()
    {
        AudioManager.Instance.Play(sound);
    }
}
