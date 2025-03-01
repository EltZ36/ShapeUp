using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public void RestartLevel()
    {
        GameManager.Instance.ClearLevel(LevelManager.Instance.currentLevelID);
        LevelManager.Instance.LoadLevel(LevelManager.Instance.currentLevelID);
        Destroy(gameObject);
    }

    public void NextLevel()
    {
        LevelManager.Instance.LoadLevel(LevelManager.Instance.currentLevelID + 1);
        Destroy(gameObject);
    }

    public void GoToMenu()
    {
        LevelManager.Instance.LeaveCurrentLevel();
        SceneManager.LoadScene("Menu");
        Destroy(gameObject);
    }
}
