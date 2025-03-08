using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StandAlone : MonoBehaviour
{
    public void OnBackButton()
    {
        Debug.Log("Load Standalone");
        SceneManager.LoadScene("LevelSelect");
    }
}
