using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Levels;

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
                return;
            }
            Levels[number].GetComponent<Image>().enabled = true;
            Levels[number].GetComponentInChildren<Image>().enabled = true;
        }
    }

    public void Load(string name)
    {
        SceneManager.LoadScene(name);
    }
}
