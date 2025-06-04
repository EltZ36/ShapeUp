using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Levels,
        LockImages,
        TrophyImages;

    void OnEnable()
    {
        foreach (int ID in GameManager.Instance.gameData.LevelCompleteMap.Keys)
        {
            int length = GameManager.Instance.gameData.LevelCompleteMap[ID].Keys.Count;
            // from https://www.sourcecodehub.com/article/10618/how-to-handle-indexoutofrangeexception-in-c-sharp-causes-symptoms-and-solutions to check for index errors.
            if (ID < 0 || ID >= Levels.Count || GameManager.Instance.gameData.LevelCompleteMap[ID][length - 1] == false)
            {
                continue;
            }
            TrophyImages[ID].GetComponent<Image>().enabled = true;
            var number = ID + 1;
            if (number >= Levels.Count)
            {
                continue;
            }
            else if (
                Levels[number] == null
                || LockImages[number] == null
                || TrophyImages[number] == null
            )
            {
                continue;
            }
            LockImages[number].GetComponent<Image>().enabled = false;
            Levels[number].GetComponent<Button>().interactable = true;
            TrophyImages[ID].GetComponent<Image>().enabled = true;
        }
    }

    public void Load(string name)
    {
        SceneManager.LoadScene(name);
    }
}
