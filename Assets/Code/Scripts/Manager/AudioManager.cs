using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Audio emitter class
public class AudioManager : MonoBehaviour
{
    public List<AudioClip> globalSounds = new List<AudioClip>();
    private List<AudioSource> asrs;
    public AudioClip[] bgm;
    public AudioSource bgmAudio;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    #endregion

    public void Play(bool useGlobal, AudioClip sound, int index)
    {
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" + index);
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
        // Replacement variable (doesn't change the original audio source)
        AudioSource source = gameObject.AddComponent<AudioSource>();

        // Plays different music in different scenes
        switch (scene.name)
        {
            case "Menu":
                source.clip = bgm[0];
                break;
            case "TestLevel":
                source.clip = bgm[1];
                break;
            default:
                source.clip = bgm[0];
                break;
        }

        // Only switch the music if it changed
        if (source.clip != bgmAudio.clip)
        {
            bgmAudio.enabled = false;
            bgmAudio.clip = source.clip;
            bgmAudio.enabled = true;
        }
    }
}
