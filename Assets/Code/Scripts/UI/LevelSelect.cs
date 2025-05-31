using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void OnBackButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnTapDragButton()
    {
        LevelManager.Instance.LoadLevel(2);
    }

    public void OnPinchSwipeButton()
    {
        LevelManager.Instance.LoadLevel(3);
    }

    public void OnLightShadowButton()
    {
        LevelManager.Instance.LoadLevel(4);
    }
}
