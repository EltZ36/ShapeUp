using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for the GameManager Singleton
/// </summary>
interface IGameManager
{
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
    /// Marks the current level complete.
    /// </summary>
    /// <returns></returns>
    void OnCurrentLevelComplete();

    /// <summary>
    /// Marks the current sub level complete.
    /// </summary>
    /// <returns></returns>
    void OnCurrentSubLevelComplete();

    /// <summary>
    /// returns a dictionary where each key represents a level id, and the value list contains the relative sublevel id that is marked as completed.
    /// </summary>
    /// <returns></returns>
    Dictionary<int, List<int>> GetLevelsProgress();

    /// <summary>
    /// Returns true if all the sub levels of the current level is complete
    /// </summary>
    /// <returns></returns>
    bool CheckAllSubLevelsComplete();

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
