using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Levels,
        LevelImages;

    void Awake()
    {
        foreach (int ID in GameManager.Instance.gameData.LevelCompleteMap.Keys)
        {
            var number = ID + 1;
            if (number >= Levels.Count)
            {
                continue;
            }
            else if (
                Levels[number] == null
                || Levels[number].GetComponent<Image>() == null
                || Levels[number].GetComponentInChildren<Image>() == null
            )
            {
                Debug.LogError($"Level Image for {number} is null");
                return;
            }
            LevelImages[number].GetComponent<Image>().enabled = false;
            Levels[number].GetComponent<Button>().interactable = true;
        }
    }

    public void Load(string name)
    {
        SceneManager.LoadScene(name);
    }
}
