using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("I'm up!");
        foreach (int ID in GameManager.Instance.gameData.LevelCompleteMap.Keys)
        {
            string trophy = "T" + ID.ToString();
            Debug.Log("Looking for " + trophy);

            try
            {
                GameObject Tob = GameObject.FindGameObjectWithTag(trophy);
                if (Tob != null)
                {
                    Debug.Log("Found: " + Tob.name);
                    Tob.GetComponent<Image>().enabled = true;
                }
            }
            catch (UnityException)
            {
                continue;
            }
        }
    }

    public void Load(string name)
    {
        SceneManager.LoadScene(name);
    }
}
