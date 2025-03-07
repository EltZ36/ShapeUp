using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoopMusic : MonoBehaviour
{
    public AudioClip music;
    private UnityEngine.SceneManagement.Scene scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    // Start is called before the first frame update
    public void PlayMusic()
    {
        AudioManager.Instance.Play(true, music, 0);
    }
}
