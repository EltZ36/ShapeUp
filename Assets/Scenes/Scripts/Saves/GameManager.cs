using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //code for saving is based on https://videlais.com/2021/02/25/using-jsonutility-in-unity-to-save-and-load-game-data/
    string saveFile;
    GameData gameData = new GameData();

    void Awake()
    {
        saveFile = Application.persistentDataPath + "/gamedata.json";
    }

    public bool BeginGame(bool onlineMode, bool ready)
    {
        if (onlineMode)
        {
            Debug.Log("In online mode");
        }
        else
        {
            Debug.Log("OffLine Mode");
        }
        if (ready)
        {
            Debug.Log("Starting Game");
            //load the scene manager to load the game
            //SceneManager.LoadScene();
        }
        return onlineMode;
    }

    public void OnQuitEvent(bool save, bool exit)
    {
        if (save == true)
        {
            SaveGame();
        }
        if (exit == true)
        {
            Application.Quit();
        }
    }

    public void SaveGame()
    {
        Debug.Log("Saving game");
    }

    public void ReadFile()
    {
        if (File.Exists(saveFile))
        {
            string fileContents = File.ReadAllText(saveFile);
            gameData = JsonUtility.FromJson<GameData>(fileContents);
        }
    }

    public void WriteFile()
    {
        string gameDataJSON = JsonUtility.ToJson(gameData);
        File.WriteAllText(saveFile, gameDataJSON);
    }
}
