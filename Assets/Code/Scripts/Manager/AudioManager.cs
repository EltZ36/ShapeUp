using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// Audio emitter class
public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioMixer audioMixer;

    [SerializeField]
    AudioMixerGroup musicGroup;

    [SerializeField]
    AudioMixerGroup sfxGroup;
    public List<AudioClip> globalSounds = new List<AudioClip>();
    private List<AudioSource> asrs;
    public AudioClip[] bgm;

    private AudioSource bgmAudio;

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

        asrs = new List<AudioSource>();
        bgmAudio = gameObject.AddComponent<AudioSource>();
        bgmAudio.outputAudioMixerGroup = musicGroup;

        SceneManager.sceneLoaded += OnSceneLoaded;
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
            invoke(globalSounds[index]);
        }
    }

    public void invoke(AudioClip sound)
    {
        var i = asrs.FindIndex(asr => asr.clip == sound);

        if (i == -1)
        {
            var asr = gameObject.AddComponent<AudioSource>();
            asr.outputAudioMixerGroup = sfxGroup;
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

    //code take from https://discussions.unity.com/t/how-to-have-different-background-music-through-different-scenes/245897
    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        // Plays different music in different scenes
        AudioClip clip;
        if (bgm.Length == 0)
        {
            return;
        }
        switch (scene.name)
        {
            case "Menu":
                clip = bgm[0];
                break;
            case "TestLevel":
                clip = bgm[1];
                break;
            default:
                clip = null;
                break;
        }
        if (clip == null || bgmAudio == null)
        {
            return;
        }
        if (bgmAudio.clip == null || bgmAudio.clip != clip)
        {
            if (bgmAudio.isPlaying)
            {
                bgmAudio.Stop();
            }
            bgmAudio.clip = clip;
            bgmAudio.Play();
        }
    }
}
