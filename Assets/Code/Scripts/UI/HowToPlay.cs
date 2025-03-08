using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour
{
    public void OnBackButton()
    {
        Debug.Log("Load Menu");
        SceneManager.LoadScene("Menu");
    }
}
