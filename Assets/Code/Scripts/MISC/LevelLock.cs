using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    [SerializeField]
    private int maxLevel = 3;

    void Awake()
    {
        foreach (int ID in GameManager.Instance.gameData.LevelCompleteMap.Keys)
        {
            int number = ID + 1;
            string level = "L" + number.ToString();
            if (number >= maxLevel)
            {
                number = maxLevel;
                continue;
            }
            try
            {
                GameObject Tob = GameObject.FindGameObjectWithTag(level);
                if (Tob != null)
                {
                    Tob.GetComponent<Image>().enabled = true;
                    Debug.Log("Trophy object with tag " + level + " found and enabled.");
                }
                else
                {
                    Debug.LogWarning("Trophy object with tag " + level + " not found.");
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
