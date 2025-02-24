using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> sounds;
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

    public void Play(int index)
    {
        asr.clip = sounds[index];
        asr.Play();
    }
}
