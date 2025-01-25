using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //code for saving is based on https://videlais.com/2021/02/25/using-jsonutility-in-unity-to-save-and-load-game-data/ and https://weeklyhow.com/how-to-save-load-game-in-unity/
    string saveFile;
    GameData gameData = new GameData();

    //make a new save file on awake
    //for the save and load, I also used gpt with this link: https://chatgpt.com/share/679499f9-167c-800c-95e6-f3774649f3f7 and modified my code based on that
    void Awake()
    {
        //you can find this in C:\Users\<user>\AppData\LocalLow\<company name>
        //for me (Elton) on windows, it's C:\Users\<user>\AppData\LocalLow\DefaultCompany/ShapeUp/gamedata.json
        saveFile = Application.persistentDataPath + "/gamedata.json";
        ReadFile();
    }

    void Update()
    {
        //this is just for testing with a being add, s being save, and R being a reset for it
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Adding");
            gameData.LevelsCompleted += 1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Pressing S");
            SaveGame();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Wiping");
            gameData = new GameData();
        }
    }

    //should be called to begin the game but this should use the scenemanager instead
    public bool BeginGame(bool onlineMode, bool ready)
    {
        if (onlineMode)
        {
            Debug.Log("In online mode");
        }
        else
        {
            Debug.Log("Offline Mode");
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

    //this just reads from the save file if it exists and converts the file back from json
    void ReadFile()
    {
        if (File.Exists(saveFile))
        {
            string fileContents = File.ReadAllText(saveFile);
            gameData = JsonUtility.FromJson<GameData>(fileContents);
            Debug.Log("Reading");
        }
        //I also used gpt, conversation: https://chatgpt.com/share/679499f9-167c-800c-95e6-f3774649f3f7 for this to make sure that there is a new save file in case it doesn't exist
        else
        {
            Debug.Log("No save file found. Creating a new save file.");
            //make new gameData class and then make a new save.
            gameData = new GameData();
            SaveGame();
        }
    }

    //converts the data from the gamedata class to json and writes the save file as json 
    void WriteFile()
    {
        if (gameData == null)
        {
            Debug.Log("gameData is null");
            gameData = new GameData();
        }
        string gameDataJSON = JsonUtility.ToJson(gameData);
        File.WriteAllText(saveFile, gameDataJSON);
        Debug.Log("Game saved: " + JsonUtility.ToJson(gameData, true)); // Debug to verify saved data
    }

    //public function to call this write file
    public void SaveGame()
    {
        WriteFile();
    }
}
