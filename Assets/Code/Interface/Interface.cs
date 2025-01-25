using UnityEngine;

/// <summary>
/// Interface for the GameManager Singleton
/// </summary>
interface IGameManager
{
    /// <summary>
    /// make a new save file on awake
    /// for the save and load, I also used gpt with this link: https://chatgpt.com/share/679499f9-167c-800c-95e6-f3774649f3f7 and modified my code based on that
    /// </summary>
    void Init();

    /// <summary>
    /// Save a game state
    /// code for saving is based on https://videlais.com/2021/02/25/using-jsonutility-in-unity-to-save-and-load-game-data/ and https://weeklyhow.com/how-to-save-load-game-in-unity/
    /// </summary>
    void SaveGame();

    /// <summary>
    /// should be called to begin the game but this should use the scenemanager instead
    /// </summary>
    bool BeginGame(bool onlineMode, bool ready);

    /// <summary>
    /// handler for quit event booleans for save and exit query
    /// </summary>
    void OnQuitEvent(bool save, bool exit);
}

/// <summary>
/// Interface for the LevelManager Singleton
/// </summary>
interface ILevelManager
{
    /// <summary>
    /// Initializes the LevelManager with level information
    /// </summary>
    void Init();

    /// <summary>
    /// Marks level as complete and invokes OnLevelComplete event.
    /// </summary>
    /// <param name="levelID">Based on LevelManager Dict, not build number</param>
    void OnLevelCompleteEvent(int levelID);

    /// <summary>
    /// Moves to the scene named LevelSelect
    /// </summary>
    void LoadLevelSelect();

    /// <summary>
    /// Moves to the scene defined by levelID
    /// </summary>
    /// <param name="levelID">Based on LevelManager Dict, not build number</param>
    void LoadLevel(int levelID);

    /// <summary>
    /// Loads SubLevel scene onto the current scene at the defined position. Ensure SubLevel has a single root node.
    /// Only one SubLevel can be loaded at a time.
    /// </summary>
    /// <param name="SubLevelID">Based on SubLevels list relative to current Level</param>
    /// <param name="position">Position of new scene in world space</param>
    /// <returns></returns>
    bool InjectSubLevel(int SubLevelID, Vector3 position);

    /// <summary>
    /// Unloads the current SubLevel.
    /// </summary>
    void UnloadCurrentSubLevel();
}
