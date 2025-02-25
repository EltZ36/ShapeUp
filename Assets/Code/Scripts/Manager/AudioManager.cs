using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Audio emitter class
public class AudioManager : MonoBehaviour
{
    public List<AudioClip> globalSounds;
    private AudioSource asr;

    #region Singleton
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        asr = GetComponent<AudioSource>();
    }
    #endregion

    public void Play(AudioClip sound)
    {
        asr.clip = sound;
        asr.Play();
    }

    public void PlayGlobal(int index)
    {
        asr.clip = globalSounds[index];
        asr.Play();
    }
}
