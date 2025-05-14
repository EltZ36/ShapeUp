using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Supercluster : MonoBehaviour
{
    public void OnBackButton()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
