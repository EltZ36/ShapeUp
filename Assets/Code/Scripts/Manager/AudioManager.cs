using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Audio emitter class
public class AudioManager : MonoBehaviour
{
    public List<AudioClip> globalSounds;
    private List<AudioSource> asrs;

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

        globalSounds = new List<AudioClip>();
        asrs = new List<AudioSource>();
    }
    #endregion

    public void Play(bool useGlobal, AudioClip sound, int index)
    {
        if (!useGlobal)
        {
            invoke(sound);
        }
        else
        {
            if (globalSounds[index])
                invoke(globalSounds[index]);
            else
                Debug.LogWarning(
                    "trying to run global audio that doesnt exist in list -> index not found"
                );
        }
    }

    public void invoke(AudioClip sound)
    {
        var i = asrs.FindIndex(asr => asr.clip == sound);

        if (i == -1)
        {
            var asr = gameObject.AddComponent<AudioSource>();
            asr.enabled = true;
            asr.playOnAwake = false;
            asr.clip = sound;
            asr.Play();
            asrs.Add(asr);
        }
        else
        {
            asrs[i].Play();
        }
    }
}
