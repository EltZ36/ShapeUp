using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    // Start is called before the first frame update
    public void ResetLevel()
    {
        GameManager.Instance.ClearLevel(LevelManager.Instance.currentLevelID);
        Debug.Log("Level Reset");
        LevelManager.Instance.LeaveCurrentLevel();
        SceneManager.LoadScene("Menu");
    }
}
