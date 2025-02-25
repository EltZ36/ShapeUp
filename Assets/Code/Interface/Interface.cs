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
    /// Clear an entire levels progress from game state
    /// </summary>
    /// <param name="lID">level id</param>
    void ClearLevel(int lID);

    /// <summary>
    /// clear a sublevels progress from game state
    /// </summary>
    /// <param name="lID">level id</param>
    /// <param name="slID">sub level id</param>
    void ClearSubLevel(int lID, int slID);

    /// <summary>
    /// handler for quit event booleans for save and exit query
    /// </summary>
    /// <param name="save">save the game?</param>
    /// <param name="slID">exit the game?</param>
    void OnQuitEvent(bool save, bool exit);
}

/// <summary>
/// Interface for the LevelManager Singleton
/// </summary>
interface ILevelManager
{
    /// <summary>
    /// Update the level progress given game data instance TODO game data instance needs to be properly deserailzed and not
    /// just "fromjson'd"
    /// https://docs.unity3d.com/6000.0/Documentation/ScriptReference/JsonUtility.FromJson.html
    /// </summary>
    /// <returns></returns>
    public void SetLevelProgress(GameData gd);

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

/// <summary>
/// Responsible for playing sounds in the game
/// interacts with sound emitter script which has a unique sound
/// or can put sounds that are global (bad name) in the list and reference them
/// with fn playglobal
/// </summary>
interface IAudioManager
{
    /// <summary>
    /// Plays the sound passed into it, typically from a sound emitter
    /// </summary>
    /// <param name="sound"></param>
    void Play(AudioClip sound);

    /// <summary>
    /// Play a sound from the global sounds list given the index
    /// try to avoid using this as much as possible and prefer play
    /// there are situations though where the audio manager instance is referenced
    /// directly (levelManager 126) and this is the easiest way in that case (prob not)
    /// </summary>
    /// <param name="index"></param>
    void PlayGlobal(int index);
}
