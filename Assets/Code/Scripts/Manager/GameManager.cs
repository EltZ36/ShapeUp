/*
Date: 1/25/25 (last change)
File: GameManager.cs
Author: Elton Zeng && CJ Moshy
Purpose: Implement save and load funcitonality for Shape Up Game
Dependancies: IGameManager, MonoBehaviour
*/

using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    #region Singleton Pattern
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        saveFile = Path.Combine(Application.persistentDataPath, "gamedata.json");
        ReadFile();
    }
    #endregion

    #region Internal Class Data
    string saveFile;
    public GameData gameData;

    #endregion

    #region EXPOSED PUBLIC FUNCTIONS

    public void SaveGame()
    {
        WriteFile();
    }

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
            return true;
        }
        else
            return false;
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
    #endregion

    #region INTERNAL READ / WRITE FUNCTIONS
    /// <summary>
    /// reads from the save file if it exists, and converts the file back from json
    /// else creates new instance of game data and calls write TODO refactor single purpose
    /// </summary>
    void ReadFile()
    {
        if (File.Exists(saveFile))
        {
            Debug.Log("Reading from save");
            string fileContents = File.ReadAllText(saveFile);
            // TODO: this is not how to use from json, will need to fix
            // gameData = JsonUtility.FromJson<GameData>(fileContents);
            // gameData.Deserialize();
            // LevelManager.Instance.SetLevelProgress(gameData);
        }
        //I also used gpt, conversation: https://chatgpt.com/share/679499f9-167c-800c-95e6-f3774649f3f7 for this to make sure that there is a new save file in case it doesn't exist
        else
        {
            Debug.Log("No save file found. Creating a new save file.");
            //make new gameData class and then make a new save.
            gameData = new GameData();
        }
    }

    /// <summary>
    /// converts the data from the gamedata class to json and writes the save file as json
    /// </summary>
    void WriteFile()
    {
        if (gameData == null)
        {
            Debug.Log("gameData is null");
            gameData = new GameData();
        }
        gameData.Serialize();
        string gameDataJSON = JsonUtility.ToJson(gameData);
        {
            try
            {
                File.WriteAllText(saveFile, gameDataJSON);
                Debug.Log("Game saved: " + JsonUtility.ToJson(gameData, true));
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to write file: {ex.Message}");
            }
        }
    #endregion

        #region OTHER
        /// <summary>
        /// Override start from monobehavior to load saved game data
        /// </summary>
        void Start()
        {
            //you can find this in C:\Users\<user>\AppData\LocalLow\<company name>
            //for me (Elton) on windows, it's C:\Users\<user>\AppData\LocalLow\DefaultCompany/ShapeUp/gamedata.json
            saveFile = Path.Combine(Application.persistentDataPath, "gamedata.json");
            ReadFile();
        }

        /// <summary>
        /// TESTING FUNCTION
        ///  A being add, S being save, and R being a reset
        /// </summary>
        //void Update()
        //{
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //    Debug.Log(gameData);
        // }
        // else if (Input.GetKeyDown(KeyCode.S))
        // {
        //     Debug.Log("Pressing S");
        //     SaveGame();
        // }
        // else if (Input.GetKeyDown(KeyCode.R))
        // {
        //     Debug.Log("Wiping");
        //     gameData = new GameData();
        //     SaveGame();
        // }
        //}

        #endregion
    }
}
