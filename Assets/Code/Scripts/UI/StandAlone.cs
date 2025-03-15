using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StandAlone : MonoBehaviour
{
    public void OnBackButton()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
