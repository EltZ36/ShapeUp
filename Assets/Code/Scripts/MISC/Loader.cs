using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    void Awake()
    {
        foreach (int ID in GameManager.Instance.gameData.LevelCompleteMap.Keys)
        {
            string trophy = "T" + ID.ToString();

            try
            {
                GameObject Tob = GameObject.FindGameObjectWithTag(trophy);
                if (Tob != null)
                {
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
