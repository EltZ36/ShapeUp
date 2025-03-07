using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public string thisLevelName;
    public string nextLevelName;

    public int levelID;

    public void RestartLevel()
    {
        if (LevelManager.Instance.currentLevelID == -1)
        {
            // do nothing
            SceneManager.LoadScene(thisLevelName);
        }
        else
        {
            GameManager.Instance.ClearLevel(LevelManager.Instance.currentLevelID);
            LevelManager.Instance.LoadLevel(LevelManager.Instance.currentLevelID);
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        if (LevelManager.Instance != null)
        {
            if (LevelManager.Instance.currentLevelID == -1)
            {
                SceneManager.LoadScene(nextLevelName);
            }
            else
            {
                LevelManager.Instance.LoadLevel(LevelManager.Instance.currentLevelID + 1);
                Destroy(gameObject);
            }
        }
        else
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }

    public void GoToMenu()
    {
        Debug.Log("Hello");
        if (LevelManager.Instance != null)
        {
            if (LevelManager.Instance.currentLevelID >= 0)
            {
                LevelManager.Instance.LeaveCurrentLevel();
            }
        }
        SceneManager.LoadScene("Menu");
        Destroy(gameObject);
    }
}
