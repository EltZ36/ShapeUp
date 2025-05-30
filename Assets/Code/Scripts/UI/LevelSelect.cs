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
        LevelManager.Instance.LoadLevel(0);
    }

    public void OnPinchSwipeButton()
    {
        LevelManager.Instance.LoadLevel(1);
    }

    public void OnLightShadowButton()
    {
        LevelManager.Instance.LoadLevel(2);
    }
}
