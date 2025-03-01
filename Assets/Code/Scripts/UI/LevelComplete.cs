using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public void RestartLevel()
    {
        LevelManager.Instance.LoadLevel(LevelManager.Instance.currentLevelID);
    }

    public void NextLevel()
    {
        LevelManager.Instance.LoadLevel(LevelManager.Instance.currentLevelID + 1);
    }

    public void GoToMenu()
    {
        LevelManager.Instance.LeaveCurrentLevel();
        SceneManager.LoadScene("Menu");
    }
}
